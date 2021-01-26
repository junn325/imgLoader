
namespace imgLoader.LoaderList
{
    partial class LoaderItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.siteName = new System.Windows.Forms.Label();
            this.imgCount = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.author = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "제목 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "작가 :";
            // 
            // siteName
            // 
            this.siteName.AutoSize = true;
            this.siteName.Location = new System.Drawing.Point(58, 30);
            this.siteName.Name = "siteName";
            this.siteName.Size = new System.Drawing.Size(42, 15);
            this.siteName.TabIndex = 0;
            this.siteName.Text = "Hiyobi";
            // 
            // imgCount
            // 
            this.imgCount.AutoSize = true;
            this.imgCount.Location = new System.Drawing.Point(106, 30);
            this.imgCount.Name = "imgCount";
            this.imgCount.Size = new System.Drawing.Size(33, 15);
            this.imgCount.TabIndex = 0;
            this.imgCount.Text = "69장";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(100, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(39, 15);
            this.title.TabIndex = 1;
            this.title.Text = "label5";
            // 
            // author
            // 
            this.author.AutoSize = true;
            this.author.Location = new System.Drawing.Point(100, 15);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(39, 15);
            this.author.TabIndex = 1;
            this.author.Text = "label5";
            // 
            // LoaderItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.author);
            this.Controls.Add(this.title);
            this.Controls.Add(this.imgCount);
            this.Controls.Add(this.siteName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "LoaderItem";
            this.Size = new System.Drawing.Size(438, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label siteName;
        private System.Windows.Forms.Label imgCount;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label author;
    }
}
