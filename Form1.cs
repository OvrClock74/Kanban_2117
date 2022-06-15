using System;
using System.Data;
using System.Drawing;
using Npgsql;
using System.Windows.Forms;

namespace Scrum
{

    public partial class Start : Form
    {
        public bool max_size_from = false; // развернута форма или нет

        public int W = 0; // сохранить ширину окна для свернуть-развернуть
        public int H = 0; // сохранить высоту окна для свернуть-развернуть
        public bool tecW = false; // проводятся ли технические работы
        //public static string cs = "Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk"; 
        public static string cs = "Host=X; Username=X; Password=X";
        public Start()
        {
            InitializeComponent();
            Show();
            panel1.Select();
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
            maskedTextBox1.ForeColor = Color.FromArgb(219, 220, 221);
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                maskedTextBox1.Select(4, 0);
            });
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
            if (!maskedTextBox1.MaskFull)
                maskedTextBox1.ForeColor = Color.FromArgb(185, 186, 189);
            maskedTextBox1.Text = maskedTextBox1.Text.Trim(); // удаляем пробелы
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
            if (tecW != true)
            Вход.BackColor = Color.FromArgb(109,122,193);
        }

        private void Вход_MouseLeave(object sender, EventArgs e)
        {
            if (tecW != true)
                Вход.BackColor = Color.FromArgb(120,136,214);
        }

        private void Вход_MouseDown(object sender, MouseEventArgs e)
        {
            if (tecW != true)
                Вход.BackColor = Color.FromArgb(97,110,171);
        }
        #endregion

        #region Авторизация на кнопку
        private void Вход_Click(object sender, EventArgs e) // Авторизация
        {
            using NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand tec_works = new NpgsqlCommand("select working from tec_work", con);
            bool works = (bool)tec_works.ExecuteScalar();
            if (works == false)
            {
                NpgsqlCommand Totalf = new NpgsqlCommand("login", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    Totalf.Parameters.AddWithValue("logn", maskedTextBox1.Text);
                    Totalf.Parameters.AddWithValue("pas", textBox2.Text.Trim());
                    int id = (int)Totalf.ExecuteScalar();
                    con.Close();
                    string user_name = maskedTextBox1.Text;
                    Главная obj = new Главная(id, user_name); // передача id в форму Главная
                    Hide();
                    obj.Show();
                }
                catch (NpgsqlException)
                {
                    con.Close();
                    textBox2.Text = "";
                    MessageBox.Show("Неверный логин или пароль!\nПовторите попытку входа.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Application.OpenForms[this.Name].Activate();
                }
            }
            else
            {
                con.Close();
                tecW = true;
                label1.Text = "!ВНИМАНИЕ!";
                label2.Text = "НА СЕРВЕРЕ ПРОВОДЯТСЯ\nТЕХНИЧЕСКИЕ РАБОТЫ";
                maskedTextBox1.Enabled = false;
                textBox2.Enabled = false;
                label1.ForeColor = Color.Red;
                label2.ForeColor = Color.Red;
                Вход.BackColor = Color.FromArgb(67, 80, 141);
                Вход.ForeColor = Color.FromArgb(142, 145, 150);
                Вход.Enabled = false;
            }
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
            if (max_size_from == false)
            {
                W = this.Size.Width;
                H = this.Size.Height;
                var rectangle = Screen.FromControl(this).Bounds;
                this.FormBorderStyle = FormBorderStyle.None;
                Size = new Size(rectangle.Width, rectangle.Height);
                Location = new Point(0, 0);
                Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
                this.Size = new Size(workingRectangle.Width, workingRectangle.Height);
                max_size_from = true;
            }
            else
            {
                this.Size = new Size(W, H);
                max_size_from = false;
            }
        }

        private void butn_minus2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            (Application.OpenForms["Start"] as Start).Вход_Click(sender, e);
        }
    }
}
