
namespace startdemos_plus
{
    partial class DemoListForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.demoListBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.eventsBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 425);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.demoListBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(767, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Demo Listings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // demoListBox
            // 
            this.demoListBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.demoListBox.Location = new System.Drawing.Point(7, 7);
            this.demoListBox.Multiline = true;
            this.demoListBox.Name = "demoListBox";
            this.demoListBox.ReadOnly = true;
            this.demoListBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.demoListBox.Size = new System.Drawing.Size(754, 386);
            this.demoListBox.TabIndex = 0;
            this.demoListBox.Text = "PLEASE WAIT...";
            this.demoListBox.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.eventsBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(767, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Recorded Events";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // eventsBox
            // 
            this.eventsBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventsBox.Location = new System.Drawing.Point(6, 6);
            this.eventsBox.Multiline = true;
            this.eventsBox.Name = "eventsBox";
            this.eventsBox.ReadOnly = true;
            this.eventsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.eventsBox.Size = new System.Drawing.Size(754, 386);
            this.eventsBox.TabIndex = 1;
            this.eventsBox.Text = "PLEASE WAIT...";
            this.eventsBox.WordWrap = false;
            // 
            // DemoListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DemoListForm";
            this.Text = "Demo List";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TextBox demoListBox;
        public System.Windows.Forms.TextBox eventsBox;
    }
}