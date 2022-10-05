namespace startdemos_plus.UI
{
    partial class ConfigureCustomMapForm
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
            this.boxInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.Controls.Add(this.boxInput, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.butOK, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(449, 443);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // boxInput
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.boxInput, 2);
            this.boxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxInput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxInput.Location = new System.Drawing.Point(3, 3);
            this.boxInput.Multiline = true;
            this.boxInput.Name = "boxInput";
            this.boxInput.Size = new System.Drawing.Size(443, 407);
            this.boxInput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter map names line-by-line in the order you go through them in the run.";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(375, 416);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(71, 23);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "Apply";
            this.butOK.UseVisualStyleBackColor = true;
            // 
            // ConfigureCustomMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(483, 500);
            this.Name = "ConfigureCustomMapForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.Text = "startdemos+ | Custom Map Order";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox boxInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butOK;
    }
}