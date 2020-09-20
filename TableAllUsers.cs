using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace Scrum
{
    public partial class TableAllUsers : Form
    {
        public bool closing = false;
        public int ID_for_show_users;
        public TableAllUsers(int id)
        {
            InitializeComponent();
            ID_for_show_users = id;
            NpgsqlConnection con = new NpgsqlConnection("Host=dumbo.db.elephantsql.com;Username=qynafvcm;Password=RyfeKiIzGjJWfRNT9578fc7B9NUUYH1y;Database=qynafvcm");
            con.Open();
            NpgsqlDataReader reader;
            NpgsqlCommand daT = new NpgsqlCommand("Select * from all_users_show(@auser)", con); // all_users_show(auser integer)
            daT.Parameters.Add("@auser", NpgsqlDbType.Integer).Value = ID_for_show_users;
            DataTable dt = new DataTable();
            reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            reader.Close();
            con.Close();

            for (int i = 0; i < dataGridView1.Columns.Count; i++) // переименование столбцов
            {
                string str = dataGridView1.Columns[i].HeaderText;
                if (str == "logn")
                    dataGridView1.Columns[i].HeaderText = "Логин";
                else if (str == "accs")
                    dataGridView1.Columns[i].HeaderText = "Уровень доступа";
                else if (str == "pas")
                    dataGridView1.Columns[i].HeaderText = "Пароль";
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 15);
            this.dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 12);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(109, 122, 193);//выбранная ячейка фон
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);//выбранная ячейка текст
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);//выбранная ячейка фон
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);//выбранная ячейка текст
        }

        private void TableAllUsers_Leave(object sender, EventArgs e) // если убрали фокус с формы
        {
            DialogResult result = MessageBox.Show(
                            "Рекомендуется закрыть окно с таблицей пользователей, так как эта информация может быть использована злоумышленниками! " +
                            "Даже если Вы уверены, что этим компьютером не будет никто пользоваться в ближайшее время, закройте и при необходимости заново откройте." +
                            "\n\nЗакрыть?\n\n",
                            "Внимание!",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2,
                            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void TableAllUsers_Deactivate(object sender, EventArgs e)
        {
            if (closing == false)
            {
                DialogResult result = MessageBox.Show(
                            "Рекомендуется закрыть окно с таблицей пользователей, так как эта информация может быть использована злоумышленниками! " +
                            "\nДаже если Вы уверены, что этим компьютером не будет никто пользоваться в ближайшее время, закройте и при необходимости заново откройте." +
                            "\n\nЗакрыть?\n\n",
                            "Внимание!",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else closing = false;
        }

        private void TableAllUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }
    }
}
