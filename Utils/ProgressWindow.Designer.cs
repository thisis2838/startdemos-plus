namespace startdemos_plus.Utils
{
    partial class ProgressWindow
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
            this.labCurAction = new System.Windows.Forms.Label();
            this.labProgress = new System.Windows.Forms.Label();
            this.labAction = new System.Windows.Forms.Label();
            this.progProgress = new System.Windows.Forms.ProgressBar();
            this.labTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.labCurAction, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labProgress, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labAction, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.progProgress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labTime, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 76);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labCurAction
            // 
            this.labCurAction.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCurAction.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labCurAction, 2);
            this.labCurAction.Location = new System.Drawing.Point(95, 56);
            this.labCurAction.Name = "labCurAction";
            this.labCurAction.Size = new System.Drawing.Size(35, 13);
            this.labCurAction.TabIndex = 3;
            this.labCurAction.Text = "label1";
            // 
            // labProgress
            // 
            this.labProgress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labProgress.AutoSize = true;
            this.labProgress.Location = new System.Drawing.Point(3, 56);
            this.labProgress.Name = "labProgress";
            this.labProgress.Size = new System.Drawing.Size(35, 13);
            this.labProgress.TabIndex = 2;
            this.labProgress.Text = "label1";
            // 
            // labAction
            // 
            this.labAction.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labAction.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labAction, 2);
            this.labAction.Location = new System.Drawing.Point(3, 6);
            this.labAction.Name = "labAction";
            this.labAction.Size = new System.Drawing.Size(35, 13);
            this.labAction.TabIndex = 0;
            this.labAction.Text = "label1";
            // 
            // progProgress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progProgress, 3);
            this.progProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progProgress.Location = new System.Drawing.Point(3, 28);
            this.progProgress.Maximum = 0;
            this.progProgress.Name = "progProgress";
            this.progProgress.Size = new System.Drawing.Size(520, 19);
            this.progProgress.TabIndex = 1;
            // 
            // labTime
            // 
            this.labTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(488, 6);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(35, 13);
            this.labTime.TabIndex = 4;
            this.labTime.Text = "label1";
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 94);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ProgressWindow";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.Text = "startdemos+";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labCurAction;
        private System.Windows.Forms.Label labProgress;
        private System.Windows.Forms.Label labAction;
        private System.Windows.Forms.ProgressBar progProgress;
        private System.Windows.Forms.Label labTime;
    }
}