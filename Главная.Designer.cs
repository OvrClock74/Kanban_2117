using System.Windows.Forms;

namespace Scrum
{
    public class DoubleBufferedPanel : System.Windows.Forms.Panel
    {
        public DoubleBufferedPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
        }
    }
    partial class Главная
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Главная));
            this.panel1 = new System.Windows.Forms.Panel();
            this.archive_button = new System.Windows.Forms.Label();
            this.users_button = new System.Windows.Forms.Label();
            this.CreateTaskB = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.butn_minus2 = new System.Windows.Forms.PictureBox();
            this.butn_minus = new System.Windows.Forms.PictureBox();
            this.butn_plus2 = new System.Windows.Forms.PictureBox();
            this.butn_plus = new System.Windows.Forms.PictureBox();
            this.butn_close = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.dataGridView8 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.control_users_panel = new System.Windows.Forms.Panel();
            this.show_user = new System.Windows.Forms.Label();
            this.delete_user = new System.Windows.Forms.Label();
            this.add_user = new System.Windows.Forms.Label();
            this.New_user_form = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.border_background_new_access_for_user = new System.Windows.Forms.Panel();
            this.background_access_for_user = new System.Windows.Forms.Panel();
            this.new_access_for_user = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.border_background_new_pass_for_user = new System.Windows.Forms.Panel();
            this.background_pass_for_user = new System.Windows.Forms.Panel();
            this.new_pass_for_user = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.border_background_new_id_for_user = new System.Windows.Forms.Panel();
            this.background_id_for_user = new System.Windows.Forms.Panel();
            this.new_id_for_user = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.add_new_user_button = new System.Windows.Forms.Label();
            this.panel_connect_1 = new System.Windows.Forms.Panel();
            this.panel_for_table_users = new System.Windows.Forms.Panel();
            this.table_users = new System.Windows.Forms.DataGridView();
            this.button_for_pass_admin = new System.Windows.Forms.Label();
            this.admin_pass_enter = new System.Windows.Forms.Panel();
            this.paas = new System.Windows.Forms.TextBox();
            this.butn_close2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_connect_2 = new System.Windows.Forms.Panel();
            this.reload_tables = new System.Windows.Forms.PictureBox();
            this.task_form = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.close_task = new System.Windows.Forms.PictureBox();
            this.AddFTask = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridView_Task = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Del_user_form = new System.Windows.Forms.Panel();
            this.otmena_udaleniya = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.border_background_admin_pass = new System.Windows.Forms.Panel();
            this.background_admin_pass = new System.Windows.Forms.Panel();
            this.admin_pass = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.border_background_login_user = new System.Windows.Forms.Panel();
            this.background_login_user = new System.Windows.Forms.Panel();
            this.login_user = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.del_user_button = new System.Windows.Forms.Label();
            this.panelCT = new System.Windows.Forms.Panel();
            this.OtmenaB = new System.Windows.Forms.Label();
            this.EnterB = new System.Windows.Forms.Label();
            this.AddF = new System.Windows.Forms.Label();
            this.border_background_panel3 = new System.Windows.Forms.Panel();
            this.background_textbox_panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.border_background_panel2 = new System.Windows.Forms.Panel();
            this.background_textbox_panel2 = new System.Windows.Forms.Panel();
            this.Срок_исполнения = new System.Windows.Forms.MaskedTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.border_background_panel = new System.Windows.Forms.Panel();
            this.background_textbox_panel = new System.Windows.Forms.Panel();
            this.namT = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.background_form = new System.Windows.Forms.Panel();
            this.panel2 = new Scrum.DoubleBufferedPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butn_minus2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_minus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_plus2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_plus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).BeginInit();
            this.control_users_panel.SuspendLayout();
            this.New_user_form.SuspendLayout();
            this.border_background_new_access_for_user.SuspendLayout();
            this.background_access_for_user.SuspendLayout();
            this.border_background_new_pass_for_user.SuspendLayout();
            this.background_pass_for_user.SuspendLayout();
            this.border_background_new_id_for_user.SuspendLayout();
            this.background_id_for_user.SuspendLayout();
            this.panel_for_table_users.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table_users)).BeginInit();
            this.admin_pass_enter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butn_close2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reload_tables)).BeginInit();
            this.task_form.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close_task)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddFTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Task)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.Del_user_form.SuspendLayout();
            this.border_background_admin_pass.SuspendLayout();
            this.background_admin_pass.SuspendLayout();
            this.border_background_login_user.SuspendLayout();
            this.background_login_user.SuspendLayout();
            this.panelCT.SuspendLayout();
            this.border_background_panel3.SuspendLayout();
            this.background_textbox_panel3.SuspendLayout();
            this.border_background_panel2.SuspendLayout();
            this.background_textbox_panel2.SuspendLayout();
            this.border_background_panel.SuspendLayout();
            this.background_textbox_panel.SuspendLayout();
            this.background_form.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.panel1.Controls.Add(this.archive_button);
            this.panel1.Controls.Add(this.users_button);
            this.panel1.Controls.Add(this.CreateTaskB);
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1664, 30);
            this.panel1.TabIndex = 5;
            // 
            // archive_button
            // 
            this.archive_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.archive_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.archive_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.archive_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.archive_button.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.archive_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(189)))));
            this.archive_button.Location = new System.Drawing.Point(1481, 0);
            this.archive_button.Name = "archive_button";
            this.archive_button.Size = new System.Drawing.Size(183, 30);
            this.archive_button.TabIndex = 11;
            this.archive_button.Text = "Архив";
            this.archive_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.archive_button.Click += new System.EventHandler(this.archive_button_Click);
            this.archive_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.archive_button_MouseDown);
            this.archive_button.MouseLeave += new System.EventHandler(this.archive_button_MouseLeave);
            this.archive_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.archive_button_MouseMove);
            // 
            // users_button
            // 
            this.users_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.users_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.users_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.users_button.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.users_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(189)))));
            this.users_button.Location = new System.Drawing.Point(444, 0);
            this.users_button.Name = "users_button";
            this.users_button.Size = new System.Drawing.Size(330, 30);
            this.users_button.TabIndex = 11;
            this.users_button.Text = "  Управление пользователями";
            this.users_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.users_button.Click += new System.EventHandler(this.users_button_Click);
            this.users_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.users_button_MouseDown);
            this.users_button.MouseLeave += new System.EventHandler(this.users_button_MouseLeave);
            this.users_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.users_button_MouseMove);
            // 
            // CreateTaskB
            // 
            this.CreateTaskB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.CreateTaskB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateTaskB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateTaskB.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CreateTaskB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(189)))));
            this.CreateTaskB.Location = new System.Drawing.Point(0, 0);
            this.CreateTaskB.Name = "CreateTaskB";
            this.CreateTaskB.Size = new System.Drawing.Size(444, 30);
            this.CreateTaskB.TabIndex = 11;
            this.CreateTaskB.Text = "    Создать заявку";
            this.CreateTaskB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CreateTaskB.Click += new System.EventHandler(this.CreateTaskB_Click);
            this.CreateTaskB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CreateTaskB_MouseDown);
            this.CreateTaskB.MouseLeave += new System.EventHandler(this.CreateTaskB_MouseLeave);
            this.CreateTaskB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CreateTaskB_MouseMove);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(105)))), ((int)(((byte)(110)))));
            this.label17.Location = new System.Drawing.Point(-4, -7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 33);
            this.label17.TabIndex = 2;
            this.label17.Text = "Desk Name";
            // 
            // butn_minus2
            // 
            this.butn_minus2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butn_minus2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butn_minus2.Image = ((System.Drawing.Image)(resources.GetObject("butn_minus2.Image")));
            this.butn_minus2.Location = new System.Drawing.Point(1564, 0);
            this.butn_minus2.Name = "butn_minus2";
            this.butn_minus2.Size = new System.Drawing.Size(34, 22);
            this.butn_minus2.TabIndex = 4;
            this.butn_minus2.TabStop = false;
            this.butn_minus2.Visible = false;
            this.butn_minus2.Click += new System.EventHandler(this.butn_minus2_Click);
            this.butn_minus2.MouseLeave += new System.EventHandler(this.butn_minus2_MouseLeave);
            // 
            // butn_minus
            // 
            this.butn_minus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butn_minus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(14)))), ((int)(((byte)(17)))));
            this.butn_minus.Image = ((System.Drawing.Image)(resources.GetObject("butn_minus.Image")));
            this.butn_minus.Location = new System.Drawing.Point(1564, 0);
            this.butn_minus.Name = "butn_minus";
            this.butn_minus.Size = new System.Drawing.Size(34, 22);
            this.butn_minus.TabIndex = 3;
            this.butn_minus.TabStop = false;
            this.butn_minus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butn_minus_MouseMove);
            // 
            // butn_plus2
            // 
            this.butn_plus2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butn_plus2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butn_plus2.Image = ((System.Drawing.Image)(resources.GetObject("butn_plus2.Image")));
            this.butn_plus2.Location = new System.Drawing.Point(1597, 0);
            this.butn_plus2.Name = "butn_plus2";
            this.butn_plus2.Size = new System.Drawing.Size(34, 22);
            this.butn_plus2.TabIndex = 2;
            this.butn_plus2.TabStop = false;
            this.butn_plus2.Visible = false;
            this.butn_plus2.Click += new System.EventHandler(this.butn_plus2_Click);
            this.butn_plus2.MouseLeave += new System.EventHandler(this.butn_plus2_MouseLeave);
            // 
            // butn_plus
            // 
            this.butn_plus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butn_plus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(14)))), ((int)(((byte)(17)))));
            this.butn_plus.Image = ((System.Drawing.Image)(resources.GetObject("butn_plus.Image")));
            this.butn_plus.Location = new System.Drawing.Point(1597, 0);
            this.butn_plus.Name = "butn_plus";
            this.butn_plus.Size = new System.Drawing.Size(34, 22);
            this.butn_plus.TabIndex = 1;
            this.butn_plus.TabStop = false;
            this.butn_plus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butn_plus_MouseMove);
            // 
            // butn_close
            // 
            this.butn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(14)))), ((int)(((byte)(17)))));
            this.butn_close.Image = ((System.Drawing.Image)(resources.GetObject("butn_close.Image")));
            this.butn_close.Location = new System.Drawing.Point(1631, 0);
            this.butn_close.Name = "butn_close";
            this.butn_close.Size = new System.Drawing.Size(34, 22);
            this.butn_close.TabIndex = 0;
            this.butn_close.TabStop = false;
            this.butn_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butn_close_MouseMove);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView1.Location = new System.Drawing.Point(11, 11);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(195, 237);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.Text = "dataGridView1";
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.dataGridView1.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseLeave);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView2.Location = new System.Drawing.Point(212, 11);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(195, 237);
            this.dataGridView2.TabIndex = 6;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.Text = "dataGridView2";
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseEnter);
            this.dataGridView2.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseLeave);
            this.dataGridView2.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView2_DataBindingComplete);
            this.dataGridView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseClick);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView3.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView3.ColumnHeadersVisible = false;
            this.dataGridView3.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView3.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView3.Location = new System.Drawing.Point(413, 11);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(195, 237);
            this.dataGridView3.TabIndex = 6;
            this.dataGridView3.TabStop = false;
            this.dataGridView3.Text = "dataGridView3";
            this.dataGridView3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            this.dataGridView3.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellMouseEnter);
            this.dataGridView3.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellMouseLeave);
            this.dataGridView3.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView3_DataBindingComplete);
            this.dataGridView3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView3_MouseClick);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.AllowUserToResizeColumns = false;
            this.dataGridView4.AllowUserToResizeRows = false;
            this.dataGridView4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView4.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView4.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView4.ColumnHeadersVisible = false;
            this.dataGridView4.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView4.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView4.Location = new System.Drawing.Point(614, 11);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView4.RowHeadersVisible = false;
            this.dataGridView4.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView4.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(195, 237);
            this.dataGridView4.TabIndex = 6;
            this.dataGridView4.TabStop = false;
            this.dataGridView4.Text = "dataGridView4";
            this.dataGridView4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellClick);
            this.dataGridView4.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellMouseEnter);
            this.dataGridView4.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellMouseLeave);
            this.dataGridView4.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView4_DataBindingComplete);
            this.dataGridView4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView4_MouseClick);
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.AllowUserToDeleteRows = false;
            this.dataGridView5.AllowUserToResizeColumns = false;
            this.dataGridView5.AllowUserToResizeRows = false;
            this.dataGridView5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView5.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView5.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView5.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView5.ColumnHeadersVisible = false;
            this.dataGridView5.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView5.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView5.Location = new System.Drawing.Point(815, 11);
            this.dataGridView5.MultiSelect = false;
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.ReadOnly = true;
            this.dataGridView5.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView5.RowHeadersVisible = false;
            this.dataGridView5.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView5.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView5.Size = new System.Drawing.Size(195, 237);
            this.dataGridView5.TabIndex = 6;
            this.dataGridView5.TabStop = false;
            this.dataGridView5.Text = "dataGridView5";
            this.dataGridView5.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellClick);
            this.dataGridView5.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellMouseEnter);
            this.dataGridView5.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellMouseLeave);
            this.dataGridView5.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView5_DataBindingComplete);
            this.dataGridView5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView5_MouseClick);
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToDeleteRows = false;
            this.dataGridView6.AllowUserToResizeColumns = false;
            this.dataGridView6.AllowUserToResizeRows = false;
            this.dataGridView6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView6.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView6.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView6.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView6.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView6.ColumnHeadersVisible = false;
            this.dataGridView6.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView6.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView6.Location = new System.Drawing.Point(1016, 11);
            this.dataGridView6.MultiSelect = false;
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView6.RowHeadersVisible = false;
            this.dataGridView6.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView6.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(195, 237);
            this.dataGridView6.TabIndex = 6;
            this.dataGridView6.TabStop = false;
            this.dataGridView6.Text = "dataGridView6";
            this.dataGridView6.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellClick);
            this.dataGridView6.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellMouseEnter);
            this.dataGridView6.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellMouseLeave);
            this.dataGridView6.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView6_DataBindingComplete);
            this.dataGridView6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView6_MouseClick);
            // 
            // dataGridView7
            // 
            this.dataGridView7.AllowUserToAddRows = false;
            this.dataGridView7.AllowUserToDeleteRows = false;
            this.dataGridView7.AllowUserToResizeColumns = false;
            this.dataGridView7.AllowUserToResizeRows = false;
            this.dataGridView7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView7.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView7.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView7.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView7.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView7.ColumnHeadersVisible = false;
            this.dataGridView7.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView7.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView7.Location = new System.Drawing.Point(1217, 11);
            this.dataGridView7.MultiSelect = false;
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.ReadOnly = true;
            this.dataGridView7.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView7.RowHeadersVisible = false;
            this.dataGridView7.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView7.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView7.Size = new System.Drawing.Size(195, 237);
            this.dataGridView7.TabIndex = 6;
            this.dataGridView7.TabStop = false;
            this.dataGridView7.Text = "dataGridView7";
            this.dataGridView7.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView7_CellClick);
            this.dataGridView7.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView7_CellMouseEnter);
            this.dataGridView7.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView7_CellMouseLeave);
            this.dataGridView7.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView7_DataBindingComplete);
            this.dataGridView7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView7_MouseClick);
            // 
            // dataGridView8
            // 
            this.dataGridView8.AllowUserToAddRows = false;
            this.dataGridView8.AllowUserToDeleteRows = false;
            this.dataGridView8.AllowUserToResizeColumns = false;
            this.dataGridView8.AllowUserToResizeRows = false;
            this.dataGridView8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView8.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView8.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView8.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.dataGridView8.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView8.ColumnHeadersVisible = false;
            this.dataGridView8.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView8.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView8.Location = new System.Drawing.Point(1418, 11);
            this.dataGridView8.MultiSelect = false;
            this.dataGridView8.Name = "dataGridView8";
            this.dataGridView8.ReadOnly = true;
            this.dataGridView8.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView8.RowHeadersVisible = false;
            this.dataGridView8.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView8.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView8.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView8.Size = new System.Drawing.Size(195, 237);
            this.dataGridView8.TabIndex = 6;
            this.dataGridView8.TabStop = false;
            this.dataGridView8.Text = "dataGridView8";
            this.dataGridView8.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView8_CellClick);
            this.dataGridView8.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView8_CellMouseEnter);
            this.dataGridView8.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView8_CellMouseLeave);
            this.dataGridView8.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView8_DataBindingComplete);
            this.dataGridView8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView8_MouseClick);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.label7.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(91, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 26);
            this.label7.TabIndex = 10;
            this.label7.Text = "Заявка";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(265, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 26);
            this.label8.TabIndex = 10;
            this.label8.Text = "Согласование";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(466, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 26);
            this.label9.TabIndex = 10;
            this.label9.Text = "Утверждение";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(690, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 26);
            this.label10.TabIndex = 10;
            this.label10.Text = "В работе";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(858, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 26);
            this.label11.TabIndex = 10;
            this.label11.Text = "Формирование";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(1074, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 26);
            this.label12.TabIndex = 10;
            this.label12.Text = "Заключение";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1275, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 26);
            this.label13.TabIndex = 10;
            this.label13.Text = "Исполнение";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(1494, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 26);
            this.label14.TabIndex = 10;
            this.label14.Text = "Оплата";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // control_users_panel
            // 
            this.control_users_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.control_users_panel.Controls.Add(this.show_user);
            this.control_users_panel.Controls.Add(this.delete_user);
            this.control_users_panel.Controls.Add(this.add_user);
            this.control_users_panel.Location = new System.Drawing.Point(122, 10);
            this.control_users_panel.Name = "control_users_panel";
            this.control_users_panel.Size = new System.Drawing.Size(330, 135);
            this.control_users_panel.TabIndex = 12;
            this.control_users_panel.Visible = false;
            // 
            // show_user
            // 
            this.show_user.Cursor = System.Windows.Forms.Cursors.Hand;
            this.show_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.show_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(189)))));
            this.show_user.Location = new System.Drawing.Point(0, 90);
            this.show_user.Name = "show_user";
            this.show_user.Size = new System.Drawing.Size(330, 45);
            this.show_user.TabIndex = 13;
            this.show_user.Text = "Все пользователи      ";
            this.show_user.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.show_user.Click += new System.EventHandler(this.show_user_Click);
            this.show_user.MouseDown += new System.Windows.Forms.MouseEventHandler(this.show_user_MouseDown);
            this.show_user.MouseLeave += new System.EventHandler(this.show_user_MouseLeave);
            this.show_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.show_user_MouseMove);
            // 
            // delete_user
            // 
            this.delete_user.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delete_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.delete_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(189)))));
            this.delete_user.Location = new System.Drawing.Point(0, 44);
            this.delete_user.Name = "delete_user";
            this.delete_user.Size = new System.Drawing.Size(330, 45);
            this.delete_user.TabIndex = 13;
            this.delete_user.Text = "Удалить      ";
            this.delete_user.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.delete_user.Click += new System.EventHandler(this.delete_user_Click);
            this.delete_user.MouseDown += new System.Windows.Forms.MouseEventHandler(this.delete_user_MouseDown);
            this.delete_user.MouseLeave += new System.EventHandler(this.delete_user_MouseLeave);
            this.delete_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.delete_user_MouseMove);
            // 
            // add_user
            // 
            this.add_user.Cursor = System.Windows.Forms.Cursors.Hand;
            this.add_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.add_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(189)))));
            this.add_user.Location = new System.Drawing.Point(0, 0);
            this.add_user.Name = "add_user";
            this.add_user.Size = new System.Drawing.Size(330, 45);
            this.add_user.TabIndex = 13;
            this.add_user.Text = "Добавить      ";
            this.add_user.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.add_user.Click += new System.EventHandler(this.add_user_Click);
            this.add_user.MouseDown += new System.Windows.Forms.MouseEventHandler(this.add_user_MouseDown);
            this.add_user.MouseLeave += new System.EventHandler(this.add_user_MouseLeave);
            this.add_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.add_user_MouseMove);
            // 
            // New_user_form
            // 
            this.New_user_form.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.New_user_form.Controls.Add(this.label32);
            this.New_user_form.Controls.Add(this.label31);
            this.New_user_form.Controls.Add(this.border_background_new_access_for_user);
            this.New_user_form.Controls.Add(this.label25);
            this.New_user_form.Controls.Add(this.border_background_new_pass_for_user);
            this.New_user_form.Controls.Add(this.label18);
            this.New_user_form.Controls.Add(this.border_background_new_id_for_user);
            this.New_user_form.Controls.Add(this.label15);
            this.New_user_form.Controls.Add(this.label22);
            this.New_user_form.Controls.Add(this.add_new_user_button);
            this.New_user_form.Location = new System.Drawing.Point(630, 386);
            this.New_user_form.Name = "New_user_form";
            this.New_user_form.Size = new System.Drawing.Size(442, 523);
            this.New_user_form.TabIndex = 13;
            this.New_user_form.Visible = false;
            this.New_user_form.VisibleChanged += new System.EventHandler(this.New_user_form_VisibleChanged);
            this.New_user_form.Paint += new System.Windows.Forms.PaintEventHandler(this.New_user_form_Paint);
            this.New_user_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.New_user_form_MouseDown);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(36, 82);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(384, 26);
            this.label32.TabIndex = 15;
            this.label32.Text = "убедитесь в уникальности нового логина";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(80, 55);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(286, 26);
            this.label31.TabIndex = 14;
            this.label31.Text = "Чтобы добавить пользователя";
            // 
            // border_background_new_access_for_user
            // 
            this.border_background_new_access_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_new_access_for_user.Controls.Add(this.background_access_for_user);
            this.border_background_new_access_for_user.Controls.Add(this.comboBox1);
            this.border_background_new_access_for_user.Location = new System.Drawing.Point(22, 366);
            this.border_background_new_access_for_user.Name = "border_background_new_access_for_user";
            this.border_background_new_access_for_user.Size = new System.Drawing.Size(399, 39);
            this.border_background_new_access_for_user.TabIndex = 5;
            // 
            // background_access_for_user
            // 
            this.background_access_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_access_for_user.Controls.Add(this.new_access_for_user);
            this.background_access_for_user.Location = new System.Drawing.Point(1, 1);
            this.background_access_for_user.Name = "background_access_for_user";
            this.background_access_for_user.Size = new System.Drawing.Size(381, 37);
            this.background_access_for_user.TabIndex = 4;
            // 
            // new_access_for_user
            // 
            this.new_access_for_user.AutoCompleteCustomSource.AddRange(new string[] {
            "Истец",
            "Бухгалтерия",
            "Оператор"});
            this.new_access_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.new_access_for_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.new_access_for_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.new_access_for_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.new_access_for_user.Location = new System.Drawing.Point(15, 6);
            this.new_access_for_user.MaxLength = 22;
            this.new_access_for_user.Name = "new_access_for_user";
            this.new_access_for_user.Size = new System.Drawing.Size(362, 26);
            this.new_access_for_user.TabIndex = 6;
            this.new_access_for_user.Enter += new System.EventHandler(this.new_access_for_user_Enter);
            this.new_access_for_user.Leave += new System.EventHandler(this.new_access_for_user_Leave);
            this.new_access_for_user.MouseLeave += new System.EventHandler(this.new_access_for_user_MouseLeave);
            this.new_access_for_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.new_access_for_user_MouseMove);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Истец",
            "Бухгалтерия",
            "Оператор"});
            this.comboBox1.Location = new System.Drawing.Point(1, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(397, 37);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.DropDownClosed += new System.EventHandler(this.comboBox1_DropDownClosed);
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            this.comboBox1.MouseLeave += new System.EventHandler(this.comboBox1_MouseLeave);
            this.comboBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseMove);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label25.Location = new System.Drawing.Point(18, 339);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(130, 18);
            this.label25.TabIndex = 6;
            this.label25.Text = "УРОВЕНЬ ДОСТУПА";
            // 
            // border_background_new_pass_for_user
            // 
            this.border_background_new_pass_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_new_pass_for_user.Controls.Add(this.background_pass_for_user);
            this.border_background_new_pass_for_user.Location = new System.Drawing.Point(22, 267);
            this.border_background_new_pass_for_user.Name = "border_background_new_pass_for_user";
            this.border_background_new_pass_for_user.Size = new System.Drawing.Size(399, 39);
            this.border_background_new_pass_for_user.TabIndex = 5;
            // 
            // background_pass_for_user
            // 
            this.background_pass_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_pass_for_user.Controls.Add(this.new_pass_for_user);
            this.background_pass_for_user.Location = new System.Drawing.Point(1, 1);
            this.background_pass_for_user.Name = "background_pass_for_user";
            this.background_pass_for_user.Size = new System.Drawing.Size(397, 37);
            this.background_pass_for_user.TabIndex = 4;
            // 
            // new_pass_for_user
            // 
            this.new_pass_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.new_pass_for_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.new_pass_for_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.new_pass_for_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.new_pass_for_user.Location = new System.Drawing.Point(15, 6);
            this.new_pass_for_user.MaxLength = 32;
            this.new_pass_for_user.Name = "new_pass_for_user";
            this.new_pass_for_user.Size = new System.Drawing.Size(371, 26);
            this.new_pass_for_user.TabIndex = 5;
            this.new_pass_for_user.Enter += new System.EventHandler(this.new_pass_for_user_Enter);
            this.new_pass_for_user.Leave += new System.EventHandler(this.new_pass_for_user_Leave);
            this.new_pass_for_user.MouseLeave += new System.EventHandler(this.new_pass_for_user_MouseLeave);
            this.new_pass_for_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.new_pass_for_user_MouseMove);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label18.Location = new System.Drawing.Point(18, 240);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 18);
            this.label18.TabIndex = 6;
            this.label18.Text = "ПАРОЛЬ";
            // 
            // border_background_new_id_for_user
            // 
            this.border_background_new_id_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_new_id_for_user.Controls.Add(this.background_id_for_user);
            this.border_background_new_id_for_user.Location = new System.Drawing.Point(22, 168);
            this.border_background_new_id_for_user.Name = "border_background_new_id_for_user";
            this.border_background_new_id_for_user.Size = new System.Drawing.Size(399, 39);
            this.border_background_new_id_for_user.TabIndex = 5;
            // 
            // background_id_for_user
            // 
            this.background_id_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_id_for_user.Controls.Add(this.new_id_for_user);
            this.background_id_for_user.Location = new System.Drawing.Point(1, 1);
            this.background_id_for_user.Name = "background_id_for_user";
            this.background_id_for_user.Size = new System.Drawing.Size(397, 37);
            this.background_id_for_user.TabIndex = 4;
            // 
            // new_id_for_user
            // 
            this.new_id_for_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.new_id_for_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.new_id_for_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.new_id_for_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.new_id_for_user.Location = new System.Drawing.Point(15, 6);
            this.new_id_for_user.MaxLength = 64;
            this.new_id_for_user.Name = "new_id_for_user";
            this.new_id_for_user.Size = new System.Drawing.Size(371, 26);
            this.new_id_for_user.TabIndex = 4;
            this.new_id_for_user.Enter += new System.EventHandler(this.new_id_for_user_Enter);
            this.new_id_for_user.Leave += new System.EventHandler(this.new_id_for_user_Leave);
            this.new_id_for_user.MouseLeave += new System.EventHandler(this.new_id_for_user_MouseLeave);
            this.new_id_for_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.new_id_for_user_MouseMove);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label15.Location = new System.Drawing.Point(18, 141);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 18);
            this.label15.TabIndex = 6;
            this.label15.Text = "НОВЫЙ ЛОГИН";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label22.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(70, 473);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 23);
            this.label22.TabIndex = 0;
            this.label22.Text = "Отмена";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label22.Click += new System.EventHandler(this.label22_Click);
            this.label22.MouseLeave += new System.EventHandler(this.label22_MouseLeave);
            this.label22.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label22_MouseMove);
            // 
            // add_new_user_button
            // 
            this.add_new_user_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(181)))), ((int)(((byte)(129)))));
            this.add_new_user_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.add_new_user_button.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.add_new_user_button.ForeColor = System.Drawing.Color.White;
            this.add_new_user_button.Location = new System.Drawing.Point(266, 466);
            this.add_new_user_button.Name = "add_new_user_button";
            this.add_new_user_button.Size = new System.Drawing.Size(126, 37);
            this.add_new_user_button.TabIndex = 1;
            this.add_new_user_button.Text = "Добавить";
            this.add_new_user_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.add_new_user_button.Click += new System.EventHandler(this.label23_Click);
            this.add_new_user_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label23_MouseDown);
            this.add_new_user_button.MouseLeave += new System.EventHandler(this.label23_MouseLeave);
            this.add_new_user_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label23_MouseMove);
            // 
            // panel_connect_1
            // 
            this.panel_connect_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(47)))));
            this.panel_connect_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_connect_1.Location = new System.Drawing.Point(1414, 372);
            this.panel_connect_1.Name = "panel_connect_1";
            this.panel_connect_1.Size = new System.Drawing.Size(442, 45);
            this.panel_connect_1.TabIndex = 13;
            this.panel_connect_1.Visible = false;
            this.panel_connect_1.Click += new System.EventHandler(this.panel_connect_1_Click);
            this.panel_connect_1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_connect_1_MouseDown);
            this.panel_connect_1.MouseLeave += new System.EventHandler(this.panel_connect_1_MouseLeave);
            this.panel_connect_1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_connect_1_MouseMove);
            // 
            // panel_for_table_users
            // 
            this.panel_for_table_users.AutoSize = true;
            this.panel_for_table_users.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_for_table_users.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.panel_for_table_users.Controls.Add(this.table_users);
            this.panel_for_table_users.Location = new System.Drawing.Point(1476, 419);
            this.panel_for_table_users.Name = "panel_for_table_users";
            this.panel_for_table_users.Size = new System.Drawing.Size(271, 47);
            this.panel_for_table_users.TabIndex = 13;
            this.panel_for_table_users.Visible = false;
            // 
            // table_users
            // 
            this.table_users.AllowUserToAddRows = false;
            this.table_users.AllowUserToDeleteRows = false;
            this.table_users.AllowUserToResizeColumns = false;
            this.table_users.AllowUserToResizeRows = false;
            this.table_users.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.table_users.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.table_users.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.table_users.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.table_users.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.table_users.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.table_users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table_users.EnableHeadersVisualStyles = false;
            this.table_users.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.table_users.Location = new System.Drawing.Point(4, 4);
            this.table_users.MaximumSize = new System.Drawing.Size(400, 315);
            this.table_users.MultiSelect = false;
            this.table_users.Name = "table_users";
            this.table_users.ReadOnly = true;
            this.table_users.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.table_users.RowHeadersVisible = false;
            this.table_users.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.table_users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.table_users.Size = new System.Drawing.Size(264, 40);
            this.table_users.TabIndex = 0;
            this.table_users.Text = "dataGridView9";
            this.table_users.Paint += new System.Windows.Forms.PaintEventHandler(this.table_users_Paint);
            // 
            // button_for_pass_admin
            // 
            this.button_for_pass_admin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_for_pass_admin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.button_for_pass_admin.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_for_pass_admin.ForeColor = System.Drawing.Color.White;
            this.button_for_pass_admin.Location = new System.Drawing.Point(-1, 43);
            this.button_for_pass_admin.Name = "button_for_pass_admin";
            this.button_for_pass_admin.Size = new System.Drawing.Size(331, 43);
            this.button_for_pass_admin.TabIndex = 1;
            this.button_for_pass_admin.Text = "Открыть таблицу";
            this.button_for_pass_admin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button_for_pass_admin.Click += new System.EventHandler(this.button_for_pass_admin_Click);
            this.button_for_pass_admin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_for_pass_admin_MouseDown);
            this.button_for_pass_admin.MouseLeave += new System.EventHandler(this.button_for_pass_admin_MouseLeave);
            this.button_for_pass_admin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_for_pass_admin_MouseMove);
            // 
            // admin_pass_enter
            // 
            this.admin_pass_enter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.admin_pass_enter.Controls.Add(this.paas);
            this.admin_pass_enter.Controls.Add(this.button_for_pass_admin);
            this.admin_pass_enter.Location = new System.Drawing.Point(1078, 393);
            this.admin_pass_enter.Name = "admin_pass_enter";
            this.admin_pass_enter.Size = new System.Drawing.Size(330, 86);
            this.admin_pass_enter.TabIndex = 14;
            this.admin_pass_enter.Visible = false;
            // 
            // paas
            // 
            this.paas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.paas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.paas.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.paas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.paas.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.paas.Location = new System.Drawing.Point(0, 8);
            this.paas.MaxLength = 32;
            this.paas.Name = "paas";
            this.paas.PasswordChar = '•';
            this.paas.PlaceholderText = "Введите Ваш пароль";
            this.paas.Size = new System.Drawing.Size(330, 26);
            this.paas.TabIndex = 3;
            this.paas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // butn_close2
            // 
            this.butn_close2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butn_close2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butn_close2.Image = ((System.Drawing.Image)(resources.GetObject("butn_close2.Image")));
            this.butn_close2.Location = new System.Drawing.Point(1631, 0);
            this.butn_close2.Name = "butn_close2";
            this.butn_close2.Size = new System.Drawing.Size(34, 22);
            this.butn_close2.TabIndex = 0;
            this.butn_close2.TabStop = false;
            this.butn_close2.Visible = false;
            this.butn_close2.Click += new System.EventHandler(this.butn_close2_Click);
            this.butn_close2.MouseLeave += new System.EventHandler(this.butn_close2_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1626, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.reload_tables_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // panel_connect_2
            // 
            this.panel_connect_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.panel_connect_2.Location = new System.Drawing.Point(1431, 423);
            this.panel_connect_2.Name = "panel_connect_2";
            this.panel_connect_2.Size = new System.Drawing.Size(39, 39);
            this.panel_connect_2.TabIndex = 13;
            this.panel_connect_2.Visible = false;
            // 
            // reload_tables
            // 
            this.reload_tables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reload_tables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.reload_tables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reload_tables.Image = ((System.Drawing.Image)(resources.GetObject("reload_tables.Image")));
            this.reload_tables.Location = new System.Drawing.Point(1626, 0);
            this.reload_tables.Name = "reload_tables";
            this.reload_tables.Size = new System.Drawing.Size(38, 36);
            this.reload_tables.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.reload_tables.TabIndex = 11;
            this.reload_tables.TabStop = false;
            this.reload_tables.Visible = false;
            this.reload_tables.Click += new System.EventHandler(this.reload_tables_Click);
            this.reload_tables.MouseLeave += new System.EventHandler(this.reload_tables_MouseLeave);
            // 
            // task_form
            // 
            this.task_form.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(80)))), ((int)(((byte)(85)))));
            this.task_form.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("task_form.BackgroundImage")));
            this.task_form.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.task_form.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.task_form.Controls.Add(this.label37);
            this.task_form.Controls.Add(this.label36);
            this.task_form.Controls.Add(this.label35);
            this.task_form.Controls.Add(this.label34);
            this.task_form.Controls.Add(this.label33);
            this.task_form.Controls.Add(this.label1);
            this.task_form.Controls.Add(this.panel3);
            this.task_form.Controls.Add(this.AddFTask);
            this.task_form.Controls.Add(this.label16);
            this.task_form.Controls.Add(this.dataGridView_Task);
            this.task_form.Controls.Add(this.label6);
            this.task_form.Controls.Add(this.label2);
            this.task_form.Controls.Add(this.label5);
            this.task_form.Controls.Add(this.label3);
            this.task_form.Controls.Add(this.label4);
            this.task_form.Controls.Add(this.pictureBox2);
            this.task_form.Location = new System.Drawing.Point(835, 6);
            this.task_form.Name = "task_form";
            this.task_form.Size = new System.Drawing.Size(381, 369);
            this.task_form.TabIndex = 9;
            this.task_form.Visible = false;
            this.task_form.VisibleChanged += new System.EventHandler(this.task_form_VisibleChanged);
            this.task_form.Paint += new System.Windows.Forms.PaintEventHandler(this.task_form_Paint);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label37.Location = new System.Drawing.Point(21, 258);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(173, 18);
            this.label37.TabIndex = 17;
            this.label37.Text = "ПРИКРЕПЛЁННЫЕ ФАЙЛЫ";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label36.Location = new System.Drawing.Point(249, 188);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(111, 18);
            this.label36.TabIndex = 17;
            this.label36.Text = "СТАТУС ОПЛАТЫ";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label35.Location = new System.Drawing.Point(21, 188);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(89, 18);
            this.label35.TabIndex = 17;
            this.label35.Text = "СТОИМОСТЬ";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label34.Location = new System.Drawing.Point(225, 120);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(135, 18);
            this.label34.TabIndex = 16;
            this.label34.Text = "СРОК ИСПОЛНЕНИЯ";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.label33.Location = new System.Drawing.Point(250, 138);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(112, 26);
            this.label33.TabIndex = 10;
            this.label33.Text = "30.07.7777";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label1.Location = new System.Drawing.Point(21, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "СОЗДАН";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(47)))));
            this.panel3.Controls.Add(this.close_task);
            this.panel3.Location = new System.Drawing.Point(-1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(381, 22);
            this.panel3.TabIndex = 15;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.task_form_MouseDown);
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.task_form_MouseMove);
            // 
            // close_task
            // 
            this.close_task.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.close_task.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_task.Image = ((System.Drawing.Image)(resources.GetObject("close_task.Image")));
            this.close_task.Location = new System.Drawing.Point(347, 0);
            this.close_task.Name = "close_task";
            this.close_task.Size = new System.Drawing.Size(34, 22);
            this.close_task.TabIndex = 16;
            this.close_task.TabStop = false;
            this.close_task.Click += new System.EventHandler(this.label1_Click);
            this.close_task.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.close_task.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            // 
            // AddFTask
            // 
            this.AddFTask.BackColor = System.Drawing.Color.Transparent;
            this.AddFTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AddFTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddFTask.Image = ((System.Drawing.Image)(resources.GetObject("AddFTask.Image")));
            this.AddFTask.Location = new System.Drawing.Point(329, 279);
            this.AddFTask.Name = "AddFTask";
            this.AddFTask.Size = new System.Drawing.Size(31, 31);
            this.AddFTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AddFTask.TabIndex = 13;
            this.AddFTask.TabStop = false;
            this.AddFTask.Visible = false;
            this.AddFTask.Click += new System.EventHandler(this.AddFTask_Click);
            this.AddFTask.MouseLeave += new System.EventHandler(this.AddFTask_MouseLeave);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.label16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label16.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(0, 333);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(379, 35);
            this.label16.TabIndex = 14;
            this.label16.Text = "На согласование";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label16.Click += new System.EventHandler(this.label16_Click);
            this.label16.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label16_MouseDown);
            this.label16.MouseLeave += new System.EventHandler(this.label16_MouseLeave);
            this.label16.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label16_MouseMove);
            // 
            // dataGridView_Task
            // 
            this.dataGridView_Task.AllowUserToAddRows = false;
            this.dataGridView_Task.AllowUserToDeleteRows = false;
            this.dataGridView_Task.AllowUserToResizeColumns = false;
            this.dataGridView_Task.AllowUserToResizeRows = false;
            this.dataGridView_Task.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Task.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Task.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView_Task.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView_Task.ColumnHeadersVisible = false;
            this.dataGridView_Task.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView_Task.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.dataGridView_Task.Location = new System.Drawing.Point(19, 279);
            this.dataGridView_Task.MinimumSize = new System.Drawing.Size(166, 29);
            this.dataGridView_Task.MultiSelect = false;
            this.dataGridView_Task.Name = "dataGridView_Task";
            this.dataGridView_Task.ReadOnly = true;
            this.dataGridView_Task.RowHeadersVisible = false;
            this.dataGridView_Task.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView_Task.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Task.Size = new System.Drawing.Size(304, 31);
            this.dataGridView_Task.TabIndex = 12;
            this.dataGridView_Task.Text = "dataGridView9";
            this.dataGridView_Task.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_Task_CellMouseClick);
            this.dataGridView_Task.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Task_CellMouseEnter);
            this.dataGridView_Task.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Task_CellMouseLeave);
            this.dataGridView_Task.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView_Task_Paint);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.label6.Location = new System.Drawing.Point(195, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 26);
            this.label6.TabIndex = 10;
            this.label6.Text = "Оплачено";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-1, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 31);
            this.label2.TabIndex = 10;
            this.label2.Text = "Название";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.label5.Location = new System.Drawing.Point(19, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "68456";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.label3.Location = new System.Drawing.Point(-1, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(380, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "Автор";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.label4.Location = new System.Drawing.Point(19, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 26);
            this.label4.TabIndex = 10;
            this.label4.Text = "30.07.7777";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(329, 279);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // Del_user_form
            // 
            this.Del_user_form.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.Del_user_form.Controls.Add(this.otmena_udaleniya);
            this.Del_user_form.Controls.Add(this.label30);
            this.Del_user_form.Controls.Add(this.label29);
            this.Del_user_form.Controls.Add(this.label28);
            this.Del_user_form.Controls.Add(this.border_background_admin_pass);
            this.Del_user_form.Controls.Add(this.label27);
            this.Del_user_form.Controls.Add(this.border_background_login_user);
            this.Del_user_form.Controls.Add(this.label26);
            this.Del_user_form.Controls.Add(this.del_user_button);
            this.Del_user_form.Location = new System.Drawing.Point(279, 408);
            this.Del_user_form.Name = "Del_user_form";
            this.Del_user_form.Size = new System.Drawing.Size(442, 446);
            this.Del_user_form.TabIndex = 14;
            this.Del_user_form.Visible = false;
            this.Del_user_form.VisibleChanged += new System.EventHandler(this.Del_user_form_VisibleChanged);
            this.Del_user_form.Paint += new System.Windows.Forms.PaintEventHandler(this.Del_user_form_Paint);
            // 
            // otmena_udaleniya
            // 
            this.otmena_udaleniya.AutoSize = true;
            this.otmena_udaleniya.Cursor = System.Windows.Forms.Cursors.Hand;
            this.otmena_udaleniya.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.otmena_udaleniya.ForeColor = System.Drawing.Color.White;
            this.otmena_udaleniya.Location = new System.Drawing.Point(69, 402);
            this.otmena_udaleniya.Name = "otmena_udaleniya";
            this.otmena_udaleniya.Size = new System.Drawing.Size(73, 23);
            this.otmena_udaleniya.TabIndex = 14;
            this.otmena_udaleniya.Text = "Отмена";
            this.otmena_udaleniya.Click += new System.EventHandler(this.otmena_udaleniya_Click);
            this.otmena_udaleniya.MouseLeave += new System.EventHandler(this.otmena_udaleniya_MouseLeave);
            this.otmena_udaleniya.MouseMove += new System.Windows.Forms.MouseEventHandler(this.otmena_udaleniya_MouseMove);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(97, 102);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(248, 26);
            this.label30.TabIndex = 7;
            this.label30.Text = "и пароль администратора";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(34, 78);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(376, 26);
            this.label29.TabIndex = 7;
            this.label29.Text = "необходимо ввести логин пользователя";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(86, 54);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(274, 26);
            this.label28.TabIndex = 7;
            this.label28.Text = "Чтобы удалить пользователя";
            // 
            // border_background_admin_pass
            // 
            this.border_background_admin_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_admin_pass.Controls.Add(this.background_admin_pass);
            this.border_background_admin_pass.Location = new System.Drawing.Point(23, 291);
            this.border_background_admin_pass.Name = "border_background_admin_pass";
            this.border_background_admin_pass.Size = new System.Drawing.Size(399, 39);
            this.border_background_admin_pass.TabIndex = 5;
            // 
            // background_admin_pass
            // 
            this.background_admin_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_admin_pass.Controls.Add(this.admin_pass);
            this.background_admin_pass.Location = new System.Drawing.Point(1, 1);
            this.background_admin_pass.Name = "background_admin_pass";
            this.background_admin_pass.Size = new System.Drawing.Size(397, 37);
            this.background_admin_pass.TabIndex = 4;
            // 
            // admin_pass
            // 
            this.admin_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.admin_pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.admin_pass.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.admin_pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.admin_pass.Location = new System.Drawing.Point(15, 6);
            this.admin_pass.MaxLength = 32;
            this.admin_pass.Name = "admin_pass";
            this.admin_pass.PasswordChar = '•';
            this.admin_pass.Size = new System.Drawing.Size(371, 26);
            this.admin_pass.TabIndex = 8;
            this.admin_pass.Enter += new System.EventHandler(this.admin_pass_Enter);
            this.admin_pass.Leave += new System.EventHandler(this.admin_pass_Leave);
            this.admin_pass.MouseLeave += new System.EventHandler(this.admin_pass_MouseLeave);
            this.admin_pass.MouseMove += new System.Windows.Forms.MouseEventHandler(this.admin_pass_MouseMove);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label27.Location = new System.Drawing.Point(19, 264);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(94, 18);
            this.label27.TabIndex = 6;
            this.label27.Text = "ВАШ ПАРОЛЬ";
            // 
            // border_background_login_user
            // 
            this.border_background_login_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_login_user.Controls.Add(this.background_login_user);
            this.border_background_login_user.Location = new System.Drawing.Point(23, 189);
            this.border_background_login_user.Name = "border_background_login_user";
            this.border_background_login_user.Size = new System.Drawing.Size(399, 39);
            this.border_background_login_user.TabIndex = 5;
            // 
            // background_login_user
            // 
            this.background_login_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_login_user.Controls.Add(this.login_user);
            this.background_login_user.Location = new System.Drawing.Point(1, 1);
            this.background_login_user.Name = "background_login_user";
            this.background_login_user.Size = new System.Drawing.Size(397, 37);
            this.background_login_user.TabIndex = 4;
            // 
            // login_user
            // 
            this.login_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.login_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.login_user.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.login_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.login_user.Location = new System.Drawing.Point(15, 6);
            this.login_user.MaxLength = 64;
            this.login_user.Name = "login_user";
            this.login_user.Size = new System.Drawing.Size(371, 26);
            this.login_user.TabIndex = 7;
            this.login_user.Enter += new System.EventHandler(this.login_user_Enter);
            this.login_user.Leave += new System.EventHandler(this.login_user_Leave);
            this.login_user.MouseLeave += new System.EventHandler(this.login_user_MouseLeave);
            this.login_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.login_user_MouseMove);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label26.Location = new System.Drawing.Point(19, 162);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(156, 18);
            this.label26.TabIndex = 6;
            this.label26.Text = "ЛОГИН ПОЛЬЗОВАТЕЛЯ";
            // 
            // del_user_button
            // 
            this.del_user_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.del_user_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.del_user_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.del_user_button.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.del_user_button.ForeColor = System.Drawing.Color.White;
            this.del_user_button.Location = new System.Drawing.Point(264, 397);
            this.del_user_button.Name = "del_user_button";
            this.del_user_button.Size = new System.Drawing.Size(126, 37);
            this.del_user_button.TabIndex = 1;
            this.del_user_button.Text = "Удалить";
            this.del_user_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.del_user_button.Click += new System.EventHandler(this.del_user_button_Click);
            this.del_user_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.del_user_button_MouseDown);
            this.del_user_button.MouseLeave += new System.EventHandler(this.del_user_button_MouseLeave);
            this.del_user_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.del_user_button_MouseMove);
            // 
            // panelCT
            // 
            this.panelCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.panelCT.Controls.Add(this.OtmenaB);
            this.panelCT.Controls.Add(this.EnterB);
            this.panelCT.Controls.Add(this.AddF);
            this.panelCT.Controls.Add(this.border_background_panel3);
            this.panelCT.Controls.Add(this.label24);
            this.panelCT.Controls.Add(this.border_background_panel2);
            this.panelCT.Controls.Add(this.label23);
            this.panelCT.Controls.Add(this.label21);
            this.panelCT.Controls.Add(this.label20);
            this.panelCT.Controls.Add(this.border_background_panel);
            this.panelCT.Controls.Add(this.label19);
            this.panelCT.Location = new System.Drawing.Point(31, 579);
            this.panelCT.Name = "panelCT";
            this.panelCT.Size = new System.Drawing.Size(444, 577);
            this.panelCT.TabIndex = 14;
            this.panelCT.Visible = false;
            this.panelCT.VisibleChanged += new System.EventHandler(this.panelCT_VisibleChanged);
            this.panelCT.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCT_Paint);
            this.panelCT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelCT_MouseDown);
            // 
            // OtmenaB
            // 
            this.OtmenaB.AutoSize = true;
            this.OtmenaB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OtmenaB.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.OtmenaB.ForeColor = System.Drawing.Color.White;
            this.OtmenaB.Location = new System.Drawing.Point(70, 504);
            this.OtmenaB.Name = "OtmenaB";
            this.OtmenaB.Size = new System.Drawing.Size(73, 23);
            this.OtmenaB.TabIndex = 9;
            this.OtmenaB.Text = "Отмена";
            this.OtmenaB.Click += new System.EventHandler(this.OtmenaB_Click);
            this.OtmenaB.MouseLeave += new System.EventHandler(this.OtmenaB_MouseLeave);
            this.OtmenaB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OtmenaB_MouseMove);
            // 
            // EnterB
            // 
            this.EnterB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(181)))), ((int)(((byte)(129)))));
            this.EnterB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EnterB.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.EnterB.ForeColor = System.Drawing.Color.White;
            this.EnterB.Location = new System.Drawing.Point(264, 497);
            this.EnterB.Name = "EnterB";
            this.EnterB.Size = new System.Drawing.Size(126, 37);
            this.EnterB.TabIndex = 8;
            this.EnterB.Text = "Принять";
            this.EnterB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.EnterB.Click += new System.EventHandler(this.EnterB_Click);
            this.EnterB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnterB_MouseDown);
            this.EnterB.MouseLeave += new System.EventHandler(this.EnterB_MouseLeave);
            this.EnterB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EnterB_MouseMove);
            // 
            // AddF
            // 
            this.AddF.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.AddF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddF.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddF.ForeColor = System.Drawing.Color.White;
            this.AddF.Location = new System.Drawing.Point(23, 387);
            this.AddF.Name = "AddF";
            this.AddF.Size = new System.Drawing.Size(399, 39);
            this.AddF.TabIndex = 3;
            this.AddF.Text = "Прикрепить файл";
            this.AddF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AddF.Click += new System.EventHandler(this.AddF_Click);
            this.AddF.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddF_MouseDown);
            this.AddF.MouseLeave += new System.EventHandler(this.AddF_MouseLeave);
            this.AddF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AddF_MouseMove);
            // 
            // border_background_panel3
            // 
            this.border_background_panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_panel3.Controls.Add(this.background_textbox_panel3);
            this.border_background_panel3.Location = new System.Drawing.Point(22, 312);
            this.border_background_panel3.Name = "border_background_panel3";
            this.border_background_panel3.Size = new System.Drawing.Size(399, 39);
            this.border_background_panel3.TabIndex = 5;
            // 
            // background_textbox_panel3
            // 
            this.background_textbox_panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_textbox_panel3.Controls.Add(this.textBox1);
            this.background_textbox_panel3.Location = new System.Drawing.Point(1, 1);
            this.background_textbox_panel3.Name = "background_textbox_panel3";
            this.background_textbox_panel3.Size = new System.Drawing.Size(397, 37);
            this.background_textbox_panel3.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.textBox1.Location = new System.Drawing.Point(15, 6);
            this.textBox1.MaxLength = 22;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(371, 26);
            this.textBox1.TabIndex = 3;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            this.textBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseMove);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label24.Location = new System.Drawing.Point(18, 283);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(89, 18);
            this.label24.TabIndex = 6;
            this.label24.Text = "СТОИМОСТЬ";
            // 
            // border_background_panel2
            // 
            this.border_background_panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_panel2.Controls.Add(this.background_textbox_panel2);
            this.border_background_panel2.Location = new System.Drawing.Point(22, 216);
            this.border_background_panel2.Name = "border_background_panel2";
            this.border_background_panel2.Size = new System.Drawing.Size(399, 39);
            this.border_background_panel2.TabIndex = 5;
            // 
            // background_textbox_panel2
            // 
            this.background_textbox_panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_textbox_panel2.Controls.Add(this.Срок_исполнения);
            this.background_textbox_panel2.Location = new System.Drawing.Point(1, 1);
            this.background_textbox_panel2.Name = "background_textbox_panel2";
            this.background_textbox_panel2.Size = new System.Drawing.Size(397, 37);
            this.background_textbox_panel2.TabIndex = 4;
            // 
            // Срок_исполнения
            // 
            this.Срок_исполнения.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.Срок_исполнения.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Срок_исполнения.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Срок_исполнения.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.Срок_исполнения.Location = new System.Drawing.Point(15, 4);
            this.Срок_исполнения.Mask = "00/00/0000";
            this.Срок_исполнения.Name = "Срок_исполнения";
            this.Срок_исполнения.Size = new System.Drawing.Size(371, 26);
            this.Срок_исполнения.TabIndex = 2;
            this.Срок_исполнения.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.Срок_исполнения_TypeValidationCompleted);
            this.Срок_исполнения.Enter += new System.EventHandler(this.Срок_исполнения_Enter);
            this.Срок_исполнения.Leave += new System.EventHandler(this.Срок_исполнения_Leave);
            this.Срок_исполнения.MouseLeave += new System.EventHandler(this.Срок_исполнения_MouseLeave);
            this.Срок_исполнения.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Срок_исполнения_MouseMove);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label23.Location = new System.Drawing.Point(18, 187);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(135, 18);
            this.label23.TabIndex = 6;
            this.label23.Text = "СРОК ИСПОЛНЕНИЯ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(127, 38);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(189, 26);
            this.label21.TabIndex = 7;
            this.label21.Text = "заполните все поля";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(109, 15);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(227, 26);
            this.label20.TabIndex = 7;
            this.label20.Text = "Для добавления заявки";
            // 
            // border_background_panel
            // 
            this.border_background_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(39)))));
            this.border_background_panel.Controls.Add(this.background_textbox_panel);
            this.border_background_panel.Location = new System.Drawing.Point(22, 118);
            this.border_background_panel.Name = "border_background_panel";
            this.border_background_panel.Size = new System.Drawing.Size(399, 39);
            this.border_background_panel.TabIndex = 5;
            // 
            // background_textbox_panel
            // 
            this.background_textbox_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.background_textbox_panel.Controls.Add(this.namT);
            this.background_textbox_panel.Location = new System.Drawing.Point(1, 1);
            this.background_textbox_panel.Name = "background_textbox_panel";
            this.background_textbox_panel.Size = new System.Drawing.Size(397, 37);
            this.background_textbox_panel.TabIndex = 4;
            // 
            // namT
            // 
            this.namT.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.namT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.namT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.namT.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.namT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.namT.Location = new System.Drawing.Point(6, 4);
            this.namT.MaxLength = 32;
            this.namT.Name = "namT";
            this.namT.Size = new System.Drawing.Size(386, 29);
            this.namT.TabIndex = 1;
            this.namT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.namT.Enter += new System.EventHandler(this.namT_Enter);
            this.namT.Leave += new System.EventHandler(this.namT_Leave);
            this.namT.MouseLeave += new System.EventHandler(this.namT_MouseLeave);
            this.namT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.namT_MouseMove);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(145)))), ((int)(((byte)(150)))));
            this.label19.Location = new System.Drawing.Point(18, 89);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(85, 18);
            this.label19.TabIndex = 6;
            this.label19.Text = "ЗАГОЛОВОК";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(54)))), ((int)(((byte)(60)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.textBox4.Location = new System.Drawing.Point(15, 4);
            this.textBox4.MaxLength = 22;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(371, 29);
            this.textBox4.TabIndex = 2;
            this.textBox4.Text = "12345678912345678912345678";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.textBox2.Location = new System.Drawing.Point(6, 4);
            this.textBox2.MaxLength = 64;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(386, 29);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.textBox3.Location = new System.Drawing.Point(15, -5);
            this.textBox3.MaxLength = 22;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(235, 26);
            this.textBox3.TabIndex = 0;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox5.ForeColor = System.Drawing.Color.Gray;
            this.textBox5.Location = new System.Drawing.Point(15, 6);
            this.textBox5.MaxLength = 22;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(371, 26);
            this.textBox5.TabIndex = 1;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.textBox6.Location = new System.Drawing.Point(15, 6);
            this.textBox6.MaxLength = 64;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(371, 26);
            this.textBox6.TabIndex = 4;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(53)))));
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(220)))), ((int)(((byte)(221)))));
            this.textBox7.Location = new System.Drawing.Point(15, 6);
            this.textBox7.MaxLength = 64;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(371, 26);
            this.textBox7.TabIndex = 4;
            // 
            // background_form
            // 
            this.background_form.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.background_form.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.background_form.Controls.Add(this.control_users_panel);
            this.background_form.Controls.Add(this.task_form);
            this.background_form.Controls.Add(this.panel2);
            this.background_form.Controls.Add(this.reload_tables);
            this.background_form.Controls.Add(this.panelCT);
            this.background_form.Controls.Add(this.label7);
            this.background_form.Controls.Add(this.label14);
            this.background_form.Controls.Add(this.label8);
            this.background_form.Controls.Add(this.label13);
            this.background_form.Controls.Add(this.label9);
            this.background_form.Controls.Add(this.label12);
            this.background_form.Controls.Add(this.label10);
            this.background_form.Controls.Add(this.label11);
            this.background_form.Controls.Add(this.pictureBox1);
            this.background_form.Location = new System.Drawing.Point(0, 52);
            this.background_form.Name = "background_form";
            this.background_form.Size = new System.Drawing.Size(1664, 615);
            this.background_form.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(136)))), ((int)(((byte)(214)))));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.New_user_form);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.Del_user_form);
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.panel_connect_2);
            this.panel2.Controls.Add(this.dataGridView3);
            this.panel2.Controls.Add(this.dataGridView8);
            this.panel2.Controls.Add(this.dataGridView7);
            this.panel2.Controls.Add(this.dataGridView6);
            this.panel2.Controls.Add(this.dataGridView5);
            this.panel2.Controls.Add(this.admin_pass_enter);
            this.panel2.Controls.Add(this.dataGridView4);
            this.panel2.Controls.Add(this.panel_for_table_users);
            this.panel2.Controls.Add(this.panel_connect_1);
            this.panel2.Location = new System.Drawing.Point(20, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1624, 478);
            this.panel2.TabIndex = 12;
            // 
            // Главная
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(14)))), ((int)(((byte)(17)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1664, 666);
            this.Controls.Add(this.background_form);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.butn_minus2);
            this.Controls.Add(this.butn_minus);
            this.Controls.Add(this.butn_plus2);
            this.Controls.Add(this.butn_plus);
            this.Controls.Add(this.butn_close2);
            this.Controls.Add(this.butn_close);
            this.Controls.Add(this.label17);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1212, 629);
            this.Name = "Главная";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Главная_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Главная_MouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.butn_minus2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_minus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_plus2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_plus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).EndInit();
            this.control_users_panel.ResumeLayout(false);
            this.New_user_form.ResumeLayout(false);
            this.New_user_form.PerformLayout();
            this.border_background_new_access_for_user.ResumeLayout(false);
            this.background_access_for_user.ResumeLayout(false);
            this.background_access_for_user.PerformLayout();
            this.border_background_new_pass_for_user.ResumeLayout(false);
            this.background_pass_for_user.ResumeLayout(false);
            this.background_pass_for_user.PerformLayout();
            this.border_background_new_id_for_user.ResumeLayout(false);
            this.background_id_for_user.ResumeLayout(false);
            this.background_id_for_user.PerformLayout();
            this.panel_for_table_users.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table_users)).EndInit();
            this.admin_pass_enter.ResumeLayout(false);
            this.admin_pass_enter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butn_close2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reload_tables)).EndInit();
            this.task_form.ResumeLayout(false);
            this.task_form.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.close_task)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddFTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Task)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.Del_user_form.ResumeLayout(false);
            this.Del_user_form.PerformLayout();
            this.border_background_admin_pass.ResumeLayout(false);
            this.background_admin_pass.ResumeLayout(false);
            this.background_admin_pass.PerformLayout();
            this.border_background_login_user.ResumeLayout(false);
            this.background_login_user.ResumeLayout(false);
            this.background_login_user.PerformLayout();
            this.panelCT.ResumeLayout(false);
            this.panelCT.PerformLayout();
            this.border_background_panel3.ResumeLayout(false);
            this.background_textbox_panel3.ResumeLayout(false);
            this.background_textbox_panel3.PerformLayout();
            this.border_background_panel2.ResumeLayout(false);
            this.background_textbox_panel2.ResumeLayout(false);
            this.background_textbox_panel2.PerformLayout();
            this.border_background_panel.ResumeLayout(false);
            this.background_textbox_panel.ResumeLayout(false);
            this.background_textbox_panel.PerformLayout();
            this.background_form.ResumeLayout(false);
            this.background_form.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.DataGridView dataGridView7;
        private System.Windows.Forms.DataGridView dataGridView8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label CreateTaskB;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label users_button;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel control_users_panel;
        private System.Windows.Forms.Label delete_user;
        private System.Windows.Forms.Label add_user;
        private System.Windows.Forms.Panel New_user_form;
        private System.Windows.Forms.Panel panel_connect_1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox new_id_for_user;
        private System.Windows.Forms.Label add_new_user_button;
        private System.Windows.Forms.TextBox new_pass_for_user;
        private System.Windows.Forms.Panel panel_for_table_users;
        private System.Windows.Forms.DataGridView table_users;
        private System.Windows.Forms.Label del_u;
        private System.Windows.Forms.Label ewq;
        private System.Windows.Forms.Label show_user;
        private System.Windows.Forms.Label button_for_pass_admin;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Panel admin_pass_enter;
        private System.Windows.Forms.TextBox p;
        private System.Windows.Forms.TextBox paas;
        private System.Windows.Forms.PictureBox butn_close;
        private System.Windows.Forms.PictureBox butn_minus2;
        private System.Windows.Forms.PictureBox butn_minus;
        private System.Windows.Forms.PictureBox butn_plus2;
        private System.Windows.Forms.PictureBox butn_plus;
        private System.Windows.Forms.PictureBox butn_close2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_connect_2;
        private System.Windows.Forms.PictureBox reload_tables;
        private System.Windows.Forms.Panel task_form;
        private System.Windows.Forms.PictureBox AddFTask;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dataGridView_Task;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Del_user_form;
        private System.Windows.Forms.Label del_user_button;
        private System.Windows.Forms.TextBox admin_pass;
        private System.Windows.Forms.TextBox login_user;
        private System.Windows.Forms.Panel panelCT;
        private System.Windows.Forms.Panel background_textbox_panel;
        private System.Windows.Forms.Panel border_background_panel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox namT;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel border_background_panel2;
        private System.Windows.Forms.Panel background_textbox_panel2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel background_textbox_panel3;
        private System.Windows.Forms.Panel border_background_panel3;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.MaskedTextBox Срок_исполнения;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label AddF;
        private System.Windows.Forms.Label EnterB;
        private System.Windows.Forms.Label OtmenaB;
        private System.Windows.Forms.Panel background_id_for_user;
        private System.Windows.Forms.Panel border_background_new_id_for_user;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel border_background_new_pass_for_user;
        private System.Windows.Forms.Panel background_pass_for_user;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel border_background_new_access_for_user;
        private System.Windows.Forms.Panel background_access_for_user;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox new_access_for_user;
        private System.Windows.Forms.Panel border_background_login_user;
        private System.Windows.Forms.Panel background_login_user;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel border_background_admin_pass;
        private System.Windows.Forms.Panel background_admin_pass;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label otmena_udaleniya;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox close_task;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label archive_button;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel background_form;
        private Scrum.DoubleBufferedPanel panel2;
    }
}