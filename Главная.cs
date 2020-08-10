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

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public Главная()
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=ybccfy;Database=scrumdesk");
            con.Open();
            string sql = ("SELECT * FROM tasks");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();
        }
        
private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

            dataGridView1.CellMouseEnter += (a, b) =>
            {
                if (b.RowIndex != -1)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[b.RowIndex].Cells[0];
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    dataGridView1.Rows[b.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                 
                }
            };


            /* int a = dataGridView1.CellMouseEnter.RowIndex;
             CellMouseEnter
             /*
                         string msg = String.Format("Row: {0}, Column: {1}",
                         dataGridView1.CurrentCell.RowIndex,
                         dataGridView1.CurrentCell.ColumnIndex);
                         MessageBox.Show(msg, "Current Cell");*/
        }
    }
}
