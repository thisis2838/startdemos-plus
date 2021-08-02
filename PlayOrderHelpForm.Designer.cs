
namespace startdemos_plus
{
    partial class PlayOrderHelpForm
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabFormatting = new System.Windows.Forms.TabPage();
            this.tabExamples = new System.Windows.Forms.TabPage();
            this.display = new System.Windows.Forms.WebBrowser();
            this.example = new System.Windows.Forms.WebBrowser();
            this.tabMain.SuspendLayout();
            this.tabFormatting.SuspendLayout();
            this.tabExamples.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabFormatting);
            this.tabMain.Controls.Add(this.tabExamples);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(776, 426);
            this.tabMain.TabIndex = 0;
            // 
            // tabFormatting
            // 
            this.tabFormatting.Controls.Add(this.display);
            this.tabFormatting.Location = new System.Drawing.Point(4, 22);
            this.tabFormatting.Name = "tabFormatting";
            this.tabFormatting.Padding = new System.Windows.Forms.Padding(3);
            this.tabFormatting.Size = new System.Drawing.Size(768, 400);
            this.tabFormatting.TabIndex = 0;
            this.tabFormatting.Text = "Formatting";
            this.tabFormatting.UseVisualStyleBackColor = true;
            // 
            // tabExamples
            // 
            this.tabExamples.Controls.Add(this.example);
            this.tabExamples.Location = new System.Drawing.Point(4, 22);
            this.tabExamples.Name = "tabExamples";
            this.tabExamples.Padding = new System.Windows.Forms.Padding(3);
            this.tabExamples.Size = new System.Drawing.Size(768, 400);
            this.tabExamples.TabIndex = 1;
            this.tabExamples.Text = "Examples";
            this.tabExamples.UseVisualStyleBackColor = true;
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(3, 3);
            this.display.MinimumSize = new System.Drawing.Size(20, 20);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(759, 391);
            this.display.TabIndex = 3;
            // 
            // example
            // 
            this.example.Location = new System.Drawing.Point(3, 3);
            this.example.MinimumSize = new System.Drawing.Size(20, 20);
            this.example.Name = "example";
            this.example.Size = new System.Drawing.Size(759, 391);
            this.example.TabIndex = 4;
            // 
            // PlayOrderHelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PlayOrderHelpForm";
            this.Text = "Play Order Formatting Help";
            this.tabMain.ResumeLayout(false);
            this.tabFormatting.ResumeLayout(false);
            this.tabExamples.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabFormatting;
        private System.Windows.Forms.TabPage tabExamples;
        private System.Windows.Forms.WebBrowser display;
        private System.Windows.Forms.WebBrowser example;
    }
}