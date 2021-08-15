
namespace startdemos_ui.Forms
{
    partial class DemoCheckEditorAddConfigForm
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
            this.labWarning = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.labText = new System.Windows.Forms.Label();
            this.boxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labWarning
            // 
            this.labWarning.AutoSize = true;
            this.labWarning.Location = new System.Drawing.Point(9, 60);
            this.labWarning.Name = "labWarning";
            this.labWarning.Size = new System.Drawing.Size(81, 13);
            this.labWarning.TabIndex = 7;
            this.labWarning.Text = "Duplicate entry!";
            this.labWarning.Visible = false;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(173, 55);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 6;
            this.butOK.Text = "Ok";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(10, 13);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(181, 13);
            this.labText.TabIndex = 5;
            this.labText.Text = "Please enter the name of your Config";
            // 
            // boxName
            // 
            this.boxName.Location = new System.Drawing.Point(12, 29);
            this.boxName.Name = "boxName";
            this.boxName.Size = new System.Drawing.Size(235, 20);
            this.boxName.TabIndex = 4;
            // 
            // DemoCheckEditorAddConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 89);
            this.Controls.Add(this.labWarning);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labText);
            this.Controls.Add(this.boxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DemoCheckEditorAddConfigForm";
            this.Text = "Demo Checks | Add Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labWarning;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.TextBox boxName;
    }
}