
namespace startdemos_ui.Forms
{
    partial class DemoPlayingForm
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
            this.gPlayOptions = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.boxWaitTime = new System.Windows.Forms.NumericUpDown();
            this.chkAutoNext = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.boxCommands = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.boxPlayOrder = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.boxEvalDemoOrder = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labCount = new System.Windows.Forms.Label();
            this.labCurDemo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labPrevDemo = new System.Windows.Forms.Label();
            this.labNextDemo = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butPlayPrev = new System.Windows.Forms.Button();
            this.butPlayNext = new System.Windows.Forms.Button();
            this.butPlayStop = new System.Windows.Forms.Button();
            this.gPlayOptions.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxWaitTime)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPlayOptions
            // 
            this.gPlayOptions.Controls.Add(this.groupBox7);
            this.gPlayOptions.Controls.Add(this.groupBox2);
            this.gPlayOptions.Controls.Add(this.tableLayoutPanel1);
            this.gPlayOptions.Location = new System.Drawing.Point(12, 12);
            this.gPlayOptions.Name = "gPlayOptions";
            this.gPlayOptions.Size = new System.Drawing.Size(743, 146);
            this.gPlayOptions.TabIndex = 0;
            this.gPlayOptions.TabStop = false;
            this.gPlayOptions.Text = "Play Options";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel3);
            this.groupBox7.Location = new System.Drawing.Point(373, 51);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(364, 87);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Settings";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 631F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.boxWaitTime, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkAutoNext, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(352, 50);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Auto-play Next";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Wait Time";
            // 
            // boxWaitTime
            // 
            this.boxWaitTime.Location = new System.Drawing.Point(103, 3);
            this.boxWaitTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.boxWaitTime.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.boxWaitTime.Name = "boxWaitTime";
            this.boxWaitTime.Size = new System.Drawing.Size(100, 20);
            this.boxWaitTime.TabIndex = 2;
            this.boxWaitTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chkAutoNext
            // 
            this.chkAutoNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAutoNext.AutoSize = true;
            this.chkAutoNext.Checked = true;
            this.chkAutoNext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoNext.Location = new System.Drawing.Point(103, 30);
            this.chkAutoNext.Name = "chkAutoNext";
            this.chkAutoNext.Size = new System.Drawing.Size(15, 14);
            this.chkAutoNext.TabIndex = 3;
            this.chkAutoNext.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.boxCommands);
            this.groupBox2.Location = new System.Drawing.Point(6, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(361, 87);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands executed per Demo Start";
            // 
            // boxCommands
            // 
            this.boxCommands.Location = new System.Drawing.Point(7, 20);
            this.boxCommands.Multiline = true;
            this.boxCommands.Name = "boxCommands";
            this.boxCommands.Size = new System.Drawing.Size(348, 60);
            this.boxCommands.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 631F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.boxPlayOrder, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 25);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Play Order";
            // 
            // boxPlayOrder
            // 
            this.boxPlayOrder.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.boxPlayOrder.Location = new System.Drawing.Point(103, 3);
            this.boxPlayOrder.Name = "boxPlayOrder";
            this.boxPlayOrder.Size = new System.Drawing.Size(625, 21);
            this.boxPlayOrder.TabIndex = 1;
            this.boxPlayOrder.Text = "-";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(12, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(743, 223);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Play";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.boxEvalDemoOrder);
            this.groupBox6.Location = new System.Drawing.Point(376, 91);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(361, 126);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Evaluated Demo Order";
            // 
            // boxEvalDemoOrder
            // 
            this.boxEvalDemoOrder.Location = new System.Drawing.Point(6, 19);
            this.boxEvalDemoOrder.Multiline = true;
            this.boxEvalDemoOrder.Name = "boxEvalDemoOrder";
            this.boxEvalDemoOrder.ReadOnly = true;
            this.boxEvalDemoOrder.Size = new System.Drawing.Size(349, 100);
            this.boxEvalDemoOrder.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel2);
            this.groupBox5.Location = new System.Drawing.Point(7, 91);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(361, 126);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Queue Information";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 541F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labCount, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labCurDemo, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labPrevDemo, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labNextDemo, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(350, 100);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Serving";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Currently Playing";
            // 
            // labCount
            // 
            this.labCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCount.AutoSize = true;
            this.labCount.Location = new System.Drawing.Point(103, 6);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(0, 13);
            this.labCount.TabIndex = 5;
            // 
            // labCurDemo
            // 
            this.labCurDemo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCurDemo.AutoSize = true;
            this.labCurDemo.Location = new System.Drawing.Point(103, 31);
            this.labCurDemo.Name = "labCurDemo";
            this.labCurDemo.Size = new System.Drawing.Size(0, 13);
            this.labCurDemo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Previously Played";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Next in Queue";
            // 
            // labPrevDemo
            // 
            this.labPrevDemo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labPrevDemo.AutoSize = true;
            this.labPrevDemo.Location = new System.Drawing.Point(103, 81);
            this.labPrevDemo.Name = "labPrevDemo";
            this.labPrevDemo.Size = new System.Drawing.Size(0, 13);
            this.labPrevDemo.TabIndex = 7;
            // 
            // labNextDemo
            // 
            this.labNextDemo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labNextDemo.AutoSize = true;
            this.labNextDemo.Location = new System.Drawing.Point(103, 56);
            this.labNextDemo.Name = "labNextDemo";
            this.labNextDemo.Size = new System.Drawing.Size(0, 13);
            this.labNextDemo.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.butPlayPrev);
            this.groupBox4.Controls.Add(this.butPlayNext);
            this.groupBox4.Controls.Add(this.butPlayStop);
            this.groupBox4.Location = new System.Drawing.Point(7, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(730, 65);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Transport Controls";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(141, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(447, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "WARNING: Only press these while the Game isn\'t loading a demo, or else a crash mi" +
    "ght occur!!";
            // 
            // butPlayPrev
            // 
            this.butPlayPrev.Location = new System.Drawing.Point(253, 19);
            this.butPlayPrev.Name = "butPlayPrev";
            this.butPlayPrev.Size = new System.Drawing.Size(75, 23);
            this.butPlayPrev.TabIndex = 2;
            this.butPlayPrev.Text = "Previous";
            this.butPlayPrev.UseVisualStyleBackColor = true;
            this.butPlayPrev.Click += new System.EventHandler(this.butPlayPrev_Click);
            // 
            // butPlayNext
            // 
            this.butPlayNext.Location = new System.Drawing.Point(415, 19);
            this.butPlayNext.Name = "butPlayNext";
            this.butPlayNext.Size = new System.Drawing.Size(75, 23);
            this.butPlayNext.TabIndex = 1;
            this.butPlayNext.Text = "Next";
            this.butPlayNext.UseVisualStyleBackColor = true;
            this.butPlayNext.Click += new System.EventHandler(this.butPlayNext_Click);
            // 
            // butPlayStop
            // 
            this.butPlayStop.Location = new System.Drawing.Point(334, 19);
            this.butPlayStop.Name = "butPlayStop";
            this.butPlayStop.Size = new System.Drawing.Size(75, 23);
            this.butPlayStop.TabIndex = 0;
            this.butPlayStop.Text = "Play";
            this.butPlayStop.UseVisualStyleBackColor = true;
            this.butPlayStop.Click += new System.EventHandler(this.butPlayStop_Click);
            // 
            // DemoPlayingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(767, 399);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gPlayOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DemoPlayingForm";
            this.Text = "DemoPlayingForm";
            this.gPlayOptions.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxWaitTime)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gPlayOptions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox boxPlayOrder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox boxCommands;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button butPlayPrev;
        private System.Windows.Forms.Button butPlayNext;
        private System.Windows.Forms.Button butPlayStop;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox boxEvalDemoOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Label labCurDemo;
        private System.Windows.Forms.Label labPrevDemo;
        private System.Windows.Forms.Label labNextDemo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown boxWaitTime;
        private System.Windows.Forms.CheckBox chkAutoNext;
        private System.Windows.Forms.Label label6;
    }
}