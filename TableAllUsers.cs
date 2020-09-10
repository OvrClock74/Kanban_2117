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
        public int ID_for_show_users;
        public TableAllUsers(int id)
        {
            InitializeComponent();
            ID_for_show_users = id;
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
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
    }
}
