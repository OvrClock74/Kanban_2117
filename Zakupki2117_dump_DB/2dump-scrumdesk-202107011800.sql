PGDMP     !                     y         	   scrumdesk    13.0    13.0 +    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16394 	   scrumdesk    DATABASE     f   CREATE DATABASE scrumdesk WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE scrumdesk;
                postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                postgres    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   postgres    false    3            �            1255    16499 R   add_usr(character varying, character varying, integer, integer, character varying)    FUNCTION     2  CREATE FUNCTION public.add_usr(logn character varying, pas character varying, aces integer, auser integer, in_fio character varying) RETURNS character varying
    LANGUAGE plpgsql
    AS $$

declare msg varchar;

BEGIN

if exists (select null from users where acces = 0 and id_u = auser) then

INSERT INTO users (login, pass, acces, fio)

VALUES (logn, pas, aces, in_fio);

msg = 'Пользователь добавлен.';

else raise exception 'Нельзя добавить пользователя!';

end if;

return msg;

END;

$$;
 �   DROP FUNCTION public.add_usr(logn character varying, pas character varying, aces integer, auser integer, in_fio character varying);
       public          postgres    false    3            �            1255    16508    all_users_show(integer)    FUNCTION     p  CREATE FUNCTION public.all_users_show(auser integer) RETURNS TABLE(accs text, logn character varying, fioo character varying, pas character varying)
    LANGUAGE plpgsql
    AS $$
declare msg varchar;

begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

return query 
		select 
            CASE 
            	WHEN acces=1 THEN 'Завхоз'
            	WHEN acces=2 THEN 'Контрактник'
            	WHEN acces=3 THEN 'Бухгалтер'
            	WHEN acces=4 THEN 'Директор'
            	WHEN acces=5 THEN 'Зам. директора'
            ELSE 'other'
       		end,
       		login, fio, pass
    		FROM users where acces = 1 or acces = 2 or acces = 3 or acces = 4 or acces = 5;

else raise exception 'Недостаточно прав для данной операции!';

end if;

