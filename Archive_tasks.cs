using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace Scrum
{
    public partial class Archive_tasks : Form
    {
        public string cell;

        public Archive_tasks()
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("Host=X; Username=X; Password=X");
            con.Open();
            NpgsqlDataReader reader;
            NpgsqlCommand daT = new NpgsqlCommand("Select date_archiving, name_t, registry_num, link_kon, (select login from users where id_u = autor), fio, sum_t, date_create, date_complete, date_duration from tasks_archive join users on id_u = tasks_archive.ispolnitel ORDER BY date_archiving DESC", con); // DESC - сортировка по убыванию
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
                else if (str == "registry_num")
                    dataGridView1.Columns[i].HeaderText = "Реестровый номер";
                else if (str == "link_kon")
                    dataGridView1.Columns[i].HeaderText = "Ссылка контракта";
                else if (str == "login")
                    dataGridView1.Columns[i].HeaderText = "Добавил";
                else if (str == "fio")
                    dataGridView1.Columns[i].HeaderText = "Исполнитель";
                else if (str == "sum_t")
                    dataGridView1.Columns[i].HeaderText = "Сумма";
                else if (str == "date_create")
                    dataGridView1.Columns[i].HeaderText = "Cоздан";
                else if (str == "date_complete")
                    dataGridView1.Columns[i].HeaderText = "Срок иполнения";
                else if (str == "date_duration")
                    dataGridView1.Columns[i].HeaderText = "Действует до";
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 13);
            this.dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 12);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(109, 122, 193);//выбранная ячейка фон
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);//выбранная ячейка текст
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 136, 214);//выбранная ячейка фон
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 255, 255);//выбранная ячейка текст
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                int row = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (row >= 0 )
                    menu.Items.Add("Скопировать").Name = "Скопировать";
                menu.Show(dataGridView1, new Point(e.X, e.Y));
                menu.ItemClicked += new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            }
        }

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Clipboard.SetText(cell);
        }
    }
}
