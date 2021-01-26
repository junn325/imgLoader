using System.ComponentModel;
using System.Windows.Forms;

namespace imgLoader.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_LinkCp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_RemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStop = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.progAll = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip_lblNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_lblTotalNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_lblItemNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Location = new System.Drawing.Point(672, 344);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(125, 22);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "다운로드 시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AllowMerge = false;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Add,
            this.toolStripMenuItem3,
            this.menuItem_LinkCp,
            this.toolStripMenuItem2,
            this.menuItem_Remove,
            this.menuItem_RemoveAll,
            this.toolStripMenuItem1,
            this.menuItem_Settings});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 132);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // menuItem_Add
            // 
            this.menuItem_Add.Name = "menuItem_Add";
            this.menuItem_Add.Size = new System.Drawing.Size(162, 22);
            this.menuItem_Add.Text = "북마크에서 추가";
            this.menuItem_Add.Click += new System.EventHandler(this.MenuItem_Add_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(159, 6);
            // 
            // menuItem_LinkCp
            // 
            this.menuItem_LinkCp.Enabled = false;
            this.menuItem_LinkCp.Name = "menuItem_LinkCp";
            this.menuItem_LinkCp.Size = new System.Drawing.Size(162, 22);
            this.menuItem_LinkCp.Text = "링크 복사";
            this.menuItem_LinkCp.Click += new System.EventHandler(this.MenuItem_LinkCp_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // menuItem_Remove
            // 
            this.menuItem_Remove.Enabled = false;
            this.menuItem_Remove.Name = "menuItem_Remove";
            this.menuItem_Remove.Size = new System.Drawing.Size(162, 22);
            this.menuItem_Remove.Text = "제거";
            this.menuItem_Remove.Click += new System.EventHandler(this.MenuItem_Remove_Click);
            // 
            // menuItem_RemoveAll
            // 
            this.menuItem_RemoveAll.Name = "menuItem_RemoveAll";
            this.menuItem_RemoveAll.Size = new System.Drawing.Size(162, 22);
            this.menuItem_RemoveAll.Text = "모두 제거";
            this.menuItem_RemoveAll.Click += new System.EventHandler(this.MenuItem_RemoveAll_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // menuItem_Settings
            // 
            this.menuItem_Settings.Name = "menuItem_Settings";
            this.menuItem_Settings.Size = new System.Drawing.Size(162, 22);
            this.menuItem_Settings.Text = "설정";
            this.menuItem_Settings.Click += new System.EventHandler(this.MenuItem_Settings_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(632, 344);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(39, 22);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "중지";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.Button2_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 13);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(785, 23);
            this.textBox1.TabIndex = 31;
            this.textBox1.Text = "주소 입력 후 엔터로 추가";
            this.textBox1.Click += new System.EventHandler(this.TextBox1_Click);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // progAll
            // 
            this.progAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progAll.Location = new System.Drawing.Point(12, 324);
            this.progAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progAll.MarqueeAnimationSpeed = 10;
            this.progAll.Name = "progAll";
            this.progAll.Size = new System.Drawing.Size(785, 12);
            this.progAll.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progAll.TabIndex = 39;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(551, 344);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 22);
            this.button3.TabIndex = 38;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_lblNum,
            this.toolStrip_lblTotalNum,
            this.toolStrip_lblItemNum,
            this.toolStrip_lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 373);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(809, 22);
            this.statusStrip1.TabIndex = 40;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip_lblNum
            // 
            this.toolStrip_lblNum.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip_lblNum.Name = "toolStrip_lblNum";
            this.toolStrip_lblNum.Size = new System.Drawing.Size(54, 17);
            this.toolStrip_lblNum.Text = "0개 항목";
            // 
            // toolStrip_lblTotalNum
            // 
            this.toolStrip_lblTotalNum.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip_lblTotalNum.Name = "toolStrip_lblTotalNum";
            this.toolStrip_lblTotalNum.Size = new System.Drawing.Size(22, 17);
            this.toolStrip_lblTotalNum.Text = "*/*";
            // 
            // toolStrip_lblItemNum
            // 
            this.toolStrip_lblItemNum.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip_lblItemNum.Name = "toolStrip_lblItemNum";
            this.toolStrip_lblItemNum.Size = new System.Drawing.Size(22, 17);
            this.toolStrip_lblItemNum.Text = "*/*";
            // 
            // toolStrip_lblStatus
            // 
            this.toolStrip_lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip_lblStatus.Name = "toolStrip_lblStatus";
            this.toolStrip_lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(335, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(18, 47);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "제목";
            this.columnHeader2.Width = 415;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "주소";
            this.columnHeader3.Width = 250;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 40);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(785, 276);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(809, 395);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.progAll);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(515, 308);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "imgDownloader";
            this.Load += new System.EventHandler(this.Main_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnStart;
        private Button btnStop;
        private TextBox textBox1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem menuItem_LinkCp;
        private Button button3;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem menuItem_Remove;
        private ToolStripMenuItem menuItem_Add;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem menuItem_RemoveAll;
        private ToolStripMenuItem menuItem_Settings;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStrip_lblTotalNum;
        private ToolStripStatusLabel toolStrip_lblItemNum;
        private ToolStripStatusLabel toolStrip_lblNum;
        private ToolStripStatusLabel toolStrip_lblStatus;
        private ProgressBar progAll;
        private Button button1;
        private CheckBox checkBox1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ListView listView1;
    }
}

