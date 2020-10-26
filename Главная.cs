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
using System.Threading;

namespace Scrum
{
    public partial class Главная : Form
    {
        //////////////////PUBLIC ПЕРЕМЕННЫЕ////////////////

        public bool clc = false; // если нажали на таблицу - перемещение таска к концу мышки
        public bool clcU = false; // если нажали на добавить пользователя
        public bool clcUd = false; // если нажали на удалить пользователя
        public bool clcUs = false; // если нажали на все пользователи

        public int ID_Main;
        public int access_user; // доступ залогиневшегося юзера
        public string C; // значение, что находится в выбираемой ячейке
        public int stage_t; // стадия на которой находится выбранный таск

        public bool max_size_from = false; // развернута форма или нет

        public int W = 0; // сохранить ширину окна для свернуть-развернуть
        public int H = 0; // сохранить высоту окна для свернуть-развернуть
        public int W_max_for_tables = 0; // сохранить ширину столбцов перед тем как развернуть
        public int H_max_for_tables = 0; // сохранить высоту столбцов перед тем как развернуть
        public int size_all_table_before_max = 0; // размеры таблиц до развертывания окна, чтоб можно было восстановить их размер
        public int location_panel2; // расположение panel2 по Х, потому что он не хочет динамически в коде присваивать значение
        public int location_panel2_2;

        public int[,] arr = new int[8,2]; // массив для сохранения локации по Х и ширины столбцов перед развертыванием окна

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

        public static string cs = "Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk";
        public Главная(int id, string user_name)
        {
            InitializeComponent();
            panel1.Select();
            location_panel2 = panel2.Location.X; // расположение panel2 по Х, потому что он не хочет динамически в коде присваивать значение
            location_panel2_2 = panel2.Location.Y;
            panel2 = new DoubleBufferedPanel();
            loadTables(ds1, dt1, dataGridView1, ds2, dt2, dataGridView2, ds3, dt3, dataGridView3, ds4, dt4, dataGridView4, ds5, dt5, dataGridView5, ds6, dt6, dataGridView6, ds7, dt7, dataGridView7, ds8, dt8, dataGridView8);
            ID_Main = id;
            #region Зашел админ или кто
            NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            NpgsqlCommand da2 = new NpgsqlCommand("select acces from users where id_u = @id", con); // получаем уровень доступа
            da2.Parameters.AddWithValue("@id", ID_Main);
            Int32 access_now_user = Convert.ToInt32(da2.ExecuteScalar());
            access_user = access_now_user;
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
                archive_button.Location = new Point(0, 0);
                archive_button.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            }
            #endregion
            ToolTip t = new ToolTip(); // всплывающая подсказкa
            t.SetToolTip(reload_tables, "Обновить");
            активный_пользователь.Text = user_name;
        }

