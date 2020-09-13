using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace Scrum
{
    public partial class Task_form : Form
    {
        public string C;
        public int stage_t;
        public int ID_Main;

        ///////Переменные для добавления файла в БД//////
        public string PathToFile;
        public string filename;
        public string TypeFile;
        public string name_fillee;

        public Task_form(string C1, string name_stage, int stage_t1, int ID_Main1)
        {
            InitializeComponent();
            C = C1; // название задачи
            stage_t = stage_t1; // стадия на которой она находится
            ID_Main = ID_Main1; // ID пользователя

            #region Вывод
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT autor, date_create, date_complete, payment, cost_t FROM tasks WHERE name_t = @name_T", con);
            Totalf.Parameters.AddWithValue("@name_T", C);
            NpgsqlDataReader reader;
            using (reader = Totalf.ExecuteReader())
            {
                if (reader.Read())
                {
                    label15.Text = C; // название

                    if (reader["autor"] is DBNull)
                    {
                        label17.Text = "!пользователь удалён!";
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
                                label17.Text = String.Format("{0}", reader2["login"]); ;
                            }
                            reader2.Close();
                        }
                        con2.Close();
                    }

                    DateTime date = Convert.ToDateTime(reader["date_create"]);
                    label18.Text = String.Format("{0}", date.ToShortDateString());
                    date = Convert.ToDateTime(reader["date_complete"]);
                    label11.Text = String.Format("{0}", date.ToShortDateString());

                    int i = Convert.ToInt32(reader["cost_t"]);
                    label16.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));
                    if ((bool)reader["payment"])
                    {
                        label14.ForeColor = Color.FromArgb(67, 181, 129);
                        label14.Text = "Оплачено";
                    }
                    else
                    {
                        label14.ForeColor = Color.FromArgb(209, 73, 73);
                        label14.Text = "Не оплачено";
                    }

                    label13.Text = name_stage;
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
            dataGridView1.DataSource = dt;
            reader.Close();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.Close();
            #endregion

            ToolTip t = new ToolTip();
            t.SetToolTip(AddFTask, "Прикрепить файл");
        }

        #region Graphics
        private void task_form_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.FromArgb(63, 64, 68), 1);
                var p2 = new Pen(Color.FromArgb(115, 117, 125), 1);
                g.DrawLine(p2, new Point(9, 48), new Point(371, 48));
                g.DrawLine(p, new Point(18, 145), new Point(362, 145));
                g.DrawLine(p, new Point(18, 214), new Point(362, 214));
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

        #region AddFTask видимость
        private void AddFTask_MouseLeave(object sender, EventArgs e)
        {
            AddFTask.Visible = false;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            AddFTask.Visible = true;
        }
        #endregion

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
            dataGridView1.DataSource = dt;
            reader.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            con.Close();
            MessageBox.Show("Файл успешно загружен");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // ЗАГРУЗИТЬ ФАЙЛ
        {
            Int32 file_id;
            string file_type;
            string C2 = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT id_f, type_file FROM files WHERE name_file = @name_F", con); // ID заявки которую выбрали 
            Totalf.Parameters.AddWithValue("@name_F", C2);
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
                    saveFileDialog1.FileName = C2;
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
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
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
        private void dataGridView1_Paint(object sender, PaintEventArgs e)
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

            panel2.Height = dataGridView1.Location.Y + dataGridView1.Height + label13.Height + label13.Height / 2;
            this.Height = panel2.Height + panel2.Location.Y;
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
                    Главная Glav = new Главная(0, "ds");
                    Glav.reload_tables_Click(sender, e); // обновление таблиц
                    MessageBox.Show(returnedValue);
                    Close();
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        Главная Glav = new Главная(0, "ds");
                        Glav.reload_tables_Click(sender, e);
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                        Close();
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
                    Главная Glav = new Главная(0, "ds");
                    Glav.reload_tables_Click(sender, e);
                    MessageBox.Show(returnedValue);
                    Close();
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        Главная Glav = new Главная(0, "ds");
                        Glav.reload_tables_Click(sender, e);
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                        Close();
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
                    Главная Glav = new Главная(0, "ds");
                    Glav.reload_tables_Click(sender, e);
                    MessageBox.Show(returnedValue);
                    Close();
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        Главная Glav = new Главная(0, "ds");
                        Glav.reload_tables_Click(sender, e);
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                        Close();
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
    }
}
