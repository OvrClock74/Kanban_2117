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

namespace Scrum
{
    public partial class Главная : Form
    {
        //////////////////PUBLIC ПЕРЕМЕННЫЕ////////////////
        public int ID
        { set { ID_Main = value; } }
        public static int ID_Main;

        public bool clc = false; // зачем - хз но находится в "Таск из таблиц"
        public string C; // Значение, что находится в выбираемой ячейке

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
        #endregion
        ///////////////////////////////////////////////////

        public Главная()
        {
            InitializeComponent();       

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=postgres");
            con.Open();

            #region Заполнение таблиц данными 
            //////////////////////////////////////////////////ТАБЛИЦА 1/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 1", con);
            ds1.Reset();
            da1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView1.DataSource = dt1;
            //////////////////////////////////////////////////ТАБЛИЦА 2/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 2", con);
            ds2.Reset();
            da2.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;
            //////////////////////////////////////////////////ТАБЛИЦА 3/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 3", con);
            ds3.Reset();
            da3.Fill(ds3);
            dt3 = ds3.Tables[0];
            dataGridView3.DataSource = dt3;
            //////////////////////////////////////////////////ТАБЛИЦА 4/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da4 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 4", con);
            ds4.Reset();
            da4.Fill(ds4);
            dt4 = ds4.Tables[0];
            dataGridView4.DataSource = dt4;
            //////////////////////////////////////////////////ТАБЛИЦА 5/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 5", con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            dataGridView5.DataSource = dt5;
            //////////////////////////////////////////////////ТАБЛИЦА 6/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 6", con);
            ds6.Reset();
            da6.Fill(ds6);
            dt6 = ds6.Tables[0];
            dataGridView6.DataSource = dt6;
            //////////////////////////////////////////////////ТАБЛИЦА 7/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 7", con);
            ds7.Reset();
            da7.Fill(ds7);
            dt7 = ds7.Tables[0];
            dataGridView7.DataSource = dt7;
            //////////////////////////////////////////////////ТАБЛИЦА 8/////////////////////////////////////////////////////////////////////
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("SELECT name_t FROM tasks WHERE stage = 8", con);
            ds8.Reset();
            da8.Fill(ds8);
            dt8 = ds8.Tables[0];
            dataGridView8.DataSource = dt8;
            #endregion

            con.Close();
        }

        private void Главная_FormClosed(object sender, FormClosedEventArgs e) // крестик - зыкрыть приложение
        {
            Application.Exit();
        }
        private void task_form_VisibleChanged(object sender, EventArgs e) // Таск на переднем плане
        {
            task_form.BringToFront();
        }

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

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=postgres");
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

