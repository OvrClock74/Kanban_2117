﻿using System;
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

namespace Scrum
{
    public partial class Главная : Form
    {
        //////////////////PUBLIC ПЕРЕМЕННЫЕ////////////////

        public int ID_Main; 
        public bool clc = false; // если нажали на таблицу - перемещение таска к концу мышки
        public bool clcU = false; // если нажали на добавить пользователя
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
            loadTables( ds1,  dt1,  dataGridView1, ds2,  dt2,  dataGridView2,ds3,  dt3,  dataGridView3,ds4,  dt4,  dataGridView4,ds5,  dt5,  dataGridView5,ds6,  dt6,  dataGridView6,ds7,  dt7,  dataGridView7,ds8,  dt8,  dataGridView8);
            ID_Main = id;
            #region Зашел админ или кто
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand da2 = new NpgsqlCommand("select acces from users where id_u = @id", con); // получаем уровень доступа
            da2.Parameters.AddWithValue("@id", ID_Main);
            Int32 access_now_user = Convert.ToInt32(da2.ExecuteScalar());
            con.Close();
            if ((access_now_user == 0) || (access_now_user == 1))
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
            ToolTip t = new ToolTip();
            t.SetToolTip(reload_tables, "Обновить");
        }

        private void Главная_FormClosed(object sender, FormClosedEventArgs e) // крестик - зыкрыть приложение
        {
            Application.Exit();
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
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 2/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 2", con);
            ds2.Reset();
            da2.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 3/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 3", con);
            ds3.Reset();
            da3.Fill(ds3);
            dt3 = ds3.Tables[0];
            dataGridView3.DataSource = dt3;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 4/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da4 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 4", con);
            ds4.Reset();
            da4.Fill(ds4);
            dt4 = ds4.Tables[0];
            dataGridView4.DataSource = dt4;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 5/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 5", con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            dataGridView5.DataSource = dt5;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 6/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 6", con);
            ds6.Reset();
            da6.Fill(ds6);
            dt6 = ds6.Tables[0];
            dataGridView6.DataSource = dt6;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 7/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 7", con);
            ds7.Reset();
            da7.Fill(ds7);
            dt7 = ds7.Tables[0];
            dataGridView7.DataSource = dt7;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.Black;
            //////////////////////////////////////////////////ТАБЛИЦА 8/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 8", con);
            ds8.Reset();
            da8.Fill(ds8);
            dt8 = ds8.Tables[0];
            dataGridView8.DataSource = dt8;
            dataGridView8.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView8.DefaultCellStyle.SelectionForeColor = Color.Black;
            #endregion

