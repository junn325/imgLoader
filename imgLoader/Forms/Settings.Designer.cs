
namespace imgLoader.Forms
{
    partial class Settings
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
            this.txtRoute = new System.Windows.Forms.TextBox();
            this.chkAuthor = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkBookTitle = new System.Windows.Forms.CheckBox();
            this.chkMainAlways = new System.Windows.Forms.CheckBox();
            this.chkExist = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRoute
            // 
            this.txtRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRoute.Location = new System.Drawing.Point(6, 32);
            this.txtRoute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRoute.Name = "txtRoute";
            this.txtRoute.Size = new System.Drawing.Size(756, 23);
            this.txtRoute.TabIndex = 27;
            this.txtRoute.Text = "더블클릭하여 다운로드 경로를 선택하거나 직접 입력";
            this.txtRoute.Click += new System.EventHandler(this.txtRoute_Click);
            this.txtRoute.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtRoute_MouseDoubleClick);
            // 
            // chkAuthor
            // 
            this.chkAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAuthor.AutoSize = true;
            this.chkAuthor.Checked = true;
            this.chkAuthor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuthor.Location = new System.Drawing.Point(6, 59);
            this.chkAuthor.Name = "chkAuthor";
            this.chkAuthor.Size = new System.Drawing.Size(158, 19);
            this.chkAuthor.TabIndex = 29;
            this.chkAuthor.Text = "폴더명에 작가 이름 표시";
            this.chkAuthor.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 368);
            this.tabControl1.TabIndex = 32;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkBookTitle);
            this.tabPage1.Controls.Add(this.chkMainAlways);
            this.tabPage1.Controls.Add(this.chkExist);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.chkAuthor);
            this.tabPage1.Controls.Add(this.txtRoute);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(768, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "일반";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkBookTitle
            // 
            this.chkBookTitle.AutoSize = true;
            this.chkBookTitle.Location = new System.Drawing.Point(354, 84);
            this.chkBookTitle.Name = "chkBookTitle";
            this.chkBookTitle.Size = new System.Drawing.Size(338, 19);
            this.chkBookTitle.TabIndex = 34;
            this.chkBookTitle.Text = "북마크에서 로드한 항목은 북마크 제목으로 폴더명을 정함";
            this.chkBookTitle.UseVisualStyleBackColor = true;
            // 
            // chkMainAlways
            // 
            this.chkMainAlways.AutoSize = true;
            this.chkMainAlways.Location = new System.Drawing.Point(354, 59);
            this.chkMainAlways.Name = "chkMainAlways";
            this.chkMainAlways.Size = new System.Drawing.Size(222, 19);
            this.chkMainAlways.TabIndex = 33;
            this.chkMainAlways.Text = "항목을 추가할 때마다 자동으로 선택";
            this.chkMainAlways.UseVisualStyleBackColor = true;
            // 
            // chkExist
            // 
            this.chkExist.AutoSize = true;
            this.chkExist.Checked = true;
            this.chkExist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExist.Location = new System.Drawing.Point(6, 84);
            this.chkExist.Name = "chkExist";
            this.chkExist.Size = new System.Drawing.Size(262, 19);
            this.chkExist.TabIndex = 32;
            this.chkExist.Text = "이미 존재하는 이미지를 항상 다시 다운로드";
            this.chkExist.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "다운로드할 경로";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(659, 387);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(125, 22);
            this.btnApply.TabIndex = 33;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 418);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "설정";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRoute;
        private System.Windows.Forms.CheckBox chkAuthor;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExist;
        private System.Windows.Forms.CheckBox chkMainAlways;
        private System.Windows.Forms.CheckBox chkBookTitle;
    }
}