        #region Graphics рисуем линии на панелях
        private void panelCT_Paint(object sender, PaintEventArgs e) // рисуем линию на форме создания таска
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(63, 64, 68), 1);
                g.DrawLine(p, new Point(10, 787), new Point(734, 787)); // разница 26 пикселей
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
                g.DrawLine(p, new Point(22, 539), new Point(421, 539));
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
        private void background_form_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(12, 14, 17), 1);
                g.DrawLine(p, new Point(0, 0), new Point(1664, 0));
                p.Dispose();
                g.Dispose(); // очищаем память
            }
        }
        #endregion

        private void Главная_MouseDown(object sender, MouseEventArgs e) // перемещение окна
        {
            if (max_size_from == false)
            {
                panel1.Select();
                base.Capture = false;
                Message m = Message.Create(base.Handle, 161, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref m);
            }
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
            NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            #region Заполнение таблиц данными 
            //////////////////////////////////////////////////ТАБЛИЦА 1/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 1 ORDER BY date_create", con);
            ds1.Reset();
            da1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView1.DataSource = dt1;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 2/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 2 ORDER BY date_complete", con);
            ds2.Reset();
            da2.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 3/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 3 ORDER BY date_complete", con);
            ds3.Reset();
            da3.Fill(ds3);
            dt3 = ds3.Tables[0];
            dataGridView3.DataSource = dt3;
            dataGridView3.Columns[1].Visible = false;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 4/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da4 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 4 ORDER BY date_complete", con);
            ds4.Reset();
            da4.Fill(ds4);
            dt4 = ds4.Tables[0];
            dataGridView4.DataSource = dt4;
            dataGridView4.Columns[1].Visible = false;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 5/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 5 ORDER BY date_complete", con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            dataGridView5.DataSource = dt5;
            dataGridView5.Columns[1].Visible = false;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 6/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 6 ORDER BY date_complete", con);
            ds6.Reset();
            da6.Fill(ds6);
            dt6 = ds6.Tables[0];
            dataGridView6.DataSource = dt6;
            dataGridView6.Columns[1].Visible = false;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 7/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 7 ORDER BY date_complete", con);
            ds7.Reset();
            da7.Fill(ds7);
            dt7 = ds7.Tables[0];
            dataGridView7.DataSource = dt7;
            dataGridView7.Columns[1].Visible = false;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            //////////////////////////////////////////////////ТАБЛИЦА 8/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("SELECT name_t, date_complete FROM tasks WHERE stage = 8 ORDER BY date_complete", con);
            ds8.Reset();
            da8.Fill(ds8);
            dt8 = ds8.Tables[0];
            dataGridView8.DataSource = dt8;
            dataGridView8.Columns[1].Visible = false;
            dataGridView8.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); //цвет не выбранных ячеек 
            dataGridView8.DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);//цвет шрифта на не выбранных ячеек
            #endregion

            con.Close();
        }
        #endregion

        #region reload_tables - Обновить
        public void reload_tables_Click(object sender, EventArgs e) // кнопка обновить таблицы
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

        #region Таски из таблиц
        #region Таск из таблицы 1
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 1;
            Task_form frm = new Task_form(C, "На согласование", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
        }
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
        }
        #endregion

        #region Таск из таблицы 2
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView2.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 2;
            Task_form frm = new Task_form(C, "На утверждение", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
            if (dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
            if (dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        #endregion

        #region Таск из таблицы 3
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView3.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 3;
            Task_form frm = new Task_form(C, "В работу", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView3_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[0];
            if (dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView3_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[0];
            if (dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        #endregion
        
        #region Таск из таблицы 4
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView4.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 4;
            Task_form frm = new Task_form(C, "На формирование", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView4_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView4.CurrentCell = dataGridView4.Rows[e.RowIndex].Cells[0];
            if (dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView4_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.CurrentCell = dataGridView4.Rows[e.RowIndex].Cells[0];
            if (dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        #endregion

        #region Таск из таблицы 5
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView5.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 5;
            Task_form frm = new Task_form(C, "На заключение", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView5_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView5.CurrentCell = dataGridView5.Rows[e.RowIndex].Cells[0];
            if (dataGridView5.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView5.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView5_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView5.CurrentCell = dataGridView5.Rows[e.RowIndex].Cells[0];
            if (dataGridView5.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView5.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        #endregion

        #region Таск из таблицы 6
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView6.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 6;
            Task_form frm = new Task_form(C, "На исполнение", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView6_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView6.CurrentCell = dataGridView6.Rows[e.RowIndex].Cells[0];
            if (dataGridView6.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView6.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView6_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView6.CurrentCell = dataGridView6.Rows[e.RowIndex].Cells[0];
            if (dataGridView6.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView6.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        #endregion

        #region Таск из таблицы 7
        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView7.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 7;
            Task_form frm = new Task_form(C, "На оплату", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView7_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView7.CurrentCell = dataGridView7.Rows[e.RowIndex].Cells[0];
            if (dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView7_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView7.CurrentCell = dataGridView7.Rows[e.RowIndex].Cells[0];
            if (dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
        }
        #endregion

        #region Таск из таблицы 8
        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView8.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 8;
            Task_form frm = new Task_form(C, "В архив", stage_t, ID_Main, access_user);
            frm.Show();
        }

        private void dataGridView8_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView8.CurrentCell = dataGridView8.Rows[e.RowIndex].Cells[0];
            if (dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно красному
            {
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 66, 67); // темный красный
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else if (dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно желтому
            {
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 147, 60); // темный желтый
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214); // фиолетовый
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void dataGridView8_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView8.CurrentCell = dataGridView8.Rows[e.RowIndex].Cells[0];
            if (dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(240, 71, 71)) // если равно темно красному
            {
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71); // красный
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
            else if (dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(250, 166, 26)) // если равно темно желтому
            {
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26); // желтый
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242);
            }
            else
            {
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 49, 53); // серый
                dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.FromArgb(219, 220, 221);
            }
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
                DateTime t = (DateTime)dataGridView2.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView2.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView2.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView2.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView2.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView2.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView2.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
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
                DateTime t = (DateTime)dataGridView3.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView3.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView3.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView3.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView3.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView3.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView3.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
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
                DateTime t = (DateTime)dataGridView4.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView4.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView4.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView4.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView4.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView4.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView4.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
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
                DateTime t = (DateTime)dataGridView5.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView5.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView5.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView5.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView5.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView5.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView5.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
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
                DateTime t = (DateTime)dataGridView6.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView6.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView6.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView6.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView6.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView6.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView6.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
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
                DateTime t = (DateTime)dataGridView7.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView7.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView7.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView7.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView7.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView7.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView7.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
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
                DateTime t = (DateTime)dataGridView8.Rows[dr.Index].Cells[1].Value;
                TimeSpan diff1 = t.Subtract(DateTime.Now); // красный или желтый цвет при истечении срока
                if (diff1.TotalDays <= 7.0)
                {
                    dataGridView8.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(240, 71, 71);
                    dataGridView8.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 71, 71);
                }
                else if (diff1.TotalDays <= 30)
                {
                    dataGridView8.Rows[dr.Index].DefaultCellStyle.BackColor = Color.FromArgb(250, 166, 26);
                    dataGridView8.Rows[dr.Index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 166, 26);
                    dataGridView8.Rows[dr.Index].DefaultCellStyle.SelectionForeColor = Color.FromArgb(240, 240, 242); // серый чуть светлее чем обычно
                    dataGridView8.Rows[dr.Index].DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                }
                height += dr.Height;
            }
            dataGridView8.Height = height + 4;
        }
        #endregion

        #region VisibleChanged - убирать красные границы
        private void New_user_form_VisibleChanged(object sender, EventArgs e)
        {
            border_background_new_id_for_user.BackColor = Color.FromArgb(36, 36, 39);
            border_background_new_pass_for_user.BackColor = Color.FromArgb(36, 36, 39);
            border_background_new_access_for_user.BackColor = Color.FromArgb(36, 36, 39);
            panel14.BackColor = Color.FromArgb(36, 36, 39);
        }

        private void Del_user_form_VisibleChanged(object sender, EventArgs e)
        {
            border_background_login_user.BackColor = Color.FromArgb(36, 36, 39);
            border_background_admin_pass.BackColor = Color.FromArgb(36, 36, 39);
        }

        private void panelCT_VisibleChanged(object sender, EventArgs e)
        {
            border_background_panel.BackColor = Color.FromArgb(36, 36, 39);
            border_background_panel2.BackColor = Color.FromArgb(36, 36, 39);
            border_background_panel3.BackColor = Color.FromArgb(36, 36, 39);
            panel8.BackColor = Color.FromArgb(36, 36, 39);
            panel3.BackColor = Color.FromArgb(36, 36, 39);
            panel6.BackColor = Color.FromArgb(36, 36, 39);
            panel10.BackColor = Color.FromArgb(36, 36, 39);
            panel12.BackColor = Color.FromArgb(36, 36, 39);
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

        #region namT, Срок_исполнения, textBox1 и все остальные строки в Добавить Заявку
        public bool clcT1 = false; // если пишем текст в создании новой заявки
        public bool clcT2 = false;
        public bool clcT3 = false;
        public bool clcT4 = false;
        public bool clcT5 = false;
        public bool clcT6 = false;
        public bool clcT7 = false;
        public bool clcT8 = false;
        /////////////////////////////////////////////////namT.Заголовок/////////////////////////////////////////////////////
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
                    DialogResult result = MessageBox.Show(
                           "Неверный формат даты!\nПроверьте вводимую дату.",
                           "Ошибка!",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
                Срок_исполнения.Text = "";
            }
            else
            {
                DateTime userDate = (DateTime)e.ReturnValue;
                if (userDate < DateTime.Now)
                {
                        DialogResult result = MessageBox.Show(
                          "Срок исполнения меньше или равен сегодняшней дате!\nПроверьте вводимую дату.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
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
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                Срок_исполнения.Select(0, 0);
            });
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
        /////////////////////////////////////////////////textbox8.Прошу вас обеспечить/////////////////////////////////////////////////////
        private void textbox8_Enter(object sender, EventArgs e)
        {
            clcT4 = true;
            panel8.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void textbox8_Leave(object sender, EventArgs e)
        {
            clcT4 = false;
            panel8.BackColor = Color.FromArgb(36, 36, 39);
            textBox8.Text = textBox8.Text.TrimStart(); // удаляем пробелы
            textBox8.Text = textBox8.Text.TrimEnd();
        }
        private void textbox8_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel8.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT4 == false)
                    panel8.BackColor = Color.Black;
        }
        private void textbox8_MouseLeave(object sender, EventArgs e)
        {
            if (panel8.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT4 == false)
                    panel8.BackColor = Color.FromArgb(36, 36, 39);
        }
        /////////////////////////////////////////////////textBox9.Предмет закупки/////////////////////////////////////////////////////
        private void textBox9_Enter(object sender, EventArgs e)
        {
            clcT5 = true;
            panel3.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void textBox9_Leave(object sender, EventArgs e)
        {
            clcT5 = false;
            panel3.BackColor = Color.FromArgb(36, 36, 39);
            textBox9.Text = textBox9.Text.TrimStart(); // удаляем пробелы
            textBox9.Text = textBox9.Text.TrimEnd();
        }
        private void textBox9_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel3.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT5 == false)
                    panel3.BackColor = Color.Black;
        }
        private void textBox9_MouseLeave(object sender, EventArgs e)
        {
            if (panel3.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT5 == false)
                    panel3.BackColor = Color.FromArgb(36, 36, 39);
        }
        /////////////////////////////////////////////////textBox10.Цель закупки/////////////////////////////////////////////////////
        private void textBox10_Enter(object sender, EventArgs e)
        {
            clcT6 = true;
            panel6.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void textBox10_Leave(object sender, EventArgs e)
        {
            clcT6 = false;
            panel6.BackColor = Color.FromArgb(36, 36, 39);
            textBox10.Text = textBox10.Text.TrimStart(); // удаляем пробелы
            textBox10.Text = textBox10.Text.TrimEnd();
        }
        private void textBox10_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel6.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT6 == false)
                    panel6.BackColor = Color.Black;
        }
        private void textBox10_MouseLeave(object sender, EventArgs e)
        {
            if (panel6.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT6 == false)
                    panel6.BackColor = Color.FromArgb(36, 36, 39);
        }
        /////////////////////////////////////////////////maskedTextBox1.Контактные данные/////////////////////////////////////////////////////
        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            clcT7 = true;
            panel10.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            clcT7 = false;
            panel10.BackColor = Color.FromArgb(36, 36, 39);
            maskedTextBox1.Text = maskedTextBox1.Text.TrimStart(); // удаляем пробелы
            maskedTextBox1.Text = maskedTextBox1.Text.TrimEnd();
        }
        private void maskedTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel10.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT7 == false)
                    panel10.BackColor = Color.Black;
        }
        private void maskedTextBox1_MouseLeave(object sender, EventArgs e)
        {
            if (panel10.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT7 == false)
                    panel10.BackColor = Color.FromArgb(36, 36, 39);
        }
        /////////////////////////////////////////////////textBox12.Перечень товара Работы Услуг закупки/////////////////////////////////////////////////////
        private void textBox12_Enter(object sender, EventArgs e)
        {
            clcT8 = true;
            panel12.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void textBox12_Leave(object sender, EventArgs e)
        {
            clcT8 = false;
            panel12.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox12_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel12.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT8 == false)
                    panel12.BackColor = Color.Black;
        }
        private void textBox12_MouseLeave(object sender, EventArgs e)
        {
            if (panel12.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT8 == false)
                    panel12.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

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
            CreateTaskB.BackColor = Color.FromArgb(32, 34, 37);
            CreateTaskB.ForeColor = Color.FromArgb(185, 186, 189);
            namT.Text = "";
            Срок_исполнения.Text = "";
            textBox1.Text = "";
        }

        private void EnterB_Click(object sender, EventArgs e) // добавить задачу
        {
            panel1.Select();
            if ((namT.Text != "") && (textBox8.Text != "") && (textBox9.Text != "") && (textBox10.Text != "") && (Срок_исполнения.MaskFull) && (textBox1.Text != "") && (maskedTextBox1.MaskFull) && (textBox12.Text != ""))
            {
                 NpgsqlConnection con = new NpgsqlConnection(cs);
                 con.Open();

                 NpgsqlCommand da2 = new NpgsqlCommand("select null from tasks where name_t = @name_T", con); // уникально ли имя
                 da2.Parameters.AddWithValue("@name_T", namT.Text);
                 if (da2.ExecuteScalar() != null)
                 {
                    DialogResult result = MessageBox.Show(
                          "Данное имя заявки уже существует!\nПожалуйста, придумайте новое название.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
                 }

                 else 
                 {
                    NpgsqlCommand da3 = new NpgsqlCommand("create_task", con) //create_task(namet character varying, auser integer, datcmplt character varying, costt integer, proshuobesp character varying, predmzak character varying, purposezak character varying, telnumber character varying, listzak character varying)
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
                        da3.Parameters.Add("proshuobesp", NpgsqlDbType.Varchar, 250).Value = textBox8.Text;
                        da3.Parameters.Add("predmzak", NpgsqlDbType.Varchar, 250).Value = textBox9.Text;
                        da3.Parameters.Add("purposezak", NpgsqlDbType.Varchar, 250).Value = textBox10.Text;
                        da3.Parameters.Add("telnumber", NpgsqlDbType.Varchar, 250).Value = maskedTextBox1.Text;
                        da3.Parameters.Add("listzak", NpgsqlDbType.Varchar, 250).Value = textBox12.Text;
                        Int32 new_task_id = Convert.ToInt32(da3.ExecuteScalar());
                        if (new_task_id != -1) // на всякий случай проверяем добавлена ли задача 
                        {
                            MessageBox.Show("Заявка добавлена!");
                        }
                        else MessageBox.Show("Заявка не добавлена!");
                        OtmenaB_Click(sender, e); //КАК КНОПКА ОТМЕНА
                        reload_tables_Click(sender, e);
                    }
                    catch (NpgsqlException ex) 
                    {
                        if (Convert.ToString(ex.Message) == "P0001: Нет разрешения на добавление задач.")
                            MessageBox.Show("Нет разрешения на добавление задач!");
                        else MessageBox.Show("Непредвиденная ошибка!");
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
                if ((textBox8.Text == ""))
                {
                    panel8.BackColor = Color.FromArgb(209, 73, 73);
                }
                if ((textBox9.Text == ""))
                {
                    panel3.BackColor = Color.FromArgb(209, 73, 73);
                }
                if ((textBox10.Text == ""))
                {
                    panel6.BackColor = Color.FromArgb(209, 73, 73);
                }
                if (!Срок_исполнения.MaskFull)
                {
                    border_background_panel2.BackColor = Color.FromArgb(209, 73, 73);
                }
                if ((textBox1.Text == ""))
                {
                    border_background_panel3.BackColor = Color.FromArgb(209, 73, 73);
                }
                if (!maskedTextBox1.MaskFull)
                {
                    panel10.BackColor = Color.FromArgb(209, 73, 73);
                }
                if ((textBox12.Text == ""))
                {
                    panel12.BackColor = Color.FromArgb(209, 73, 73);
                }
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            new_fio_for_user.Text = "";
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
            else
            {
                panel_connect_1.BackColor = Color.FromArgb(41, 43, 47);
                if (New_user_form.Visible == true)
                    add_user.BackColor = Color.FromArgb(41, 43, 47);
            }
        }

        private void add_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcU == false)
            {
                add_user.BackColor = Color.FromArgb(56, 58, 63);
                add_user.ForeColor = Color.FromArgb(219, 220, 221);
            }
            else
            {
                panel_connect_1.BackColor = Color.FromArgb(56, 58, 63);
                if (New_user_form.Visible == true)
                    add_user.BackColor = Color.FromArgb(56, 58, 63);
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
                New_user_form.Location = new Point(control_users_panel.Location.X - 5 - New_user_form.Width, control_users_panel.Location.Y + add_user.Location.Y);
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
        public bool clcT2_4 = false;
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
            NpgsqlConnection con = new NpgsqlConnection(cs);
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
                    table_users.Columns[i].HeaderText = "Логин";
                else if (str == "accs")
                    table_users.Columns[i].HeaderText = "Доступ";
                else if (str == "fioo")
                    table_users.Columns[i].HeaderText = "ФИО";
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            panel_connect_2.Location = new Point(border_background_new_id_for_user.Location.X + border_background_new_id_for_user.Width + 297, New_user_form.Location.Y + border_background_new_id_for_user.Location.Y );
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

        #region new_pass_for_user
        private void new_fio_for_user_Enter(object sender, EventArgs e)
        {
            clcT2_4 = true;
            panel14.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void new_fio_for_user_Leave(object sender, EventArgs e)
        {
            clcT2_4 = false;
            panel14.BackColor = Color.FromArgb(36, 36, 39);
            new_fio_for_user.Text = new_fio_for_user.Text.TrimStart(); // удаляем пробелы
            new_fio_for_user.Text = new_fio_for_user.Text.TrimEnd();
        }
        private void new_fio_for_user_MouseLeave(object sender, EventArgs e)
        {
            if (panel14.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2_4 == false)
                    panel14.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void new_fio_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel14.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT2_4 == false)
                    panel14.BackColor = Color.Black;
        }
        #endregion

        #region new_access_for_user
        private void new_access_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_new_access_for_user.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT2_3 == false)
                    border_background_new_access_for_user.BackColor = Color.Black;
        }
        private void new_access_for_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void new_access_for_user_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_new_access_for_user.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2_3 == false)
                    border_background_new_access_for_user.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void new_access_for_user_Click(object sender, EventArgs e)
        {
            clcT2_3 = true;
            border_background_new_access_for_user.BackColor = Color.FromArgb(120, 136, 214);
            comboBox1.Select();
        }
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
        }
        private void comboBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (border_background_new_access_for_user.BackColor != Color.FromArgb(209, 73, 73)) // проверка на красный цвет должна быть везде
                if (clcT2_3 == false)
                    border_background_new_access_for_user.BackColor = Color.Black;
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            if (border_background_new_access_for_user.BackColor != Color.FromArgb(209, 73, 73))
                if (clcT2_3 == false)
                    border_background_new_access_for_user.BackColor = Color.FromArgb(36, 36, 39);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            clcT2_3 = true;
            border_background_new_access_for_user.BackColor = Color.FromArgb(120, 136, 214);
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            clcT2_3 = false;
            border_background_new_access_for_user.BackColor = Color.FromArgb(36, 36, 39);
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            new_access_for_user.Text = comboBox1.Text;
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
            if ((new_id_for_user.Text != "") && (new_pass_for_user.Text != "") && (new_fio_for_user.Text != "") && (new_access_for_user.Text != ""))
            {
                NpgsqlConnection con = new NpgsqlConnection(cs);
                con.Open();
                NpgsqlCommand da2 = new NpgsqlCommand("select id_u from users where login = @logn", con); // уникально ли имя
                da2.Parameters.AddWithValue("@logn", login_user.Text);
                if (da2.ExecuteScalar() != null)
                {
                    DialogResult result = MessageBox.Show(
                          "Введённый логин уже существует!\nПросмотрите таблицу всех пользователей.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    NpgsqlCommand da3 = new NpgsqlCommand("add_usr", con) //add_usr(logn character varying, pas character varying, aces integer, auser integer, in_fio character varying)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    try
                    {
                        da3.Parameters.Add("logn", NpgsqlDbType.Varchar, 250).Value = new_id_for_user.Text;
                        da3.Parameters.Add("pas", NpgsqlDbType.Varchar, 250).Value = new_pass_for_user.Text;
                        da3.Parameters.Add("in_fio", NpgsqlDbType.Varchar, 250).Value = new_fio_for_user.Text;
                        int access = 6;
                        switch (new_access_for_user.Text)
                        {
                            case "Завхоз":
                                access = 1;
                                break;
                            case "Контрактник":
                                access = 2;
                                break;
                            case "Бухгалтер":
                                access = 3;
                                break;
                            case "Зам. директора":
                                access = 5;
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
                if (new_fio_for_user.Text == "")
                {
                    panel14.BackColor = Color.FromArgb(209, 73, 73);
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
            else
            {
                panel_connect_1.BackColor = Color.FromArgb(41, 43, 47);
                if (Del_user_form.Visible == true)
                    delete_user.BackColor = Color.FromArgb(41, 43, 47);
            }
        }

        private void delete_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcUd == false)
            {
                delete_user.BackColor = Color.FromArgb(56, 58, 63);
                delete_user.ForeColor = Color.FromArgb(219, 220, 221);
            }
            else
            {
                panel_connect_1.BackColor = Color.FromArgb(56, 58, 63);
                if (Del_user_form.Visible == true)
                    delete_user.BackColor = Color.FromArgb(56, 58, 63);
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
            NpgsqlConnection con = new NpgsqlConnection(cs);
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

            panel_connect_2.Location = new Point(border_background_login_user.Location.X + border_background_login_user.Width + 297, Del_user_form.Location.Y + border_background_login_user.Location.Y);
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
                    NpgsqlConnection con = new NpgsqlConnection(cs);
                    con.Open();

                    NpgsqlCommand da2 = new NpgsqlCommand("select id_u from users where login = @logn", con); // уникально ли имя
                    da2.Parameters.AddWithValue("@logn", login_user.Text);

                    if (da2.ExecuteScalar() == null)
                    {
                    DialogResult result = MessageBox.Show(
                          "Введённого пользователя не существует!\nПросмотрите таблицу всех пользователей.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                    {
                        NpgsqlCommand da4 = new NpgsqlCommand("select login from users where (id_u = @id) and (pass = @pass) and (acces = 0)", con); // получаем уровень доступа
                        da4.Parameters.Add("@pass", NpgsqlDbType.Varchar, 250).Value = admin_pass.Text;
                        da4.Parameters.Add("@id", NpgsqlDbType.Integer).Value = ID_Main;
                        if (da4.ExecuteScalar() == null)
                        {
                        admin_pass.Text = "";
                        border_background_admin_pass.BackColor = Color.FromArgb(209, 73, 73);
                        DialogResult result = MessageBox.Show(
                          "Неверный пароль!\nПовторите попытку.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
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
            else
            {
                show_user.BackColor = Color.FromArgb(41, 43, 47);
            }
        }

        private void show_user_MouseMove(object sender, MouseEventArgs e)
        {
             show_user.BackColor = Color.FromArgb(56, 58, 63);
             show_user.ForeColor = Color.FromArgb(219, 220, 221);
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

                panel1.Select();
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
            NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            NpgsqlCommand da2 = new NpgsqlCommand("select login from users where (id_u = @id) and (pass = @pass) and (acces = 0)", con); // получаем уровень доступа
            da2.Parameters.Add("@pass", NpgsqlDbType.Varchar, 250).Value = paas.Text;
            da2.Parameters.Add("@id", NpgsqlDbType.Integer).Value = ID_Main;
            if (da2.ExecuteScalar() == null)
            {
                DialogResult result = MessageBox.Show(
                          "Неверный пароль!\nПовторите попытку.",
                          "Ошибка!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                bool check_open = false;
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc) // открыта уже форма или нет, чтоб повторно не открывалась
                {
                    //iterate through
                    if (frm.Name == "TableAllUsers")
                    {
                        check_open = false;
                    }
                    else
                    {
                        check_open = true;
                    }
                }
                if (check_open == true)
                {
                    paas.Text = "";
                    panel1.Select();
                    admin_pass_enter.Visible = false;
                    TableAllUsers obj2 = new TableAllUsers(ID_Main); // передача id в форму Главная
                    obj2.Show();
                } 
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
            if (max_size_from == false)
            {
                double koef = (double) dataGridView1.Width / background_form.Width;

                W = this.Size.Width;
                H = this.Size.Height;
                W_max_for_tables = dataGridView1.MaximumSize.Width;
                H_max_for_tables = dataGridView1.MaximumSize.Height;
                size_all_table_before_max = dataGridView1.Size.Width;
                #region arr[, ]
                arr[0, 0] = dataGridView1.Location.X;
                arr[0, 1] = label7.Location.X;
                //
                arr[1, 0] = dataGridView2.Location.X ;
                arr[1, 1] = label8.Location.X;
                //
                arr[2, 0] = dataGridView3.Location.X;
                arr[2, 1] = label9.Location.X;
                //
                arr[3, 0] = dataGridView4.Location.X;
                arr[3, 1] = label10.Location.X;
                //
                arr[4, 0] = dataGridView5.Location.X;
                arr[4, 1] = label11.Location.X;
                //
                arr[5, 0] = dataGridView6.Location.X;
                arr[5, 1] = label12.Location.X;
                //
                arr[6, 0] = dataGridView7.Location.X;
                arr[6, 1] = label13.Location.X;
                //
                arr[7, 0] = dataGridView8.Location.X;
                arr[7, 1] = label14.Location.X;
                #endregion

                //
                // Maximazed
                //
                var rectangle = Screen.FromControl(this).Bounds;
                this.FormBorderStyle = FormBorderStyle.None;
                Size = new Size(rectangle.Width, rectangle.Height);
                Location = new Point(0, 0);
                Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
                this.Size = new Size(workingRectangle.Width, workingRectangle.Height);
                max_size_from = true;
                //
                // MaximumSize unlimited
                //
                dataGridView1.MaximumSize = new Size(5000, 5000);
                dataGridView2.MaximumSize = new Size(5000, 5000);
                dataGridView3.MaximumSize = new Size(5000, 5000);
                dataGridView4.MaximumSize = new Size(5000, 5000);
                dataGridView5.MaximumSize = new Size(5000, 5000);
                dataGridView6.MaximumSize = new Size(5000, 5000);
                dataGridView7.MaximumSize = new Size(5000, 5000);
                dataGridView8.MaximumSize = new Size(5000, 5000);
                //
                // Width
                //
                dataGridView1.Width = (int)(background_form.Width * koef);
                dataGridView2.Width = dataGridView1.Width;
                dataGridView3.Width = dataGridView1.Width;
                dataGridView4.Width = dataGridView1.Width;
                dataGridView5.Width = dataGridView1.Width;
                dataGridView6.Width = dataGridView1.Width;
                dataGridView7.Width = dataGridView1.Width;
                dataGridView8.Width = dataGridView1.Width;
                //
                // Переменные / коэффициенты
                //
                int размер_панели_с_таблицами = background_form.Width - location_panel2 * 2;
                double расстояние = ((double)((double)размер_панели_с_таблицами / 8) - dataGridView1.Width) / 2; // между таблицами расстояние
                double отступ_внутри = (размер_панели_с_таблицами -(расстояние * 7 + dataGridView1.Width * 8))/2; // отступ перед первым столбцом
                //
                // dataGridView.Location
                //
                dataGridView1.Location = new Point((int)отступ_внутри, 11); // начальная точка
                dataGridView2.Location = new Point(dataGridView1.Location.X + dataGridView1.Width + (int)расстояние, 11); // 6 пикселей - расстояние между таблицами
                dataGridView3.Location = new Point(dataGridView2.Location.X + dataGridView1.Width + (int)расстояние, 11);
                dataGridView4.Location = new Point(dataGridView3.Location.X + dataGridView1.Width + (int)расстояние, 11);
                dataGridView5.Location = new Point(dataGridView4.Location.X + dataGridView1.Width + (int)расстояние, 11);
                dataGridView6.Location = new Point(dataGridView5.Location.X + dataGridView1.Width + (int)расстояние, 11);
                dataGridView7.Location = new Point(dataGridView6.Location.X + dataGridView1.Width + (int)расстояние, 11);
                dataGridView8.Location = new Point(dataGridView7.Location.X + dataGridView1.Width + (int)расстояние, 11);
                //
                // label.Location
                //
                label7.Location = new Point(location_panel2 + dataGridView1.Location.X + dataGridView1.Width / 2 - label7.Width / 2, label7.Location.Y);
                label8.Location = new Point(location_panel2 + dataGridView2.Location.X + dataGridView1.Width / 2 - label8.Width / 2, label7.Location.Y);
                label9.Location = new Point(location_panel2 + dataGridView3.Location.X + dataGridView1.Width / 2 - label9.Width / 2, label7.Location.Y);
                label10.Location = new Point(location_panel2 + dataGridView4.Location.X + dataGridView1.Width / 2 - label10.Width / 2, label7.Location.Y);
                label11.Location = new Point(location_panel2 + dataGridView5.Location.X + dataGridView1.Width / 2 - label11.Width / 2, label7.Location.Y);
                label12.Location = new Point(location_panel2 + dataGridView6.Location.X + dataGridView1.Width / 2 - label12.Width / 2, label7.Location.Y);
                label13.Location = new Point(location_panel2 + dataGridView7.Location.X + dataGridView1.Width / 2 - label13.Width / 2, label7.Location.Y);
                label14.Location = new Point(location_panel2 + dataGridView8.Location.X + dataGridView1.Width / 2 - label14.Width / 2, label7.Location.Y);
                //
                // MaximumSize
                //
                dataGridView1.MaximumSize = new Size(dataGridView1.Width, this.Height - location_panel2 - 44); // 44 = 22 снизу - 11*2
                dataGridView2.MaximumSize = new Size(dataGridView2.Width, this.Height - location_panel2 - 44);
                dataGridView3.MaximumSize = new Size(dataGridView3.Width, this.Height - location_panel2 - 44);
                dataGridView4.MaximumSize = new Size(dataGridView4.Width, this.Height - location_panel2 - 44);
                dataGridView5.MaximumSize = new Size(dataGridView5.Width, this.Height - location_panel2 - 44);
                dataGridView6.MaximumSize = new Size(dataGridView6.Width, this.Height - location_panel2 - 44);
                dataGridView7.MaximumSize = new Size(dataGridView7.Width, this.Height - location_panel2 - 44);
                dataGridView8.MaximumSize = new Size(dataGridView8.Width, this.Height - location_panel2 - 44);
            }
            else
            {
                this.Size = new Size(W, H);

                dataGridView1.Width = size_all_table_before_max;
                dataGridView2.Width = size_all_table_before_max;
                dataGridView3.Width = size_all_table_before_max;
                dataGridView4.Width = size_all_table_before_max;
                dataGridView5.Width = size_all_table_before_max;
                dataGridView6.Width = size_all_table_before_max;
                dataGridView7.Width = size_all_table_before_max;
                dataGridView8.Width = size_all_table_before_max;

                dataGridView1.Location = new Point(arr[0, 0], 11); // 6 пикселей - расстояние между таблицами
                dataGridView2.Location = new Point(arr[1, 0], 11); 
                dataGridView3.Location = new Point(arr[2, 0], 11);
                dataGridView4.Location = new Point(arr[3, 0], 11);
                dataGridView5.Location = new Point(arr[4, 0], 11);
                dataGridView6.Location = new Point(arr[5, 0], 11);
                dataGridView7.Location = new Point(arr[6, 0], 11);
                dataGridView8.Location = new Point(arr[7, 0], 11);

                label7.Location = new Point(arr[0, 1], label7.Location.Y);
                label8.Location = new Point(arr[1, 1], label7.Location.Y);
                label9.Location = new Point(arr[2, 1], label7.Location.Y);
                label10.Location = new Point(arr[3, 1], label7.Location.Y);
                label11.Location = new Point(arr[4, 1], label7.Location.Y);
                label12.Location = new Point(arr[5, 1], label7.Location.Y);
                label13.Location = new Point(arr[6, 1], label7.Location.Y);
                label14.Location = new Point(arr[7, 1], label7.Location.Y);

                dataGridView1.MaximumSize = new Size(W_max_for_tables, H_max_for_tables); 
                dataGridView2.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);
                dataGridView3.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);
                dataGridView4.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);
                dataGridView5.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);
                dataGridView6.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);
                dataGridView7.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);
                dataGridView8.MaximumSize = new Size(W_max_for_tables, H_max_for_tables);

                max_size_from = false;
            }
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
        private void Del_user_form_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }
        #endregion

        #region archive_button
        private void archive_button_Click(object sender, EventArgs e)
        {
            bool check_open = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc) // открыта уже форма или нет, чтоб повторно не открывалась
            {
                if (frm.Name == "Archive_tasks")
                {
                    check_open = false;
                }
                else
                {
                    check_open = true;
                }
            }
            if (check_open == true)
            {
                Archive_tasks obj1 = new Archive_tasks();
                obj1.Show();
            }
        }

        private void archive_button_MouseMove(object sender, MouseEventArgs e)
        {
            archive_button.BackColor = Color.FromArgb(56, 58, 63);
            archive_button.ForeColor = Color.FromArgb(219, 220, 221);
        }

        private void archive_button_MouseLeave(object sender, EventArgs e)
        {
            archive_button.BackColor = Color.FromArgb(32, 34, 37);
            archive_button.ForeColor = Color.FromArgb(185, 186, 189);
        }

        private void archive_button_MouseDown(object sender, MouseEventArgs e)
        {
            archive_button.BackColor = Color.FromArgb(58, 60, 65);
            archive_button.ForeColor = Color.FromArgb(255, 255, 255);
        }


        #endregion

        
    }
}
