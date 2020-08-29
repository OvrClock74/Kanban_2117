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
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Логин")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Логин";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Пароль")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.PasswordChar = '•';
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.PasswordChar = (char)0;
                textBox2.Text = "Пароль";
                textBox2.ForeColor = Color.Gray;
            }
            if (textBox2.Text != "Пароль" && textBox2.Text != "")
            {
                textBox2.ForeColor = Color.Black;
                textBox2.PasswordChar = '•';
            }
        }
        #endregion

        #region Цвет кнопки ВХОД
        private void Вход_MouseMove(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(3, 171, 255);
            pictureBox1.BackColor = Color.FromArgb(3,171,255); // голубой
        }

        private void Вход_MouseLeave(object sender, EventArgs e)
        {
            Вход.BackColor = Color.FromArgb(3, 143, 244);
            pictureBox1.BackColor = Color.FromArgb(3, 143, 244);  // синий
        }

        private void Вход_MouseDown(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(112, 179, 227);
            pictureBox1.BackColor = Color.FromArgb(112, 179, 227); // серый
        }

        private void Вход_MouseUp(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(3, 143, 244);
            pictureBox1.BackColor = Color.FromArgb(3, 143, 244); // синий
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(112, 179, 227);
            pictureBox1.BackColor = Color.FromArgb(112, 179, 227);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Вход.BackColor = Color.FromArgb(3, 143, 244);
            pictureBox1.BackColor = Color.FromArgb(3, 143, 244);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(3, 171, 255);
            pictureBox1.BackColor = Color.FromArgb(3, 171, 255);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Вход.BackColor = Color.FromArgb(3, 143, 244);
            pictureBox1.BackColor = Color.FromArgb(3, 143, 244);
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

        #region Цвет кнопки ВХОД
        private void Start_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }

        private void Name1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Select();
        }

        private void Start_Deactivate(object sender, EventArgs e)
        {
            panel1.Select();
        }
        #endregion
    }
}
