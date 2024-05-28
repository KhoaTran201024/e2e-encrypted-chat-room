namespace Lab3
{
    partial class udp
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
            this.serverBtn = new System.Windows.Forms.Button();
            this.clientBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverBtn
            // 
            this.serverBtn.Location = new System.Drawing.Point(12, 195);
            this.serverBtn.Name = "serverBtn";
            this.serverBtn.Size = new System.Drawing.Size(269, 115);
            this.serverBtn.TabIndex = 0;
            this.serverBtn.Text = "UDP Server";
            this.serverBtn.UseVisualStyleBackColor = true;
            this.serverBtn.Click += new System.EventHandler(this.serverBtn_Click);
            // 
            // clientBtn
            // 
            this.clientBtn.Location = new System.Drawing.Point(547, 195);
            this.clientBtn.Name = "clientBtn";
            this.clientBtn.Size = new System.Drawing.Size(269, 115);
            this.clientBtn.TabIndex = 1;
            this.clientBtn.Text = "UDP Client";
            this.clientBtn.UseVisualStyleBackColor = true;
            this.clientBtn.Click += new System.EventHandler(this.clientBtn_Click);
            // 
            // udp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 453);
            this.Controls.Add(this.clientBtn);
            this.Controls.Add(this.serverBtn);
            this.Font = new System.Drawing.Font("JetBrainsMono NF SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "udp";
            this.Text = "UDP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button serverBtn;
        private System.Windows.Forms.Button clientBtn;
    }
}