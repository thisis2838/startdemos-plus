namespace startdemos_plus.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabCtrlMaster = new System.Windows.Forms.TabControl();
            this.tabPgAbout = new System.Windows.Forms.TabPage();
            this.tabPgGameHook = new System.Windows.Forms.TabPage();
            this.tabPgDemoCollect = new System.Windows.Forms.TabPage();
            this.tabPgDemoPlayOrder = new System.Windows.Forms.TabPage();
            this.tabPgDemoChecks = new System.Windows.Forms.TabPage();
            this.butHelp = new System.Windows.Forms.Button();
            this.tabCtrlMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrlMaster
            // 
            this.tabCtrlMaster.Controls.Add(this.tabPgAbout);
            this.tabCtrlMaster.Controls.Add(this.tabPgGameHook);
            this.tabCtrlMaster.Controls.Add(this.tabPgDemoCollect);
            this.tabCtrlMaster.Controls.Add(this.tabPgDemoPlayOrder);
            this.tabCtrlMaster.Controls.Add(this.tabPgDemoChecks);
            this.tabCtrlMaster.Location = new System.Drawing.Point(12, 12);
            this.tabCtrlMaster.Name = "tabCtrlMaster";
            this.tabCtrlMaster.SelectedIndex = 0;
            this.tabCtrlMaster.Size = new System.Drawing.Size(860, 437);
            this.tabCtrlMaster.TabIndex = 0;
            // 
            // tabPgAbout
            // 
            this.tabPgAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPgAbout.Name = "tabPgAbout";
            this.tabPgAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgAbout.Size = new System.Drawing.Size(852, 411);
            this.tabPgAbout.TabIndex = 0;
            this.tabPgAbout.Text = "About";
            this.tabPgAbout.UseVisualStyleBackColor = true;
            // 
            // tabPgGameHook
            // 
            this.tabPgGameHook.Location = new System.Drawing.Point(4, 22);
            this.tabPgGameHook.Name = "tabPgGameHook";
            this.tabPgGameHook.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgGameHook.Size = new System.Drawing.Size(852, 411);
            this.tabPgGameHook.TabIndex = 1;
            this.tabPgGameHook.Text = "Game Hooking";
            this.tabPgGameHook.UseVisualStyleBackColor = true;
            // 
            // tabPgDemoCollect
            // 
            this.tabPgDemoCollect.Location = new System.Drawing.Point(4, 22);
            this.tabPgDemoCollect.Name = "tabPgDemoCollect";
            this.tabPgDemoCollect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgDemoCollect.Size = new System.Drawing.Size(852, 411);
            this.tabPgDemoCollect.TabIndex = 2;
            this.tabPgDemoCollect.Text = "Demo Parsing";
            this.tabPgDemoCollect.UseVisualStyleBackColor = true;
            // 
            // tabPgDemoPlayOrder
            // 
            this.tabPgDemoPlayOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPgDemoPlayOrder.Name = "tabPgDemoPlayOrder";
            this.tabPgDemoPlayOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgDemoPlayOrder.Size = new System.Drawing.Size(852, 411);
            this.tabPgDemoPlayOrder.TabIndex = 3;
            this.tabPgDemoPlayOrder.Text = "Demo Ordering and Playing";
            this.tabPgDemoPlayOrder.UseVisualStyleBackColor = true;
            // 
            // tabPgDemoChecks
            // 
            this.tabPgDemoChecks.Location = new System.Drawing.Point(4, 22);
            this.tabPgDemoChecks.Name = "tabPgDemoChecks";
            this.tabPgDemoChecks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgDemoChecks.Size = new System.Drawing.Size(852, 411);
            this.tabPgDemoChecks.TabIndex = 4;
            this.tabPgDemoChecks.Text = "Edit Demo Checks";
            this.tabPgDemoChecks.UseVisualStyleBackColor = true;
            // 
            // butHelp
            // 
            this.butHelp.Location = new System.Drawing.Point(796, 5);
            this.butHelp.Name = "butHelp";
            this.butHelp.Size = new System.Drawing.Size(75, 23);
            this.butHelp.TabIndex = 1;
            this.butHelp.Text = "Show Help";
            this.butHelp.UseVisualStyleBackColor = true;
            this.butHelp.Click += new System.EventHandler(this.butHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.butHelp);
            this.Controls.Add(this.tabCtrlMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "startdemos+";
            this.tabCtrlMaster.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrlMaster;
        private System.Windows.Forms.TabPage tabPgAbout;
        private System.Windows.Forms.TabPage tabPgGameHook;
        private System.Windows.Forms.TabPage tabPgDemoCollect;
        private System.Windows.Forms.TabPage tabPgDemoPlayOrder;
        private System.Windows.Forms.TabPage tabPgDemoChecks;
        private System.Windows.Forms.Button butHelp;
    }
}

