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
    public partial class Archive_tasks : Form
    {
        public Archive_tasks()
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            NpgsqlDataReader reader;
            NpgsqlCommand daT = new NpgsqlCommand("Select date_archiving, name_t, (select login from users where id_u = autor), cost_t, date_create, date_complete from tasks_archive", con); // all_users_show(auser integer)
            DataTable dt = new DataTable();
            reader = daT.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            reader.Close();
            con.Close();

            for (int i = 0; i < dataGridView1.Columns.Count; i++) // переименование столбцов
            {
                string str = dataGridView1.Columns[i].HeaderText;
                if (str == "date_archiving")
                    dataGridView1.Columns[i].HeaderText = "Завершено";
                else if (str == "name_t")
                    dataGridView1.Columns[i].HeaderText = "Название заявки";
                else if (str == "login")
                    dataGridView1.Columns[i].HeaderText = "Добавил";
                else if (str == "cost_t")
                    dataGridView1.Columns[i].HeaderText = "Стоимость";
                else if (str == "date_create")
                    dataGridView1.Columns[i].HeaderText = "Дата создания";
                else if (str == "date_complete")
                    dataGridView1.Columns[i].HeaderText = "Срок исполнения";
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
