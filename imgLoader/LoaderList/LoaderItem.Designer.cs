
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
            this.siteName = new System.Windows.Forms.Label();
            this.imgCount = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.author = new System.Windows.Forms.Label();
            this.route = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // siteName
            // 
            this.siteName.AutoSize = true;
            this.siteName.Location = new System.Drawing.Point(6, 34);
            this.siteName.Name = "siteName";
            this.siteName.Size = new System.Drawing.Size(42, 15);
            this.siteName.TabIndex = 0;
            this.siteName.Text = "Hiyobi";
            // 
            // imgCount
            // 
            this.imgCount.AutoSize = true;
            this.imgCount.Location = new System.Drawing.Point(55, 34);
            this.imgCount.Name = "imgCount";
            this.imgCount.Size = new System.Drawing.Size(33, 15);
            this.imgCount.TabIndex = 0;
            this.imgCount.Text = "69장";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(6, 4);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(39, 15);
            this.title.TabIndex = 1;
            this.title.Text = "(제목)";
            // 
            // author
            // 
            this.author.AutoSize = true;
            this.author.Location = new System.Drawing.Point(6, 19);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(39, 15);
            this.author.TabIndex = 1;
            this.author.Text = "(작가)";
            // 
            // route
            // 
            this.route.AutoSize = true;
            this.route.Location = new System.Drawing.Point(98, 34);
            this.route.Name = "route";
            this.route.Size = new System.Drawing.Size(39, 15);
            this.route.TabIndex = 2;
            this.route.Text = "(경로)";
            // 
            // LoaderItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.route);
            this.Controls.Add(this.author);
            this.Controls.Add(this.title);
            this.Controls.Add(this.imgCount);
            this.Controls.Add(this.siteName);
            this.DoubleBuffered = true;
            this.Name = "LoaderItem";
            this.Size = new System.Drawing.Size(514, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label siteName;
        private System.Windows.Forms.Label imgCount;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label author;
        private System.Windows.Forms.Label route;
    }
}
