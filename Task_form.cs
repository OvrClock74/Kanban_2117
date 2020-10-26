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
        public int access_user; 

        public ToolTip t1 = new ToolTip();

        ///////Переменные для добавления файла в БД//////
        public string PathToFile;
        public string filename;
        public string TypeFile;
        public string name_fillee;

        public static string cs = "Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk";
        public Task_form(string C1, string name_stage, int stage_t1, int ID_Main1, int acces)
        {
            InitializeComponent();
            C = C1; // название задачи
            stage_t = stage_t1; // стадия на которой она находится
            ID_Main = ID_Main1; // ID пользователя
            access_user = acces; // доступ залогиневшегося юзера
            if (stage_t == 1)
                button1.Visible = true; // если первая стадия, то есть кнопка PDF
            if (stage_t == 4)
            {
                this.Size = new Size(Size.Width, Size.Height + 64);
                panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + 64);
                label7.Location = new Point(label7.Location.X, label7.Location.X + 64);
                perechenText.Location = new Point(perechenText.Location.X, label7.Location.X + 64);
                label23.Location = new Point(label7.Location.X, label7.Location.X - 64);
                label24.Location = new Point(label7.Location.X, label7.Location.X - 64);
                label23.Visible = true;
                label24.Visible = true;
            }
            try
            {
            #region Вывод
            NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("SELECT (SELECT fio FROM users WHERE id_u = (SELECT autor FROM tasks where name_t = @taskname)), " +
                "date_create, date_complete, payment, cost_t, proshu_obesp, predm_zak, purpose_zak, tel_number, list_zak," +
                " link_zak, link_kon, registry_num, sum_t, date_duration, ispolnitel " +
                "FROM tasks WHERE name_t = @taskname", con);
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
                            label31.Text = String.Format("{0}", reader["link_zak"]);
                        else
                            label31.Text = String.Format("{0}", reader["link_kon"]);

                        label33.Text = String.Format("{0}", reader["registry_num"]);
                            label24.Text = String.Format("{0}", reader["ispolnitel"]);


                            // даты
                            DateTime date = Convert.ToDateTime(reader["date_create"]);
                    label18.Text = String.Format("{0}", date.ToShortDateString());
                    date = Convert.ToDateTime(reader["date_complete"]);
                    label11.Text = String.Format("{0}", date.ToShortDateString());
                    date = Convert.ToDateTime(reader["date_duration"]);
                    label30.Text = String.Format("{0}", date.ToShortDateString());

                        // цена
                        int i = Convert.ToInt32(reader["cost_t"]);
                    label16.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));
                        i = Convert.ToInt32(reader["sum_t"]);
                        label32.Text = i.ToString("C0", new System.Globalization.CultureInfo("ru-RU"));

                        // оплачено
                        if ((bool)reader["payment"])
                    {
                        if (access_user == 3 || access_user == 0)
                        {
                            checkBox1.Location = new Point(checkBox1.Location.X + 26, checkBox1.Location.Y);
                            label14.Visible = false;
                            checkBox1.ForeColor = Color.FromArgb(67, 181, 129);
                            checkBox1.Text = "Оплачено";
                            checkBox1.Checked = true;
                            checkBox1.Visible = true;
                            t1.SetToolTip(checkBox1, "Отменить оплату");
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
                            checkBox1.Visible = true;
                            label14.Visible = false;
                            checkBox1.ForeColor = Color.FromArgb(209, 73, 73);
                            checkBox1.Text = "Не оплачено";
                            t1.SetToolTip(checkBox1, "Оплатить");
                        }
                        else
                        {
                            label14.ForeColor = Color.FromArgb(209, 73, 73);
                            label14.Text = "Не оплачено";
                        }
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
            }
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
            NpgsqlConnection con = new NpgsqlConnection(cs);
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
                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                    MessageBox.Show(returnedValue);
                    Close();
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                        Close();
                    }
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\nОбратитесь к администратору.");
                }
            }
            else if (stage_t == 2 || stage_t == 3)
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
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show(returnedValue);
                        Close();
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show("Информация устарела!\nТаблицы обновлены в соответствии с текущим статусом заявок.");
                        Close();
                    }
                    else if (Convert.ToString(ex.Message) == "P0001: Вы не можете переместить задачу на текущей стадии.")
                        MessageBox.Show("Вы не можете переместить задачу на текущей стадии: Недостаточно прав!\nОбратитесь к администратору.");
                }
            }
            else if ((stage_t == 4) || (stage_t == 5) || (stage_t == 6))
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
                    (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                    MessageBox.Show(returnedValue);
                    Close();
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
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

                    if (checkBox1.Checked == false)
                    {
                        DialogResult result = MessageBox.Show(
                           "Оплата не проведена! Вы не можете отправить задачу в архив, пока она имеет статус «Не оплачено‎».",
                           "Ошибка!",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                    {
                        string returnedValue = da3.ExecuteScalar().ToString();
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
                        MessageBox.Show(returnedValue);
                        Close();
                    }
                }
                catch (NpgsqlException ex)
                {
                    if (Convert.ToString(ex.Message) == "P0001: Информация устарела! Пожалуйста, обновите таблицы.")
                    {
                        (Application.OpenForms["Главная"] as Главная).reload_tables_Click(sender, e); // вызов метода из другой формы
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
        }

        private void button1_Click(object sender, EventArgs e)
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
                    //string tmpPath = "..\\Zakupki 2117\\template.docx"; // для выпуска
                    string tmpPath = @"C:\Users\Павел\Desktop\template.docx"; // во время отладки
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
    }
}
