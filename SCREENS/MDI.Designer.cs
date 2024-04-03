namespace SGMOSOL
{
    partial class MDI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI));
            this.MainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.Tip = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bhojanalayaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printReceiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dengiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculationFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pctControls = new System.Windows.Forms.PictureBox();
            this.mainMenu2 = new System.Windows.Forms.MainMenu(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.totalDengiReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctControls)).BeginInit();
            this.SuspendLayout();
            // 
            // Tip
            // 
            this.Tip.ShowAlways = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bhojanalayaToolStripMenuItem,
            this.dengiToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bhojanalayaToolStripMenuItem
            // 
            this.bhojanalayaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printReceiptToolStripMenuItem});
            this.bhojanalayaToolStripMenuItem.Name = "bhojanalayaToolStripMenuItem";
            this.bhojanalayaToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.bhojanalayaToolStripMenuItem.Text = "Bhojanalaya";
            // 
            // printReceiptToolStripMenuItem
            // 
            this.printReceiptToolStripMenuItem.Name = "printReceiptToolStripMenuItem";
            this.printReceiptToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.printReceiptToolStripMenuItem.Text = "Print Receipt";
            this.printReceiptToolStripMenuItem.Click += new System.EventHandler(this.printReceiptToolStripMenuItem_Click);
            // 
            // dengiToolStripMenuItem
            // 
            this.dengiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalDengiReportToolStripMenuItem1});
            this.dengiToolStripMenuItem.Name = "dengiToolStripMenuItem";
            this.dengiToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.dengiToolStripMenuItem.Text = "Dengi";
            this.dengiToolStripMenuItem.Click += new System.EventHandler(this.dengiToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.calculationFormToolStripMenuItem,
            this.encryptionToolStripMenuItem1});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // calculationFormToolStripMenuItem
            // 
            this.calculationFormToolStripMenuItem.Name = "calculationFormToolStripMenuItem";
            this.calculationFormToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.calculationFormToolStripMenuItem.Text = "Calculation Form";
            this.calculationFormToolStripMenuItem.Click += new System.EventHandler(this.calculationFormToolStripMenuItem_Click);
            // 
            // encryptionToolStripMenuItem1
            // 
            this.encryptionToolStripMenuItem1.Name = "encryptionToolStripMenuItem1";
            this.encryptionToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.encryptionToolStripMenuItem1.Text = "Encryption";
            this.encryptionToolStripMenuItem1.Click += new System.EventHandler(this.encryptionToolStripMenuItem1_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pctControls
            // 
            this.pctControls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pctControls.BackgroundImage")));
            this.pctControls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pctControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pctControls.Image = ((System.Drawing.Image)(resources.GetObject("pctControls.Image")));
            this.pctControls.Location = new System.Drawing.Point(0, 24);
            this.pctControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pctControls.Name = "pctControls";
            this.pctControls.Size = new System.Drawing.Size(1020, 136);
            this.pctControls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctControls.TabIndex = 33;
            this.pctControls.TabStop = false;
            // 
            // toolTip2
            // 
            this.toolTip2.ShowAlways = true;
            // 
            // totalDengiReportToolStripMenuItem1
            // 
            this.totalDengiReportToolStripMenuItem1.Name = "totalDengiReportToolStripMenuItem1";
            this.totalDengiReportToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.totalDengiReportToolStripMenuItem1.Text = "Total Dengi Report";
            this.totalDengiReportToolStripMenuItem1.Click += new System.EventHandler(this.totalDengiReportToolStripMenuItem1_Click);
            // 
            // MDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 587);
            this.Controls.Add(this.pctControls);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Menu = this.mainMenu2;
            this.Name = "MDI";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDI_FormClosing);
            this.Load += new System.EventHandler(this.MDI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.ToolTip Tip;
        internal System.Windows.Forms.MainMenu MainMenu1;
        internal System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bhojanalayaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dengiToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        internal System.Windows.Forms.PictureBox pctControls;
        internal System.Windows.Forms.MainMenu mainMenu2;
        internal System.Windows.Forms.ToolTip toolTip2;
        internal System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printReceiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculationFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem totalDengiReportToolStripMenuItem1;
    }
}

