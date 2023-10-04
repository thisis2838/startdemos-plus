namespace startdemos_plus.Frontend
{
    partial class ErrorWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.picErrorIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.boxMessage = new System.Windows.Forms.TextBox();
            this.butReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picErrorIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.boxMessage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.butReport, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 343);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.picErrorIcon, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(516, 50);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // picErrorIcon
            // 
            this.picErrorIcon.Location = new System.Drawing.Point(3, 3);
            this.picErrorIcon.Name = "picErrorIcon";
            this.picErrorIcon.Size = new System.Drawing.Size(44, 44);
            this.picErrorIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picErrorIcon.TabIndex = 0;
            this.picErrorIcon.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(416, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "An unrecoverable error has occured that crashed startdemos+\r\nPlease copy the cont" +
    "ents below and report it to the page opened by the Report button.";
            // 
            // boxMessage
            // 
            this.boxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxMessage.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxMessage.Location = new System.Drawing.Point(3, 53);
            this.boxMessage.Multiline = true;
            this.boxMessage.Name = "boxMessage";
            this.boxMessage.ReadOnly = true;
            this.boxMessage.Size = new System.Drawing.Size(510, 257);
            this.boxMessage.TabIndex = 1;
            // 
            // butReport
            // 
            this.butReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butReport.Location = new System.Drawing.Point(438, 316);
            this.butReport.Name = "butReport";
            this.butReport.Size = new System.Drawing.Size(75, 23);
            this.butReport.TabIndex = 2;
            this.butReport.Text = "Report";
            this.butReport.UseVisualStyleBackColor = true;
            this.butReport.Click += new System.EventHandler(this.butReport_Click);
            // 
            // ErrorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "ErrorWindow";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.Text = "startdemos+ | Error";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picErrorIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox picErrorIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox boxMessage;
        private System.Windows.Forms.Button butReport;
    }
}