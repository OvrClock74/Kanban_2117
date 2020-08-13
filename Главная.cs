using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Npgsql;
using System.Windows.Forms;

namespace Scrum
{
    public partial class Главная : Form
    {
        public static int ID_Main;
        public int ID
        { set { ID_Main = value; } }

        public bool clc = false;

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public Главная()
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            string sql = ("SELECT name_t FROM tasks");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();
        }
        
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*  //////////////////////////////////////////ИЗМЕНЕНИЕ ЦВЕТА ПРИ НАВЕДЕНИИ///////////////////////////////////////////
            dataGridView1.CellMouseEnter += (a, b) =>
            {
                if (b.RowIndex != -1)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[b.RowIndex].Cells[0];
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                 
                }
            };*/


            /* int a = dataGridView1.CellMouseEnter.RowIndex;
             CellMouseEnter
             /*
                         string msg = String.Format("Row: {0}, Column: {1}",
                         dataGridView1.CurrentCell.RowIndex,
                         dataGridView1.CurrentCell.ColumnIndex);
                         MessageBox.Show(msg, "Current Cell");*/
        }

        private void Главная_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clc = true;
            string C = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            //, date_create, date_complete, cost_t, payment //
            string sql = ("SELECT autor FROM tasks WHERE name_t");
            NpgsqlCommand Totalf = new NpgsqlCommand(sql, con)
            { 
                CommandType = CommandType.StoredProcedure 
            };
            Totalf.Parameters.AddWithValue("name_t", C);
            Totalf.ExecuteReader();
             int id = (int)Totalf.ExecuteScalar();
            con.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (clc == true)
            {
                groupBox1.Visible = true;
                groupBox1.Top = (e.Y + 85);
                groupBox1.Left = (e.X + 55);
            }
        }
    }
}
