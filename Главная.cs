using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Npgsql;
using System.Windows.Forms;
using NPOI.Util;
using NpgsqlTypes;
using System.IO;
using System.Media;
using System.Globalization;

namespace Scrum
{
    public partial class Главная : Form
    {
        //////////////////PUBLIC ПЕРЕМЕННЫЕ////////////////

        public int ID_Main;
        public bool clc = false; // если нажали на таблицу - перемещение таска к концу мышки
        public bool clcU = false; // если нажали на добавить пользователя
        public bool clcUd = false; // если нажали на удалить пользователя
        public bool clcUs = false; // если нажали на все пользователи
        public string C; // Значение, что находится в выбираемой ячейке
        public int stage_t; // стадия на которой находится выбранный таск

        ///////Переменные для добавления файла в БД//////
        public string PathToFile;
        public string filename;
        public string TypeFile;
        public string name_fillee;

        #region DataSet и DataTable 
        ///////////////////ТАБЛИЦА 1///////////////////
        private DataSet ds1 = new DataSet();
        private DataTable dt1 = new DataTable();
        ///////////////////ТАБЛИЦА 2///////////////////
        private DataSet ds2 = new DataSet();
        private DataTable dt2 = new DataTable();
        ///////////////////ТАБЛИЦА 3///////////////////
        private DataSet ds3 = new DataSet();
        private DataTable dt3 = new DataTable();
        ///////////////////ТАБЛИЦА 4///////////////////
        private DataSet ds4 = new DataSet();
        private DataTable dt4 = new DataTable();
        ///////////////////ТАБЛИЦА 5///////////////////
        private DataSet ds5 = new DataSet();
        private DataTable dt5 = new DataTable();
        ///////////////////ТАБЛИЦА 6///////////////////
        private DataSet ds6 = new DataSet();
        private DataTable dt6 = new DataTable();
        ///////////////////ТАБЛИЦА 7///////////////////
        private DataSet ds7 = new DataSet();
        private DataTable dt7 = new DataTable();
        ///////////////////ТАБЛИЦА 8///////////////////
        private DataSet ds8 = new DataSet();
        private DataTable dt8 = new DataTable();
        ///////////////////ТАБЛИЦА В ТАСКЕ///////////////////
        private DataSet dsT = new DataSet();
        private DataTable dtT = new DataTable();
        #endregion
        ///////////////////////////////////////////////////

        public Главная(int id)
        {
            InitializeComponent();
            panel1.Select();
            loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
            ID_Main = id;
            #region Зашел админ или кто
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand da2 = new NpgsqlCommand("select acces from users where id_u = @id", con); // получаем уровень доступа
            da2.Parameters.AddWithValue("@id", ID_Main);
            Int32 access_now_user = Convert.ToInt32(da2.ExecuteScalar());
            con.Close();
            if ((access_now_user == 1))
            {
                CreateTaskB.Visible = true;
                users_button.Visible = false;
            }
            else if (access_now_user == 0)
            {
                CreateTaskB.Visible = true;
                users_button.Visible = true;
            }
            else
            {
                CreateTaskB.Visible = false;
                users_button.Visible = false;
            }
            #endregion
            ToolTip t = new ToolTip(); // всплывающая подсказкa
            t.SetToolTip(reload_tables, "Обновить");
            t.Dispose();
        }

