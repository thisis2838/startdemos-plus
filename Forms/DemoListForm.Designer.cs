
namespace startdemos_ui.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoListForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDemoChecks = new System.Windows.Forms.TabPage();
            this.gTotals = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.labTotalAdjustedTime = new System.Windows.Forms.Label();
            this.labTotalTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDemoList = new System.Windows.Forms.DataGridView();
            this.dgvDemoListIndicies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListDemoNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListLastModifiedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListMapNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListTickCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListAdjustedTickCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListPlayerNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoListEventsCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDemoCheckResults = new System.Windows.Forms.TabPage();
            this.dgvDemoCheckResults = new System.Windows.Forms.DataGridView();
            this.dgvDemoCheckResultsDemoIndexes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoCheckResultsTick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoCheckResultsDemoNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoCheckResultsTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoCheckResultsCheckNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemoCheckResultsEvaluatedValued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabDemoChecks.SuspendLayout();
            this.gTotals.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemoList)).BeginInit();
            this.tabDemoCheckResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemoCheckResults)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDemoChecks);
            this.tabControl1.Controls.Add(this.tabDemoCheckResults);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDemoChecks
            // 
            this.tabDemoChecks.Controls.Add(this.gTotals);
            this.tabDemoChecks.Controls.Add(this.dgvDemoList);
            this.tabDemoChecks.Location = new System.Drawing.Point(4, 22);
            this.tabDemoChecks.Name = "tabDemoChecks";
            this.tabDemoChecks.Padding = new System.Windows.Forms.Padding(3);
            this.tabDemoChecks.Size = new System.Drawing.Size(768, 400);
            this.tabDemoChecks.TabIndex = 0;
            this.tabDemoChecks.Text = "Demo List";
            this.tabDemoChecks.UseVisualStyleBackColor = true;
            // 
            // gTotals
            // 
            this.gTotals.Controls.Add(this.tableLayoutPanel2);
            this.gTotals.Location = new System.Drawing.Point(7, 313);
            this.gTotals.Name = "gTotals";
            this.gTotals.Size = new System.Drawing.Size(755, 81);
            this.gTotals.TabIndex = 1;
            this.gTotals.TabStop = false;
            this.gTotals.Text = "Totals";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 541F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labTotalAdjustedTime, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labTotalTime, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(730, 50);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Adjusted Ticks";
            // 
            // labTotalAdjustedTime
            // 
            this.labTotalAdjustedTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTotalAdjustedTime.AutoSize = true;
            this.labTotalAdjustedTime.Location = new System.Drawing.Point(103, 31);
            this.labTotalAdjustedTime.Name = "labTotalAdjustedTime";
            this.labTotalAdjustedTime.Size = new System.Drawing.Size(0, 13);
            this.labTotalAdjustedTime.TabIndex = 3;
            // 
            // labTotalTime
            // 
            this.labTotalTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTotalTime.AutoSize = true;
            this.labTotalTime.Location = new System.Drawing.Point(103, 6);
            this.labTotalTime.Name = "labTotalTime";
            this.labTotalTime.Size = new System.Drawing.Size(0, 13);
            this.labTotalTime.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Ticks";
            // 
            // dgvDemoList
            // 
            this.dgvDemoList.AllowUserToAddRows = false;
            this.dgvDemoList.AllowUserToDeleteRows = false;
            this.dgvDemoList.AllowUserToOrderColumns = true;
            this.dgvDemoList.AllowUserToResizeColumns = false;
            this.dgvDemoList.AllowUserToResizeRows = false;
            this.dgvDemoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDemoList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDemoListIndicies,
            this.dgvDemoListDemoNames,
            this.dgvDemoListLastModifiedTime,
            this.dgvDemoListMapNames,
            this.dgvDemoListTickCount,
            this.dgvDemoListAdjustedTickCounts,
            this.dgvDemoListPlayerNames,
            this.dgvDemoListEventsCounts});
            this.dgvDemoList.Location = new System.Drawing.Point(3, 7);
            this.dgvDemoList.Name = "dgvDemoList";
            this.dgvDemoList.ReadOnly = true;
            this.dgvDemoList.RowHeadersVisible = false;
            this.dgvDemoList.Size = new System.Drawing.Size(762, 300);
            this.dgvDemoList.TabIndex = 0;
            // 
            // dgvDemoListIndicies
            // 
            this.dgvDemoListIndicies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvDemoListIndicies.HeaderText = "Index";
            this.dgvDemoListIndicies.Name = "dgvDemoListIndicies";
            this.dgvDemoListIndicies.ReadOnly = true;
            this.dgvDemoListIndicies.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemoListIndicies.Width = 40;
            // 
            // dgvDemoListDemoNames
            // 
            this.dgvDemoListDemoNames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvDemoListDemoNames.HeaderText = "Demo Name";
            this.dgvDemoListDemoNames.MinimumWidth = 150;
            this.dgvDemoListDemoNames.Name = "dgvDemoListDemoNames";
            this.dgvDemoListDemoNames.ReadOnly = true;
            this.dgvDemoListDemoNames.Width = 150;
            // 
            // dgvDemoListLastModifiedTime
            // 
            this.dgvDemoListLastModifiedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvDemoListLastModifiedTime.HeaderText = "Last Modified Time";
            this.dgvDemoListLastModifiedTime.MinimumWidth = 120;
            this.dgvDemoListLastModifiedTime.Name = "dgvDemoListLastModifiedTime";
            this.dgvDemoListLastModifiedTime.ReadOnly = true;
            this.dgvDemoListLastModifiedTime.Width = 120;
            // 
            // dgvDemoListMapNames
            // 
            this.dgvDemoListMapNames.HeaderText = "Map Name";
            this.dgvDemoListMapNames.MinimumWidth = 100;
            this.dgvDemoListMapNames.Name = "dgvDemoListMapNames";
            this.dgvDemoListMapNames.ReadOnly = true;
            // 
            // dgvDemoListTickCount
            // 
            this.dgvDemoListTickCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvDemoListTickCount.HeaderText = "Ticks";
            this.dgvDemoListTickCount.Name = "dgvDemoListTickCount";
            this.dgvDemoListTickCount.ReadOnly = true;
            this.dgvDemoListTickCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemoListTickCount.Width = 60;
            // 
            // dgvDemoListAdjustedTickCounts
            // 
            this.dgvDemoListAdjustedTickCounts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvDemoListAdjustedTickCounts.HeaderText = "Adjusted TIcks";
            this.dgvDemoListAdjustedTickCounts.Name = "dgvDemoListAdjustedTickCounts";
            this.dgvDemoListAdjustedTickCounts.ReadOnly = true;
            this.dgvDemoListAdjustedTickCounts.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemoListAdjustedTickCounts.Width = 95;
            // 
            // dgvDemoListPlayerNames
            // 
            this.dgvDemoListPlayerNames.HeaderText = "Player Name";
            this.dgvDemoListPlayerNames.Name = "dgvDemoListPlayerNames";
            this.dgvDemoListPlayerNames.ReadOnly = true;
            // 
            // dgvDemoListEventsCounts
            // 
            this.dgvDemoListEventsCounts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvDemoListEventsCounts.HeaderText = "Events";
            this.dgvDemoListEventsCounts.Name = "dgvDemoListEventsCounts";
            this.dgvDemoListEventsCounts.ReadOnly = true;
            this.dgvDemoListEventsCounts.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemoListEventsCounts.Width = 60;
            // 
            // tabDemoCheckResults
            // 
            this.tabDemoCheckResults.Controls.Add(this.dgvDemoCheckResults);
            this.tabDemoCheckResults.Location = new System.Drawing.Point(4, 22);
            this.tabDemoCheckResults.Name = "tabDemoCheckResults";
            this.tabDemoCheckResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabDemoCheckResults.Size = new System.Drawing.Size(768, 400);
            this.tabDemoCheckResults.TabIndex = 1;
            this.tabDemoCheckResults.Text = "Demo Check Results";
            this.tabDemoCheckResults.UseVisualStyleBackColor = true;
            // 
            // dgvDemoCheckResults
            // 
            this.dgvDemoCheckResults.AllowUserToAddRows = false;
            this.dgvDemoCheckResults.AllowUserToDeleteRows = false;
            this.dgvDemoCheckResults.AllowUserToOrderColumns = true;
            this.dgvDemoCheckResults.AllowUserToResizeColumns = false;
            this.dgvDemoCheckResults.AllowUserToResizeRows = false;
            this.dgvDemoCheckResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDemoCheckResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDemoCheckResultsDemoIndexes,
            this.dgvDemoCheckResultsTick,
            this.dgvDemoCheckResultsDemoNames,
            this.dgvDemoCheckResultsTypes,
            this.dgvDemoCheckResultsCheckNames,
            this.dgvDemoCheckResultsEvaluatedValued});
            this.dgvDemoCheckResults.Location = new System.Drawing.Point(3, 6);
            this.dgvDemoCheckResults.Name = "dgvDemoCheckResults";
            this.dgvDemoCheckResults.ReadOnly = true;
            this.dgvDemoCheckResults.RowHeadersVisible = false;
            this.dgvDemoCheckResults.Size = new System.Drawing.Size(762, 388);
            this.dgvDemoCheckResults.TabIndex = 1;
            // 
            // dgvDemoCheckResultsDemoIndexes
            // 
            this.dgvDemoCheckResultsDemoIndexes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvDemoCheckResultsDemoIndexes.HeaderText = "Index";
            this.dgvDemoCheckResultsDemoIndexes.Name = "dgvDemoCheckResultsDemoIndexes";
            this.dgvDemoCheckResultsDemoIndexes.ReadOnly = true;
            this.dgvDemoCheckResultsDemoIndexes.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemoCheckResultsDemoIndexes.Width = 40;
            // 
            // dgvDemoCheckResultsTick
            // 
            this.dgvDemoCheckResultsTick.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvDemoCheckResultsTick.HeaderText = "Tick";
            this.dgvDemoCheckResultsTick.Name = "dgvDemoCheckResultsTick";
            this.dgvDemoCheckResultsTick.ReadOnly = true;
            this.dgvDemoCheckResultsTick.Width = 60;
            // 
            // dgvDemoCheckResultsDemoNames
            // 
            this.dgvDemoCheckResultsDemoNames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvDemoCheckResultsDemoNames.HeaderText = "Demo Name";
            this.dgvDemoCheckResultsDemoNames.Name = "dgvDemoCheckResultsDemoNames";
            this.dgvDemoCheckResultsDemoNames.ReadOnly = true;
            this.dgvDemoCheckResultsDemoNames.Width = 84;
            // 
            // dgvDemoCheckResultsTypes
            // 
            this.dgvDemoCheckResultsTypes.HeaderText = "Result Type";
            this.dgvDemoCheckResultsTypes.Name = "dgvDemoCheckResultsTypes";
            this.dgvDemoCheckResultsTypes.ReadOnly = true;
            // 
            // dgvDemoCheckResultsCheckNames
            // 
            this.dgvDemoCheckResultsCheckNames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvDemoCheckResultsCheckNames.HeaderText = "Check Name";
            this.dgvDemoCheckResultsCheckNames.Name = "dgvDemoCheckResultsCheckNames";
            this.dgvDemoCheckResultsCheckNames.ReadOnly = true;
            this.dgvDemoCheckResultsCheckNames.Width = 87;
            // 
            // dgvDemoCheckResultsEvaluatedValued
            // 
            this.dgvDemoCheckResultsEvaluatedValued.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvDemoCheckResultsEvaluatedValued.HeaderText = "Evaluated Values";
            this.dgvDemoCheckResultsEvaluatedValued.Name = "dgvDemoCheckResultsEvaluatedValued";
            this.dgvDemoCheckResultsEvaluatedValued.ReadOnly = true;
            this.dgvDemoCheckResultsEvaluatedValued.Width = 105;
            // 
            // DemoListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DemoListForm";
            this.Text = "Demo List";
            this.tabControl1.ResumeLayout(false);
            this.tabDemoChecks.ResumeLayout(false);
            this.gTotals.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemoList)).EndInit();
            this.tabDemoCheckResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemoCheckResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabDemoChecks;
        private System.Windows.Forms.TabPage tabDemoCheckResults;
        private System.Windows.Forms.DataGridView dgvDemoList;
        private System.Windows.Forms.GroupBox gTotals;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labTotalAdjustedTime;
        public System.Windows.Forms.Label labTotalTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDemoCheckResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoCheckResultsDemoIndexes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoCheckResultsTick;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoCheckResultsDemoNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoCheckResultsTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoCheckResultsCheckNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoCheckResultsEvaluatedValued;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListIndicies;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListDemoNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListLastModifiedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListMapNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListTickCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListAdjustedTickCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListPlayerNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemoListEventsCounts;
        public System.Windows.Forms.TabControl tabControl1;
    }
}