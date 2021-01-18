using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace Scrum
{
    public partial class Task_form : Form
    {
        public string C; // название задачи
        public int stage_t; // стадия на которой она находится
        public int ID_Main; // ID пользователя
        public int access_user; // доступ залогиневшегося юзера
        public bool trig = false; // чтобы в первый раз не была красная граница у доп строчек для доп контента
        public bool into = false; // чтобы при первом открытии таблицы пользователей не была выделена ячейка
        public Int64 id_isp; // ID выбранного исполнитлея (в тот момент, когда нажали по таблице всех исполнителей)

        public ToolTip t1 = new ToolTip();

        public static string cs = "Host=dumbo.db.elephantsql.com;Username=vjstrxrf;Password=p1CHdtbdVOA3VQmrHvhp-NYS43jRaIlU;Database=vjstrxrf";
        public Task_form(string C1, string name_stage, int stage_t1, int ID_Main1, int acces)
        {
            InitializeComponent();
            C = C1;
            stage_t = stage_t1;
            ID_Main = ID_Main1;
            access_user = acces;
            if (stage_t == 1 && (access_user == 0 || access_user == 1 || access_user == 4 || access_user == 5))
                print_btn.Visible = true; // если первая стадия и админ или завхоз или директор или зам директора, то есть кнопка PDF
            if (access_user == 0 || access_user == 4 || access_user == 5)
                deleteTaskB.Visible = true; // если админ или директор или зам директора, то есть кнопка УДАЛИТЬ ТАСК
            try
            {
                #region Вывод
                NpgsqlConnection con = new NpgsqlConnection(cs);
                con.Open();
                NpgsqlCommand Totalf = new NpgsqlCommand("SELECT (SELECT fio FROM users WHERE id_u = (SELECT autor FROM tasks where name_t = @taskname)), (SELECT fio as fioisp FROM users WHERE id_u = (SELECT ispolnitel FROM tasks where name_t = @taskname)), date_create, date_complete, payment, cost_t, proshu_obesp, predm_zak, purpose_zak, tel_number, list_zak, link_zak, link_kon, registry_num, sum_t, date_duration FROM tasks WHERE name_t = @taskname", con);
                Totalf.Parameters.AddWithValue("@taskname", C);
                NpgsqlDataReader reader;
                using (reader = Totalf.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label15.Text = C; // название
                                          // ФИО
                        if (reader["fio"] is DBNull)
                        {
                            label17.Text = "!пользователь удалён!";
                        }
                        else
                        {
                            label17.Text = String.Format("{0}", reader["fio"]);
                        }

                        phoneNumText.Text = String.Format("{0}", reader["tel_number"]);
                        proshuObespText.Text = String.Format("{0}", reader["proshu_obesp"]);
                        predmetZakText.Text = String.Format("{0}", reader["predm_zak"]);
                        celZakText.Text = String.Format("{0}", reader["purpose_zak"]);
                        perechenText.Text = String.Format("{0}", reader["list_zak"]);
                        if (stage_t == 5)
                            linkLabel1.Text = String.Format("{0}", reader["link_zak"]);
                        else
                            linkLabel1.Text = String.Format("{0}", reader["link_kon"]);

                        label33.Text = String.Format("{0}", reader["registry_num"]);
                        label24.Text = String.Format("{0}", reader["fioisp"]);

                        // даты
                        DateTime date = Convert.ToDateTime(reader["date_create"]);
                        label18.Text = String.Format("{0}", date.ToShortDateString());
                        date = Convert.ToDateTime(reader["date_complete"]);
                        label11.Text = String.Format("{0}", date.ToShortDateString());

                        // цена
                        int i = Convert.ToInt32(reader["cost_t"]);
                        label16.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));

                        // оплачено
                        if ((bool)reader["payment"])
                        {
                            if (access_user == 3 || access_user == 0)
                            {
                                if (stage_t == 8)
                                {
                                    checkBox1.Location = new Point(checkBox1.Location.X + 26, checkBox1.Location.Y);
                                    label14.Visible = false;
                                    checkBox1.ForeColor = Color.FromArgb(67, 181, 129);
                                    checkBox1.Text = "Оплачено";
                                    checkBox1.Checked = true;
                                    checkBox1.Visible = true;
                                    t1.SetToolTip(checkBox1, "Отменить оплату");
                                }
                                label14.ForeColor = Color.FromArgb(67, 181, 129);
                                label14.Text = "Оплачено";
                            }
                            else
                            {
                                label14.ForeColor = Color.FromArgb(67, 181, 129);
                                label14.Text = "Оплачено";
                            }
                        }
                        else
                        {
                            if (access_user == 3 || access_user == 0)
                            {
                                if (stage_t == 8)
                                {
                                    label14.Visible = false;
                                    checkBox1.ForeColor = Color.FromArgb(209, 73, 73);
                                    checkBox1.Text = "Не оплачено";
                                    checkBox1.Visible = true;
                                    t1.SetToolTip(checkBox1, "Оплатить");
                                }
                                label14.ForeColor = Color.FromArgb(209, 73, 73);
                                label14.Text = "Не оплачено";
                            }
                            else
                            {
                                label14.ForeColor = Color.FromArgb(209, 73, 73);
                                label14.Text = "Не оплачено";
                            }
                        }

                        if (stage_t == 4)
                        {
                            // размер формы, панели на форме и местоположение перечня товаров
                            this.Size = new Size(Size.Width, Size.Height + 64);
                            panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + 64);
                            label7.Location = new Point(19, 387 + 64);
                            perechenText.Location = new Point(19, 406 + 64);
                            // исполнитель
                            label23.Location = new Point(19, 387);
                            label24.Location = new Point(19, 406);
                            label23.Visible = true;
                            label24.Visible = true;
                        }
                        else if (stage_t == 5)
                        {
                            // размер формы, панели на форме и местоположение перечня товаров
                            this.Size = new Size(Size.Width, Size.Height + 128);
                            panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + 128);
                            label7.Location = new Point(19, 387 + 128);
                            perechenText.Location = new Point(19, 406 + 128);
                            // исполнитель
                            label23.Location = new Point(19, 387);
                            label24.Location = new Point(19, 406);
                            label23.Visible = true;
                            label24.Visible = true;
                            // ссылка на закупку
                            label28.Text = "ССЫЛКА НА ЗАКУПКУ";
                            label28.Location = new Point(19, 387 + 64);
                            linkLabel1.Location = new Point(19, 406 + 64);
                            label28.Visible = true;
                            linkLabel1.Visible = true;
                            // кнопка "скопировать ссылку"
                            pictureBox2.Location = new Point(linkLabel1.Location.X + linkLabel1.Size.Width, linkLabel1.Location.Y - 3);
                            pictureBox3.Location = pictureBox2.Location;
                            pictureBox2.Visible = true;
                        }
                        else if (stage_t == 6)
                        {
                            // размер формы, панели на форме и местоположение перечня товаров
                            this.Size = new Size(Size.Width, Size.Height + 192);
                            panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + 192);
                            label7.Location = new Point(19, 387 + 192);
                            perechenText.Location = new Point(19, 406 + 192);
                            // исполнитель
                            label23.Location = new Point(19, 387);
                            label24.Location = new Point(19, 406);
                            label23.Visible = true;
                            label24.Visible = true;
                            // ссылка на контракт
                            label28.Location = new Point(19, 387 + 64);
                            linkLabel1.Location = new Point(19, 406 + 64);
                            label28.Visible = true;
                            linkLabel1.Visible = true;
                            // кнопка "скопировать ссылку"
                            pictureBox2.Location = new Point(linkLabel1.Location.X + linkLabel1.Size.Width, linkLabel1.Location.Y - 3);
                            pictureBox3.Location = pictureBox2.Location;
                            pictureBox2.Visible = true;
                            // реестровый номер
                            label27.Location = new Point(19, 387 + 128);
                            label33.Location = new Point(19, 406 + 128);
                            label27.Visible = true;
                            label33.Visible = true;
                            // итоговая сумма контракта 
                            label26.Visible = true;
                            label32.Visible = true;
                            i = Convert.ToInt32(reader["sum_t"]);
                            label32.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));
                        }
                        else if (stage_t == 7 || stage_t == 8)
                        {
                            // размер формы, панели на форме и местоположение перечня товаров
                            this.Size = new Size(Size.Width, Size.Height + 192);
                            panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + 192);
                            label7.Location = new Point(19, 387 + 192);
                            perechenText.Location = new Point(19, 406 + 192);
                            // исполнитель
                            label23.Location = new Point(19, 387);
                            label24.Location = new Point(19, 406);
                            label23.Visible = true;
                            label24.Visible = true;
                            // ссылка на контракт
                            label28.Location = new Point(19, 387 + 64);
                            linkLabel1.Location = new Point(19, 406 + 64);
                            label28.Visible = true;
                            linkLabel1.Visible = true;
                            // кнопка "скопировать ссылку"
                            pictureBox2.Location = new Point(linkLabel1.Location.X + linkLabel1.Size.Width, linkLabel1.Location.Y - 3);
                            pictureBox3.Location = pictureBox2.Location;
                            pictureBox2.Visible = true;
                            // реестровый номер
                            label27.Location = new Point(19, 387 + 128);
                            label33.Location = new Point(19, 406 + 128);
                            label27.Visible = true;
                            label33.Visible = true;
                            // итоговая сумма контракта 
                            label26.Visible = true;
                            label32.Visible = true;
                            i = Convert.ToInt32(reader["sum_t"]);
                            label32.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));
                            // срок действия контракта 
                            label29.Visible = true;
                            label30.Visible = true;
                            date = Convert.ToDateTime(reader["date_duration"]);
                            label30.Text = String.Format("{0}", date.ToShortDateString());
                        }

                        //кнопка "на следующую стадию"
                        label13.Text = name_stage;
                    }
                    reader.Close();
                }
                con.Close();
                #endregion
            }
            catch (NpgsqlException)
            {
                DialogResult result = MessageBox.Show(
                         "В данный момент невозможно просматривать больше задач.\nПовторите попытку позже.",
                         "Ошибка!",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
                (Application.OpenForms["Главная"] as Главная).Activate();
                Application.OpenForms[this.Name].Activate();
            }

            ToolTip t = new ToolTip(); // всплывающая подсказкa
            t.SetToolTip(pictureBox3, "Скопировать ссылку");
        }

        #region Graphics
        private void task_form_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(95, 97, 105), 1);
                var p2 = new Pen(Color.FromArgb(115, 117, 125), 1);
                g.DrawLine(p2, new Point(9, 32), new Point(731, 32));
                g.DrawLine(p, new Point(15, 58), new Point(725, 58));
                p.Dispose();
                p2.Dispose();
                g.Dispose(); // очищаем память
            }
        }
        #endregion

        private void Task_form_MouseDown(object sender, MouseEventArgs e) // перемещение окна
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        #region Крестик на таске
        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            close_task.BackColor = Color.FromArgb(187, 66, 67);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            close_task.BackColor = Color.FromArgb(209, 73, 73);
        }
        #endregion

        #region Кнопка Переместить_таск на следующую стадию
        #region Цвет кнопки Переместить таск
        private void label16_MouseMove(object sender, MouseEventArgs e)
        {
            label13.BackColor = Color.FromArgb(109, 122, 193);
        }
        private void label16_MouseLeave(object sender, EventArgs e)
        {
            label13.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void label16_MouseDown(object sender, MouseEventArgs e)
        {
            label13.BackColor = Color.FromArgb(97, 110, 171);
        }
        #endregion

        #region Переместить таск
        private void label16_Click(object sender, EventArgs e) // переместить таск
        {
            panel2.Select();
            NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            NpgsqlCommand Totalf2 = new NpgsqlCommand("SELECT id_t FROM tasks WHERE name_t = @name_T", con); // ID заявки которую выбрали 
            Totalf2.Parameters.AddWithValue("@name_T", C);
            Int32 new_task_id = Convert.ToInt32(Totalf2.ExecuteScalar());
            // 1 и 7 СТАДИИ
            if ((stage_t == 1 || stage_t == 7) && (access_user == 1 || access_user == 0))
            {

                NpgsqlCommand NM = new NpgsqlCommand("SELECT id_u FROM users WHERE id_u = (SELECT autor FROM tasks where name_t = @taskname)", con);
                NM.Parameters.AddWithValue("@taskname", C);
                Int32 id_u = Convert.ToInt32(NM.ExecuteScalar());

                // если перемещает тот завхоз, который создал заявку
                if ((id_u == ID_Main) || access_user == 0)
                {
                    NpgsqlCommand da3 = new NpgsqlCommand("moving_task_2", con) //moving_task_2(idt integer, auser integer, now_stage_task integer)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    try
                    {
                        da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                        da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                        da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                        string returnedValue = da3.ExecuteScalar().ToString();
                        con.Close();
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show(returnedValue);
                        Close();
                    }
                    catch (NpgsqlException ex)
                    {
                        con.Close();
                        if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                        {
                            (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                            MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            Close();
                        }
                        else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                        {
                            MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                            (Application.OpenForms["Главная"] as Главная).Activate();
                            Application.OpenForms[this.Name].Activate();
                        }    
                        else
                        {
                            MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            (Application.OpenForms["Главная"] as Главная).Activate();
                            Application.OpenForms[this.Name].Activate();
                        }    
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Не вы являетесь создателем данной заявки!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }


            }
            // 2 СТАДИЯ
            else if (stage_t == 2 && (access_user == 4 || access_user == 5 || access_user == 0))
            {
                NpgsqlCommand da3 = new NpgsqlCommand("moving_task_4", con) //moving_task_2(idt integer, auser integer, now_stage_task integer)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                    da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                    da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                    string returnedValue = da3.ExecuteScalar().ToString();
                    con.Close();
                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                    MessageBox.Show(returnedValue);
                    Close();
                }
                catch (NpgsqlException ex)
                {
                    con.Close();
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        Close();
                    }
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                    {
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                        (Application.OpenForms["Главная"] as Главная).Activate();
                        Application.OpenForms[this.Name].Activate();
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        (Application.OpenForms["Главная"] as Главная).Activate();
                        Application.OpenForms[this.Name].Activate();
                    }
                }
            }
            // 3 СТАДИЯ
            else if (stage_t == 3 && (access_user == 4 || access_user == 5 || access_user == 0))
            {
                panel1.Size = new Size(740, 55);
                panel1.Location = new Point(0, 498);
                panel1.Visible = true;
                label13.Text = "Подтвердить";
                textBox8.PlaceholderText = "Выберите исполнителя";
                textBox8.Text = textBox8.Text.Trim();
                if (trig != false) // если это не первое нажатие, то выполнять всё снизу
                    if (textBox8.Text != "")
                    {
                        NpgsqlCommand da3 = new NpgsqlCommand("moving_task_4", con) //moving_task_2(idt integer, auser integer, now_stage_task integer)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        try
                        {
                            da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                            da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                            da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                            string returnedValue = da3.ExecuteScalar().ToString();
                            //
                            NpgsqlCommand Totalf = new NpgsqlCommand("UPDATE tasks SET ispolnitel = @isp WHERE id_t = @idt", con);
                            Totalf.Parameters.Add("@idt", NpgsqlDbType.Integer).Value = new_task_id;
                            Totalf.Parameters.Add("@isp", NpgsqlDbType.Integer).Value = id_isp;
                            Totalf.ExecuteNonQuery();
                            //
                            con.Close();
                            (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                            MessageBox.Show(returnedValue);
                            Close();
                        }
                        catch (NpgsqlException ex)
                        {
                            con.Close();
                            if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                            {
                                (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                Close();
                            }
                            else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                            {
                                MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                                (Application.OpenForms["Главная"] as Главная).Activate();
                                Application.OpenForms[this.Name].Activate();
                            }
                            else
                            {
                                MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                (Application.OpenForms["Главная"] as Главная).Activate();
                                Application.OpenForms[this.Name].Activate();
                            }
                        }
                    }
                    else
                    {
                        panel4.BackColor = Color.FromArgb(209, 73, 73);
                        SystemSounds.Beep.Play();
                    }
                trig = true;
            }
            // 4 СТАДИЯ
            else if (stage_t == 4 && (access_user == 2 || access_user == 0))
            {

                NpgsqlCommand ispS = new NpgsqlCommand("SELECT ispolnitel FROM tasks WHERE name_t = @taskname", con);
                ispS.Parameters.AddWithValue("@taskname", C);
                Int32 id_isp = Convert.ToInt32(ispS.ExecuteScalar());

                // если перемещает тот исполнитель, которого назначили на эту заявку
                if ((id_isp == ID_Main) || access_user == 0)
                {
                    panel1.Size = new Size(740, 55);
                    panel1.Location = new Point(0, 562);
                    panel1.Visible = true;
                    label13.Text = "Подтвердить";
                    textBox8.PlaceholderText = "Ссылка на закупку";
                    textBox8.Text = textBox8.Text.Trim();
                    if (trig != false) // если это не первое нажатие, то выполнять всё снизу
                        if (textBox8.Text != "")
                        {

                            NpgsqlCommand da3 = new NpgsqlCommand("moving_task_1", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            try
                            {
                                da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                                da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                                da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                                string returnedValue = da3.ExecuteScalar().ToString();
                                // Выскакивающая строка добавляется в бд
                                NpgsqlCommand Totalf = new NpgsqlCommand("UPDATE tasks SET link_zak = @link WHERE id_t = @idt", con);
                                Totalf.Parameters.Add("@idt", NpgsqlDbType.Integer).Value = new_task_id;
                                Totalf.Parameters.Add("@link", NpgsqlDbType.Varchar, 250).Value = textBox8.Text;
                                Totalf.ExecuteNonQuery();
                                //
                                con.Close();
                                (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                MessageBox.Show(returnedValue);
                                Close();
                            }
                            catch (NpgsqlException ex)
                            {
                                con.Close();
                                if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                                {
                                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                    MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    Close();
                                }
                                else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                                {
                                    MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                                    (Application.OpenForms["Главная"] as Главная).Activate();
                                    Application.OpenForms[this.Name].Activate();
                                }
                                else
                                {
                                    MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    (Application.OpenForms["Главная"] as Главная).Activate();
                                    Application.OpenForms[this.Name].Activate();
                                }
                            }
                        }
                        else
                        {
                            panel4.BackColor = Color.FromArgb(209, 73, 73);
                            SystemSounds.Beep.Play();
                        }
                    trig = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Не вы назначены исполнителем!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            // 5 СТАДИЯ
            else if (stage_t == 5 && (access_user == 2 || access_user == 0))
            {
                NpgsqlCommand ispS = new NpgsqlCommand("SELECT ispolnitel FROM tasks WHERE name_t = @taskname", con);
                ispS.Parameters.AddWithValue("@taskname", C);
                Int32 id_isp = Convert.ToInt32(ispS.ExecuteScalar());

                // если перемещает тот исполнитель, которого назначили на эту заявку
                if ((id_isp == ID_Main) || access_user == 0)
                {
                    panel1.Size = new Size(740, 153);
                    panel1.Location = new Point(0, 528);

                    //кнопка addlink для добавления ссылки на файл на компе
                    panel4.Width = 642;
                    panel3.Width = 640;
                    textBox8.Width = 614;
                    addlink.Location = new Point(681, 8);
                    addlink.Visible = true;
                    t1.SetToolTip(addlink, "Добавить путь к файлу");
                    //

                    panel1.Visible = true;
                    label13.Text = "Подтвердить";
                    textBox8.PlaceholderText = "Ссылка на контракт или путь к файлу";
                    textBox8.Text = textBox8.Text.Trim();
                    textBox10.Text = textBox10.Text.Trim();
                    textBox11.Text = textBox11.Text.Trim();
                    if (trig != false) // если это не первое нажатие, то выполнять всё снизу
                        if ((textBox8.Text != "") && (textBox10.Text != "") && (textBox11.Text != ""))
                        {

                            NpgsqlCommand da3 = new NpgsqlCommand("moving_task_1", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            try
                            {
                                da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                                da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                                da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                                string returnedValue = da3.ExecuteScalar().ToString();
                                // Выскакивающая строка добавляется в бд
                                NpgsqlCommand Totalf = new NpgsqlCommand("UPDATE tasks SET link_kon = @link, registry_num = @reg, sum_t = @sm WHERE id_t = @idt", con); // registry_num отныне является Исполнителем по контракту
                                Totalf.Parameters.Add("@idt", NpgsqlDbType.Integer).Value = new_task_id;
                                Totalf.Parameters.Add("@link", NpgsqlDbType.Varchar, 250).Value = textBox8.Text;
                                Totalf.Parameters.Add("@reg", NpgsqlDbType.Varchar, 250).Value = textBox10.Text;
                                Totalf.Parameters.Add("@sm", NpgsqlDbType.Integer).Value = Convert.ToInt32(textBox11.Text.Trim(new char[] { '\u20BD' }));
                                Totalf.ExecuteNonQuery();
                                //
                                con.Close();
                                (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                MessageBox.Show(returnedValue);
                                Close();
                            }
                            catch (NpgsqlException ex)
                            {
                                con.Close();
                                if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                                {
                                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                    MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    Close();
                                }
                                else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                                {
                                    MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                                    (Application.OpenForms["Главная"] as Главная).Activate();
                                    Application.OpenForms[this.Name].Activate();
                                }
                                else
                                {
                                    MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    (Application.OpenForms["Главная"] as Главная).Activate();
                                    Application.OpenForms[this.Name].Activate();
                                }
                            }
                        }
                        else
                        {
                            if (textBox8.Text == "")
                                panel4.BackColor = Color.FromArgb(209, 73, 73);
                            if (textBox10.Text == "")
                                panel6.BackColor = Color.FromArgb(209, 73, 73);
                            if (textBox11.Text == "")
                                panel8.BackColor = Color.FromArgb(209, 73, 73);
                            SystemSounds.Beep.Play();
                        }
                    trig = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Не вы назначены исполнителем!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                } 
            }
            // 6 СТАДИЯ
            else if (stage_t == 6 && (access_user == 2 || access_user == 0))
            {
                NpgsqlCommand ispS = new NpgsqlCommand("SELECT ispolnitel FROM tasks WHERE name_t = @taskname", con);
                ispS.Parameters.AddWithValue("@taskname", C);
                Int32 id_isp = Convert.ToInt32(ispS.ExecuteScalar());

                // если перемещает тот исполнитель, которого назначили на эту заявку
                if ((id_isp == ID_Main) || access_user == 0)
                {
                    panel1.Size = new Size(740, 55);
                    panel1.Location = new Point(0, 690);
                    panel1.Visible = true;
                    label13.Text = "Подтвердить";
                    textBox8.PlaceholderText = "Срок действия контракта";
                    textBox8.Text = textBox8.Text.Trim();
                    if (trig != false) // если это не первое нажатие, то выполнять всё снизу
                        if (textBox8.Text != "")
                        {
                            NpgsqlCommand da3 = new NpgsqlCommand("moving_task_1", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            try
                            {
                                da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                                da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                                da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                                string returnedValue = da3.ExecuteScalar().ToString();
                                // Выскакивающая строка добавляется в бд
                                NpgsqlCommand Totalf = new NpgsqlCommand("UPDATE tasks SET date_duration = @dt WHERE id_t = @idt", con);
                                Totalf.Parameters.Add("@idt", NpgsqlDbType.Integer).Value = new_task_id;
                                Totalf.Parameters.Add("@dt", NpgsqlDbType.Date).Value = Convert.ToDateTime(textBox8.Text);
                                Totalf.ExecuteNonQuery();
                                //
                                con.Close();
                                (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                MessageBox.Show(returnedValue);
                                Close();
                            }
                            catch (NpgsqlException ex)
                            {
                                con.Close();
                                if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                                {
                                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                                    MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    Close();
                                }
                                else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                                {
                                    MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                                    (Application.OpenForms["Главная"] as Главная).Activate();
                                    Application.OpenForms[this.Name].Activate();
                                }
                                else
                                {
                                    MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    (Application.OpenForms["Главная"] as Главная).Activate();
                                    Application.OpenForms[this.Name].Activate();
                                }
                            }
                        }
                        else
                        {
                            panel4.BackColor = Color.FromArgb(209, 73, 73);
                            SystemSounds.Beep.Play();
                        }
                    trig = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Не вы назначены исполнителем!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                } 
            }
            // 8 СТАДИЯ
            else if (stage_t == 8 && (access_user == 3 || access_user == 0))
            {
                NpgsqlCommand da3 = new NpgsqlCommand("moving_task_3", con) //moving_task_2(idt integer, auser integer, now_stage_task integer)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                    da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                    da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;

                    if (checkBox1.Checked == false)
                    {
                        con.Close();
                        DialogResult result = MessageBox.Show(
                           "Оплата не проведена!\nВы не можете отправить задачу в архив, пока она имеет статус «Не оплачено‎».",
                           "Ошибка!",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
                        (Application.OpenForms["Главная"] as Главная).Activate();
                        Application.OpenForms[this.Name].Activate();
                    }
                    else
                    {
                        string returnedValue = da3.ExecuteScalar().ToString();
                        con.Close();
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show(returnedValue);
                        Close();
                    }
                }
                catch (NpgsqlException ex)
                {
                    con.Close();
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        Close();
                    }
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                    {
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.");
                        (Application.OpenForms["Главная"] as Главная).Activate();
                        Application.OpenForms[this.Name].Activate();
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная ошибка!\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        (Application.OpenForms["Главная"] as Главная).Activate();
                        Application.OpenForms[this.Name].Activate();
                    }
                }
            }
            else
            {
                con.Close();
                MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\n\nОбратитесь к администратору.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            con.Close();
        }
        #endregion

        #endregion

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) // специально инверсия
            {
                DialogResult result = MessageBox.Show(
                           "Подтвердить проведение оплаты?",
                           "Внимание",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button2,
                           MessageBoxOptions.DefaultDesktopOnly);
                (Application.OpenForms["Главная"] as Главная).Activate();
                Application.OpenForms[this.Name].Activate();
                if (result == DialogResult.Yes)
                {
                    NpgsqlConnection con = new NpgsqlConnection(cs);
                    con.Open();
                    NpgsqlCommand Totalf = new NpgsqlCommand("update tasks set payment = true where name_t = @name_T", con);
                    Totalf.Parameters.AddWithValue("@name_T", C);
                    Totalf.ExecuteNonQuery();
                    con.Close();

                    checkBox1.ForeColor = Color.FromArgb(67, 181, 129);
                    checkBox1.Location = new Point(checkBox1.Location.X + 26, checkBox1.Location.Y);
                    checkBox1.Text = "Оплачено";
                    t1.SetToolTip(checkBox1, "Отменить оплату");
                }
                else checkBox1.Checked = false;
            }
            else
            {
                DialogResult result = MessageBox.Show(
                           "Отменить оплату?",
                           "Внимание",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button2,
                           MessageBoxOptions.DefaultDesktopOnly);
                (Application.OpenForms["Главная"] as Главная).Activate();
                Application.OpenForms[this.Name].Activate();
                if (result == DialogResult.Yes)
                {
                    NpgsqlConnection con = new NpgsqlConnection(cs);
                    con.Open();
                    NpgsqlCommand Totalf = new NpgsqlCommand("update tasks set payment = false where name_t = @name_T", con);
                    Totalf.Parameters.AddWithValue("@name_T", C);
                    Totalf.ExecuteNonQuery();
                    con.Close();

                    checkBox1.ForeColor = Color.FromArgb(209, 73, 73);
                    checkBox1.Location = new Point(checkBox1.Location.X - 26, checkBox1.Location.Y);
                    checkBox1.Text = "Не оплачено";
                    t1.SetToolTip(checkBox1, "Оплатить");
                }
                else checkBox1.Checked = true;
            }
            panel2.Select();
        }

        #region textBox8, 10 и 11
        private void textBox8_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel4.BackColor != Color.FromArgb(209, 73, 73))
                if (textBox8.Focused == false)
                    panel4.BackColor = Color.FromArgb(67, 181, 129);
        }
        private void textBox8_MouseLeave(object sender, EventArgs e)
        {
            if (panel4.BackColor != Color.FromArgb(209, 73, 73))
                if (textBox8.Focused == false && textBox8.Text == "" && maskedTextBox3.Focused == false)
                    panel4.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox8_Enter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(67, 181, 129);
            if (stage_t == 3)
            {
                //////////////////////////////////////////ТАБЛИЦА ВСЕХ ЮЗЕРОВ//////////////////////////////////////////////
                NpgsqlConnection con = new NpgsqlConnection(cs);
                con.Open();

                NpgsqlDataReader reader;
                NpgsqlCommand daT = new NpgsqlCommand("Select * from users_ispolniteli(@auser)", con);
                daT.Parameters.Add("@auser", NpgsqlDbType.Integer).Value = ID_Main;
                DataTable dt = new DataTable();
                reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                table_users.DataSource = dt;
                table_users.Columns[2].Visible = false;
                reader.Close();

                con.Close();
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                panel11.BringToFront();
                panel11.Visible = true;
                panel11.Location = new Point(21, panel1.Location.Y + 8 - panel11.Height);
            }
            if (stage_t == 5)
            {
                textBox8.Text = "";
            }
        }
        private void textBox8_MouseDown(object sender, MouseEventArgs e) // на стадии заключения (6-я)
        {
            if (stage_t == 6) // для появления maskedTextBox блокируем Enter в этот текстБокс
            {
                maskedTextBox3.Location = textBox8.Location;
                textBox8.Visible = false;
                maskedTextBox3.Visible = true;
                maskedTextBox3.Select();
            }
        }
        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (stage_t != 6)
            {
                textBox8.Text = textBox8.Text.Trim();
                if (textBox8.Text == "")
                    panel4.BackColor = Color.FromArgb(36, 36, 39);
                else
                    panel4.BackColor = Color.FromArgb(67, 181, 129);
            }
        }
        ////
        private void maskedTextBox3_Leave(object sender, EventArgs e)
        {
            if (maskedTextBox3.MaskFull)
            {
                textBox8.Text = maskedTextBox3.Text;
                maskedTextBox3.ValidatingType = typeof(DateTime);
            }
            textBox8.Visible = true;
            maskedTextBox3.Visible = false;
        }
        private void maskedTextBox3_Enter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(67, 181, 129);
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                maskedTextBox3.Select(0, 0);
            });
        }
        private void maskedTextBox3_MouseLeave(object sender, EventArgs e)
        {
            if (panel4.BackColor != Color.FromArgb(209, 73, 73))
                if (maskedTextBox3.Focused == false && maskedTextBox3.Text == "")
                    panel4.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void maskedTextBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel4.BackColor != Color.FromArgb(209, 73, 73))
                if (maskedTextBox3.Focused == false)
                    panel4.BackColor = Color.FromArgb(67, 181, 129);
        }
        private void maskedTextBox3_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (maskedTextBox3.MaskFull)
                if (!e.IsValidInput)
                {
                    DialogResult result = MessageBox.Show(
                           "Неверный формат даты!\nПроверьте вводимую дату.",
                           "Ошибка!",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
                    panel4.BackColor = Color.FromArgb(209, 73, 73);
                    textBox8.Text = "";
                    maskedTextBox3.Text = "";
                    (Application.OpenForms["Главная"] as Главная).Activate();
                    Application.OpenForms[this.Name].Activate();
                }
                else
                {
                    DateTime userDate = (DateTime)e.ReturnValue;
                    if (userDate < DateTime.Now)
                    {
                        DialogResult result = MessageBox.Show(
                          "Срок действия контракта меньше или равен сегодняшней дате!\nПроверьте вводимую дату.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
                        panel4.BackColor = Color.FromArgb(209, 73, 73);
                        textBox8.Text = "";
                        maskedTextBox3.Text = "";
                        (Application.OpenForms["Главная"] as Главная).Activate();
                        Application.OpenForms[this.Name].Activate();
                    }
                }
        }
        //
        //
        private void textBox10_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel6.BackColor != Color.FromArgb(209, 73, 73))
                if (textBox10.Focused == false)
                    panel6.BackColor = Color.FromArgb(67, 181, 129);
        }
        private void textBox10_MouseLeave(object sender, EventArgs e)
        {
            if (panel6.BackColor != Color.FromArgb(209, 73, 73))
                if (textBox10.Focused == false && textBox10.Text == "")
                    panel6.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox10_Enter(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(67, 181, 129);
        }
        private void textBox10_Leave(object sender, EventArgs e)
        {
            textBox10.Text = textBox10.Text.Trim();
            if (textBox10.Text == "")
                panel6.BackColor = Color.FromArgb(36, 36, 39);
            else
                panel6.BackColor = Color.FromArgb(67, 181, 129);
        }
        //
        //
        private void textBox11_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel8.BackColor != Color.FromArgb(209, 73, 73))
                if (textBox11.Focused == false)
                    panel8.BackColor = Color.FromArgb(67, 181, 129);
        }
        private void textBox11_MouseLeave(object sender, EventArgs e)
        {
            if (panel8.BackColor != Color.FromArgb(209, 73, 73))
                if (textBox11.Focused == false && textBox11.Text == "")
                    panel8.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox11_Enter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(67, 181, 129);
            if (textBox11.Text == "")
                textBox11.Text = "\u20BD";
        }
        private void textBox11_Leave(object sender, EventArgs e)
        {
            textBox11.Text = textBox11.Text.Trim();
            if (textBox11.Text == "\u20BD")
                textBox11.Text = "";
            if (textBox11.Text == "")
                panel8.BackColor = Color.FromArgb(36, 36, 39);
            else
                panel8.BackColor = Color.FromArgb(67, 181, 129);
        }
        #endregion

        #region Фокус на другой элемент
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            panel2.Select();
            panel1.Visible = false;
            panel11.Visible = false;
            trig = false; // надо сбрасывать тригер при исчезновении panel1
            textBox8.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            maskedTextBox3.Text = "";
            if (stage_t == 3)
                label13.Text = "В работу";
            if (stage_t == 4)
                label13.Text = "На формирование";
            if (stage_t == 5)
                label13.Text = "На заключение";
            if (stage_t == 6)
                label13.Text = "На исполнение";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel2.Select();
            panel11.Visible = false;
        }
        #endregion

        #region print_btn Печать
        private void print_btn_Click(object sender, EventArgs e) // печать
        {
            string pathFull = @"C:\Печать2117.docx";
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "pdf files (*.pdf)|*.pdf";
                sfd.FilterIndex = 1;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (sfd.ShowDialog() == DialogResult.OK && Path.GetExtension(sfd.FileName) == ".pdf")
                {
                    pathFull = Path.GetFullPath(sfd.FileName);
                    // template path
                    string tmpPath = "..\\Zakupki 2117\\template.docx"; // для выпуска
                    //string tmpPath = @"C:\Users\Павел\Desktop\template.docx"; // во время отладки
                    // shadow file name
                    string shadowFile = Path.GetDirectoryName(sfd.FileName) + @"\temp.docx";
                    // Create shadow File
                    File.Copy(tmpPath, shadowFile, true);
                    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(shadowFile);

                    object oBookMark = "ProshuObesp";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = char.ToLower(proshuObespText.Text[0]) + proshuObespText.Text.Substring(1);

                    oBookMark = "PredmetZak";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = predmetZakText.Text;

                    oBookMark = "CelZak";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = celZakText.Text;

                    oBookMark = "Srok";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = label11.Text;

                    oBookMark = "Summa";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = label16.Text;

                    oBookMark = "FIO1";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = label17.Text;

                    oBookMark = "PhoneNum";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = phoneNumText.Text;

                    oBookMark = "Perechen";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = perechenText.Text;

                    oBookMark = "FIO";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = label17.Text;

                    oBookMark = "Date";
                    doc.Bookmarks.get_Item(ref oBookMark).Range.Text = DateTime.Now.ToString("d");

                    doc.ExportAsFixedFormat(pathFull, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

                    doc.Close();
                    app.Quit();
                    File.Delete(shadowFile);
                }
            }
        }

        private void print_btn_MouseMove(object sender, MouseEventArgs e)
        {
            print_btn.ForeColor = Color.FromArgb(255, 255, 255);
            print_btn.BackColor = Color.FromArgb(120, 136, 214);
        }

        private void print_btn_MouseLeave(object sender, EventArgs e)
        {
            print_btn.ForeColor = Color.FromArgb(219, 220, 221);
            print_btn.BackColor = Color.FromArgb(99, 112, 183);
        }
        #endregion

        #region table_users
        private void table_users_Paint(object sender, PaintEventArgs e) // высота таблицы под кол-во ячеек
        {
            this.table_users.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 15);
            this.table_users.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);//выбранная ячейка фон
            this.table_users.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);//выбранная ячейка текст
            this.table_users.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
            this.table_users.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53);
            this.table_users.DefaultCellStyle.Font = new Font("Calibri", 15);
            this.table_users.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);//выбранная ячейка фон
            this.table_users.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//выбранная ячейка текст
            this.table_users.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек 
            this.table_users.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            int height = table_users.Location.Y; //+ table_users.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in table_users.Rows)
            {
                height += dr.Height;
            }
            table_users.Height = height;
            if (into == false)
                this.table_users.ClearSelection();
            into = true;
            panel11.Location = new Point(21, panel1.Location.Y + 8 - panel11.Height);
        }

        private void table_users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox8.Text = (string)table_users.Rows[e.RowIndex].Cells[0].Value;
            id_isp = (Int64)table_users.Rows[e.RowIndex].Cells[2].Value;
            panel11.Visible = false;
        }
        #endregion

        #region KeyPress для трех textBox
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (stage_t == 3)
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
            else
            {
                textBox11.Select(textBox11.Text.Length - 1, 0);
                textBox11.Focus();
                textBox11.ScrollToCaret();
            }
        }
        #endregion

        //ССЫЛКА linkLabel1//
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.linkLabel1.LinkVisited = true;
                var psi = new ProcessStartInfo
                {
                    FileName = linkLabel1.Text,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                MessageBox.Show("Ссылка не существует!\nВозможно, допущена ошибка в тексте URL, или данного пути больше нет.\n\nПопробуйте скопировать и вставить ссылку в Вашем браузере.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                (Application.OpenForms["Главная"] as Главная).Activate();
                Application.OpenForms[this.Name].Activate();
            }
        }

        #region Кнопка "скопировать ссылку"
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
        }
        #endregion

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text); // копировать в буфер ссылку
        }

        private void table_users_CellMouseEnter(object sender, DataGridViewCellEventArgs e) // ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            table_users.CurrentCell = table_users.Rows[e.RowIndex].Cells[0];
            table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
            table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }

        private void table_users_CellMouseLeave(object sender, DataGridViewCellEventArgs e) // ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            table_users.CurrentCell = table_users.Rows[e.RowIndex].Cells[0];
            if (table_users.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (table_users.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                table_users.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }

        private void linkLabel1_MouseMove(object sender, MouseEventArgs e)
        {
            linkLabel1.LinkColor = Color.FromArgb(255, 255, 255);
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.FromArgb(219, 220, 221);
        }

        #region deleteTaskB Удаление таска
        private void deleteTaskB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                            "Удаление заявки - необратимый процесс! Данная заявка удалится без возможности восстановления.\n\nВы уверены?\n\n",
                            "Внимание!",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2,
                            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.OK)
            {
                NpgsqlConnection con = new NpgsqlConnection(cs);
                con.Open();
                NpgsqlCommand Totalf = new NpgsqlCommand("del_task", con) // del_task(taskname character varying, auser integer)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    Totalf.Parameters.Add("taskname", NpgsqlDbType.Varchar).Value = C;
                    Totalf.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                    Totalf.ExecuteNonQuery();
                    con.Close();
                    this.Close();
                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                    MessageBox.Show("Заявка удалена!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                catch (NpgsqlException)
                {
                    con.Close();
                    MessageBox.Show("При удалении произошла ошибка, повторите попытку позже.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    (Application.OpenForms["Главная"] as Главная).Activate();
                    Application.OpenForms[this.Name].Activate();
                }
            }
        }

        private void deleteTaskB_MouseMove(object sender, MouseEventArgs e)
        {
            deleteTaskB.ForeColor = Color.FromArgb(240, 71, 71);
            deleteTaskB.Font = new Font(print_btn.Font.Name, 12, FontStyle.Bold | FontStyle.Underline);
        }

        private void deleteTaskB_MouseLeave(object sender, EventArgs e)
        {
            deleteTaskB.ForeColor = Color.FromArgb(209, 73, 73);
            deleteTaskB.Font = new Font(print_btn.Font.Name, 12, FontStyle.Bold | FontStyle.Regular);
        }
        #endregion

        private void addlink_Click(object sender, EventArgs e) // добавление пути к файлу контракта
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            
            string fullPath = "";
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                fullPath = choofdlog.FileName;
            }
            textBox8.Text = fullPath;
        }
    }
}
