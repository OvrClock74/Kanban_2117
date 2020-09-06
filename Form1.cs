using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;

namespace Scrum
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            Show();
            Name1.Select();
        }
        private void Start_Load(object sender, EventArgs e)
        {
            ActiveControl = null;
           
        }

        #region ТекстБокс ЛОГИН и ПАРОЛЬ
        public bool clcT1 = false;
        public bool clcT2 = false;
        //
        //textBox1
        //
        private void textBox1_Enter(object sender, EventArgs e)
        {
            clcT1 = true;
            border_background_panel2.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcT1 ==false)
            border_background_panel2.BackColor = Color.Black;  
        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (clcT1 == false)
                border_background_panel2.BackColor = Color.FromArgb(40,41,44);
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            clcT1 = false;
            border_background_panel2.BackColor = Color.FromArgb(40, 41, 44);
            textBox1.Text = textBox1.Text.TrimStart(); // удаляем пробелы
            textBox1.Text = textBox1.Text.TrimEnd();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }
        //
        //textBox2
        //
        private void textBox2_Enter(object sender, EventArgs e)
        {
            clcT2 = true;
            border_background_panel.BackColor = Color.FromArgb(120, 136, 214);
        }
        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (clcT2 == false)
                border_background_panel.BackColor = Color.Black;
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            if (clcT2 == false)
                border_background_panel.BackColor = Color.FromArgb(40, 41, 44);
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            clcT2 = false;
            border_background_panel.BackColor = Color.FromArgb(40, 41, 44);
            textBox2.Text = textBox2.Text.TrimStart(); // удаляем пробелы
            textBox2.Text = textBox2.Text.TrimEnd();
        }
        #endregion

        #region Цвет кнопки ВХОД
        private void Вход_MouseMove(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(109,122,193);
        }

        private void Вход_MouseLeave(object sender, EventArgs e)
        {
            Вход.BackColor = Color.FromArgb(120,136,214);
        }

        private void Вход_MouseDown(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(97,110,171);
        }
        #endregion

        #region Авторизация на кнопку
        private void Вход_Click(object sender, EventArgs e) // Авторизация
        {
            var cs = "Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk";
            using NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("login", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                Totalf.Parameters.AddWithValue("logn", textBox1.Text.Trim());
                Totalf.Parameters.AddWithValue("pas", textBox2.Text.Trim());
                int id = (int)Totalf.ExecuteScalar();
                Главная obj = new Главная(id); // передача id в форму Главная
                Hide();
                obj.Show();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("Неверный логин или пароль!\nПовторите попытку входа.");
            }
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var cs = "Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk";
            using NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();
            NpgsqlCommand Totalf = new NpgsqlCommand("login", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                Totalf.Parameters.AddWithValue("logn", textBox1.Text.Trim());
                Totalf.Parameters.AddWithValue("pas", textBox2.Text.Trim());
                Totalf.ExecuteNonQuery();
                int id = (int)Totalf.ExecuteScalar();
                Главная obj = new Главная(id); // передача id в форму Главная
                Hide();
                obj.Show();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("Неверный логин или пароль!\nПовторите попытку входа.");
            }
            con.Close();
        }
        #endregion

        private void Start_MouseDown(object sender, MouseEventArgs e) // перетаскивание формы без рамки
        {
            panel1.Select();
            base.Capture = false;
            Message m = Message.Create(base.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        #region Фокус на другой элемент
        private void Name1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }

        private void Start_Deactivate(object sender, EventArgs e)
        {
            panel1.Select();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }
        #endregion

        #region Крест, свернуть, развернуть
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
        #endregion

        
    }
}
