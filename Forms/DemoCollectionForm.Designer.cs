
namespace startdemos_ui.Forms
{
    partial class DemoCollectionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butDemoPathBrowse = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.boxDemoPath = new System.Windows.Forms.TextBox();
            this.boxTickRate = new System.Windows.Forms.NumericUpDown();
            this.chk0thTick = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.boxIndexOrder = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butProcess = new System.Windows.Forms.Button();
            this.butOpenDemoList = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labFoundDemosCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labProcessedDemosCount = new System.Windows.Forms.Label();
            this.labProcessing = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gCustomMapOrder = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.butCustomMapOrderBrowse = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.boxCustomMapOrderPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxTickRate)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gCustomMapOrder.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butDemoPathBrowse);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Demo Path";
            // 
            // butDemoPathBrowse
            // 
            this.butDemoPathBrowse.Location = new System.Drawing.Point(653, 20);
            this.butDemoPathBrowse.Name = "butDemoPathBrowse";
            this.butDemoPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.butDemoPathBrowse.TabIndex = 2;
            this.butDemoPathBrowse.Text = "Browse";
            this.butDemoPathBrowse.UseVisualStyleBackColor = true;
            this.butDemoPathBrowse.Click += new System.EventHandler(this.butDemoPathBrowse_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 541F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.boxDemoPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.boxTickRate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chk0thTick, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(641, 75);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "0th Tick";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Tickrate";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path";
            // 
            // boxDemoPath
            // 
            this.boxDemoPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxDemoPath.Location = new System.Drawing.Point(103, 3);
            this.boxDemoPath.Name = "boxDemoPath";
            this.boxDemoPath.Size = new System.Drawing.Size(535, 20);
            this.boxDemoPath.TabIndex = 2;
            this.boxDemoPath.TextChanged += new System.EventHandler(this.boxDemoPath_TextChanged);
            // 
            // boxTickRate
            // 
            this.boxTickRate.DecimalPlaces = 7;
            this.boxTickRate.Location = new System.Drawing.Point(103, 28);
            this.boxTickRate.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.boxTickRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.boxTickRate.Name = "boxTickRate";
            this.boxTickRate.Size = new System.Drawing.Size(100, 20);
            this.boxTickRate.TabIndex = 4;
            this.boxTickRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // chk0thTick
            // 
            this.chk0thTick.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chk0thTick.AutoSize = true;
            this.chk0thTick.Location = new System.Drawing.Point(103, 55);
            this.chk0thTick.Name = "chk0thTick";
            this.chk0thTick.Size = new System.Drawing.Size(15, 14);
            this.chk0thTick.TabIndex = 6;
            this.chk0thTick.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Order Demos by";
            // 
            // boxIndexOrder
            // 
            this.boxIndexOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxIndexOrder.FormattingEnabled = true;
            this.boxIndexOrder.Items.AddRange(new object[] {
            "Last Modified Date (from oldest to earliest)",
            "Demo File Name",
            "Demo Map Name",
            "Custom Map Order"});
            this.boxIndexOrder.Location = new System.Drawing.Point(103, 3);
            this.boxIndexOrder.Name = "boxIndexOrder";
            this.boxIndexOrder.Size = new System.Drawing.Size(259, 21);
            this.boxIndexOrder.TabIndex = 8;
            this.boxIndexOrder.SelectedIndexChanged += new System.EventHandler(this.boxIndexOrder_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.butProcess);
            this.groupBox2.Controls.Add(this.butOpenDemoList);
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(12, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 131);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Demo List";
            // 
            // butProcess
            // 
            this.butProcess.Location = new System.Drawing.Point(6, 100);
            this.butProcess.Name = "butProcess";
            this.butProcess.Size = new System.Drawing.Size(362, 23);
            this.butProcess.TabIndex = 4;
            this.butProcess.Text = "Process Demos";
            this.butProcess.UseVisualStyleBackColor = true;
            this.butProcess.Click += new System.EventHandler(this.butProcess_Click);
            // 
            // butOpenDemoList
            // 
            this.butOpenDemoList.Location = new System.Drawing.Point(374, 100);
            this.butOpenDemoList.Name = "butOpenDemoList";
            this.butOpenDemoList.Size = new System.Drawing.Size(362, 23);
            this.butOpenDemoList.TabIndex = 3;
            this.butOpenDemoList.Text = "Open Demo List";
            this.butOpenDemoList.UseVisualStyleBackColor = true;
            this.butOpenDemoList.Click += new System.EventHandler(this.butOpenDemoList_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 541F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labFoundDemosCount, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labProcessedDemosCount, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labProcessing, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(730, 75);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Processing";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Processed Demos";
            // 
            // labFoundDemosCount
            // 
            this.labFoundDemosCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFoundDemosCount.AutoSize = true;
            this.labFoundDemosCount.Location = new System.Drawing.Point(103, 6);
            this.labFoundDemosCount.Name = "labFoundDemosCount";
            this.labFoundDemosCount.Size = new System.Drawing.Size(0, 13);
            this.labFoundDemosCount.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Found Demos";
            // 
            // labProcessedDemosCount
            // 
            this.labProcessedDemosCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labProcessedDemosCount.AutoSize = true;
            this.labProcessedDemosCount.Location = new System.Drawing.Point(103, 31);
            this.labProcessedDemosCount.Name = "labProcessedDemosCount";
            this.labProcessedDemosCount.Size = new System.Drawing.Size(0, 13);
            this.labProcessedDemosCount.TabIndex = 3;
            // 
            // labProcessing
            // 
            this.labProcessing.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labProcessing.AutoSize = true;
            this.labProcessing.Location = new System.Drawing.Point(103, 56);
            this.labProcessing.Name = "labProcessing";
            this.labProcessing.Size = new System.Drawing.Size(0, 13);
            this.labProcessing.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gCustomMapOrder);
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(12, 119);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(743, 125);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Demo Indexing";
            // 
            // gCustomMapOrder
            // 
            this.gCustomMapOrder.Controls.Add(this.label9);
            this.gCustomMapOrder.Controls.Add(this.butCustomMapOrderBrowse);
            this.gCustomMapOrder.Controls.Add(this.tableLayoutPanel4);
            this.gCustomMapOrder.Location = new System.Drawing.Point(7, 52);
            this.gCustomMapOrder.Name = "gCustomMapOrder";
            this.gCustomMapOrder.Size = new System.Drawing.Size(730, 66);
            this.gCustomMapOrder.TabIndex = 1;
            this.gCustomMapOrder.TabStop = false;
            this.gCustomMapOrder.Text = "Custom Map Order";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(6, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(322, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Map names are entered line-by-line. Press Show Help for more Info!";
            // 
            // butCustomMapOrderBrowse
            // 
            this.butCustomMapOrderBrowse.Location = new System.Drawing.Point(646, 20);
            this.butCustomMapOrderBrowse.Name = "butCustomMapOrderBrowse";
            this.butCustomMapOrderBrowse.Size = new System.Drawing.Size(75, 23);
            this.butCustomMapOrderBrowse.TabIndex = 3;
            this.butCustomMapOrderBrowse.Text = "Browse";
            this.butCustomMapOrderBrowse.UseVisualStyleBackColor = true;
            this.butCustomMapOrderBrowse.Click += new System.EventHandler(this.butCustomMapOrderBrowse_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 548F));
            this.tableLayoutPanel4.Controls.Add(this.boxCustomMapOrderPath, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(634, 25);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // boxCustomMapOrderPath
            // 
            this.boxCustomMapOrderPath.Location = new System.Drawing.Point(96, 3);
            this.boxCustomMapOrderPath.Name = "boxCustomMapOrderPath";
            this.boxCustomMapOrderPath.Size = new System.Drawing.Size(535, 20);
            this.boxCustomMapOrderPath.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Input List";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 541F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.boxIndexOrder, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(641, 25);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // DemoCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(767, 399);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DemoCollectionForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxTickRate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.gCustomMapOrder.ResumeLayout(false);
            this.gCustomMapOrder.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox boxDemoPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labProcessedDemosCount;
        private System.Windows.Forms.Label labProcessing;
        private System.Windows.Forms.Button butDemoPathBrowse;
        private System.Windows.Forms.Button butOpenDemoList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckBox chk0thTick;
        private System.Windows.Forms.Button butProcess;
        public System.Windows.Forms.Label labFoundDemosCount;
        public System.Windows.Forms.NumericUpDown boxTickRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gCustomMapOrder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button butCustomMapOrderBrowse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox boxIndexOrder;
        private System.Windows.Forms.TextBox boxCustomMapOrderPath;
    }
}