        #region Graphics рисуем линии на панелях
        private void panelCT_Paint(object sender, PaintEventArgs e) // рисуем линию на форме создания таска
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(63, 64, 68), 1);
                g.DrawLine(p, new Point(22, 471), new Point(421, 471)); // разница 26 пикселей
                p.Dispose();
                g.Dispose(); // очищаем память
            }
        }
        private void New_user_form_Paint(object sender, PaintEventArgs e) // линию на форме добавления пользователя
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(63, 64, 68), 1);
                g.DrawLine(p, new Point(22, 440), new Point(421, 440));
                p.Dispose();
                g.Dispose(); // очищаем память
            }
        }
        private void Del_user_form_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(63, 64, 68), 1);
                g.DrawLine(p, new Point(22, 369), new Point(421, 369));
                p.Dispose();
                g.Dispose(); // очищаем память
            }
        }
        private void task_form_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(63, 64, 68), 1);
                g.DrawLine(p, new Point(18, 167), new Point(362, 167));
                g.DrawLine(p, new Point(18, 236), new Point(362, 236));
                p.Dispose();
                g.Dispose(); // очищаем память
            }
        }
        #endregion

        private void Главная_MouseDown(object sender, MouseEventArgs e) // перемещение окна
        {
            panel1.Select();
            base.Capture = false;
            Message m = Message.Create(base.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        private void task_form_VisibleChanged(object sender, EventArgs e)
        {
            task_form.BringToFront(); // Таск на переднем плане
        }

        #region Загрузка таблиц
        public static void loadTables
            (DataSet ds1, DataTable dt1, DataGridView dataGridView1,
            DataSet ds2, DataTable dt2, DataGridView dataGridView2,
            DataSet ds3, DataTable dt3, DataGridView dataGridView3,
            DataSet ds4, DataTable dt4, DataGridView dataGridView4,
            DataSet ds5, DataTable dt5, DataGridView dataGridView5,
            DataSet ds6, DataTable dt6, DataGridView dataGridView6,
            DataSet ds7, DataTable dt7, DataGridView dataGridView7,
            DataSet ds8, DataTable dt8, DataGridView dataGridView8)
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            #region Заполнение таблиц данными 
            //////////////////////////////////////////////////ТАБЛИЦА 1/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 1", con);
            ds1.Reset();
            da1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView1.DataSource = dt1;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 2/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 2", con);
            ds2.Reset();
            da2.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 3/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 3", con);
            ds3.Reset();
            da3.Fill(ds3);
            dt3 = ds3.Tables[0];
            dataGridView3.DataSource = dt3;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 4/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da4 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 4", con);
            ds4.Reset();
            da4.Fill(ds4);
            dt4 = ds4.Tables[0];
            dataGridView4.DataSource = dt4;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 5/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 5", con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            dataGridView5.DataSource = dt5;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 6/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 6", con);
            ds6.Reset();
            da6.Fill(ds6);
            dt6 = ds6.Tables[0];
            dataGridView6.DataSource = dt6;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 7/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 7", con);
            ds7.Reset();
            da7.Fill(ds7);
            dt7 = ds7.Tables[0];
            dataGridView7.DataSource = dt7;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 8/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 8", con);
            ds8.Reset();
            da8.Fill(ds8);
            dt8 = ds8.Tables[0];
            dataGridView8.DataSource = dt8;
            dataGridView8.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView8.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            #endregion

            con.Close();
        }
        #endregion

        #region reload_tables - Обновить
        private void reload_tables_Click(object sender, EventArgs e) // кнопка обновить таблицы
        {
            loadTables(ds1, dt1, dataGridView1,
             ds2, dt2, dataGridView2,
             ds3, dt3, dataGridView3,
             ds4, dt4, dataGridView4,
             ds5, dt5, dataGridView5,
             ds6, dt6, dataGridView6,
             ds7, dt7, dataGridView7,
             ds8, dt8, dataGridView8);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            reload_tables.Visible = true;
        }
        private void reload_tables_MouseLeave(object sender, EventArgs e)
        {
            reload_tables.Visible = false; ;
        }
        #endregion

        #region Загрузка и выгрузка файлов в и из БД
        public static void databaseFilePut(int id_T, string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД
        {
            byte[] file;
            using (var stream = new FileStream(varFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            using (var sqlWrite = new NpgsqlCommand("add_fille", con)) //public.add_fille(id_task integer, name_f character varying, type_f character varying, file_c bytea)
            {
                sqlWrite.CommandType = CommandType.StoredProcedure;
                sqlWrite.Parameters.Add("id_task", NpgsqlTypes.NpgsqlDbType.Integer, file.Length).Value = id_T;
                sqlWrite.Parameters.Add("name_f", NpgsqlTypes.NpgsqlDbType.Varchar, file.Length).Value = name_fille;
                sqlWrite.Parameters.Add("type_f", NpgsqlTypes.NpgsqlDbType.Varchar, file.Length).Value = type_fille;
                sqlWrite.Parameters.Add("file_c", NpgsqlTypes.NpgsqlDbType.Bytea, file.Length).Value = file;
                sqlWrite.ExecuteNonQuery();
            }
            con.Close();
        }

        public static void databaseFileRead(int IdInCell, string varPathToNewLocation) // выгрузка любых файлов из БД
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            using (var sqlQuery = new NpgsqlCommand(@"SELECT file_content FROM files WHERE id_f = @f_id", con))
            {
                sqlQuery.Parameters.AddWithValue("@f_id", IdInCell);
                using (NpgsqlDataReader dr = sqlQuery.ExecuteReader(System.Data.CommandBehavior.Default))
                {
                    if (dr.Read())
                    {
                        byte[] fileData = (byte[])dr.GetValue(0);
                        using (FileStream fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.ReadWrite))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                bw.Write(fileData);
                                bw.Close();
                            }
                        }
                    }
                    dr.Close();
                }
            }
            con.Close();
        }
        #endregion

        private void dataGridView_Task_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // ЗАГРУЗИТЬ ФАЙЛ
        {
            Int32 file_id;
            string file_type;
            C = (string)dataGridView_Task.Rows[e.RowIndex].Cells[0].Value;
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT id_f, type_file FROM files WHERE name_file = @name_F", con); // ID заявки которую выбрали 
            Totalf.Parameters.AddWithValue("@name_F", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    file_id = Convert.ToInt32(reader["id_f"]);
                    file_type = Convert.ToString(reader["type_file"]);
                    reader.Close();
                    Stream myStream;
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = "c:\\";
                    saveFileDialog1.Title = "Сохранить как...";
                    saveFileDialog1.FileName = C;
                    saveFileDialog1.Filter = "Files (*" + file_type + ")|*" + file_type;
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if ((myStream = saveFileDialog1.OpenFile()) != null)
                        {
                            string path = Path.GetFullPath(saveFileDialog1.FileName);
                            myStream.Close();
                            databaseFileRead(file_id, path);
                        }
                    }
                }
            }
            con.Close();
        }

        #region dataGridView_Task
        private void dataGridView_Task_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_Task.CurrentCell = dataGridView_Task.Rows[e.RowIndex].Cells[0];
            dataGridView_Task.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView_Task.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView_Task_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_Task.CurrentCell = dataGridView_Task.Rows[e.RowIndex].Cells[0];
            dataGridView_Task.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView_Task.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        private void dataGridView_Task_Paint(object sender, PaintEventArgs e)
        {
            dataGridView_Task.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView_Task.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView_Task.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView_Task.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView_Task.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек 
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView_Task.Rows)
            {
                height += dr.Height;
            }
            int Y = dataGridView_Task.Height;

            dataGridView_Task.Height = height + 3;

            background_textbox_panel.Height = dataGridView_Task.Height;

            task_form.Height = dataGridView_Task.Location.Y + dataGridView_Task.Height
                + label16.Height + label16.Height / 2;
        }
        #endregion

        void task_filling(string name_stage)
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C; // название

                    if (reader["autor"] is DBNull)
                    {
                        label3.Text = "!пользователь удалён!";
                    }
                    else
                    {
                        NpgsqlConnection con2 = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
                        con2.Open();
                        NpgsqlCommand loginA = new NpgsqlCommand("SELECT login FROM users WHERE id_u = @id", con2); // логин вместо id
                        loginA.Parameters.AddWithValue("@id", Convert.ToInt32(reader["autor"]));
                        NpgsqlDataReader reader2;
                        using (reader2 = loginA.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                label3.Text = String.Format("{0}", reader2["login"]); ;
                            }
                            reader2.Close();
                        }
                        con2.Close();
                    }

                    DateTime date = Convert.ToDateTime(reader["date_create"]);
                    label4.Text = String.Format("{0}", date.ToShortDateString());
                    date = Convert.ToDateTime(reader["date_complete"]);
                    label33.Text = String.Format("{0}", date.ToShortDateString());

                    int i = Convert.ToInt32(reader["cost_t"]);
                    label5.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));
                    if ((bool)reader["payment"])
                    {
                        label6.ForeColor = Color.FromArgb(67, 181, 129);
                        label6.Text = "Оплачено";
                    }
                    else
                    {
                        label6.ForeColor = Color.FromArgb(209, 73, 73);
                        label6.Text = "Не оплачено";
                    }

                    label16.Text = name_stage;
                }
                reader.Close();
            }
            //////////////////////////////////////////////КАКИЕ ФАЙЛЫ ПРИКРЕПЛЕНЫ////////////////////////////////////////////////////////////
            NpgsqlCommand Totalf2 = new NpgsqlCommand("SELECT id_t FROM tasks WHERE name_t = @name_T", con); // ID заявки которую выбрали 
            Totalf2.Parameters.AddWithValue("@name_T", C);
            Int32 new_task_id = Convert.ToInt32(Totalf2.ExecuteScalar());

            NpgsqlCommand daT = new NpgsqlCommand("SELECT name_file FROM files WHERE taskid = @idtask", con);
            daT.Parameters.AddWithValue("@idtask", new_task_id);

            DataTable dt = new DataTable();
            reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(reader);
            dataGridView_Task.DataSource = dt;
            reader.Close();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.Close();
        }

        private void AddFTask_Click(object sender, EventArgs e) // СОХРАНИТЬ ФАЙЛ
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            PathToFile = openFileDialog1.FileName; // получаем путь к выбранному файлу
            TypeFile = Path.GetExtension(PathToFile); // тип выбранного файла
            name_fillee = Path.GetFileNameWithoutExtension(openFileDialog1.FileName); // только имя выбранного файла

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT id_t FROM tasks WHERE name_t = @name_T", con); // ID заявки которую выбрали 
            Totalf.Parameters.AddWithValue("@name_T", C);
            Int32 new_task_id = Convert.ToInt32(Totalf.ExecuteScalar());
            databaseFilePut(new_task_id, name_fillee, TypeFile, PathToFile); // databaseFilePut(int id_T , string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД 

            //////////////////////////////////////////////ОБНОВЛЕНИЕ ТАБЛИЦЫ С ПРИКРЕПЛЕННЫМИ ФАЙЛАМИ////////////////////////////////////////////////////////////
            NpgsqlDataReader reader;
            NpgsqlCommand daT = new NpgsqlCommand("SELECT name_file FROM files WHERE taskid = @idtask", con);
            daT.Parameters.AddWithValue("@idtask", new_task_id);
            DataTable dt = new DataTable();
            reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(reader);
            dataGridView_Task.DataSource = dt;
            reader.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            con.Close();
            MessageBox.Show("Файл успешно загружен");
        }

        #region Таски из таблиц
        #region Таск из таблицы 1
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 1;
            task_filling("На согласование");
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView1.Location.Y);
                task_form.Left = (e.X + +dataGridView1.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 2
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView2.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 2;
            task_filling("На утверждение");
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView2.Location.Y);
                task_form.Left = (e.X + +dataGridView2.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 3
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView3.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 3;
            task_filling("В работу");
        }

        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView3.Location.Y);
                task_form.Left = (e.X + +dataGridView3.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }
        private void dataGridView3_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[0];
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView3_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[0];
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 4
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView4.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 4;
            task_filling("На формирование");
        }

        private void dataGridView4_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView4.Location.Y);
                task_form.Left = (e.X + +dataGridView4.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }
        private void dataGridView4_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView4.CurrentCell = dataGridView4.Rows[e.RowIndex].Cells[0];
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView4_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.CurrentCell = dataGridView4.Rows[e.RowIndex].Cells[0];
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 5
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView5.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 5;
            task_filling("На заключение");
        }

        private void dataGridView5_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView5.Location.Y);
                task_form.Left = (e.X + +dataGridView5.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }
        private void dataGridView5_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView5.CurrentCell = dataGridView5.Rows[e.RowIndex].Cells[0];
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView5_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView5.CurrentCell = dataGridView5.Rows[e.RowIndex].Cells[0];
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 6
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView6.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 6;
            task_filling("На исполнение");
        }

        private void dataGridView6_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView6.Location.Y);
                task_form.Left = (e.X + +dataGridView6.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }
        private void dataGridView6_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView6.CurrentCell = dataGridView6.Rows[e.RowIndex].Cells[0];
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView6_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView6.CurrentCell = dataGridView6.Rows[e.RowIndex].Cells[0];
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 7
        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView7.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 7;
            task_filling("На оплату");
        }

        private void dataGridView7_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView7.Location.Y);
                task_form.Left = (e.X + +dataGridView7.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }
        private void dataGridView7_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView7.CurrentCell = dataGridView7.Rows[e.RowIndex].Cells[0];
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView7_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView7.CurrentCell = dataGridView7.Rows[e.RowIndex].Cells[0];
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 8
        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView8.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 8;
            task_filling("В архив");
        }

        private void dataGridView8_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Top = (e.Y + dataGridView8.Location.Y);
                task_form.Left = (e.X + +dataGridView8.Location.X);
                task_form.Visible = true;
                clc = false;
            }
        }
        private void dataGridView8_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView8.CurrentCell = dataGridView8.Rows[e.RowIndex].Cells[0];
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView8_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView8.CurrentCell = dataGridView8.Rows[e.RowIndex].Cells[0];
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion
        #endregion 

        #region Размеры таблиц стадий
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                height += dr.Height;
            }
            dataGridView1.Height = height + 3;
        }
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView2.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView2.Rows)
            {
                height += dr.Height;
            }
            dataGridView2.Height = height + 4;
        }

        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView3.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView3.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView3.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView3.Rows)
            {
                height += dr.Height;
            }
            dataGridView3.Height = height + 4;
        }

        private void dataGridView4_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView4.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView4.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView4.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView4.Rows)
            {
                height += dr.Height;
            }
            dataGridView4.Height = height + 4;
        }

        private void dataGridView5_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView5.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView5.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView5.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView5.Rows)
            {
                height += dr.Height;
            }
            dataGridView5.Height = height + 4;
        }

        private void dataGridView6_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView6.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView6.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView6.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView6.Rows)
            {
                height += dr.Height;
            }
            dataGridView6.Height = height + 4;
        }

        private void dataGridView7_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView7.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView7.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView7.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView7.Rows)
            {
                height += dr.Height;
            }
            dataGridView7.Height = height + 4;
        }

        private void dataGridView8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView8.DefaultCellStyle.Font = new Font("Calibri", 13);
            dataGridView8.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);
            dataGridView8.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            dataGridView8.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView8.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView8.Rows)
            {
                height += dr.Height;
            }
            dataGridView8.Height = height + 4;
        }
        #endregion

        #region Крестик на таске
        private void label1_Click(object sender, EventArgs e)
        {
            task_form.Visible = false;
            clc = false;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            close_task.BackColor = Color.FromArgb(187, 66,67);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            close_task.BackColor = Color.FromArgb(209, 73, 73);
        }
        #endregion

        #region Перемещение таска
        int deltaX = 0;
        int deltaY = 0;
        private void task_form_MouseDown(object sender, MouseEventArgs e)
        {
            deltaX = e.X;
            deltaY = e.Y;
        }

        private void task_form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // или любую другую, какая удобнее
            {
                Point pos = new Point(Cursor.Position.X - deltaX, Cursor.Position.Y - deltaY);
                task_form.Location = PointToClient(pos);
            }
        }
        #endregion

        ////////////////////////////////////////////////////////ДОБАВИТЬ ТАСК//////////////////////////////////////////////////////
        #region Добавить таск
        private void CreateTaskB_Click(object sender, EventArgs e)
        {
            // закрыть Управление пользователями
            label22_Click(sender, e); // кнопка назад на добавлении нового юзера
            if (clcUs == true) // панель для ввода пароля админа для показа всей таблицы пользователей
            {
                clcUs = false;
                paas.Text = "";
                admin_pass_enter.Visible = false;
            }
            control_users_panel.Visible = false;
            //
            if (panelCT.Visible == false)
            {
                panel1.Select();
                panelCT.Location = new Point(0, panel1.Location.Y + panel1.Height);
                CreateTaskB.ForeColor = Color.FromArgb(255, 255, 255);
                CreateTaskB.BackColor = Color.FromArgb(45, 47, 51);
                panelCT.Visible = true;
            }
            else
            {
                OtmenaB_Click(sender, e);
            }
        }

        #region Цвет кнопки CreateTaskB
        private void CreateTaskB_MouseMove(object sender, MouseEventArgs e)
        {
            if (panelCT.Visible == false)
            {
                CreateTaskB.BackColor = Color.FromArgb(56, 58, 63);
                CreateTaskB.ForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        private void CreateTaskB_MouseLeave(object sender, EventArgs e)
        {
            if (panelCT.Visible == false)
            {
                CreateTaskB.BackColor = Color.FromArgb(32, 34, 37);
                CreateTaskB.ForeColor = Color.FromArgb(185, 186, 189);
            }
        }
        private void CreateTaskB_MouseDown(object sender, MouseEventArgs e)
        {
            if (panelCT.Visible == false)
            {
                CreateTaskB.BackColor = Color.FromArgb(58, 60, 65);
                CreateTaskB.ForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        #endregion

        #region namT, Срок_исполнения, textBox1
        /////////////////////////////////////////////////namT.Заголовок/////////////////////////////////////////////////////
        public bool clcT1 = false; // если пишем текст в создании новой заявки
        public bool clcT2 = false;
        public bool clcT3 = false;
        private void namT_Enter(object sender, EventArgs e)
        {
            clcT1 = true;
            border_background_panel.BackColor = Color.FromArgb(120, 136, 214);
        } 
        private void namT_Leave(object sender, EventArgs e)
        {
            clcT1 = false;
            border_background_panel.BackColor = Color.FromArgb(36, 36, 39);
            textBox1.Text = textBox1.Text.TrimStart(); // удаляем пробелы
            textBox1.Text = textBox1.Text.TrimEnd();
        }
        private void namT_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_panel.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT1 == false)
                border_background_panel.BackColor = Color.Black;
        }
        private void namT_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_panel.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT1 == false)
                border_background_panel.BackColor = Color.FromArgb(36, 36, 39);
        }
        ///////////////////////////////////////////////Срок_исполнения///////////////////////////////////////////////////
        private void Срок_исполнения_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (Срок_исполнения.MaskFull)
            if (!e.IsValidInput)
            {
                MessageBox.Show("Неверный формат даты!\nПроверьте вводимую дату.");
                Срок_исполнения.Text = "";
            }
            else
            {
                DateTime userDate = (DateTime)e.ReturnValue;
                if (userDate < DateTime.Now)
                {
                    MessageBox.Show("Срок исполнения меньше или равен сегодняшней дате!\nПроверьте вводимую дату.");
                    Срок_исполнения.Text = "";
                }
            }
        }
        private void Срок_исполнения_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_panel2.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2 == false)
                border_background_panel2.BackColor = Color.FromArgb(36, 36, 39);
        }

        private void Срок_исполнения_Enter(object sender, EventArgs e)
        {
            clcT2 = true;
            border_background_panel2.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void Срок_исполнения_Leave(object sender, EventArgs e)
        {
            if (!Срок_исполнения.MaskFull)
            {
                    Срок_исполнения.Text = "";
            }
            else Срок_исполнения.ValidatingType = typeof(DateTime);
            clcT2 = false;
            border_background_panel2.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void Срок_исполнения_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_panel2.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2 == false)
                border_background_panel2.BackColor = Color.Black;
        }
        ///////////////////////////////////////////textBox1.Стоимость///////////////////////////////////////////////////
        private void textBox1_Enter(object sender, EventArgs e)
        {
            clcT3 = true;
            border_background_panel3.BackColor = Color.FromArgb(120, 136, 214);
            textBox1.Text = "\u20BD"; 
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.TrimStart(); // удаляем пробелы
            textBox1.Text = textBox1.Text.TrimEnd();
            if (textBox1.Text == "\u20BD") 
            textBox1.Text = "";
            clcT3 = false;
            border_background_panel3.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_panel3.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT3 == false)
                border_background_panel3.BackColor = Color.Black;

        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_panel3.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT3 == false)
                border_background_panel3.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) // только цифры
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 127 && number != 8)
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
            else 
            {
                textBox1.Select(textBox1.Text.Length - 1, 0);

                textBox1.Focus();

                textBox1.ScrollToCaret();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Цвет кнопки ПрикрепитьФайл
        private void AddF_MouseMove(object sender, MouseEventArgs e)
        {
            AddF.BackColor = Color.FromArgb(109,122,193); 
        }
        private void AddF_MouseLeave(object sender, EventArgs e)
        {
            AddF.BackColor = Color.FromArgb(120,136,214); 
        }

        private void AddF_MouseDown(object sender, MouseEventArgs e)
        {
            AddF.BackColor = Color.FromArgb(97, 110, 171);
        }
        #endregion

        private void AddF_Click(object sender, EventArgs e) // СОХРАНИТЬ ФАЙЛ
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            PathToFile = openFileDialog1.FileName; // получаем путь к выбранному файлу
            TypeFile = Path.GetExtension(PathToFile); // тип выбранного файла
            name_fillee = Path.GetFileNameWithoutExtension(openFileDialog1.FileName); // только имя выбранного файла
            if (name_fillee != "" && name_fillee != null)
            {
                AddF.Text = Path.GetFileName(openFileDialog1.FileName);
                AddF.ForeColor = Color.FromArgb(185, 186, 189);
                AddF.BackColor = Color.FromArgb(114, 128, 198);
            }
        }

        #region Цвета кнопок Отмена и Добавить
        private void OtmenaB_MouseMove(object sender, MouseEventArgs e)
        {
            OtmenaB.Font = new Font(OtmenaB.Font.Name, 14, FontStyle.Bold | FontStyle.Underline);
        }

        private void OtmenaB_MouseLeave(object sender, EventArgs e)
        {
            OtmenaB.Font = new Font(OtmenaB.Font.Name, 14, FontStyle.Bold | FontStyle.Regular);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void EnterB_MouseMove(object sender, MouseEventArgs e)
        {
            EnterB.BackColor = Color.FromArgb(104, 162, 118);
        }

        private void EnterB_MouseLeave(object sender, EventArgs e)
        {
            EnterB.BackColor = Color.FromArgb(67, 181, 129);
        }

        private void EnterB_MouseDown(object sender, MouseEventArgs e)
        {
            EnterB.BackColor = Color.FromArgb(92, 144, 105);
        }
        #endregion

        private void OtmenaB_Click(object sender, EventArgs e) // Кнопка Отмена
        {
            panelCT.Visible = false;
            //border_background_panel.BackColor = 
            CreateTaskB.BackColor = Color.FromArgb(32, 34, 37);
            CreateTaskB.ForeColor = Color.FromArgb(185, 186, 189);
            panelCT.Visible = false;
            namT.Text = "";
            Срок_исполнения.Text = "";
            textBox1.Text = "";
            AddF.Text = "Прикрепить файл";
            AddF.ForeColor = Color.FromArgb(219,220,221); // белый текст
            AddF.BackColor = Color.FromArgb(120, 136, 214); // фиолетовый фон
        }

        private void EnterB_Click(object sender, EventArgs e) // добавить задачу
        {
            panel1.Select();
            if ((namT.Text != "")  && (textBox1.Text != "") && (Срок_исполнения.MaskFull) && (AddF.Text != "Прикрепить файл"))
            {
                 NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
                 con.Open();

                 NpgsqlCommand da2 = new NpgsqlCommand("select null from tasks where name_t = @name_T", con); // уникально ли имя
                 da2.Parameters.AddWithValue("@name_T", namT.Text);
                 if (da2.ExecuteScalar() != null)
                 {
                    MessageBox.Show("Данное имя заявки уже существует!");
                 }

                 else 
                 {
                    NpgsqlCommand da3 = new NpgsqlCommand("create_task", con) //create_task(namet varchar, auser integer, datCmplt date, costT int)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    try
                    {
                        da3.Parameters.Add("namet", NpgsqlDbType.Varchar, 250).Value = namT.Text;
                        da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                        da3.Parameters.Add("datcmplt", NpgsqlDbType.Varchar, 250).Value = Convert.ToString(Срок_исполнения.Text);
                        textBox1.Text = textBox1.Text.Trim(new char [] {'\u20BD'});
                        da3.Parameters.Add("costt", NpgsqlDbType.Integer).Value = Convert.ToInt32(textBox1.Text);
                        Int32 new_task_id = Convert.ToInt32(da3.ExecuteScalar());
                        if (new_task_id != -1) // на всякий случай проверяем добавлена ли задача 
                        {
                            databaseFilePut(new_task_id, name_fillee, TypeFile, PathToFile); // databaseFilePut(int id_T , string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД

                            OtmenaB_Click(sender, e); //КАК КНОПКА ОТМЕНА

                            reload_tables_Click(sender, e);
                            MessageBox.Show("Заявка добавлена!");
                        }
                        else MessageBox.Show("Заявка не добавлена!");
                    }
                    catch (NpgsqlException ex) 
                    {
                        if (Convert.ToString(ex.Message) == "P0001: Нет разрешения на добавление задач.")
                            MessageBox.Show("Нет разрешения на добавление задач!");
                    }
                    con.Close();
                }    
            }
            else
            {
                if ((namT.Text == ""))
                {
                    border_background_panel.BackColor = Color.FromArgb(209,73,73);
                }

                if (!Срок_исполнения.MaskFull)
                {
                    border_background_panel2.BackColor = Color.FromArgb(209, 73, 73);
                }
                if ((textBox1.Text == ""))
                {
                    border_background_panel3.BackColor = Color.FromArgb(209, 73, 73);
                }
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Кнопка Переместить_таск на следующую стадию
        #region Цвет кнопки Переместить таск
        private void label16_MouseMove(object sender, MouseEventArgs e)
        {
            label16.BackColor = Color.FromArgb(109, 122, 193);
        }
        private void label16_MouseLeave(object sender, EventArgs e)
        {
            label16.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void label16_MouseDown(object sender, MouseEventArgs e)
        {
            label16.BackColor = Color.FromArgb(97, 110, 171);
        }
        #endregion

        #region Плюсик и панель за плюсиком, добавляющим таск
        private void label16_Click(object sender, EventArgs e) // переместить таск
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand Totalf2 = new NpgsqlCommand("SELECT id_t FROM tasks WHERE name_t = @name_T", con); // ID заявки которую выбрали 
            Totalf2.Parameters.AddWithValue("@name_T", C);
            Int32 new_task_id = Convert.ToInt32(Totalf2.ExecuteScalar());
            if ((stage_t == 1) || (stage_t == 7))
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
                    task_form.Visible = false;
                    loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
                    MessageBox.Show(returnedValue);
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        task_form.Visible = false;
                        loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                    }    
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\nОбратитесь к администратору.");
                }
            }
            else if ((stage_t == 2) || (stage_t == 3) || (stage_t == 4) || (stage_t == 5) || (stage_t == 6))
            {
                NpgsqlCommand da3 = new NpgsqlCommand("moving_task_1", con) //moving_task_2(idt integer, auser integer, now_stage_task integer)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    da3.Parameters.Add("idt", NpgsqlDbType.Integer).Value = new_task_id;
                    da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                    da3.Parameters.Add("now_stage_task", NpgsqlDbType.Integer).Value = stage_t;
                    string returnedValue = da3.ExecuteScalar().ToString();
                    task_form.Visible = false;
                    loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
                    MessageBox.Show(returnedValue);
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        task_form.Visible = false;
                        loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                    }
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\nОбратитесь к администратору.");
                }
            }
            else if (stage_t == 8)
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
                    string returnedValue = da3.ExecuteScalar().ToString();
                    task_form.Visible = false;
                    loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
                    MessageBox.Show(returnedValue);
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        task_form.Visible = false;
                        loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                    }
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\nОбратитесь к администратору.");
                }
            }
            else MessageBox.Show("Ошибка!\nОбратитесь к администратору.");
            con.Close();
        }
        #endregion
        #endregion

        ////////////////////////////////////////////////////УПРАВЛЕНИЕ ПОЛЬЗОВАТЕЛЯМИ//////////////////////////////////////////////
        #region Управление пользователями

        #region panel_connect_1 - НАЗАД
        private void panel_connect_1_MouseDown(object sender, MouseEventArgs e)
        {
            panel_connect_1.BackColor = Color.FromArgb(58, 60, 65);
            if (New_user_form.Visible == true)
            add_user.BackColor = Color.FromArgb(58, 60, 65);
            if (Del_user_form.Visible == true)
            delete_user.BackColor = Color.FromArgb(58, 60, 65);
        }
        private void panel_connect_1_MouseLeave(object sender, EventArgs e)
        {
            panel_connect_1.BackColor = Color.FromArgb(41, 43, 47);
            if (New_user_form.Visible == true)
                add_user.BackColor = Color.FromArgb(41, 43, 47);
            if (Del_user_form.Visible == true)
                delete_user.BackColor = Color.FromArgb(41, 43, 47);
        }
        private void panel_connect_1_MouseMove(object sender, MouseEventArgs e)
        {
            panel_connect_1.BackColor = Color.FromArgb(56, 58, 63);
            if (New_user_form.Visible == true)
                add_user.BackColor = Color.FromArgb(56, 58, 63);
            if (Del_user_form.Visible == true)
                delete_user.BackColor = Color.FromArgb(56, 58, 63);
        }
        private void panel_connect_1_Click(object sender, EventArgs e)
        {
            label22_Click(sender, e);
        }
        #endregion

        #region label22 - отмена
        private void label22_MouseMove(object sender, MouseEventArgs e)
        {
            label22.Font = new Font(OtmenaB.Font.Name, 14, FontStyle.Bold | FontStyle.Underline);
        }
        private void label22_MouseLeave(object sender, EventArgs e)
        {
            label22.Font = new Font(OtmenaB.Font.Name, 14, FontStyle.Bold | FontStyle.Regular);
        }
        private void label22_Click(object sender, EventArgs e)
        {
            new_id_for_user.Text = "";
            new_pass_for_user.Text = "";
            new_access_for_user.Text = "";

            add_user.BackColor = Color.FromArgb(51, 52, 57); 
            panel_connect_1.Visible = false;
            New_user_form.Visible = false;
            clcU = false;
            //////////////////////////////////////////////////////////////////////////////
            login_user.Text = "";
            admin_pass.Text = "";

            delete_user.BackColor = Color.FromArgb(51, 52, 57); 
            panel_connect_1.Visible = false;
            Del_user_form.Visible = false;
            clcUd = false;

            add_user.ForeColor = Color.FromArgb(185, 186, 189); // Кнопка "Добавить" становится серого шрифта
            delete_user.ForeColor = Color.FromArgb(185, 186, 189); // Кнопка "Добавить" становится серого шрифта
        }
        #endregion

        #region users_button Управление пользователями
        private void users_button_MouseMove(object sender, MouseEventArgs e)
        {
            if (control_users_panel.Visible == false)
            {
                users_button.BackColor = Color.FromArgb(56, 58, 63);
                users_button.ForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        private void users_button_MouseLeave(object sender, EventArgs e)
        {
            if (control_users_panel.Visible == false)
            {
                users_button.BackColor = Color.FromArgb(32, 34, 37);
                users_button.ForeColor = Color.FromArgb(185, 186, 189);
            }
        }
        private void users_button_MouseDown(object sender, MouseEventArgs e)
        {
            if (control_users_panel.Visible == false)
            {
                users_button.BackColor = Color.FromArgb(58, 60, 65);
                users_button.ForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void users_button_Click(object sender, EventArgs e)
        {
            OtmenaB_Click(sender, e); //КАК КНОПКА ОТМЕНА
            if (control_users_panel.Visible == false)
            {
                users_button.BackColor = Color.FromArgb(32, 34, 37);
                control_users_panel.Location = new Point(users_button.Location.X, panel1.Location.Y + users_button.Height);
                control_users_panel.Visible = true;
            }
            else
            {
                label22_Click(sender, e); // кнопка назад на добавлении нового юзера
                if (clcUs == true) // панель для ввода пароля админа для показа всей таблицы пользователей
                {
                    clcUs = false;
                    paas.Text = "";
                    admin_pass_enter.Visible = false;
                }
                control_users_panel.Visible = false;
            }
        }
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////

        #region add_user 
        private void add_user_MouseDown(object sender, MouseEventArgs e)
        {
            add_user.BackColor = Color.FromArgb(58, 60, 65); 
        }

        private void add_user_MouseLeave(object sender, EventArgs e)
        {
            if (clcU == false)
            {
                add_user.BackColor = Color.FromArgb(51, 52, 57); 
                add_user.ForeColor = Color.FromArgb(185, 186, 189);
            }
        }

        private void add_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcU == false)
            {
                add_user.BackColor = Color.FromArgb(56, 58, 63);
                add_user.ForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        private void add_user_Click(object sender, EventArgs e)
        {
            if (clcUd == true)
            {
                label22_Click(sender, e);
            }
            if (clcUs == true)
            {
                show_user.BackColor = Color.FromArgb(51, 52, 57);
                show_user.ForeColor = Color.FromArgb(185, 186, 189);
                clcUs = false;
                paas.Text = "";
                admin_pass_enter.Visible = false;
            }
            if (clcU != true) // если нажали на ЭТУ кнопку, то показывать
            {
                add_user.BackColor = Color.FromArgb(41, 43, 47);
                add_user.ForeColor = Color.FromArgb(255, 255, 255);

                clcU = true;
                New_user_form.Location = new Point(control_users_panel.Location.X - 5 - New_user_form.Width, control_users_panel.Location.Y);
                panel_connect_1.Location = New_user_form.Location;
                panel_connect_1.Width = control_users_panel.Location.X - New_user_form.Location.X + add_user.Location.X + 2;
                panel_connect_1.BringToFront(); // на переднем плане
                panel_connect_1.Visible = true;
                New_user_form.Visible = true;
            }
            else // иначе закрыть
            {
                label22_Click(sender, e);
            } 
        }
        #endregion

        // если пишем текст в нижних текстбоксах
        public bool clcT2_1 = false; 
        public bool clcT2_2 = false;
        public bool clcT2_3 = false;
        //
        #region new_id_for_user
        private void new_id_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_new_id_for_user.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT2_1 == false)
                    border_background_new_id_for_user.BackColor = Color.Black;
        }
        private void new_id_for_user_Enter(object sender, EventArgs e)
        {
            clcT2_1 = true;
            border_background_new_id_for_user.BackColor = Color.FromArgb(120, 136, 214);
            //////////////////////////////////////////ТАБЛИЦА ВСЕХ ЮЗЕРОВ//////////////////////////////////////////////
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlDataReader reader;
            NpgsqlCommand daT = new NpgsqlCommand("Select * from all_users_without_pass(@auser)", con);
            daT.Parameters.Add("@auser", NpgsqlDbType.Integer).Value = ID_Main;
            DataTable dt = new DataTable();
            reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(reader);
            table_users.DataSource = dt;
            reader.Close();
            con.Close();

            for (int i = 0; i < table_users.Columns.Count; i++) // переименование столбцов
            {
                string str = table_users.Columns[i].HeaderText;
                if (str == "logn")
                {
                    table_users.Columns[i].HeaderText = "Логин";
                }
                else if (str == "accs")
                    table_users.Columns[i].HeaderText = "Доступ";
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            panel_connect_2.Location = new Point(border_background_new_id_for_user.Location.X + border_background_new_id_for_user.Width - 3, New_user_form.Location.Y + border_background_new_id_for_user.Location.Y );
            panel_connect_2.BringToFront(); // на переднем плане
            panel_for_table_users.BringToFront();
            panel_for_table_users.Location = new Point(panel_connect_2.Location.X + panel_connect_2.Width, panel_connect_2.Location.Y);
            panel_connect_2.Visible = true;
            panel_for_table_users.Visible = true;
        }

        ////////////////////////////////////////////высота под кол-во ячеек//////////////////////////////////////////////
        private void table_users_Paint(object sender, PaintEventArgs e)
        {
            this.table_users.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 15);
            this.table_users.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);//выбранная ячейка фон
            this.table_users.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);//выбранная ячейка текст
            this.table_users.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
            this.table_users.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53);
            this.table_users.DefaultCellStyle.Font = new Font("Calibri", 12);
            this.table_users.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53);//выбранная ячейка фон
            this.table_users.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//выбранная ячейка текст
            this.table_users.DefaultCellStyle.ForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек 
            this.table_users.DefaultCellStyle.BackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            int height = table_users.Location.Y + table_users.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in table_users.Rows)
            {
                height += dr.Height;
            }
            table_users.Height = height;
            int width = table_users.Location.Y + table_users.ColumnHeadersHeight;
            foreach (DataGridViewColumn dr in table_users.Columns)
            {
                width += dr.Width;
            }
            table_users.Width = width;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void new_id_for_user_Leave(object sender, EventArgs e)
        {
            clcT2_1 = false;
            border_background_new_id_for_user.BackColor = Color.FromArgb(36, 36, 39);
            new_id_for_user.Text = new_id_for_user.Text.TrimStart(); // удаляем пробелы
            new_id_for_user.Text = new_id_for_user.Text.TrimEnd();
                panel_connect_2.Visible = false;
                panel_for_table_users.Visible = false;
        }
        private void new_id_for_user_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_new_id_for_user.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2_1 == false)
                    border_background_new_id_for_user.BackColor = Color.FromArgb(36, 36, 39);
        }
        #endregion

        #region new_pass_for_user
        private void new_pass_for_user_Enter(object sender, EventArgs e)
        {
            clcT2_2 = true;
            border_background_new_pass_for_user.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void new_pass_for_user_Leave(object sender, EventArgs e)
        {
            clcT2_2 = false;
            border_background_new_pass_for_user.BackColor = Color.FromArgb(36, 36, 39);
            new_pass_for_user.Text = new_pass_for_user.Text.TrimStart(); // удаляем пробелы
            new_pass_for_user.Text = new_pass_for_user.Text.TrimEnd();
        }
        private void new_pass_for_user_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_new_pass_for_user.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2_2 == false)
                    border_background_new_pass_for_user.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void new_pass_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_new_pass_for_user.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT2_2 == false)
                    border_background_new_pass_for_user.BackColor = Color.Black;
        }
        #endregion

        #region new_access_for_user
        private void new_access_for_user_Leave(object sender, EventArgs e)
        {
            clcT2_3 = false;
            border_background_new_access_for_user.BackColor = Color.FromArgb(36, 36, 39);
            new_access_for_user.Text = new_access_for_user.Text.TrimStart(); // удаляем пробелы
            new_access_for_user.Text = new_access_for_user.Text.TrimEnd();
        }
        private void new_access_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_new_access_for_user.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT2_3 == false)
                    border_background_new_access_for_user.BackColor = Color.Black;
        }
        private void new_access_for_user_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_new_access_for_user.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2_3 == false)
                    border_background_new_access_for_user.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void new_access_for_user_Enter(object sender, EventArgs e)
        {
            clcT2_3 = true;
            border_background_new_access_for_user.BackColor = Color.FromArgb(120, 136, 214);
        }
        #endregion

        #region add_new_user_button - Принять Нового пользователя 
        private void label23_MouseLeave(object sender, EventArgs e)
        {
            add_new_user_button.BackColor = Color.FromArgb(67, 181, 129); // зелёный
        }
        private void label23_MouseMove(object sender, MouseEventArgs e)
        {
            add_new_user_button.BackColor = Color.FromArgb(104, 162, 118); 
        }
        private void label23_MouseDown(object sender, MouseEventArgs e)
        {
            add_new_user_button.BackColor = Color.FromArgb(92, 144, 105); 
        }
        private void label23_Click(object sender, EventArgs e) // ДОБАВИТЬ НОВОГО ПОЛЬЗОВАТЕЛЯ
        {
            panel1.Select();
            if ((new_id_for_user.Text != "") && (new_pass_for_user.Text != "") && (new_access_for_user.Text != ""))
            {
                NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
                con.Open();


                NpgsqlCommand da2 = new NpgsqlCommand("select id_u from users where login = @logn", con); // уникально ли имя
                da2.Parameters.AddWithValue("@logn", login_user.Text);
                if (da2.ExecuteScalar() != null)
                {
                    MessageBox.Show("Введённый логин уже существует!\nПросмотрите таблицу всех пользователей.");
                }

                else
                {
                    NpgsqlCommand da3 = new NpgsqlCommand("add_usr", con) //add_usr(logn character varying, pas character varying, aces integer, auser integer)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    try
                    {
                        da3.Parameters.Add("logn", NpgsqlDbType.Varchar, 250).Value = new_id_for_user.Text;
                        da3.Parameters.Add("pas", NpgsqlDbType.Varchar, 250).Value = new_pass_for_user.Text;
                        int access = 4;
                        switch (new_access_for_user.Text)
                        {
                            case "Истец":
                                access = 1;
                                break;
                            case "Оператор":
                                access = 2;
                                break;
                            case "Бухгалтерия":
                                access = 3;
                                break;
                        }
                        da3.Parameters.Add("aces", NpgsqlDbType.Integer).Value = access;
                        da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                        da3.ExecuteNonQuery();

                        //////////////////////////////КАК КНОПКА НАЗАД/////////////////////////////////
                        label22_Click(sender, e);
                        ///////////////////////////////////////////////////////////////////////////////
                        MessageBox.Show("Пользователь добавлен!");
                    }
                    catch (NpgsqlException ex)
                    {
                        if (Convert.ToString(ex.Message) == "P0001: Нельзя добавить пользователя!")
                            MessageBox.Show("Нет прав на добавление пользователей!");
                    }
                    con.Close();
                }
            }
            else
            {
                if (new_id_for_user.Text == "")
                {
                    border_background_new_id_for_user.BackColor = Color.FromArgb(209, 73, 73);
                }
                if (new_pass_for_user.Text == "")
                {
                    border_background_new_pass_for_user.BackColor = Color.FromArgb(209, 73, 73);
                }
                if (new_access_for_user.Text == "")
                {
                    border_background_new_access_for_user.BackColor = Color.FromArgb(209, 73, 73);
                }
            }
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////

        #region delete_user
        private void delete_user_MouseDown(object sender, MouseEventArgs e)
        {
            delete_user.BackColor = Color.FromArgb(58, 60, 65);
        }

        private void delete_user_MouseLeave(object sender, EventArgs e)
        {
            if (clcUd == false)
            {
                delete_user.BackColor = Color.FromArgb(51, 52, 57);
                delete_user.ForeColor = Color.FromArgb(185, 186, 189);
            }
        }

        private void delete_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcUd == false)
            {
                delete_user.BackColor = Color.FromArgb(56, 58, 63);
                delete_user.ForeColor = Color.FromArgb(219, 220, 221);
            }
        }

        private void delete_user_Click(object sender, EventArgs e)
        {
            if (clcU == true)
            {
                label22_Click(sender, e);
            }
            if (clcUs == true)
            {
                show_user.BackColor = Color.FromArgb(51, 52, 57);
                show_user.ForeColor = Color.FromArgb(185, 186, 189);
                clcUs = false;
                paas.Text = "";
                admin_pass_enter.Visible = false;
            }
            if (clcUd != true) // если нажали на ЭТУ кнопку, то показывать
            {
                delete_user.BackColor = Color.FromArgb(41, 43, 47);
                delete_user.ForeColor = Color.FromArgb(255, 255, 255);

                clcUd = true;
                Del_user_form.Location = new Point(control_users_panel.Location.X - 5 - Del_user_form.Width, control_users_panel.Location.Y + delete_user.Location.Y);
                panel_connect_1.Location = Del_user_form.Location;
                panel_connect_1.Width = control_users_panel.Location.X - Del_user_form.Location.X + delete_user.Location.X + 2;
                panel_connect_1.BringToFront(); // на переднем плане
                panel_connect_1.Visible = true;
                Del_user_form.Visible = true;
            }
            else // иначе закрыть
            {
                label22_Click(sender, e);
            }
        }
        #endregion

        // если пишем текст в нижних текстбоксах
        public bool clcT3_1 = false;
        public bool clcT3_2 = false;
        //
        #region login_user
        private void login_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_login_user.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT3_1 == false)
                    border_background_login_user.BackColor = Color.Black;
        }
        private void login_user_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_login_user.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT3_1 == false)
                    border_background_login_user.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void login_user_Enter(object sender, EventArgs e)
        {
            clcT3_1 = true;
            border_background_login_user.BackColor = Color.FromArgb(120, 136, 214);
            //////////////////////////////////////////ТАБЛИЦА ВСЕХ ЮЗЕРОВ//////////////////////////////////////////////
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlDataReader reader;
            NpgsqlCommand daT = new NpgsqlCommand("Select * from all_users_without_pass(@auser)", con);
            daT.Parameters.Add("@auser", NpgsqlDbType.Integer).Value = ID_Main;
            DataTable dt = new DataTable();
            reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(reader);
            table_users.DataSource = dt;
            reader.Close();
            con.Close();

            for (int i = 0; i < table_users.Columns.Count; i++) // переименование столбцов
            {
                string str = table_users.Columns[i].HeaderText;
                if (str == "logn")
                {
                    table_users.Columns[i].HeaderText = "Логин";
                }
                else if (str == "accs")
                    table_users.Columns[i].HeaderText = "Доступ";
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            panel_connect_2.Location = new Point(border_background_login_user.Location.X + border_background_login_user.Width - 3, Del_user_form.Location.Y + border_background_login_user.Location.Y);
            panel_connect_2.BringToFront(); // на переднем плане
            panel_for_table_users.BringToFront();
            panel_for_table_users.Location = new Point(panel_connect_2.Location.X + panel_connect_2.Width, panel_connect_2.Location.Y);
            panel_connect_2.Visible = true;
            panel_for_table_users.Visible = true;
        }
        private void login_user_Leave(object sender, EventArgs e)
        {
            clcT3_1 = false;
            border_background_login_user.BackColor = Color.FromArgb(36, 36, 39);
            login_user.Text = login_user.Text.TrimStart(); // удаляем пробелы
            login_user.Text = login_user.Text.TrimEnd();
            panel_connect_2.Visible = false;
            panel_for_table_users.Visible = false;
        }
        #endregion

        #region admin_pass
        private void admin_pass_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_admin_pass.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT3_2 == false)
                    border_background_admin_pass.BackColor = Color.Black;
        }
        private void admin_pass_Enter(object sender, EventArgs e)
        {
            clcT3_2 = true;
            border_background_admin_pass.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void admin_pass_Leave(object sender, EventArgs e)
        {
            clcT3_2 = false;
            border_background_admin_pass.BackColor = Color.FromArgb(36, 36, 39);
            admin_pass.Text = admin_pass.Text.TrimStart(); // удаляем пробелы
            admin_pass.Text = admin_pass.Text.TrimEnd();
        }
        private void admin_pass_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_admin_pass.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT3_2 == false)
                    border_background_admin_pass.BackColor = Color.FromArgb(36, 36, 39);
        }
        #endregion

        #region Кнопка Принять Удаление пользователя
        private void del_user_button_MouseLeave(object sender, EventArgs e)
        {
            del_user_button.BackColor = Color.FromArgb(209, 73, 73); //синий как на стартовой
        }
        private void del_user_button_MouseMove(object sender, MouseEventArgs e)
        {
            del_user_button.BackColor = Color.FromArgb(187, 66, 67); // голубой ПРИ НАВЕДЕНИИ
        }
        private void del_user_button_MouseDown(object sender, MouseEventArgs e)
        {
            del_user_button.BackColor = Color.FromArgb(166, 60, 60); // серый
        }
        private void del_user_button_Click(object sender, EventArgs e) // УДАЛИТЬ ПОЛЬЗОВАТЕЛЯ
        {
                panel1.Select();
                if ((login_user.Text != "") && (admin_pass.Text != ""))
                {
                    NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
                    con.Open();

                    NpgsqlCommand da2 = new NpgsqlCommand("select id_u from users where login = @logn", con); // уникально ли имя
                    da2.Parameters.AddWithValue("@logn", login_user.Text);

                    if (da2.ExecuteScalar() == null)
                    {
                        MessageBox.Show("Введённого пользователя не существует!\nПросмотрите таблицу всех пользователей.");
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(
                            "Удаление пользователя - необратимый процесс! Это приведет к потере автора у задач, созданных им.\n\nВы уверены?\n\n",
                            "Внимание!",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2,
                            MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.OK)
                        {
                            int id_User = (int)da2.ExecuteScalar(); // если введенный логин существует, то берём его айди
                            NpgsqlCommand da3 = new NpgsqlCommand("del_usr", con) //del_usr(id integer, auser integer)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            try
                            {
                                da3.Parameters.Add("id", NpgsqlDbType.Integer).Value = id_User;
                                da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                                da3.ExecuteNonQuery();

                                //////////////////////////////КАК КНОПКА НАЗАД/////////////////////////////////
                                label22_Click(sender, e);
                                ///////////////////////////////////////////////////////////////////////////////
                                MessageBox.Show("Пользователь удален!");
                            }
                            catch (NpgsqlException ex)
                            {
                                if (Convert.ToString(ex.Message) == "P0001: Удалить нельзя")
                                    MessageBox.Show("Нет прав на удаление пользователей!");
                            }
                            con.Close();
                        }
                    }
                }
                else
                {
                    if (login_user.Text == "")
                    {
                        border_background_login_user.BackColor = Color.FromArgb(209, 73, 73);
                    }
                    if (admin_pass.Text == "")
                    {
                        border_background_admin_pass.BackColor = Color.FromArgb(209, 73, 73);
                    }
                }
            
        }
        #endregion

        #region otmena_udaleniya - Кнопка 
        private void otmena_udaleniya_MouseMove(object sender, MouseEventArgs e)
        {
            otmena_udaleniya.Font = new Font(OtmenaB.Font.Name, 14, FontStyle.Bold | FontStyle.Underline);
        }

        private void otmena_udaleniya_MouseLeave(object sender, EventArgs e)
        {
            otmena_udaleniya.Font = new Font(OtmenaB.Font.Name, 14, FontStyle.Bold | FontStyle.Regular);
        }

        private void otmena_udaleniya_Click(object sender, EventArgs e)
        {
            label22_Click(sender, e);
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Кнопка Все пользователи
        private void show_user_MouseDown(object sender, MouseEventArgs e)
        {
            show_user.BackColor = Color.FromArgb(58, 60, 65);
        }

        private void show_user_MouseLeave(object sender, EventArgs e)
        {
            if (clcUs == false)
            {
                show_user.BackColor = Color.FromArgb(51, 52, 57);
                show_user.ForeColor = Color.FromArgb(185, 186, 189);
            }
        }

        private void show_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcUs == false)
            {
                show_user.BackColor = Color.FromArgb(56, 58, 63);
                show_user.ForeColor = Color.FromArgb(219, 220, 221);
            }
        }

        private void show_user_Click(object sender, EventArgs e)
        {
            if (clcU == true || clcUd == true)
            {
                label22_Click(sender, e);
            }
            if (clcUs != true) // если нажали на ЭТУ кнопку, то показывать
            {
                show_user.BackColor = Color.FromArgb(41, 43, 47);
                show_user.ForeColor = Color.FromArgb(255, 255, 255);

                clcUs = true;
                admin_pass_enter.Location = new Point(control_users_panel.Location.X, control_users_panel.Location.Y + control_users_panel.Height);
                admin_pass_enter.BringToFront();
                admin_pass_enter.Visible = true;
            }
            else // иначе закрыть
            {
                clcUs = false;
                paas.Text = "";
                admin_pass_enter.Visible = false;
            }
        }
        #endregion

        #region Кнопка для перехода на таблицу всех юзеров
        private void button_for_pass_admin_MouseDown(object sender, MouseEventArgs e)
        {
            button_for_pass_admin.BackColor = Color.FromArgb(97, 110, 171); 
        }

        private void button_for_pass_admin_MouseLeave(object sender, EventArgs e)
        {
            button_for_pass_admin.BackColor = Color.FromArgb(120, 136, 214); 
        }

        private void button_for_pass_admin_MouseMove(object sender, MouseEventArgs e)
        {
            button_for_pass_admin.BackColor = Color.FromArgb(109, 122, 193); 
        }

        private void button_for_pass_admin_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand da2 = new NpgsqlCommand("select login from users where (id_u = @id) and (pass = @pass) and (acces = 0)", con); // получаем уровень доступа
            da2.Parameters.Add("@pass", NpgsqlDbType.Varchar, 250).Value = paas.Text;
            da2.Parameters.Add("@id", NpgsqlDbType.Integer).Value = ID_Main;
            if (da2.ExecuteScalar() == null)
            {
                MessageBox.Show("Неверный пароль!\nПовторите попытку.");
            }
            else
            {
                TableAllUsers obj2 = new TableAllUsers(ID_Main); // передача id в форму Главная
                obj2.Show();
            }
            con.Close();
        }
        #endregion
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region кнопки Закрыть, развернуть, свернуть
        private void butn_close2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void butn_plus2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void butn_minus2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void butn_close_MouseMove(object sender, MouseEventArgs e)
        {
            butn_close2.Visible = true;
        }
        private void butn_plus_MouseMove(object sender, MouseEventArgs e)
        {
            butn_plus2.Visible = true;
        }
        private void butn_minus_MouseMove(object sender, MouseEventArgs e)
        {
            butn_minus2.Visible = true;
        }
        private void butn_close2_MouseLeave(object sender, EventArgs e)
        {
            butn_close2.Visible = false;
        }
        private void butn_plus2_MouseLeave(object sender, EventArgs e)
        {
            butn_plus2.Visible = false;
        }
        private void butn_minus2_MouseLeave(object sender, EventArgs e)
        {
            butn_minus2.Visible = false;
        }
        #endregion

        #region Фокус на другой элемент
        private void panelCT_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select(); // убрать фокус с текст боксов
        }
        private void New_user_form_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }


        #endregion

        
    }
}
