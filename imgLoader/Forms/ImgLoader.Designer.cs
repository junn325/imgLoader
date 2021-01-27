
namespace imgLoader.Forms
{
    partial class ImgLoader
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
            this.loaderList1 = new imgLoader.LoaderList.LoaderList();
            this.tmp = new imgLoader.LoaderList.LoaderItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.loaderList1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loaderList1
            // 
            this.loaderList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaderList1.AutoScroll = true;
            this.loaderList1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loaderList1.Controls.Add(this.tmp);
            this.loaderList1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.loaderList1.Location = new System.Drawing.Point(12, 61);
            this.loaderList1.Name = "loaderList1";
            this.loaderList1.Size = new System.Drawing.Size(776, 310);
            this.loaderList1.TabIndex = 0;
            this.loaderList1.WrapContents = false;
            // 
            // tmp
            // 
            this.tmp.Author = "imgL";
            this.tmp.BackColor = System.Drawing.Color.White;
            this.tmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tmp.ImgCount = "9999장장";
            this.tmp.Location = new System.Drawing.Point(3, 3);
            this.tmp.Name = "tmp";
            this.tmp.Route = "(경로)";
            this.tmp.SiteName = "hiyobi";
            this.tmp.Size = new System.Drawing.Size(619, 53);
            this.tmp.TabIndex = 0;
            this.tmp.Title = "테스트";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(707, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(620, 377);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 24);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(533, 377);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 24);
            this.button3.TabIndex = 1;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // ImgLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loaderList1);
            this.Name = "ImgLoader";
            this.Text = "ImgLoader";
            this.Load += new System.EventHandler(this.ImgLoader_Load);
            this.loaderList1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LoaderList.LoaderList loaderList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private LoaderList.LoaderItem tmp;
        private System.Windows.Forms.Label label1;
    }
}