        public static void databaseFileRead(string varID, string varPathToNewLocation) // выгрузка любых файлов из БД
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=postgres");
            con.Open();
            using (var sqlQuery = new NpgsqlCommand(@"SELECT dannie FROM test WHERE id = 3", con))
            {
                using (NpgsqlDataReader dr = sqlQuery.ExecuteReader(System.Data.CommandBehavior.Default))
                {
                    if (dr.Read())
                    {
                        // read in using GetValue and cast to byte array
                        byte[] fileData = (byte[])dr.GetValue(0);

                        // write bytes to disk as file
                        using (FileStream fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.ReadWrite))
                        {
                            // use a binary writer to write the bytes to disk
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                bw.Write(fileData);
                                bw.Close();
                            }
                        }
                    }
                    // close reader to database
                    dr.Close();
                }
            }
            con.Close();
        }
        #endregion

        #region Таск из таблицы 1
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            task_form.Visible = true;
            C = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value; // Значение, что находится в выбираемой ячейке

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=postgres");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            using (NpgsqlDataReader reader = Totalf.ExecuteReader())
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

                }
                reader.Close();
            }
            con.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                task_form.Visible = true;
                task_form.Top = (e.Y + dataGridView1.Location.Y);
                task_form.Left = (e.X + +dataGridView1.Location.X);
                clc = false;
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView1.CellMouseEnter += (a, b) =>
            {
                if (b.RowIndex != -1)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[b.RowIndex].Cells[0];
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 100, 200);
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
                }
            };
        }
        #endregion

        #region Таск из таблицы 2
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            string C = (string)dataGridView2.Rows[e.RowIndex].Cells[0].Value; // то что находится в выбираемой ячейке

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();

            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            using (NpgsqlDataReader reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label2.Text = C;
                    label3.Text = (String.Format("{0}", reader["autor"]));
                    label4.Text = (String.Format("{0}", reader["date_create"]));
                    label5.Text = (String.Format("{0}", reader["cost_t"]));
                    if ((bool)reader["payment"])
                    {
                        label6.Text = "Оплачено";
                    }
                    else label6.Text = "Не оплачено";

                }
            }
            con.Close();
        }
        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e) //ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ
        {
            dataGridView1.CellMouseEnter += (a, b) =>
            {
                if (b.RowIndex != -1)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[b.RowIndex].Cells[0];
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(3, 143, 244);
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
                }
            };
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
        #region ДОБАВИТЬ ТАСК

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
        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.Visible = false;
            Срок_исполнения.Select();
        }
        ///////////////////////////////////////////////Срок_исполнения///////////////////////////////////////////////////
        private void Срок_исполнения_Leave(object sender, EventArgs e)
        {
            if (Срок_исполнения.Text == "  .  .")
            {
                textBox2.Visible = true;
                Срок_исполнения.Text = "";
            }
        }
        private void Срок_исполнения_MouseMove(object sender, MouseEventArgs e)
        {
            if (Срок_исполнения.BackColor == Color.LightCoral)
            {
                Срок_исполнения.ForeColor = Color.Gray;
                Срок_исполнения.BackColor = Color.White;
            }
        }
        ///////////////////////////////////////////textBox2.Срок исполнения///////////////////////////////////////////////
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Срок исполнения";
                textBox2.ForeColor = Color.Gray;
            }
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
                textBox1.Text = "";
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
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
            AddF.BackColor = Color.White;
            AddF.ForeColor = Color.Black;
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
            // databaseFilePut(int id_T, string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД
        }

        private void OtmenaB_Click(object sender, EventArgs e) // цвет кнопки Отмена
        {
            CreateTaskB.BackColor = Color.FromArgb(0, 100, 200); // синий
            textBox2.Visible = true;
            Срок_исполнения.Visible = false;
            panelCT.Visible = false;

            namT.Text = "Заголовок";
            namT.ForeColor = Color.Gray;
            namT.BackColor = Color.White;

            textBox2.Text = "Срок исполнения";
            textBox2.ForeColor = Color.Gray;
            textBox2.BackColor = Color.White;

            textBox1.Text = "Стоимость";
            textBox1.ForeColor = Color.Gray; 
            textBox1.BackColor = Color.White;
        }

        private void EnterB_Click(object sender, EventArgs e) // добавить задачу
        {
            panel1.Select();
            if ((namT.Text != "Заголовок") && (Срок_исполнения.MaskFull) && (textBox1.Text != "Стоимость") && (namT.Text != "")  && (textBox1.Text != ""))
            {
                
                NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=postgres");
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
                    //System.DateTime price = Convert.ToDateTime       (textBox1.Text); 

                    da3.Parameters.Add("namet", NpgsqlDbType.Varchar, 250).Value = namT.Text;
                    da3.Parameters.Add("auser", NpgsqlDbType.Integer).Value = ID_Main;
                    da3.Parameters.Add("datcmplt", NpgsqlDbType.Varchar, 250).Value = Срок_исполнения.Text;
                    da3.Parameters.Add("costt", NpgsqlDbType.Integer).Value = Convert.ToInt32(textBox1.Text);
                    Int32 new_task_id = Convert.ToInt32(da3.ExecuteScalar());
                    if (new_task_id != -1) // на всякий случай проверяем добавлена ли задача 
                    {
                        databaseFilePut(new_task_id, name_fillee, TypeFile, PathToFile); // databaseFilePut(int id_T , string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД
                    }
                    else MessageBox.Show("Задача не добавлена!");
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
                if (!Срок_исполнения.MaskFull)
                {
                    Срок_исполнения.ForeColor = Color.Red;
                    Срок_исполнения.BackColor = Color.LightCoral;
                    textBox2.Visible = true;
                    textBox2.BringToFront();


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

        private void AddFTask_Click(object sender, EventArgs e) // СОХРАНИТЬ ФАЙЛ
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            PathToFile = openFileDialog1.FileName; // получаем путь к выбранному файлу
            TypeFile = Path.GetExtension(PathToFile); // тип выбранного файла
            name_fillee = Path.GetFileNameWithoutExtension(openFileDialog1.FileName); // только имя выбранного файла

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=postgres");
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT id_t FROM tasks WHERE name_t = @name_T", con); // ID заявки которую выбрали 
            Totalf.Parameters.AddWithValue("@name_T", C);
            Int32 new_task_id = Convert.ToInt32(Totalf.ExecuteScalar());
            databaseFilePut(new_task_id, name_fillee, TypeFile, PathToFile); // databaseFilePut(int id_T , string name_fille, string type_fille, string varFilePath) // загрузка любых файлов в БД 
            con.Close();
            MessageBox.Show("Файл успешно загружен");
        }

        private void button1_Click(object sender, EventArgs e) // ЗАГРУЗИТЬ ФАЙЛ
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "c:\\",
                Title = "Сохранить как...",
                Filter = "PDF files (*.pdf)|*.pdf|" + "Word files (*.doc)|*.doc|" + "Exel files (*.xlsx)|*.xlsx|" + "TXT files (*.txt)|*.txt|" + "All files (*.*)|*.*|",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    string path = Path.GetFullPath(saveFileDialog1.FileName);
                    string path2 = Path.GetDirectoryName(saveFileDialog1.FileName);
                    myStream.Close();
                    databaseFileRead("231", path);
                }
            }
        }

     
    }
}
