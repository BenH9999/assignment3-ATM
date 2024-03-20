namespace assignment3_ATM
{
    partial class ATMForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCurrentAccount = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblInstruction2 = new System.Windows.Forms.Label();
            this.lblInstruction3 = new System.Windows.Forms.Label();
            this.lblInstruction4 = new System.Windows.Forms.Label();
            this.lblExtra = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCurrentAccount
            // 
            this.lblCurrentAccount.AutoSize = true;
            this.lblCurrentAccount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentAccount.Location = new System.Drawing.Point(487, 9);
            this.lblCurrentAccount.Name = "lblCurrentAccount";
            this.lblCurrentAccount.Size = new System.Drawing.Size(126, 21);
            this.lblCurrentAccount.TabIndex = 0;
            this.lblCurrentAccount.Text = "Current Account:";
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstruction.Location = new System.Drawing.Point(463, 174);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(18, 21);
            this.lblInstruction.TabIndex = 1;
            this.lblInstruction.Text = "a";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInput.Location = new System.Drawing.Point(131, 39);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(18, 21);
            this.lblInput.TabIndex = 2;
            this.lblInput.Text = "a";
            // 
            // lblInstruction2
            // 
            this.lblInstruction2.AutoSize = true;
            this.lblInstruction2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstruction2.Location = new System.Drawing.Point(463, 215);
            this.lblInstruction2.Name = "lblInstruction2";
            this.lblInstruction2.Size = new System.Drawing.Size(18, 21);
            this.lblInstruction2.TabIndex = 3;
            this.lblInstruction2.Text = "a";
            // 
            // lblInstruction3
            // 
            this.lblInstruction3.AutoSize = true;
            this.lblInstruction3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstruction3.Location = new System.Drawing.Point(463, 236);
            this.lblInstruction3.Name = "lblInstruction3";
            this.lblInstruction3.Size = new System.Drawing.Size(18, 21);
            this.lblInstruction3.TabIndex = 4;
            this.lblInstruction3.Text = "a";
            // 
            // lblInstruction4
            // 
            this.lblInstruction4.AutoSize = true;
            this.lblInstruction4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstruction4.Location = new System.Drawing.Point(463, 257);
            this.lblInstruction4.Name = "lblInstruction4";
            this.lblInstruction4.Size = new System.Drawing.Size(18, 21);
            this.lblInstruction4.TabIndex = 5;
            this.lblInstruction4.Text = "a";
            // 
            // lblExtra
            // 
            this.lblExtra.AutoSize = true;
            this.lblExtra.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblExtra.Location = new System.Drawing.Point(463, 133);
            this.lblExtra.Name = "lblExtra";
            this.lblExtra.Size = new System.Drawing.Size(18, 21);
            this.lblExtra.TabIndex = 6;
            this.lblExtra.Text = "a";
            // 
            // ATMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblExtra);
            this.Controls.Add(this.lblInstruction4);
            this.Controls.Add(this.lblInstruction3);
            this.Controls.Add(this.lblInstruction2);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.lblCurrentAccount);
            this.Name = "ATMForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblCurrentAccount;
        private Label lblInstruction;
        private Label lblInput;
        private Label lblInstruction2;
        private Label lblInstruction3;
        private Label lblInstruction4;
        private Label lblExtra;
    }
}