            con.Close();
        }

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
        #endregion

        #region Загрузка и выгрузка файлов в и из БД
        public static void databaseFilePut(int id_T , string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД
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

        #region Цвет в таблице прикрепленных файлов
        private void dataGridView_Task_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 130);
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void dataGridView_Task_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Размер таблицы прикрепленный файлов
        private void dataGridView_Task_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView_Task.Rows)
            {
                height += dr.Height;
            }
            int Y = dataGridView_Task.Height;
            dataGridView_Task.Height = height + 3;
            panel3.Height = dataGridView_Task.Height;
            AddFTask.Location = new Point(AddFTask.Location.X, (panel3.Height/2) - (AddFTask.Height/2));
            task_form.Height = dataGridView_Task.Location.Y + label16.Height + dataGridView_Task.Height + (label16.Location.Y - (dataGridView_Task.Location.Y + Y));
        }
        #endregion

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

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "На согласование";
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
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 2
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView2.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 2;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "На утверждение";
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
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 3
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView3.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 3;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "В работу";
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
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView3_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[0];
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 4
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView4.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 4;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "На формирование";
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
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView4_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.CurrentCell = dataGridView4.Rows[e.RowIndex].Cells[0];
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 5
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView5.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 5;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "На заключение";
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
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView5_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView5.CurrentCell = dataGridView5.Rows[e.RowIndex].Cells[0];
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView5.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 6
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView6.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 6;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "На исполнение";
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
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView6_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView6.CurrentCell = dataGridView6.Rows[e.RowIndex].Cells[0];
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView6.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 7
        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView7.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 7;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "На оплату";
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
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView7_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView7.CurrentCell = dataGridView7.Rows[e.RowIndex].Cells[0];
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView7.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region Таск из таблицы 8
        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            C = (string)dataGridView8.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке
            stage_t = 8;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("Добавил(а) {0}", reader["autor"]));
                    label4.Text = (String.Format("{0:d} — {1:d}", reader["date_create"], reader["date_complete"]));
                    label5.Text = (String.Format("Стоимость: {0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";
                    label16.Text = "Оплачено";
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
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 150, 240);
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void dataGridView8_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView8.CurrentCell = dataGridView8.Rows[e.RowIndex].Cells[0];
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView8.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion
        #endregion 

        #region Размеры таблиц стадий
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                height += dr.Height;
            }
            dataGridView1.Height = height + 3;
        }
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView2.Rows)
            {
                height += dr.Height;
            }
            dataGridView2.Height = height + 3;
        }

        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView3.Rows)
            {
                height += dr.Height;
            }
            dataGridView3.Height = height + 3;
        }

        private void dataGridView4_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView4.Rows)
            {
                height += dr.Height;
            }
            dataGridView4.Height = height + 3;
        }

        private void dataGridView5_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView5.Rows)
            {
                height += dr.Height;
            }
            dataGridView5.Height = height + 3;
        }

        private void dataGridView6_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView6.Rows)
            {
                height += dr.Height;
            }
            dataGridView6.Height = height + 3;
        }

        private void dataGridView7_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView7.Rows)
            {
                height += dr.Height;
            }
            dataGridView7.Height = height + 3;
        }

        private void dataGridView8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int height = 0;
            foreach (DataGridViewRow dr in dataGridView8.Rows)
            {
                height += dr.Height;
            }
            dataGridView8.Height = height + 3;
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
            label1.ForeColor = Color.FromArgb(255, 255, 255);
            label1.BackColor = Color.FromArgb(255, 25, 0);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.FromArgb(0, 0, 0);
            label1.BackColor = Color.FromArgb(255, 255, 192);
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

        #region Цвет кнопки СоздатьТаск
        private void CreateTaskB_MouseMove(object sender, MouseEventArgs e)
        {
            CreateTaskB.BackColor = Color.FromArgb(3, 171, 255); // голубой
        }
        private void CreateTaskB_MouseLeave(object sender, EventArgs e)
        {
            CreateTaskB.BackColor = Color.FromArgb(0, 100, 200); // синий
        }
        private void CreateTaskB_MouseUp(object sender, MouseEventArgs e)
        {
            CreateTaskB.BackColor = Color.FromArgb(0, 100, 200); // синий
        }
        private void CreateTaskB_MouseDown(object sender, MouseEventArgs e)
        {
            CreateTaskB.BackColor = Color.FromArgb(112, 179, 227); // серый
        }
        #endregion

        #region Кнопка СоздатьТаск КОТОРАЯ НАХОДИТСЯ НА ВЫСКАКИВАЮЩЕЙ ПАНЕЛИ С ДОБАВЛЕНИЕМ ТАСКА
        private void label19_Click(object sender, EventArgs e)
        {
            OtmenaB_Click(sender, e);
        }
        private void label19_MouseMove(object sender, MouseEventArgs e)
        {
            label19.BackColor = Color.FromArgb(3, 171, 255); // голубой
        }
        private void label19_MouseLeave(object sender, EventArgs e)
        {
            label19.BackColor = Color.FromArgb(0, 100, 200); // синий
        }
        private void label19_MouseDown(object sender, MouseEventArgs e)
        {
            label19.BackColor = Color.FromArgb(112, 179, 227); // серый
        }
        private void label19_MouseUp(object sender, MouseEventArgs e)
        {
            label19.BackColor = Color.FromArgb(0, 100, 200); // синий
        }
        #endregion

        #region Серый текст в Добавить Таск
        /////////////////////////////////////////////////namT.Заголовок/////////////////////////////////////////////////////
        private void namT_Enter(object sender, EventArgs e)
        {
            namT.ForeColor = Color.Black;
            if (namT.Text == "Заголовок")
            {
                namT.Text = "";
            }
        } 
        private void namT_Leave(object sender, EventArgs e)
        {
            if (namT.Text == "")
            {
                namT.Text = "Заголовок";
                namT.ForeColor = Color.Gray;
            }
        }
        private void namT_MouseMove(object sender, MouseEventArgs e)
        {
            if (namT.BackColor == Color.LightCoral)
            {
                namT.ForeColor = Color.Gray;
                namT.BackColor = Color.White;
            }
        }
        ///////////////////////////////////////////////Срок_исполнения///////////////////////////////////////////////////
        private void Срок_исполнения_Leave(object sender, EventArgs e)
        {
            if (Срок_исполнения.Text == "  .  .")
            {
                textBox2.Visible = true;
                textBox2.BringToFront();
                Срок_исполнения.Text = "";
            }
        }
        private void Срок_исполнения_MouseMove(object sender, MouseEventArgs e)
        {
            if (Срок_исполнения.BackColor == Color.LightCoral)
            {
                Срок_исполнения.ForeColor = Color.Black;
                Срок_исполнения.BackColor = Color.White;
            }
        }
        ///////////////////////////////////////////textBox2.Срок исполнения///////////////////////////////////////////////
        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.Visible = false;
            //Срок_исполнения.BringToFront();
            Срок_исполнения.Select();
        }
        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox2.BackColor == Color.LightCoral)
            {
                textBox2.ForeColor = Color.Gray;
                textBox2.BackColor = Color.White;
            }
        }
        ///////////////////////////////////////////textBox1.Стоимость///////////////////////////////////////////////////
        private void textBox1_Enter(object sender, EventArgs e)
        {

            textBox1.ForeColor = Color.Black;
            if (textBox1.Text == "Стоимость")
            {
                textBox1.Text = "\u20BD";
                
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.TrimStart(); // удаляем пробелы
            textBox1.Text = textBox1.Text.TrimEnd();
            if (textBox1.Text == "" || textBox1.Text == "\u20BD")
            {
                textBox1.Text = "Стоимость";
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox1.BackColor == Color.LightCoral)
            {
                textBox1.ForeColor = Color.Gray;
                textBox1.BackColor = Color.White;
            }
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
            AddF.BackColor = Color.LightGray; 
        }
        private void AddF_MouseLeave(object sender, EventArgs e)
        {
            AddF.BackColor = Color.White; 
        }

        private void AddF_MouseUp(object sender, MouseEventArgs e)
        {
            if (AddF.ForeColor == Color.White)
            {
                AddF.BackColor = Color.White;
                AddF.ForeColor = Color.Black;
            }
        }

        private void AddF_MouseDown(object sender, MouseEventArgs e)
        {
            AddF.BackColor = Color.FromArgb(0, 100, 200);
            AddF.ForeColor = Color.White;
        }
        #endregion

        #region Цвета кнопок Отмена и Добавить
        private void OtmenaB_MouseMove(object sender, MouseEventArgs e)
        {
            OtmenaB.BackColor = Color.FromArgb(3, 216, 255); // голубой ПРИ НАВЕДЕНИИ
        }

        private void OtmenaB_MouseLeave(object sender, EventArgs e)
        {
            OtmenaB.BackColor = Color.FromArgb(3, 171, 244); //синий как на стартовой
        }

        private void OtmenaB_MouseDown(object sender, MouseEventArgs e)
        {
            OtmenaB.BackColor = Color.FromArgb(112, 179, 227); // серый
        }

        private void OtmenaB_MouseUp(object sender, MouseEventArgs e)
        {
            OtmenaB.BackColor = Color.FromArgb(3, 216, 255); //синий как на стартовой
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void EnterB_MouseMove(object sender, MouseEventArgs e)
        {
           EnterB.BackColor = Color.FromArgb(3, 216, 255); // голубой ПРИ НАВЕДЕНИИ
        }

        private void EnterB_MouseLeave(object sender, EventArgs e)
        {
            EnterB.BackColor = Color.FromArgb(3, 171, 244); //синий как на стартовой
        }

        private void EnterB_MouseDown(object sender, MouseEventArgs e)
        {
            EnterB.BackColor = Color.FromArgb(112, 179, 227); // серый
        }

        private void EnterB_MouseUp(object sender, MouseEventArgs e)
        {
            EnterB.BackColor = Color.FromArgb(3, 216, 255); //синий как на стартовой
        }
        #endregion

        private void CreateTaskB_Click(object sender, EventArgs e)
        {
            panel1.Select();
            panelCT.Location = CreateTaskB.Location;
            panelCT.Visible = true;            
        }

        private void AddF_Click(object sender, EventArgs e) // СОХРАНИТЬ ФАЙЛ
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            PathToFile = openFileDialog1.FileName; // получаем путь к выбранному файлу
            TypeFile = Path.GetExtension(PathToFile); // тип выбранного файла
            name_fillee = Path.GetFileNameWithoutExtension(openFileDialog1.FileName); // только имя выбранного файла
            AddF.Text = "Файл прикреплен";
            AddF.ForeColor = Color.Gray;
        }

        private void OtmenaB_Click(object sender, EventArgs e) // Кнопка Отмена
        {
            CreateTaskB.BackColor = Color.FromArgb(0, 100, 200); // синий
            AddF.Text = "Прикрепить файл";
            AddF.ForeColor = Color.Black;
            OtmenaB.BackColor = Color.FromArgb(3, 171, 255);
            textBox2.Visible = true;
            panelCT.Visible = false;

            namT.Text = "Заголовок";
            namT.ForeColor = Color.Gray;
            namT.BackColor = Color.White;

            textBox2.Text = "Срок исполнения";
            textBox2.ForeColor = Color.Gray;
            textBox2.BackColor = Color.White;

            Срок_исполнения.ForeColor = Color.Black;
            Срок_исполнения.BackColor = Color.White;
            Срок_исполнения.Text = "";

            textBox1.Text = "Стоимость";
            textBox1.ForeColor = Color.Gray; 
            textBox1.BackColor = Color.White;
        }

        private void EnterB_Click(object sender, EventArgs e) // добавить задачу
        {
            panel1.Select();
            if ((namT.Text != "Заголовок") && (Срок_исполнения.MaskFull) && (textBox1.Text != "Стоимость") && (namT.Text != "")  && (textBox1.Text != ""))
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
                        da3.Parameters.Add("datcmplt", NpgsqlDbType.Varchar, 250).Value = Срок_исполнения.Text;
                        da3.Parameters.Add("costt", NpgsqlDbType.Integer).Value = Convert.ToInt32(textBox1.Text);
                        Int32 new_task_id = Convert.ToInt32(da3.ExecuteScalar());
                        if (new_task_id != -1) // на всякий случай проверяем добавлена ли задача 
                        {
                            databaseFilePut(new_task_id, name_fillee, TypeFile, PathToFile); // databaseFilePut(int id_T , string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД

                            //////////////////////////////КАК КНОПКА ОТМЕНА/////////////////////////////////
                            CreateTaskB.BackColor = Color.FromArgb(0, 100, 200); // синий
                            AddF.Text = "Прикрепить файл";
                            AddF.ForeColor = Color.Black;
                            textBox2.Visible = true;
                            panelCT.Visible = false;
                            EnterB.BackColor = Color.FromArgb(3, 171, 255);

                            namT.Text = "Заголовок";
                            namT.ForeColor = Color.Gray;
                            namT.BackColor = Color.White;

                            textBox2.Text = "Срок исполнения";
                            textBox2.ForeColor = Color.Gray;
                            textBox2.BackColor = Color.White;

                            Срок_исполнения.ForeColor = Color.Black;
                            Срок_исполнения.BackColor = Color.White;

                            textBox1.Text = "Стоимость";
                            textBox1.ForeColor = Color.Gray;
                            textBox1.BackColor = Color.White;
                            /////////////////////////////////////////////////////////////////////////////////////
                            
                            MessageBox.Show("Заявка добавлена!");
                        }
                        else MessageBox.Show("Заявка не добавлена!");
                    }
                    catch (NpgsqlException)
                    {
                        MessageBox.Show("Нет разрешения на добавление задач!");
                    }
                    con.Close();
                }    
            }
            else
            {
                if (namT.Text == "Заголовок" || (namT.Text == ""))
                {
                    namT.ForeColor = Color.Red;
                    namT.BackColor = Color.LightCoral;
                }
                if (textBox2.Text == "Срок исполнения" || (textBox2.Text == ""))
                {
                    textBox2.ForeColor = Color.Red;
                    textBox2.BackColor = Color.LightCoral;
                }
                if (!Срок_исполнения.MaskFull)
                {
                    textBox2.Visible = true;
                    //textBox2.BringToFront();
                }
                if (textBox1.Text == "Стоимость" || (textBox1.Text == ""))
                {
                    textBox1.ForeColor = Color.Red;
                    textBox1.BackColor = Color.LightCoral;
                }
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Кнопка Переместить_таск на следующую стадию
        #region Цвет кнопки Переместить таск
        private void label16_MouseMove(object sender, MouseEventArgs e)
        {
            label16.BackColor = Color.FromArgb(255, 255, 50);
        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            label16.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void label16_MouseDown(object sender, MouseEventArgs e)
        {
            label16.BackColor = Color.FromArgb(255, 255, 0);
        }

        private void label16_MouseUp(object sender, MouseEventArgs e)
        {
            label16.BackColor = Color.FromArgb(255, 255, 192);
        }
        #endregion

        #region Плюсик и панель за плюсиком, добавляющим таск
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 0);
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 50);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            AddFTask_Click(sender, e);
        }
        
        private void AddFTask_MouseDown(object sender, MouseEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 0);
        }

        private void AddFTask_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void AddFTask_MouseMove(object sender, MouseEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 50);
        }
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
        #region Кнопка УправлениеПользователями
        private void users_button_MouseMove(object sender, MouseEventArgs e)
        {
            users_button.BackColor = Color.FromArgb(3, 171, 255); // голубой
        }
        private void users_button_MouseLeave(object sender, EventArgs e)
        {
            users_button.BackColor = Color.FromArgb(0, 100, 200); // синий
        }

        private void users_button_MouseUp(object sender, MouseEventArgs e)
        {
            users_button.BackColor = Color.FromArgb(0, 100, 200); // синий
        }

        private void users_button_MouseDown(object sender, MouseEventArgs e)
        {
            users_button.BackColor = Color.FromArgb(112, 179, 227); // серый
        }
        private void users_button_Click(object sender, EventArgs e)
        {
            panel1.Select();
            control_users_panel.Location = users_button.Location;
            control_users_panel.Visible = true;
        }
        #endregion

        #region Кнопка УправлениеПользователями КОТОРАЯ НАХОДИТСЯ НА ВЫСКАКИВАЮЩЕЙ ПАНЕЛИ С ДОБАВЛЕНИЕМ ТАСКА
        private void label18_MouseDown(object sender, MouseEventArgs e)
        {
            label18.BackColor = Color.FromArgb(112, 179, 227); // серый
        }

        private void label18_MouseUp(object sender, MouseEventArgs e)
        {
            label18.BackColor = Color.FromArgb(0, 100, 200); // синий
        }

        private void label18_MouseMove(object sender, MouseEventArgs e)
        {
            label18.BackColor = Color.FromArgb(3, 171, 255); // голубой
        }

        private void label18_MouseLeave(object sender, EventArgs e)
        {
            label18.BackColor = Color.FromArgb(0, 100, 200); // синий
        }
        private void label18_Click(object sender, EventArgs e)
        {
            add_user.BackColor = Color.FromArgb(3, 171, 255); // голубой
            panel_connect_1.Visible = false;
            New_user_form.Visible = false;
            control_users_panel.Visible = false;
            clcU = false;
        }
        #endregion

        #region Кнопка Добавить пользователя
        private void add_user_MouseDown(object sender, MouseEventArgs e)
        {
            add_user.BackColor = Color.FromArgb(112, 179, 227); // серый
        }

        private void add_user_MouseLeave(object sender, EventArgs e)
        {
            if (clcU != true)
            {
                add_user.BorderStyle = BorderStyle.FixedSingle;
                add_user.BackColor = Color.FromArgb(3, 171, 255); // голубой
            }
            else
            {
                add_user.BackColor = Color.FromArgb(0, 100, 200); // синий
                add_user.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void add_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcU != true)
            {
                add_user.BorderStyle = BorderStyle.Fixed3D;
                add_user.BackColor = Color.FromArgb(3, 140, 255); // темнее, чем голубой
            }
            else
            {
                add_user.BackColor = Color.FromArgb(0, 100, 200); // синий
                add_user.BorderStyle = BorderStyle.FixedSingle;
            }
        }
        private void add_user_Click(object sender, EventArgs e)
        {
            if (clcU != true) // если нажали на ЭТУ кнопку, то показывать
            {
                clcU = true;
                New_user_form.Location = new Point(control_users_panel.Location.X - 9 - New_user_form.Width, control_users_panel.Location.Y + add_user.Location.Y + 1);
                panel_connect_1.Location = New_user_form.Location;
                panel_connect_1.Width = control_users_panel.Location.X - New_user_form.Location.X + add_user.Location.X + 2;
                panel_connect_1.BringToFront(); // на переднем плане
                panel_connect_1.Visible = true;
                New_user_form.Visible = true;
            }
            else // иначе закрыть
            {
                panel1.Select();
                clcU = false;
                panel_connect_1.Visible = false;
                New_user_form.Visible = false;
            } 
        }
        #endregion

        #region Кнопка Удалить пользователя
        private void delete_user_MouseDown(object sender, MouseEventArgs e)
        {
            delete_user.BackColor = Color.FromArgb(0, 100, 200); // синий
        }

        private void delete_user_MouseLeave(object sender, EventArgs e)
        {
            delete_user.BorderStyle = BorderStyle.FixedSingle;
            delete_user.BackColor = Color.FromArgb(3, 171, 255); // голубой
        }

        private void delete_user_MouseMove(object sender, MouseEventArgs e)
        {
            delete_user.BorderStyle = BorderStyle.Fixed3D;
            delete_user.BackColor = Color.FromArgb(3, 140, 255); // темнее, чем голубой
        }

        private void delete_user_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Кнопка Принять Нового пользователя
        private void label23_MouseLeave(object sender, EventArgs e) 
        {
            label23.BackColor = Color.FromArgb(3, 171, 244); //синий как на стартовой
        }
        private void label23_MouseMove(object sender, MouseEventArgs e)
        {
            label23.BackColor = Color.FromArgb(3, 216, 255); // голубой ПРИ НАВЕДЕНИИ
        }
        private void label23_MouseDown(object sender, MouseEventArgs e)
        {
            label23.BackColor = Color.FromArgb(112, 179, 227); // серый
        }
        private void label23_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Серый текст в Добавить пользователя (таблица всех юзеров без паролей)
        private void new_id_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (new_id_for_user.BackColor == Color.LightCoral)
            {
                new_id_for_user.ForeColor = Color.Gray;
                new_id_for_user.BackColor = Color.White;
            }
        }
        private void new_id_for_user_Enter(object sender, EventArgs e)
        {
            new_id_for_user.ForeColor = Color.Black;
            if (new_id_for_user.Text == "Логин")
            {
                new_id_for_user.Text = "";
            }
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
            panel_for_table_users.Location = new Point(New_user_form.Location.X - 9 - panel_for_table_users.Width, New_user_form.Location.Y + new_id_for_user.Location.Y + 1);
            panel_connect_2.Location = new Point(panel_for_table_users.Location.X + panel_for_table_users.Size.Width, New_user_form.Location.Y + new_id_for_user.Location.Y + 2);
            panel_connect_2.Width = New_user_form.Location.X - panel_for_table_users.Location.X - panel_for_table_users.Width + new_id_for_user.Location.X + 1;
            panel_connect_2.BringToFront(); // на переднем плане
            panel_connect_2.Visible = true;
            panel_for_table_users.Visible = true;
        }

        ////////////////////////////////////////////высота под кол-во ячеек//////////////////////////////////////////////
        private void table_users_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) 
        {
            int height = table_users.Location.Y + table_users.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in table_users.Rows)
            {
                height += dr.Height;
            }
            table_users.Height = height;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void new_id_for_user_Leave(object sender, EventArgs e)
        {
            if (new_id_for_user.Text == "")
            {
                new_id_for_user.Text = "Логин";
                new_id_for_user.ForeColor = Color.Gray;
            }
            panel_connect_2.Visible = false;
            panel_for_table_users.Visible = false;
        }
        /////////////////////////////////////////////////////////////////////
        private void new_pass_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (new_pass_for_user.BackColor == Color.LightCoral)
            {
                new_pass_for_user.ForeColor = Color.Gray;
                new_pass_for_user.BackColor = Color.White;
            }
        }
        private void new_pass_for_user_Enter(object sender, EventArgs e)
        {
            new_pass_for_user.ForeColor = Color.Black;
            if (new_pass_for_user.Text == "Пароль")
            {
                new_pass_for_user.Text = "";
            }
        }
        private void new_pass_for_user_Leave(object sender, EventArgs e)
        {
            if (new_pass_for_user.Text == "")
            {
                new_pass_for_user.Text = "Пароль";
                new_pass_for_user.ForeColor = Color.Gray;
            }
        }
        /////////////////////////////////////////////////////////////////////
        private void new_access_for_user_MouseMove(object sender, MouseEventArgs e)
        {
            if (new_access_for_user.BackColor == Color.LightCoral)
            {
                new_access_for_user.ForeColor = Color.Gray;
                new_access_for_user.BackColor = Color.White;
            }
        }
        private void new_access_for_user_Enter(object sender, EventArgs e)
        {
            new_access_for_user.ForeColor = Color.Black;
            if (new_access_for_user.Text == "Уровень доступа")
            {
                new_access_for_user.Text = "";
            }
        }
        private void new_access_for_user_Leave(object sender, EventArgs e)
        {
            if (new_access_for_user.Text == "")
            {
                new_access_for_user.Text = "Уровень доступа";
                new_access_for_user.ForeColor = Color.Gray;
            }
        }
        //////////////////////////////КНОПКА НАЗАД///////////////////////////////////////
        private void panel_connect_1_MouseDown(object sender, MouseEventArgs e)
        {
            panel_connect_1.BackColor = Color.FromArgb(112, 179, 227); // серый
        }
        private void panel_connect_1_MouseLeave(object sender, EventArgs e)
        {
            panel_connect_1.BackColor = Color.FromArgb(0, 100, 200); // синий
        }
        private void panel_connect_1_MouseMove(object sender, MouseEventArgs e)
        {
            panel_connect_1.BackColor = Color.FromArgb(3, 171, 255); // голубой
        }
        private void panel_connect_1_Click(object sender, EventArgs e)
        {
            label22_Click(sender, e);
        }
        ////////////////////////////////КНОПКА НАЗАД (ТЕКСТ)/////////////////////////////
        private void label22_MouseMove(object sender, MouseEventArgs e)
        {
            panel_connect_1_MouseMove(sender, e);
        }
        private void label22_MouseDown(object sender, MouseEventArgs e)
        {
            panel_connect_1_MouseDown(sender, e);
        }
        private void label22_MouseLeave(object sender, EventArgs e)
        {
            panel_connect_1_MouseLeave(sender, e);
        }
        private void label22_Click(object sender, EventArgs e)
        {
            panel1.Select();
            add_user.BackColor = Color.FromArgb(3, 171, 255); // голубой
            panel_connect_1.Visible = false;
            New_user_form.Visible = false;
            clcU = false;
        }

        



        ////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
