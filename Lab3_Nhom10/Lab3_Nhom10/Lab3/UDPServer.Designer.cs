namespace Lab3
{
    partial class UDPServer
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.listBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Logs";
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(177, 37);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(139, 44);
            this.portText.TabIndex = 2;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(65, 184);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(1139, 487);
            this.logBox.TabIndex = 3;
            this.logBox.Text = "";
            // 
            // listBtn
            // 
            this.listBtn.Location = new System.Drawing.Point(958, 51);
            this.listBtn.Name = "listBtn";
            this.listBtn.Size = new System.Drawing.Size(245, 100);
            this.listBtn.TabIndex = 4;
            this.listBtn.Text = "Listen";
            this.listBtn.UseVisualStyleBackColor = true;
            this.listBtn.Click += new System.EventHandler(this.listBtn_Click);
            // 
            // UDPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 694);
            this.Controls.Add(this.listBtn);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("JetBrainsMono NF SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "UDPServer";
            this.Text = "UDP Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Button listBtn;
    }
}