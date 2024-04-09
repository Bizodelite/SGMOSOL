namespace SGMOSOL.SCREENS.BedSystem
{
    partial class dgScanPrievew
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
            this.pbImgPrev = new System.Windows.Forms.PictureBox();
            this.OK_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImgPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImgPrev
            // 
            this.pbImgPrev.Location = new System.Drawing.Point(12, 12);
            this.pbImgPrev.Name = "pbImgPrev";
            this.pbImgPrev.Size = new System.Drawing.Size(502, 306);
            this.pbImgPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImgPrev.TabIndex = 3;
            this.pbImgPrev.TabStop = false;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(447, 324);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 2;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // dgScanPrievew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 358);
            this.Controls.Add(this.pbImgPrev);
            this.Controls.Add(this.OK_Button);
            this.Name = "dgScanPrievew";
            this.Text = "Scan Prievew";
            this.Load += new System.EventHandler(this.dgScanPrievew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImgPrev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pbImgPrev;
        internal System.Windows.Forms.Button OK_Button;
    }
}