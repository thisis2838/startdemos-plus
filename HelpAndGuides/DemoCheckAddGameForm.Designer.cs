
namespace HelpAndGuides
{
    partial class DemoCheckAddGameForm
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
            this.boxName = new System.Windows.Forms.TextBox();
            this.labText = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.labWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // boxName
            // 
            this.boxName.Location = new System.Drawing.Point(11, 25);
            this.boxName.Name = "boxName";
            this.boxName.Size = new System.Drawing.Size(235, 20);
            this.boxName.TabIndex = 0;
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(9, 9);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(246, 13);
            this.labText.TabIndex = 1;
            this.labText.Text = "Please enter the your game\'s Directory Name here:";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(172, 51);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "Ok";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // labWarning
            // 
            this.labWarning.AutoSize = true;
            this.labWarning.Location = new System.Drawing.Point(8, 56);
            this.labWarning.Name = "labWarning";
            this.labWarning.Size = new System.Drawing.Size(81, 13);
            this.labWarning.TabIndex = 3;
            this.labWarning.Text = "Duplicate entry!";
            this.labWarning.Visible = false;
            // 
            // DemoCheckAddGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 81);
            this.Controls.Add(this.labWarning);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labText);
            this.Controls.Add(this.boxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DemoCheckAddGameForm";
            this.Text = "Demo Checking | Add a Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox boxName;
        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Label labWarning;
    }
}