END;
$$;
 4   DROP FUNCTION public.all_users_show(auser integer);
       public          postgres    false    3            �            1255    16507    all_users_without_pass(integer)    FUNCTION     `  CREATE FUNCTION public.all_users_without_pass(auser integer) RETURNS TABLE(accs text, logn character varying, fioo character varying)
    LANGUAGE plpgsql
    AS $$
begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

return query 
		select 
            CASE 
            	WHEN acces=4 THEN 'Директор'
            	WHEN acces=5 THEN 'Зам. директора'
            	WHEN acces=1 THEN 'Завхоз'
            	WHEN acces=2 THEN 'Контрактник'
            	WHEN acces=3 THEN 'Бухгалтер'       	
            ELSE 'other'
       		end,
       		login, fio
    		FROM users where acces = 1 or acces = 2 or acces = 3 or acces = 4 or acces = 5
    	ORDER BY acces;

else raise exception 'Недостаточно прав для данной операции!';

end if;

END;
$$;
 <   DROP FUNCTION public.all_users_without_pass(auser integer);
       public          postgres    false    3            �            1255    16506 �   create_task(character varying, integer, character varying, integer, character varying, character varying, character varying, character varying, character varying)    FUNCTION     �  CREATE FUNCTION public.create_task(namet character varying, auser integer, datcmplt character varying, costt integer, proshuobesp character varying, predmzak character varying, purposezak character varying, telnumber character varying, listzak character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$

declare msg int;

begin
	
if exists (select null from users where (acces = 0 or acces = 1) and id_u = auser) then	

INSERT INTO tasks (name_t, autor, date_create, date_complete, stage, cost_t, proshu_obesp, predm_zak, purpose_zak, tel_number, list_zak)

VALUES (namet, auser, now(), to_date(datcmplt, 'DD-MM-YYYY'), 1, costt, proshuobesp, predmzak, purposezak, telnumber, listzak);

msg = (select id_t from tasks where name_t = nameT and autor = auser and date_complete = to_date(datcmplt, 'DD-MM-YYYY') and cost_t = costT);

else raise exception 'Нет разрешения на добавление задач.';

end if;

return msg;

END;

$$;
   DROP FUNCTION public.create_task(namet character varying, auser integer, datcmplt character varying, costt integer, proshuobesp character varying, predmzak character varying, purposezak character varying, telnumber character varying, listzak character varying);
       public          postgres    false    3            �            1255    24768 $   del_task(character varying, integer)    FUNCTION     �  CREATE FUNCTION public.del_task(taskname character varying, auser integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

delete from tasks where name_t = taskname;

msg = 'Заявка удалена.';

else raise exception 'Удалить нельзя. Недостаточно прав.';

end if;

return msg;

END;

$$;
 J   DROP FUNCTION public.del_task(taskname character varying, auser integer);
       public          postgres    false    3            �            1255    24769    del_tasks_archiving()    FUNCTION     �   CREATE FUNCTION public.del_tasks_archiving() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
   delete from tasks_archive where date_archiving < now() - interval '30 days';
   return null;
END;
$$;
 ,   DROP FUNCTION public.del_tasks_archiving();
       public          postgres    false    3            �            1255    16400    del_usr(integer, integer)    FUNCTION     �  CREATE FUNCTION public.del_usr(id integer, auser integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

delete from users where id_u = id;

msg = 'Пользователь удален.';

else raise exception 'Удалить нельзя';

end if;

return msg;

END;

$$;
 9   DROP FUNCTION public.del_usr(id integer, auser integer);
       public          postgres    false    3            �            1255    16401 +   login(character varying, character varying)    FUNCTION     �  CREATE FUNCTION public.login(logn character varying, pas character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$

declare id int;

BEGIN

if exists (select null from users where login = logn and pass = pas) then

id = id_u from users where login = logn;

else raise exception 'Неверный логин или пароль.';

end if;

return id;

END;

$$;
 K   DROP FUNCTION public.login(logn character varying, pas character varying);
       public          postgres    false    3            �            1255    16402 (   moving_task_1(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_1(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 2 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

update tasks set stage = (stage + 1) where id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_1(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16403 (   moving_task_2(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_2(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 1 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

update tasks set stage = (stage + 1) where id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_2(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16404 (   moving_task_3(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_3(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 3 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

insert into tasks_archive (id_t, name_t, autor, date_create, date_complete, link_kon, registry_num, sum_t, date_duration, ispolnitel) 
select id_t, name_t, autor, date_create, date_complete, link_kon, registry_num, sum_t, date_duration, ispolnitel from tasks where id_t = idt;

DELETE FROM tasks WHERE id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_3(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16509 (   moving_task_4(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_4(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 4 or acces = 5 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

update tasks set stage = (stage + 1) where id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_4(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16517    users_ispolniteli(integer)    FUNCTION     �  CREATE FUNCTION public.users_ispolniteli(auser integer) RETURNS TABLE(fioo character varying)
    LANGUAGE plpgsql
    AS $$
begin
	
if exists (select null from users where (acces = 4 or acces = 5 or acces = 0) and id_u = auser) then  

return query 
		select 
            fio
    	FROM users where acces = 2 
    	ORDER BY fio;

else raise exception 'Недостаточно прав для данной операции!';

end if;

END;
$$;
 7   DROP FUNCTION public.users_ispolniteli(auser integer);
       public          postgres    false    3            �            1259    16421    tasks    TABLE     y  CREATE TABLE public.tasks (
    id_t bigint NOT NULL,
    name_t character varying(50) NOT NULL,
    autor integer,
    date_create date DEFAULT CURRENT_DATE,
    date_complete date,
    stage integer DEFAULT 1 NOT NULL,
    payment boolean DEFAULT false NOT NULL,
    cost_t integer NOT NULL,
    proshu_obesp character varying,
    predm_zak character varying,
    purpose_zak character varying,
    tel_number character varying,
    list_zak character varying,
    link_zak character varying,
    link_kon character varying,
    registry_num character varying,
    sum_t integer,
    date_duration date,
    ispolnitel integer
);
    DROP TABLE public.tasks;
       public         heap    postgres    false    3            �            1259    16427    tasks_archive    TABLE     }  CREATE TABLE public.tasks_archive (
    id_t bigint NOT NULL,
    name_t character varying(50) NOT NULL,
    autor integer,
    date_create date NOT NULL,
    date_complete date NOT NULL,
    date_archiving date DEFAULT CURRENT_DATE,
    link_kon character varying,
    registry_num character varying,
    sum_t integer,
    date_duration date,
    ispolnitel character varying
);
 !   DROP TABLE public.tasks_archive;
       public         heap    postgres    false    3            �            1259    16431    tasks_archive_id_t_seq    SEQUENCE        CREATE SEQUENCE public.tasks_archive_id_t_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.tasks_archive_id_t_seq;
       public          postgres    false    3    201            �           0    0    tasks_archive_id_t_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.tasks_archive_id_t_seq OWNED BY public.tasks_archive.id_t;
          public          postgres    false    202            �            1259    16433    tasks_id_t_seq    SEQUENCE     w   CREATE SEQUENCE public.tasks_id_t_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.tasks_id_t_seq;
       public          postgres    false    200    3            �           0    0    tasks_id_t_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.tasks_id_t_seq OWNED BY public.tasks.id_t;
          public          postgres    false    203            �            1259    16435    users    TABLE     �   CREATE TABLE public.users (
    id_u integer NOT NULL,
    login character varying(50) NOT NULL,
    pass character varying(50) NOT NULL,
    acces integer NOT NULL,
    fio character varying NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false    3            �            1259    16438    users_id_u_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_u_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.users_id_u_seq;
       public          postgres    false    3    204            �           0    0    users_id_u_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.users_id_u_seq OWNED BY public.users.id_u;
          public          postgres    false    205            A           2604    16442 
   tasks id_t    DEFAULT     h   ALTER TABLE ONLY public.tasks ALTER COLUMN id_t SET DEFAULT nextval('public.tasks_id_t_seq'::regclass);
 9   ALTER TABLE public.tasks ALTER COLUMN id_t DROP DEFAULT;
       public          postgres    false    203    200            C           2604    16443    tasks_archive id_t    DEFAULT     x   ALTER TABLE ONLY public.tasks_archive ALTER COLUMN id_t SET DEFAULT nextval('public.tasks_archive_id_t_seq'::regclass);
 A   ALTER TABLE public.tasks_archive ALTER COLUMN id_t DROP DEFAULT;
       public          postgres    false    202    201            D           2604    16444 
   users id_u    DEFAULT     h   ALTER TABLE ONLY public.users ALTER COLUMN id_u SET DEFAULT nextval('public.users_id_u_seq'::regclass);
 9   ALTER TABLE public.users ALTER COLUMN id_u DROP DEFAULT;
       public          postgres    false    205    204            �          0    16421    tasks 
   TABLE DATA                 public          postgres    false    200            �          0    16427    tasks_archive 
   TABLE DATA                 public          postgres    false    201            �          0    16435    users 
   TABLE DATA                 public          postgres    false    204            �           0    0    tasks_archive_id_t_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.tasks_archive_id_t_seq', 1, false);
          public          postgres    false    202            �           0    0    tasks_id_t_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.tasks_id_t_seq', 61, true);
          public          postgres    false    203            �           0    0    users_id_u_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.users_id_u_seq', 22, true);
          public          postgres    false    205            H           2606    16471     tasks_archive tasks_archive_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.tasks_archive
    ADD CONSTRAINT tasks_archive_pkey PRIMARY KEY (id_t);
 J   ALTER TABLE ONLY public.tasks_archive DROP CONSTRAINT tasks_archive_pkey;
       public            postgres    false    201            F           2606    16473    tasks tasks_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_pkey PRIMARY KEY (id_t);
 :   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_pkey;
       public            postgres    false    200            J           2606    16475    users users_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id_u);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    204            M           2620    24770 !   tasks_archive del_tasks_evr_month    TRIGGER     �   CREATE TRIGGER del_tasks_evr_month AFTER INSERT ON public.tasks_archive FOR EACH ROW EXECUTE FUNCTION public.del_tasks_archiving();
 :   DROP TRIGGER del_tasks_evr_month ON public.tasks_archive;
       public          postgres    false    207    201            L           2606    16486 &   tasks_archive tasks_archive_autor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tasks_archive
    ADD CONSTRAINT tasks_archive_autor_fkey FOREIGN KEY (autor) REFERENCES public.users(id_u) ON DELETE SET NULL;
 P   ALTER TABLE ONLY public.tasks_archive DROP CONSTRAINT tasks_archive_autor_fkey;
       public          postgres    false    201    2890    204            K           2606    16491    tasks tasks_autor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_autor_fkey FOREIGN KEY (autor) REFERENCES public.users(id_u) ON DELETE SET NULL;
 @   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_autor_fkey;
       public          postgres    false    200    2890    204            �   �  x��[oE���)FE�V�Y��/ �@�P��H���K�:ik���E�)Mz�K/PD(4mS���&�ǎ��0�8��z������j`5��3;�����3��ԉ�ǿ8ŦN���-4Ε���z�V��3�L�>~��E�M�]ٓ[r~�9�`)�J��I���{<�J�=;���˵���v;x${�Z�t�V+6{��%�a�{�;�K+��lw�4�uG-%�ޓF��>n18ãV��-[lH_]�]_]�6]ْ>��粗��ߐ=���;���2���-�BP�?���P�pM���?����B��Z\\�<W�P.V�'�+ߨ_�>>_�̝mT�M�r����8��V
ǲ���ދ�\j��/T&�9k�V���¬U�,�Jů����|�da{G���p8qS��#s%�s��CS/��uqF��0<uE5�+� ��`�*mB�!ֹ$�i�S��H�X;��Tk0�j� �<-B��ׅ�w����ԭ=`�z�l�-�1�&��:�3�.�����5@ �}�]n��9�}�Q�#��d��0�pF d�cxL�9^�Q���QI!�t11�&LƘgpFW��Ăz��'�@���$m�������&�-B��tUq ����:����i�7���4���|Pjt7 �M,y� �u-��*��:��v��FL�Z����xI�"��Їơ�?�����}\�H�.B�Igәl̵�����	��I��������%�ނ�1
B�ͼp>���ԫtG��1�`2�.��}׷䐴Q���-���/=����#)��8 R���q�&3���Gk��ϼ��h���C�(�3 :^An0g��>	��Wn��"+��h�����b}����y��fLƛ�	�(���2����y�#��Ԍԃ�d�Mrw��z���m��d�m�4>AC��V���ʜ��5�`2�.�?F��1�jS5�Ui�_���v7�ԗ�{+�R��g"� �Dn0��4�a(Xz�Q�`��Al���Os z4+3(@uS;��V�6��.�~��A݁�[{���v2�$�lL��X���T���*��##4�(ڡS�;C1�1�� �(����FL&ܥ�����K׃4J��v�a#w@xډ�0�`4��{*��5�X>�w��N>���l=���Wt��_���tpU�4��:`�@(�7�z'`|MޕϠ��Z��MG��!��7�5���6pH{/��o�fpf���������7���ƹb�RO���ɗϊ����B������4�ɔ����qN�p�ۗt̃�ף�����r�S��:��b7�}��A��1�_@�q��_f�j>��]W?�3m��M�3���׋BL�芔��'d�/#3���O�=�H�QM|d`(*=���ޓ��5�`����@O��~>�³�RSG����90��5�i�{m!��Ǉ��)�[S�V��|B w�����(I���Q��;!��=
.P:���W�Kқ���s�}G�B�X���Ɣ��@��;�<z�|��P*N�Uj��B ����I�n6_����hGT�;V�2_��u���ݬU�?_���v�a�庼��~yM�OzҒ/#a�<�?��@p@�С� B}��      �   �  x����n�0�����z|O:�B�B#�"�)6�M<��t⤨�(e�ĦX 6}�T	���W�I8N����&:Jb;�}��O��7���tkvU�n���F��>�u�f�ݻ������p���x��8�q�)��S��3�ϵ	f�Z�4��FH�&zQ��r��ٜT��)����C\��2ˉ�>��Rpɸ�B�T*�~��.��u'����}q��=ro��x\�>��5�sq)�`�c���F�y�o�yQ>���6Y��D*�I8!1���+��F��	L�P�#��
���0L��ܫ���>�<?" t'�ƨ"/��;rg�Q���!�	�`�N�gCvF1��(
����3	�����+%)]ye�Df�����D7S�H�����IY���=��iQ<�uBf���fa�	.��~fSg���垚��g��޾[�9��a�W�_���E�s,f=R�MyyE�xs����h�\U      �   �  x����JQ��>�ݍR��;��Z�� �H4���?A�P+#\�
&I�μQ����m������}����&�՛,Ƛ���~�������j��mfmUX1�ß����2>x������V������U�-��/��Rw����0�֐f7�o3TG���^_H w]<9&�D�e> ����N�'}.<O�@�]fW��IШ�!00�x:�[��n[q�8Ƿ�ab����9�������c���6Ut ��r����E�p�R��4ȡ��*9�Q�W&-�4%!9Hx6w)�hI-��q���(���0�!�6�a�G�d۰`�0�O��k��IqN2_(WW�@6��	��6f�^K�0ﱙ:׉�,�(RQ�6!��C��z7#�z��u:��,��J����x�t]d�Z����#9kԧ�-�;����PS��      +    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16394 	   scrumdesk    DATABASE     f   CREATE DATABASE scrumdesk WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE scrumdesk;
                postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                postgres    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   postgres    false    3            �            1255    16499 R   add_usr(character varying, character varying, integer, integer, character varying)    FUNCTION     2  CREATE FUNCTION public.add_usr(logn character varying, pas character varying, aces integer, auser integer, in_fio character varying) RETURNS character varying
    LANGUAGE plpgsql
    AS $$

declare msg varchar;

BEGIN

if exists (select null from users where acces = 0 and id_u = auser) then

INSERT INTO users (login, pass, acces, fio)

VALUES (logn, pas, aces, in_fio);

msg = 'Пользователь добавлен.';

else raise exception 'Нельзя добавить пользователя!';

end if;

return msg;

END;

$$;
 �   DROP FUNCTION public.add_usr(logn character varying, pas character varying, aces integer, auser integer, in_fio character varying);
       public          postgres    false    3            �            1255    16508    all_users_show(integer)    FUNCTION     p  CREATE FUNCTION public.all_users_show(auser integer) RETURNS TABLE(accs text, logn character varying, fioo character varying, pas character varying)
    LANGUAGE plpgsql
    AS $$
declare msg varchar;

begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

return query 
		select 
            CASE 
            	WHEN acces=1 THEN 'Завхоз'
            	WHEN acces=2 THEN 'Контрактник'
            	WHEN acces=3 THEN 'Бухгалтер'
            	WHEN acces=4 THEN 'Директор'
            	WHEN acces=5 THEN 'Зам. директора'
            ELSE 'other'
       		end,
       		login, fio, pass
    		FROM users where acces = 1 or acces = 2 or acces = 3 or acces = 4 or acces = 5;

else raise exception 'Недостаточно прав для данной операции!';

end if;

END;
$$;
 4   DROP FUNCTION public.all_users_show(auser integer);
       public          postgres    false    3            �            1255    16507    all_users_without_pass(integer)    FUNCTION     `  CREATE FUNCTION public.all_users_without_pass(auser integer) RETURNS TABLE(accs text, logn character varying, fioo character varying)
    LANGUAGE plpgsql
    AS $$
begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

return query 
		select 
            CASE 
            	WHEN acces=4 THEN 'Директор'
            	WHEN acces=5 THEN 'Зам. директора'
            	WHEN acces=1 THEN 'Завхоз'
            	WHEN acces=2 THEN 'Контрактник'
            	WHEN acces=3 THEN 'Бухгалтер'       	
            ELSE 'other'
       		end,
       		login, fio
    		FROM users where acces = 1 or acces = 2 or acces = 3 or acces = 4 or acces = 5
    	ORDER BY acces;

else raise exception 'Недостаточно прав для данной операции!';

end if;

END;
$$;
 <   DROP FUNCTION public.all_users_without_pass(auser integer);
       public          postgres    false    3            �            1255    16506 �   create_task(character varying, integer, character varying, integer, character varying, character varying, character varying, character varying, character varying)    FUNCTION     �  CREATE FUNCTION public.create_task(namet character varying, auser integer, datcmplt character varying, costt integer, proshuobesp character varying, predmzak character varying, purposezak character varying, telnumber character varying, listzak character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$

declare msg int;

begin
	
if exists (select null from users where (acces = 0 or acces = 1) and id_u = auser) then	

INSERT INTO tasks (name_t, autor, date_create, date_complete, stage, cost_t, proshu_obesp, predm_zak, purpose_zak, tel_number, list_zak)

VALUES (namet, auser, now(), to_date(datcmplt, 'DD-MM-YYYY'), 1, costt, proshuobesp, predmzak, purposezak, telnumber, listzak);

msg = (select id_t from tasks where name_t = nameT and autor = auser and date_complete = to_date(datcmplt, 'DD-MM-YYYY') and cost_t = costT);

else raise exception 'Нет разрешения на добавление задач.';

end if;

return msg;

END;

$$;
   DROP FUNCTION public.create_task(namet character varying, auser integer, datcmplt character varying, costt integer, proshuobesp character varying, predmzak character varying, purposezak character varying, telnumber character varying, listzak character varying);
       public          postgres    false    3            �            1255    24768 $   del_task(character varying, integer)    FUNCTION     �  CREATE FUNCTION public.del_task(taskname character varying, auser integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

delete from tasks where name_t = taskname;

msg = 'Заявка удалена.';

else raise exception 'Удалить нельзя. Недостаточно прав.';

end if;

return msg;

END;

$$;
 J   DROP FUNCTION public.del_task(taskname character varying, auser integer);
       public          postgres    false    3            �            1255    24769    del_tasks_archiving()    FUNCTION     �   CREATE FUNCTION public.del_tasks_archiving() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
   delete from tasks_archive where date_archiving < now() - interval '30 days';
   return null;
END;
$$;
 ,   DROP FUNCTION public.del_tasks_archiving();
       public          postgres    false    3            �            1255    16400    del_usr(integer, integer)    FUNCTION     �  CREATE FUNCTION public.del_usr(id integer, auser integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where acces = 0 and id_u = auser) then

delete from users where id_u = id;

msg = 'Пользователь удален.';

else raise exception 'Удалить нельзя';

end if;

return msg;

END;

$$;
 9   DROP FUNCTION public.del_usr(id integer, auser integer);
       public          postgres    false    3            �            1255    16401 +   login(character varying, character varying)    FUNCTION     �  CREATE FUNCTION public.login(logn character varying, pas character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$

declare id int;

BEGIN

if exists (select null from users where login = logn and pass = pas) then

id = id_u from users where login = logn;

else raise exception 'Неверный логин или пароль.';

end if;

return id;

END;

$$;
 K   DROP FUNCTION public.login(logn character varying, pas character varying);
       public          postgres    false    3            �            1255    16402 (   moving_task_1(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_1(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 2 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

update tasks set stage = (stage + 1) where id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_1(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16403 (   moving_task_2(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_2(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 1 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

update tasks set stage = (stage + 1) where id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_2(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16404 (   moving_task_3(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_3(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 3 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

insert into tasks_archive (id_t, name_t, autor, date_create, date_complete, link_kon, registry_num, sum_t, date_duration, ispolnitel) 
select id_t, name_t, autor, date_create, date_complete, link_kon, registry_num, sum_t, date_duration, ispolnitel from tasks where id_t = idt;

DELETE FROM tasks WHERE id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_3(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16509 (   moving_task_4(integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.moving_task_4(idt integer, auser integer, now_stage_task integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
declare msg varchar;
begin
	
if exists (select null from users where (acces = 4 or acces = 5 or acces = 0) and id_u = auser) then

if ((select stage from tasks where id_t = idt) = now_stage_task) then 

update tasks set stage = (stage + 1) where id_t = idt;

msg = 'Выполнено!';

else raise exception 'Информация устарела! Пожалуйста, обновите таблицы.';

end if;

else raise exception 'Вы не можете переместить задачу на текущей стадии.';

end if;

return msg;

END;

$$;
 X   DROP FUNCTION public.moving_task_4(idt integer, auser integer, now_stage_task integer);
       public          postgres    false    3            �            1255    16517    users_ispolniteli(integer)    FUNCTION     �  CREATE FUNCTION public.users_ispolniteli(auser integer) RETURNS TABLE(fioo character varying)
    LANGUAGE plpgsql
    AS $$
begin
	
if exists (select null from users where (acces = 4 or acces = 5 or acces = 0) and id_u = auser) then  

return query 
		select 
            fio
    	FROM users where acces = 2 
    	ORDER BY fio;

else raise exception 'Недостаточно прав для данной операции!';

end if;

END;
$$;
 7   DROP FUNCTION public.users_ispolniteli(auser integer);
       public          postgres    false    3            �            1259    16421    tasks    TABLE     y  CREATE TABLE public.tasks (
    id_t bigint NOT NULL,
    name_t character varying(50) NOT NULL,
    autor integer,
    date_create date DEFAULT CURRENT_DATE,
    date_complete date,
    stage integer DEFAULT 1 NOT NULL,
    payment boolean DEFAULT false NOT NULL,
    cost_t integer NOT NULL,
    proshu_obesp character varying,
    predm_zak character varying,
    purpose_zak character varying,
    tel_number character varying,
    list_zak character varying,
    link_zak character varying,
    link_kon character varying,
    registry_num character varying,
    sum_t integer,
    date_duration date,
    ispolnitel integer
);
    DROP TABLE public.tasks;
       public         heap    postgres    false    3            �            1259    16427    tasks_archive    TABLE     }  CREATE TABLE public.tasks_archive (
    id_t bigint NOT NULL,
    name_t character varying(50) NOT NULL,
    autor integer,
    date_create date NOT NULL,
    date_complete date NOT NULL,
    date_archiving date DEFAULT CURRENT_DATE,
    link_kon character varying,
    registry_num character varying,
    sum_t integer,
    date_duration date,
    ispolnitel character varying
);
 !   DROP TABLE public.tasks_archive;
       public         heap    postgres    false    3            �            1259    16431    tasks_archive_id_t_seq    SEQUENCE        CREATE SEQUENCE public.tasks_archive_id_t_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.tasks_archive_id_t_seq;
       public          postgres    false    3    201            �           0    0    tasks_archive_id_t_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.tasks_archive_id_t_seq OWNED BY public.tasks_archive.id_t;
          public          postgres    false    202            �            1259    16433    tasks_id_t_seq    SEQUENCE     w   CREATE SEQUENCE public.tasks_id_t_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.tasks_id_t_seq;
       public          postgres    false    200    3            �           0    0    tasks_id_t_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.tasks_id_t_seq OWNED BY public.tasks.id_t;
          public          postgres    false    203            �            1259    16435    users    TABLE     �   CREATE TABLE public.users (
    id_u integer NOT NULL,
    login character varying(50) NOT NULL,
    pass character varying(50) NOT NULL,
    acces integer NOT NULL,
    fio character varying NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false    3            �            1259    16438    users_id_u_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_u_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.users_id_u_seq;
       public          postgres    false    3    204            �           0    0    users_id_u_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.users_id_u_seq OWNED BY public.users.id_u;
          public          postgres    false    205            A           2604    16442 
   tasks id_t    DEFAULT     h   ALTER TABLE ONLY public.tasks ALTER COLUMN id_t SET DEFAULT nextval('public.tasks_id_t_seq'::regclass);
 9   ALTER TABLE public.tasks ALTER COLUMN id_t DROP DEFAULT;
       public          postgres    false    203    200            C           2604    16443    tasks_archive id_t    DEFAULT     x   ALTER TABLE ONLY public.tasks_archive ALTER COLUMN id_t SET DEFAULT nextval('public.tasks_archive_id_t_seq'::regclass);
 A   ALTER TABLE public.tasks_archive ALTER COLUMN id_t DROP DEFAULT;
       public          postgres    false    202    201            D           2604    16444 
   users id_u    DEFAULT     h   ALTER TABLE ONLY public.users ALTER COLUMN id_u SET DEFAULT nextval('public.users_id_u_seq'::regclass);
 9   ALTER TABLE public.users ALTER COLUMN id_u DROP DEFAULT;
       public          postgres    false    205    204            �          0    16421    tasks 
   TABLE DATA                 public          postgres    false    200   �       �          0    16427    tasks_archive 
   TABLE DATA                 public          postgres    false    201   ~       �          0    16435    users 
   TABLE DATA                 public          postgres    false    204   o       �           0    0    tasks_archive_id_t_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.tasks_archive_id_t_seq', 1, false);
          public          postgres    false    202            �           0    0    tasks_id_t_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.tasks_id_t_seq', 61, true);
          public          postgres    false    203            �           0    0    users_id_u_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.users_id_u_seq', 22, true);
          public          postgres    false    205            H           2606    16471     tasks_archive tasks_archive_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.tasks_archive
    ADD CONSTRAINT tasks_archive_pkey PRIMARY KEY (id_t);
 J   ALTER TABLE ONLY public.tasks_archive DROP CONSTRAINT tasks_archive_pkey;
       public            postgres    false    201            F           2606    16473    tasks tasks_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_pkey PRIMARY KEY (id_t);
 :   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_pkey;
       public            postgres    false    200            J           2606    16475    users users_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id_u);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    204            M           2620    24770 !   tasks_archive del_tasks_evr_month    TRIGGER     �   CREATE TRIGGER del_tasks_evr_month AFTER INSERT ON public.tasks_archive FOR EACH ROW EXECUTE FUNCTION public.del_tasks_archiving();
 :   DROP TRIGGER del_tasks_evr_month ON public.tasks_archive;
       public          postgres    false    207    201            L           2606    16486 &   tasks_archive tasks_archive_autor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tasks_archive
    ADD CONSTRAINT tasks_archive_autor_fkey FOREIGN KEY (autor) REFERENCES public.users(id_u) ON DELETE SET NULL;
 P   ALTER TABLE ONLY public.tasks_archive DROP CONSTRAINT tasks_archive_autor_fkey;
       public          postgres    false    201    2890    204            K           2606    16491    tasks tasks_autor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_autor_fkey FOREIGN KEY (autor) REFERENCES public.users(id_u) ON DELETE SET NULL;
 @   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_autor_fkey;
       public          postgres    false    200    2890    204           