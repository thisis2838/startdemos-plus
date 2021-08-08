
namespace startdemos_ui.Forms
{
    partial class DemoCheckEditor
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
            this.gDCEChecksList = new System.Windows.Forms.GroupBox();
            this.gDCEChecksListGame = new System.Windows.Forms.GroupBox();
            this.butDCEChecksListGameRemove = new System.Windows.Forms.Button();
            this.butDCEChecksListGameAdd = new System.Windows.Forms.Button();
            this.cbDCEChecksListGames = new System.Windows.Forms.ComboBox();
            this.butDCEChecksListRemove = new System.Windows.Forms.Button();
            this.butDCEChecksListAdd = new System.Windows.Forms.Button();
            this.dgvDCEChecksListGrid = new System.Windows.Forms.DataGridView();
            this.dgvDCEChecksListGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gDCECheckData = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labDCEConditionsName = new System.Windows.Forms.Label();
            this.boxDCEConditionsName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableDCEConditions = new System.Windows.Forms.TableLayoutPanel();
            this.labDCEConditionsTickCompare = new System.Windows.Forms.Label();
            this.labDCEConditionsMap = new System.Windows.Forms.Label();
            this.labDCEConditionsTargetVariable = new System.Windows.Forms.Label();
            this.boxDCEConditionsTargetVariable = new System.Windows.Forms.TextBox();
            this.boxDCEConditionsMap = new System.Windows.Forms.TextBox();
            this.boxDCEConditionsTickCompare = new System.Windows.Forms.TextBox();
            this.labDCEConditionsNot = new System.Windows.Forms.Label();
            this.chkDCEConditionsNot = new System.Windows.Forms.CheckBox();
            this.gDCEReturnType = new System.Windows.Forms.GroupBox();
            this.tableDCEReturnType = new System.Windows.Forms.TableLayoutPanel();
            this.cbDCEReturnType = new System.Windows.Forms.ComboBox();
            this.labDCEReturnType = new System.Windows.Forms.Label();
            this.gDCECheckDataEvaluation = new System.Windows.Forms.GroupBox();
            this.tableDCEEvaluationData = new System.Windows.Forms.TableLayoutPanel();
            this.gDCEEvaluationDataDirective = new System.Windows.Forms.Label();
            this.cbDCEEvaluationDataType = new System.Windows.Forms.ComboBox();
            this.labDCEEvaluationDataType = new System.Windows.Forms.Label();
            this.cbDCEEvaluationDataDirective = new System.Windows.Forms.ComboBox();
            this.butDCERefresh = new System.Windows.Forms.Button();
            this.butOpenDemoEvents = new System.Windows.Forms.Button();
            this.labError = new System.Windows.Forms.Label();
            this.butSave = new System.Windows.Forms.Button();
            this.gDCEChecksList.SuspendLayout();
            this.gDCEChecksListGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDCEChecksListGrid)).BeginInit();
            this.gDCECheckData.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableDCEConditions.SuspendLayout();
            this.gDCEReturnType.SuspendLayout();
            this.tableDCEReturnType.SuspendLayout();
            this.gDCECheckDataEvaluation.SuspendLayout();
            this.tableDCEEvaluationData.SuspendLayout();
            this.SuspendLayout();
            // 
            // gDCEChecksList
            // 
            this.gDCEChecksList.Controls.Add(this.gDCEChecksListGame);
            this.gDCEChecksList.Controls.Add(this.butDCEChecksListRemove);
            this.gDCEChecksList.Controls.Add(this.butDCEChecksListAdd);
            this.gDCEChecksList.Controls.Add(this.dgvDCEChecksListGrid);
            this.gDCEChecksList.Location = new System.Drawing.Point(12, 12);
            this.gDCEChecksList.Name = "gDCEChecksList";
            this.gDCEChecksList.Size = new System.Drawing.Size(253, 375);
            this.gDCEChecksList.TabIndex = 3;
            this.gDCEChecksList.TabStop = false;
            this.gDCEChecksList.Text = "Checks List";
            // 
            // gDCEChecksListGame
            // 
            this.gDCEChecksListGame.Controls.Add(this.butDCEChecksListGameRemove);
            this.gDCEChecksListGame.Controls.Add(this.butDCEChecksListGameAdd);
            this.gDCEChecksListGame.Controls.Add(this.cbDCEChecksListGames);
            this.gDCEChecksListGame.Location = new System.Drawing.Point(6, 19);
            this.gDCEChecksListGame.Name = "gDCEChecksListGame";
            this.gDCEChecksListGame.Size = new System.Drawing.Size(240, 79);
            this.gDCEChecksListGame.TabIndex = 5;
            this.gDCEChecksListGame.TabStop = false;
            this.gDCEChecksListGame.Text = "Configurations";
            // 
            // butDCEChecksListGameRemove
            // 
            this.butDCEChecksListGameRemove.Location = new System.Drawing.Point(78, 46);
            this.butDCEChecksListGameRemove.Name = "butDCEChecksListGameRemove";
            this.butDCEChecksListGameRemove.Size = new System.Drawing.Size(75, 23);
            this.butDCEChecksListGameRemove.TabIndex = 5;
            this.butDCEChecksListGameRemove.Text = "Remove";
            this.butDCEChecksListGameRemove.UseVisualStyleBackColor = true;
            this.butDCEChecksListGameRemove.Click += new System.EventHandler(this.butDCEChecksListGameRemove_Click);
            // 
            // butDCEChecksListGameAdd
            // 
            this.butDCEChecksListGameAdd.Location = new System.Drawing.Point(159, 46);
            this.butDCEChecksListGameAdd.Name = "butDCEChecksListGameAdd";
            this.butDCEChecksListGameAdd.Size = new System.Drawing.Size(75, 23);
            this.butDCEChecksListGameAdd.TabIndex = 4;
            this.butDCEChecksListGameAdd.Text = "Add";
            this.butDCEChecksListGameAdd.UseVisualStyleBackColor = true;
            this.butDCEChecksListGameAdd.Click += new System.EventHandler(this.butDCEChecksListGameAdd_Click);
            // 
            // cbDCEChecksListGames
            // 
            this.cbDCEChecksListGames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDCEChecksListGames.FormattingEnabled = true;
            this.cbDCEChecksListGames.Location = new System.Drawing.Point(6, 20);
            this.cbDCEChecksListGames.Name = "cbDCEChecksListGames";
            this.cbDCEChecksListGames.Size = new System.Drawing.Size(228, 21);
            this.cbDCEChecksListGames.TabIndex = 0;
            this.cbDCEChecksListGames.SelectedIndexChanged += new System.EventHandler(this.cbDCEChecksListGames_SelectedIndexChanged);
            // 
            // butDCEChecksListRemove
            // 
            this.butDCEChecksListRemove.Location = new System.Drawing.Point(90, 346);
            this.butDCEChecksListRemove.Name = "butDCEChecksListRemove";
            this.butDCEChecksListRemove.Size = new System.Drawing.Size(75, 23);
            this.butDCEChecksListRemove.TabIndex = 4;
            this.butDCEChecksListRemove.Text = "Remove";
            this.butDCEChecksListRemove.UseVisualStyleBackColor = true;
            this.butDCEChecksListRemove.Click += new System.EventHandler(this.butDCEChecksListRemove_Click);
            // 
            // butDCEChecksListAdd
            // 
            this.butDCEChecksListAdd.Location = new System.Drawing.Point(171, 346);
            this.butDCEChecksListAdd.Name = "butDCEChecksListAdd";
            this.butDCEChecksListAdd.Size = new System.Drawing.Size(75, 23);
            this.butDCEChecksListAdd.TabIndex = 3;
            this.butDCEChecksListAdd.Text = "Add";
            this.butDCEChecksListAdd.UseVisualStyleBackColor = true;
            this.butDCEChecksListAdd.Click += new System.EventHandler(this.butDCEChecksListAdd_Click);
            // 
            // dgvDCEChecksListGrid
            // 
            this.dgvDCEChecksListGrid.AllowUserToAddRows = false;
            this.dgvDCEChecksListGrid.AllowUserToDeleteRows = false;
            this.dgvDCEChecksListGrid.AllowUserToResizeRows = false;
            this.dgvDCEChecksListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDCEChecksListGrid.ColumnHeadersVisible = false;
            this.dgvDCEChecksListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDCEChecksListGridColumn});
            this.dgvDCEChecksListGrid.Location = new System.Drawing.Point(6, 104);
            this.dgvDCEChecksListGrid.Name = "dgvDCEChecksListGrid";
            this.dgvDCEChecksListGrid.ReadOnly = true;
            this.dgvDCEChecksListGrid.RowHeadersVisible = false;
            this.dgvDCEChecksListGrid.ShowCellToolTips = false;
            this.dgvDCEChecksListGrid.ShowEditingIcon = false;
            this.dgvDCEChecksListGrid.ShowRowErrors = false;
            this.dgvDCEChecksListGrid.Size = new System.Drawing.Size(240, 236);
            this.dgvDCEChecksListGrid.TabIndex = 1;
            // 
            // dgvDCEChecksListGridColumn
            // 
            this.dgvDCEChecksListGridColumn.Frozen = true;
            this.dgvDCEChecksListGridColumn.HeaderText = "Checks";
            this.dgvDCEChecksListGridColumn.Name = "dgvDCEChecksListGridColumn";
            this.dgvDCEChecksListGridColumn.ReadOnly = true;
            this.dgvDCEChecksListGridColumn.Width = 299;
            // 
            // gDCECheckData
            // 
            this.gDCECheckData.Controls.Add(this.groupBox2);
            this.gDCECheckData.Controls.Add(this.groupBox1);
            this.gDCECheckData.Controls.Add(this.gDCEReturnType);
            this.gDCECheckData.Controls.Add(this.gDCECheckDataEvaluation);
            this.gDCECheckData.Location = new System.Drawing.Point(271, 41);
            this.gDCECheckData.Name = "gDCECheckData";
            this.gDCECheckData.Size = new System.Drawing.Size(484, 299);
            this.gDCECheckData.TabIndex = 4;
            this.gDCECheckData.TabStop = false;
            this.gDCECheckData.Text = "Check Definition";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labDCEConditionsName);
            this.groupBox2.Controls.Add(this.boxDCEConditionsName);
            this.groupBox2.Location = new System.Drawing.Point(6, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 49);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Identification";
            // 
            // labDCEConditionsName
            // 
            this.labDCEConditionsName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEConditionsName.AutoSize = true;
            this.labDCEConditionsName.Location = new System.Drawing.Point(6, 23);
            this.labDCEConditionsName.Name = "labDCEConditionsName";
            this.labDCEConditionsName.Size = new System.Drawing.Size(35, 13);
            this.labDCEConditionsName.TabIndex = 10;
            this.labDCEConditionsName.Text = "Name";
            // 
            // boxDCEConditionsName
            // 
            this.boxDCEConditionsName.Location = new System.Drawing.Point(103, 20);
            this.boxDCEConditionsName.Name = "boxDCEConditionsName";
            this.boxDCEConditionsName.Size = new System.Drawing.Size(363, 20);
            this.boxDCEConditionsName.TabIndex = 11;
            this.boxDCEConditionsName.TextChanged += new System.EventHandler(this.boxDCEConditionsName_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableDCEConditions);
            this.groupBox1.Location = new System.Drawing.Point(6, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 131);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conditions";
            // 
            // tableDCEConditions
            // 
            this.tableDCEConditions.ColumnCount = 2;
            this.tableDCEConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableDCEConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDCEConditions.Controls.Add(this.labDCEConditionsTickCompare, 0, 2);
            this.tableDCEConditions.Controls.Add(this.labDCEConditionsMap, 0, 1);
            this.tableDCEConditions.Controls.Add(this.labDCEConditionsTargetVariable, 0, 0);
            this.tableDCEConditions.Controls.Add(this.boxDCEConditionsTargetVariable, 1, 0);
            this.tableDCEConditions.Controls.Add(this.boxDCEConditionsMap, 1, 1);
            this.tableDCEConditions.Controls.Add(this.boxDCEConditionsTickCompare, 1, 2);
            this.tableDCEConditions.Controls.Add(this.labDCEConditionsNot, 0, 3);
            this.tableDCEConditions.Controls.Add(this.chkDCEConditionsNot, 1, 3);
            this.tableDCEConditions.Location = new System.Drawing.Point(7, 20);
            this.tableDCEConditions.Name = "tableDCEConditions";
            this.tableDCEConditions.RowCount = 4;
            this.tableDCEConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEConditions.Size = new System.Drawing.Size(462, 100);
            this.tableDCEConditions.TabIndex = 0;
            // 
            // labDCEConditionsTickCompare
            // 
            this.labDCEConditionsTickCompare.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEConditionsTickCompare.AutoSize = true;
            this.labDCEConditionsTickCompare.Location = new System.Drawing.Point(3, 56);
            this.labDCEConditionsTickCompare.Name = "labDCEConditionsTickCompare";
            this.labDCEConditionsTickCompare.Size = new System.Drawing.Size(73, 13);
            this.labDCEConditionsTickCompare.TabIndex = 5;
            this.labDCEConditionsTickCompare.Text = "Tick Compare";
            // 
            // labDCEConditionsMap
            // 
            this.labDCEConditionsMap.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEConditionsMap.AutoSize = true;
            this.labDCEConditionsMap.Location = new System.Drawing.Point(3, 31);
            this.labDCEConditionsMap.Name = "labDCEConditionsMap";
            this.labDCEConditionsMap.Size = new System.Drawing.Size(28, 13);
            this.labDCEConditionsMap.TabIndex = 1;
            this.labDCEConditionsMap.Text = "Map";
            // 
            // labDCEConditionsTargetVariable
            // 
            this.labDCEConditionsTargetVariable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEConditionsTargetVariable.AutoSize = true;
            this.labDCEConditionsTargetVariable.Location = new System.Drawing.Point(3, 6);
            this.labDCEConditionsTargetVariable.Name = "labDCEConditionsTargetVariable";
            this.labDCEConditionsTargetVariable.Size = new System.Drawing.Size(90, 13);
            this.labDCEConditionsTargetVariable.TabIndex = 0;
            this.labDCEConditionsTargetVariable.Text = "Target Variable(s)";
            // 
            // boxDCEConditionsTargetVariable
            // 
            this.boxDCEConditionsTargetVariable.Location = new System.Drawing.Point(99, 3);
            this.boxDCEConditionsTargetVariable.Name = "boxDCEConditionsTargetVariable";
            this.boxDCEConditionsTargetVariable.Size = new System.Drawing.Size(360, 20);
            this.boxDCEConditionsTargetVariable.TabIndex = 4;
            this.boxDCEConditionsTargetVariable.TextChanged += new System.EventHandler(this.boxDCEConditionsTargetVariable_TextChanged);
            // 
            // boxDCEConditionsMap
            // 
            this.boxDCEConditionsMap.Location = new System.Drawing.Point(99, 28);
            this.boxDCEConditionsMap.Name = "boxDCEConditionsMap";
            this.boxDCEConditionsMap.Size = new System.Drawing.Size(189, 20);
            this.boxDCEConditionsMap.TabIndex = 6;
            this.boxDCEConditionsMap.TextChanged += new System.EventHandler(this.boxDCEConditionsMap_TextChanged);
            // 
            // boxDCEConditionsTickCompare
            // 
            this.boxDCEConditionsTickCompare.Location = new System.Drawing.Point(99, 53);
            this.boxDCEConditionsTickCompare.Name = "boxDCEConditionsTickCompare";
            this.boxDCEConditionsTickCompare.Size = new System.Drawing.Size(120, 20);
            this.boxDCEConditionsTickCompare.TabIndex = 7;
            this.boxDCEConditionsTickCompare.TextChanged += new System.EventHandler(this.boxDCEConditionsTickCompare_TextChanged);
            // 
            // labDCEConditionsNot
            // 
            this.labDCEConditionsNot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEConditionsNot.AutoSize = true;
            this.labDCEConditionsNot.Location = new System.Drawing.Point(3, 81);
            this.labDCEConditionsNot.Name = "labDCEConditionsNot";
            this.labDCEConditionsNot.Size = new System.Drawing.Size(24, 13);
            this.labDCEConditionsNot.TabIndex = 8;
            this.labDCEConditionsNot.Text = "Not";
            // 
            // chkDCEConditionsNot
            // 
            this.chkDCEConditionsNot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDCEConditionsNot.AutoSize = true;
            this.chkDCEConditionsNot.Location = new System.Drawing.Point(99, 80);
            this.chkDCEConditionsNot.Name = "chkDCEConditionsNot";
            this.chkDCEConditionsNot.Size = new System.Drawing.Size(15, 14);
            this.chkDCEConditionsNot.TabIndex = 9;
            this.chkDCEConditionsNot.UseVisualStyleBackColor = true;
            this.chkDCEConditionsNot.CheckedChanged += new System.EventHandler(this.chkDCEConditionsNot_CheckedChanged);
            // 
            // gDCEReturnType
            // 
            this.gDCEReturnType.Controls.Add(this.tableDCEReturnType);
            this.gDCEReturnType.Location = new System.Drawing.Point(246, 18);
            this.gDCEReturnType.Name = "gDCEReturnType";
            this.gDCEReturnType.Size = new System.Drawing.Size(232, 54);
            this.gDCEReturnType.TabIndex = 2;
            this.gDCEReturnType.TabStop = false;
            this.gDCEReturnType.Text = "Return Type";
            // 
            // tableDCEReturnType
            // 
            this.tableDCEReturnType.ColumnCount = 2;
            this.tableDCEReturnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableDCEReturnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDCEReturnType.Controls.Add(this.cbDCEReturnType, 1, 0);
            this.tableDCEReturnType.Controls.Add(this.labDCEReturnType, 0, 0);
            this.tableDCEReturnType.Location = new System.Drawing.Point(7, 20);
            this.tableDCEReturnType.Name = "tableDCEReturnType";
            this.tableDCEReturnType.RowCount = 1;
            this.tableDCEReturnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEReturnType.Size = new System.Drawing.Size(222, 25);
            this.tableDCEReturnType.TabIndex = 0;
            // 
            // cbDCEReturnType
            // 
            this.cbDCEReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDCEReturnType.FormattingEnabled = true;
            this.cbDCEReturnType.Items.AddRange(new object[] {
            "None",
            "BeginOnce",
            "BeginMultiple",
            "EndOnce",
            "EndMultiple",
            "Note"});
            this.cbDCEReturnType.Location = new System.Drawing.Point(99, 3);
            this.cbDCEReturnType.Name = "cbDCEReturnType";
            this.cbDCEReturnType.Size = new System.Drawing.Size(120, 21);
            this.cbDCEReturnType.TabIndex = 2;
            this.cbDCEReturnType.SelectedIndexChanged += new System.EventHandler(this.cbDCEReturnType_SelectedIndexChanged);
            // 
            // labDCEReturnType
            // 
            this.labDCEReturnType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEReturnType.AutoSize = true;
            this.labDCEReturnType.Location = new System.Drawing.Point(3, 6);
            this.labDCEReturnType.Name = "labDCEReturnType";
            this.labDCEReturnType.Size = new System.Drawing.Size(31, 13);
            this.labDCEReturnType.TabIndex = 0;
            this.labDCEReturnType.Text = "Type";
            // 
            // gDCECheckDataEvaluation
            // 
            this.gDCECheckDataEvaluation.Controls.Add(this.tableDCEEvaluationData);
            this.gDCECheckDataEvaluation.Location = new System.Drawing.Point(6, 18);
            this.gDCECheckDataEvaluation.Name = "gDCECheckDataEvaluation";
            this.gDCECheckDataEvaluation.Size = new System.Drawing.Size(232, 79);
            this.gDCECheckDataEvaluation.TabIndex = 1;
            this.gDCECheckDataEvaluation.TabStop = false;
            this.gDCECheckDataEvaluation.Text = "Evaluation Data";
            // 
            // tableDCEEvaluationData
            // 
            this.tableDCEEvaluationData.ColumnCount = 2;
            this.tableDCEEvaluationData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableDCEEvaluationData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 385F));
            this.tableDCEEvaluationData.Controls.Add(this.gDCEEvaluationDataDirective, 0, 1);
            this.tableDCEEvaluationData.Controls.Add(this.cbDCEEvaluationDataType, 1, 0);
            this.tableDCEEvaluationData.Controls.Add(this.labDCEEvaluationDataType, 0, 0);
            this.tableDCEEvaluationData.Controls.Add(this.cbDCEEvaluationDataDirective, 1, 1);
            this.tableDCEEvaluationData.Location = new System.Drawing.Point(7, 20);
            this.tableDCEEvaluationData.Name = "tableDCEEvaluationData";
            this.tableDCEEvaluationData.RowCount = 2;
            this.tableDCEEvaluationData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEEvaluationData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableDCEEvaluationData.Size = new System.Drawing.Size(222, 50);
            this.tableDCEEvaluationData.TabIndex = 0;
            // 
            // gDCEEvaluationDataDirective
            // 
            this.gDCEEvaluationDataDirective.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gDCEEvaluationDataDirective.AutoSize = true;
            this.gDCEEvaluationDataDirective.Location = new System.Drawing.Point(3, 31);
            this.gDCEEvaluationDataDirective.Name = "gDCEEvaluationDataDirective";
            this.gDCEEvaluationDataDirective.Size = new System.Drawing.Size(49, 13);
            this.gDCEEvaluationDataDirective.TabIndex = 1;
            this.gDCEEvaluationDataDirective.Text = "Directive";
            // 
            // cbDCEEvaluationDataType
            // 
            this.cbDCEEvaluationDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDCEEvaluationDataType.FormattingEnabled = true;
            this.cbDCEEvaluationDataType.Items.AddRange(new object[] {
            "None",
            "Position",
            "ConsoleCommand",
            "UserCommand"});
            this.cbDCEEvaluationDataType.Location = new System.Drawing.Point(99, 3);
            this.cbDCEEvaluationDataType.Name = "cbDCEEvaluationDataType";
            this.cbDCEEvaluationDataType.Size = new System.Drawing.Size(120, 21);
            this.cbDCEEvaluationDataType.TabIndex = 2;
            this.cbDCEEvaluationDataType.SelectedIndexChanged += new System.EventHandler(this.cbDCEEvaluationDataType_SelectedIndexChanged);
            // 
            // labDCEEvaluationDataType
            // 
            this.labDCEEvaluationDataType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDCEEvaluationDataType.AutoSize = true;
            this.labDCEEvaluationDataType.Location = new System.Drawing.Point(3, 6);
            this.labDCEEvaluationDataType.Name = "labDCEEvaluationDataType";
            this.labDCEEvaluationDataType.Size = new System.Drawing.Size(31, 13);
            this.labDCEEvaluationDataType.TabIndex = 0;
            this.labDCEEvaluationDataType.Text = "Type";
            // 
            // cbDCEEvaluationDataDirective
            // 
            this.cbDCEEvaluationDataDirective.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDCEEvaluationDataDirective.FormattingEnabled = true;
            this.cbDCEEvaluationDataDirective.Items.AddRange(new object[] {
            "None",
            "Direct",
            "Difference",
            "Substring"});
            this.cbDCEEvaluationDataDirective.Location = new System.Drawing.Point(99, 28);
            this.cbDCEEvaluationDataDirective.Name = "cbDCEEvaluationDataDirective";
            this.cbDCEEvaluationDataDirective.Size = new System.Drawing.Size(120, 21);
            this.cbDCEEvaluationDataDirective.TabIndex = 3;
            this.cbDCEEvaluationDataDirective.SelectedIndexChanged += new System.EventHandler(this.cbDCEEvaluationDataDirective_SelectedIndexChanged);
            // 
            // butDCERefresh
            // 
            this.butDCERefresh.Location = new System.Drawing.Point(578, 363);
            this.butDCERefresh.Name = "butDCERefresh";
            this.butDCERefresh.Size = new System.Drawing.Size(96, 23);
            this.butDCERefresh.TabIndex = 6;
            this.butDCERefresh.Text = "Restore from File";
            this.butDCERefresh.UseVisualStyleBackColor = true;
            this.butDCERefresh.Click += new System.EventHandler(this.butDCERefresh_Click);
            // 
            // butOpenDemoEvents
            // 
            this.butOpenDemoEvents.Location = new System.Drawing.Point(272, 12);
            this.butOpenDemoEvents.Name = "butOpenDemoEvents";
            this.butOpenDemoEvents.Size = new System.Drawing.Size(483, 23);
            this.butOpenDemoEvents.TabIndex = 7;
            this.butOpenDemoEvents.Text = "Open latest Demo Check Results";
            this.butOpenDemoEvents.UseVisualStyleBackColor = true;
            this.butOpenDemoEvents.Click += new System.EventHandler(this.butOpenDemoEvents_Click);
            // 
            // labError
            // 
            this.labError.AutoSize = true;
            this.labError.Location = new System.Drawing.Point(271, 368);
            this.labError.Name = "labError";
            this.labError.Size = new System.Drawing.Size(0, 13);
            this.labError.TabIndex = 8;
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(680, 363);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 23);
            this.butSave.TabIndex = 9;
            this.butSave.Text = "Save to File";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // DemoCheckEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(767, 399);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.labError);
            this.Controls.Add(this.butOpenDemoEvents);
            this.Controls.Add(this.butDCERefresh);
            this.Controls.Add(this.gDCECheckData);
            this.Controls.Add(this.gDCEChecksList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DemoCheckEditor";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DemoCheckEditor_Load);
            this.gDCEChecksList.ResumeLayout(false);
            this.gDCEChecksListGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDCEChecksListGrid)).EndInit();
            this.gDCECheckData.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableDCEConditions.ResumeLayout(false);
            this.tableDCEConditions.PerformLayout();
            this.gDCEReturnType.ResumeLayout(false);
            this.tableDCEReturnType.ResumeLayout(false);
            this.tableDCEReturnType.PerformLayout();
            this.gDCECheckDataEvaluation.ResumeLayout(false);
            this.tableDCEEvaluationData.ResumeLayout(false);
            this.tableDCEEvaluationData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gDCEChecksList;
        private System.Windows.Forms.GroupBox gDCEChecksListGame;
        private System.Windows.Forms.Button butDCEChecksListGameRemove;
        private System.Windows.Forms.Button butDCEChecksListGameAdd;
        private System.Windows.Forms.ComboBox cbDCEChecksListGames;
        private System.Windows.Forms.Button butDCEChecksListRemove;
        private System.Windows.Forms.Button butDCEChecksListAdd;
        private System.Windows.Forms.DataGridView dgvDCEChecksListGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDCEChecksListGridColumn;
        private System.Windows.Forms.GroupBox gDCECheckData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labDCEConditionsName;
        private System.Windows.Forms.TextBox boxDCEConditionsName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableDCEConditions;
        private System.Windows.Forms.Label labDCEConditionsTickCompare;
        private System.Windows.Forms.Label labDCEConditionsMap;
        private System.Windows.Forms.Label labDCEConditionsTargetVariable;
        private System.Windows.Forms.TextBox boxDCEConditionsTargetVariable;
        private System.Windows.Forms.TextBox boxDCEConditionsMap;
        private System.Windows.Forms.TextBox boxDCEConditionsTickCompare;
        private System.Windows.Forms.Label labDCEConditionsNot;
        private System.Windows.Forms.CheckBox chkDCEConditionsNot;
        private System.Windows.Forms.GroupBox gDCEReturnType;
        private System.Windows.Forms.TableLayoutPanel tableDCEReturnType;
        private System.Windows.Forms.ComboBox cbDCEReturnType;
        private System.Windows.Forms.Label labDCEReturnType;
        private System.Windows.Forms.GroupBox gDCECheckDataEvaluation;
        private System.Windows.Forms.TableLayoutPanel tableDCEEvaluationData;
        private System.Windows.Forms.Label gDCEEvaluationDataDirective;
        private System.Windows.Forms.ComboBox cbDCEEvaluationDataType;
        private System.Windows.Forms.Label labDCEEvaluationDataType;
        private System.Windows.Forms.ComboBox cbDCEEvaluationDataDirective;
        private System.Windows.Forms.Button butDCERefresh;
        private System.Windows.Forms.Button butOpenDemoEvents;
        private System.Windows.Forms.Label labError;
        private System.Windows.Forms.Button butSave;
    }
}