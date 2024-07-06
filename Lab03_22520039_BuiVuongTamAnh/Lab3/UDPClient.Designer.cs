namespace Lab3
{
    partial class UDPClient
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
            this.ipText = new System.Windows.Forms.TextBox();
            this.portText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.msgText = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(890, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // ipText
            // 
            this.ipText.Location = new System.Drawing.Point(120, 70);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(553, 30);
            this.ipText.TabIndex = 2;
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(1010, 70);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(158, 30);
            this.portText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Message";
            // 
            // msgText
            // 
            this.msgText.Location = new System.Drawing.Point(43, 233);
            this.msgText.Name = "msgText";
            this.msgText.Size = new System.Drawing.Size(1148, 327);
            this.msgText.TabIndex = 5;
            this.msgText.Text = "";
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(883, 598);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(285, 64);
            this.sendBtn.TabIndex = 6;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(120, 598);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(285, 64);
            this.clearBtn.TabIndex = 7;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // UDPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 694);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.msgText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.ipText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UDPClient";
            this.Text = "UDP Client";
            this.Load += new System.EventHandler(this.UDPClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipText;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox msgText;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button clearBtn;
    }
}