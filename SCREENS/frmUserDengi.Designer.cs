using System.Drawing;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS
{
    partial class frmUserDengi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserDengi));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnconvert = new System.Windows.Forms.Button();
            this.lblName1 = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.FlowLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlMobile = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMobile1 = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.pnlAddress = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAddress1 = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.pnlAmount = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAmount1 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.pnlDocument = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDocType = new System.Windows.Forms.Label();
            this.lblDocDetail = new System.Windows.Forms.Label();
            this.pnlPincode = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPincode1 = new System.Windows.Forms.Label();
            this.lblPincode = new System.Windows.Forms.Label();
            this.pnlTaluka = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTaluka1 = new System.Windows.Forms.Label();
            this.lblTaluka = new System.Windows.Forms.Label();
            this.pnlState = new System.Windows.Forms.FlowLayoutPanel();
            this.lblState1 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.pnlAmtWord = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDengiHead1 = new System.Windows.Forms.Label();
            this.lblDengiHead = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDistrict1 = new System.Windows.Forms.Label();
            this.lblDistrict = new System.Windows.Forms.Label();
            this.pnlGotra = new System.Windows.Forms.FlowLayoutPanel();
            this.lblGotra1 = new System.Windows.Forms.Label();
            this.lblGotra = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAmtWords = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.pnlMode = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.tableAdapterManager1 = new SGMOSOL.DataSet.DengiReceiptDataSetTableAdapters.TableAdapterManager();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlName.SuspendLayout();
            this.pnlMobile.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.pnlAmount.SuspendLayout();
            this.pnlDocument.SuspendLayout();
            this.pnlPincode.SuspendLayout();
            this.pnlTaluka.SuspendLayout();
            this.pnlState.SuspendLayout();
            this.pnlAmtWord.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.pnlGotra.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, -5);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1345, 114);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1337, 117);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnconvert
            // 
            this.btnconvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnconvert.Location = new System.Drawing.Point(1139, 805);
            this.btnconvert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnconvert.Name = "btnconvert";
            this.btnconvert.Size = new System.Drawing.Size(193, 41);
            this.btnconvert.TabIndex = 2;
            this.btnconvert.Text = "मराठी रूपांतर";
            this.btnconvert.UseVisualStyleBackColor = true;
            this.btnconvert.Visible = false;
            this.btnconvert.Click += new System.EventHandler(this.btnconvert_Click);
            // 
            // lblName1
            // 
            this.lblName1.AutoSize = true;
            this.lblName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblName1.Location = new System.Drawing.Point(3, 0);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(150, 31);
            this.lblName1.TabIndex = 0;
            this.lblName1.Text = "Name (नाव)";
            this.lblName1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlName.Controls.Add(this.lblName1);
            this.pnlName.Controls.Add(this.lblName);
            this.pnlName.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlName.Location = new System.Drawing.Point(19, 453);
            this.pnlName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(1313, 75);
            this.pnlName.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(159, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 51);
            this.lblName.TabIndex = 1;
            // 
            // pnlMobile
            // 
            this.pnlMobile.BackColor = System.Drawing.Color.Silver;
            this.pnlMobile.Controls.Add(this.lblMobile1);
            this.pnlMobile.Controls.Add(this.lblMobile);
            this.pnlMobile.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMobile.Location = new System.Drawing.Point(683, 537);
            this.pnlMobile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlMobile.Name = "pnlMobile";
            this.pnlMobile.Size = new System.Drawing.Size(647, 50);
            this.pnlMobile.TabIndex = 6;
            // 
            // lblMobile1
            // 
            this.lblMobile1.AutoSize = true;
            this.lblMobile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobile1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblMobile1.Location = new System.Drawing.Point(3, 0);
            this.lblMobile1.Name = "lblMobile1";
            this.lblMobile1.Size = new System.Drawing.Size(191, 31);
            this.lblMobile1.TabIndex = 1;
            this.lblMobile1.Text = "Mobile(मोबाईल)";
            this.lblMobile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobile.Location = new System.Drawing.Point(200, 0);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(0, 51);
            this.lblMobile.TabIndex = 2;
            // 
            // pnlAddress
            // 
            this.pnlAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlAddress.Controls.Add(this.lblAddress1);
            this.pnlAddress.Controls.Add(this.lblAddress);
            this.pnlAddress.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddress.Location = new System.Drawing.Point(19, 595);
            this.pnlAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(1312, 82);
            this.pnlAddress.TabIndex = 7;
            // 
            // lblAddress1
            // 
            this.lblAddress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAddress1.Location = new System.Drawing.Point(3, 0);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(350, 31);
            this.lblAddress1.TabIndex = 2;
            this.lblAddress1.Text = "Address(पत्ता)";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(359, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(0, 51);
            this.lblAddress.TabIndex = 3;
            // 
            // pnlAmount
            // 
            this.pnlAmount.BackColor = System.Drawing.Color.Silver;
            this.pnlAmount.Controls.Add(this.lblAmount1);
            this.pnlAmount.Controls.Add(this.lblAmount);
            this.pnlAmount.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAmount.Location = new System.Drawing.Point(20, 234);
            this.pnlAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlAmount.Name = "pnlAmount";
            this.pnlAmount.Size = new System.Drawing.Size(1312, 54);
            this.pnlAmount.TabIndex = 8;
            // 
            // lblAmount1
            // 
            this.lblAmount1.AutoSize = true;
            this.lblAmount1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAmount1.Location = new System.Drawing.Point(3, 0);
            this.lblAmount1.Name = "lblAmount1";
            this.lblAmount1.Size = new System.Drawing.Size(198, 31);
            this.lblAmount1.TabIndex = 0;
            this.lblAmount1.Text = "Amount (रक्कम)";
            this.lblAmount1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(207, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(0, 51);
            this.lblAmount.TabIndex = 1;
            // 
            // pnlDocument
            // 
            this.pnlDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlDocument.Controls.Add(this.lblDocType);
            this.pnlDocument.Controls.Add(this.lblDocDetail);
            this.pnlDocument.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDocument.Location = new System.Drawing.Point(20, 345);
            this.pnlDocument.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlDocument.Name = "pnlDocument";
            this.pnlDocument.Size = new System.Drawing.Size(648, 50);
            this.pnlDocument.TabIndex = 9;
            this.pnlDocument.Tag = "";
            // 
            // lblDocType
            // 
            this.lblDocType.AutoSize = true;
            this.lblDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocType.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDocType.Location = new System.Drawing.Point(3, 0);
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size(146, 31);
            this.lblDocType.TabIndex = 4;
            this.lblDocType.Text = "Document";
            // 
            // lblDocDetail
            // 
            this.lblDocDetail.AutoSize = true;
            this.lblDocDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocDetail.Location = new System.Drawing.Point(155, 0);
            this.lblDocDetail.Name = "lblDocDetail";
            this.lblDocDetail.Size = new System.Drawing.Size(0, 51);
            this.lblDocDetail.TabIndex = 3;
            // 
            // pnlPincode
            // 
            this.pnlPincode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlPincode.Controls.Add(this.lblPincode1);
            this.pnlPincode.Controls.Add(this.lblPincode);
            this.pnlPincode.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPincode.Location = new System.Drawing.Point(687, 747);
            this.pnlPincode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlPincode.Name = "pnlPincode";
            this.pnlPincode.Size = new System.Drawing.Size(645, 50);
            this.pnlPincode.TabIndex = 10;
            // 
            // lblPincode1
            // 
            this.lblPincode1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPincode1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPincode1.Location = new System.Drawing.Point(3, 0);
            this.lblPincode1.Name = "lblPincode1";
            this.lblPincode1.Size = new System.Drawing.Size(230, 31);
            this.lblPincode1.TabIndex = 1;
            this.lblPincode1.Text = "Pincode (पिन कोड)";
            this.lblPincode1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPincode
            // 
            this.lblPincode.AutoSize = true;
            this.lblPincode.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPincode.Location = new System.Drawing.Point(239, 0);
            this.lblPincode.Name = "lblPincode";
            this.lblPincode.Size = new System.Drawing.Size(0, 51);
            this.lblPincode.TabIndex = 2;
            // 
            // pnlTaluka
            // 
            this.pnlTaluka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlTaluka.Controls.Add(this.lblTaluka1);
            this.pnlTaluka.Controls.Add(this.lblTaluka);
            this.pnlTaluka.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTaluka.Location = new System.Drawing.Point(19, 743);
            this.pnlTaluka.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTaluka.Name = "pnlTaluka";
            this.pnlTaluka.Size = new System.Drawing.Size(663, 50);
            this.pnlTaluka.TabIndex = 11;
            // 
            // lblTaluka1
            // 
            this.lblTaluka1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaluka1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTaluka1.Location = new System.Drawing.Point(3, 0);
            this.lblTaluka1.Name = "lblTaluka1";
            this.lblTaluka1.Size = new System.Drawing.Size(195, 31);
            this.lblTaluka1.TabIndex = 1;
            this.lblTaluka1.Text = "Taluka (तालुका)";
            this.lblTaluka1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTaluka
            // 
            this.lblTaluka.AutoSize = true;
            this.lblTaluka.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaluka.Location = new System.Drawing.Point(204, 0);
            this.lblTaluka.Name = "lblTaluka";
            this.lblTaluka.Size = new System.Drawing.Size(0, 51);
            this.lblTaluka.TabIndex = 2;
            // 
            // pnlState
            // 
            this.pnlState.BackColor = System.Drawing.Color.Silver;
            this.pnlState.Controls.Add(this.lblState1);
            this.pnlState.Controls.Add(this.lblState);
            this.pnlState.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlState.Location = new System.Drawing.Point(19, 689);
            this.pnlState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(664, 50);
            this.pnlState.TabIndex = 12;
            // 
            // lblState1
            // 
            this.lblState1.AutoSize = true;
            this.lblState1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblState1.Location = new System.Drawing.Point(3, 0);
            this.lblState1.Name = "lblState1";
            this.lblState1.Size = new System.Drawing.Size(152, 31);
            this.lblState1.TabIndex = 0;
            this.lblState1.Text = "State (राज्य)";
            this.lblState1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(161, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(0, 51);
            this.lblState.TabIndex = 3;
            // 
            // pnlAmtWord
            // 
            this.pnlAmtWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlAmtWord.Controls.Add(this.lblDengiHead1);
            this.pnlAmtWord.Controls.Add(this.lblDengiHead);
            this.pnlAmtWord.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAmtWord.Location = new System.Drawing.Point(675, 345);
            this.pnlAmtWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlAmtWord.Name = "pnlAmtWord";
            this.pnlAmtWord.Size = new System.Drawing.Size(656, 50);
            this.pnlAmtWord.TabIndex = 13;
            // 
            // lblDengiHead1
            // 
            this.lblDengiHead1.AutoSize = true;
            this.lblDengiHead1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDengiHead1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDengiHead1.Location = new System.Drawing.Point(3, 0);
            this.lblDengiHead1.Name = "lblDengiHead1";
            this.lblDengiHead1.Size = new System.Drawing.Size(258, 31);
            this.lblDengiHead1.TabIndex = 2;
            this.lblDengiHead1.Text = "Dengi Type (तपशिल)";
            // 
            // lblDengiHead
            // 
            this.lblDengiHead.AutoSize = true;
            this.lblDengiHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDengiHead.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDengiHead.Location = new System.Drawing.Point(267, 0);
            this.lblDengiHead.Name = "lblDengiHead";
            this.lblDengiHead.Size = new System.Drawing.Size(0, 36);
            this.lblDengiHead.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Silver;
            this.flowLayoutPanel2.Controls.Add(this.lblDistrict1);
            this.flowLayoutPanel2.Controls.Add(this.lblDistrict);
            this.flowLayoutPanel2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel2.Location = new System.Drawing.Point(691, 689);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(641, 50);
            this.flowLayoutPanel2.TabIndex = 14;
            // 
            // lblDistrict1
            // 
            this.lblDistrict1.AutoSize = true;
            this.lblDistrict1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrict1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDistrict1.Location = new System.Drawing.Point(3, 0);
            this.lblDistrict1.Name = "lblDistrict1";
            this.lblDistrict1.Size = new System.Drawing.Size(188, 31);
            this.lblDistrict1.TabIndex = 0;
            this.lblDistrict1.Text = "District (जिल्हा)";
            this.lblDistrict1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDistrict
            // 
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrict.Location = new System.Drawing.Point(197, 0);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(0, 51);
            this.lblDistrict.TabIndex = 3;
            // 
            // pnlGotra
            // 
            this.pnlGotra.BackColor = System.Drawing.Color.Silver;
            this.pnlGotra.Controls.Add(this.lblGotra1);
            this.pnlGotra.Controls.Add(this.lblGotra);
            this.pnlGotra.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGotra.Location = new System.Drawing.Point(20, 537);
            this.pnlGotra.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGotra.Name = "pnlGotra";
            this.pnlGotra.Size = new System.Drawing.Size(659, 50);
            this.pnlGotra.TabIndex = 15;
            // 
            // lblGotra1
            // 
            this.lblGotra1.AutoSize = true;
            this.lblGotra1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGotra1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblGotra1.Location = new System.Drawing.Point(3, 0);
            this.lblGotra1.Name = "lblGotra1";
            this.lblGotra1.Size = new System.Drawing.Size(147, 31);
            this.lblGotra1.TabIndex = 1;
            this.lblGotra1.Text = "Gotra (गोत्र)";
            this.lblGotra1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGotra
            // 
            this.lblGotra.AutoSize = true;
            this.lblGotra.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGotra.Location = new System.Drawing.Point(156, 0);
            this.lblGotra.Name = "lblGotra";
            this.lblGotra.Size = new System.Drawing.Size(0, 39);
            this.lblGotra.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.Silver;
            this.flowLayoutPanel3.Controls.Add(this.lblAmtWords);
            this.flowLayoutPanel3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel3.Location = new System.Drawing.Point(20, 289);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1312, 49);
            this.flowLayoutPanel3.TabIndex = 16;
            // 
            // lblAmtWords
            // 
            this.lblAmtWords.AutoSize = true;
            this.lblAmtWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtWords.Location = new System.Drawing.Point(3, 0);
            this.lblAmtWords.Name = "lblAmtWords";
            this.lblAmtWords.Size = new System.Drawing.Size(0, 51);
            this.lblAmtWords.TabIndex = 5;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel4.Controls.Add(this.label1);
            this.flowLayoutPanel4.Controls.Add(this.lblValue);
            this.flowLayoutPanel4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel4.Location = new System.Drawing.Point(20, 118);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1312, 108);
            this.flowLayoutPanel4.TabIndex = 17;
            this.flowLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel4_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 31);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 38.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(9, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(396, 73);
            this.lblValue.TabIndex = 1;
            this.lblValue.Text = "Display Text";
            this.lblValue.Click += new System.EventHandler(this.lblValue_Click);
            // 
            // pnlMode
            // 
            this.pnlMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlMode.Controls.Add(this.label2);
            this.pnlMode.Controls.Add(this.lblMode);
            this.pnlMode.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMode.Location = new System.Drawing.Point(19, 398);
            this.pnlMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(1311, 50);
            this.pnlMode.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Payment Mode (पेमेंट मोड)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Location = new System.Drawing.Point(293, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(0, 51);
            this.lblMode.TabIndex = 1;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.Connection = null;
            this.tableAdapterManager1.UpdateOrder = SGMOSOL.DataSet.DengiReceiptDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // frmUserDengi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(1371, 837);
            this.Controls.Add(this.pnlGotra);
            this.Controls.Add(this.pnlAmount);
            this.Controls.Add(this.pnlMobile);
            this.Controls.Add(this.pnlPincode);
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.pnlMode);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.pnlAmtWord);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.pnlState);
            this.Controls.Add(this.pnlTaluka);
            this.Controls.Add(this.pnlDocument);
            this.Controls.Add(this.pnlAddress);
            this.Controls.Add(this.btnconvert);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmUserDengi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dengi Receipt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUserDengi_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUserDengi_FormClosed);
            this.Load += new System.EventHandler(this.frmUserDengi_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.pnlMobile.ResumeLayout(false);
            this.pnlMobile.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.pnlAmount.ResumeLayout(false);
            this.pnlAmount.PerformLayout();
            this.pnlDocument.ResumeLayout(false);
            this.pnlDocument.PerformLayout();
            this.pnlPincode.ResumeLayout(false);
            this.pnlPincode.PerformLayout();
            this.pnlTaluka.ResumeLayout(false);
            this.pnlTaluka.PerformLayout();
            this.pnlState.ResumeLayout(false);
            this.pnlState.PerformLayout();
            this.pnlAmtWord.ResumeLayout(false);
            this.pnlAmtWord.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.pnlGotra.ResumeLayout(false);
            this.pnlGotra.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.pnlMode.ResumeLayout(false);
            this.pnlMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnconvert;
        private System.Windows.Forms.Label lblName1;
        private System.Windows.Forms.FlowLayoutPanel pnlName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.FlowLayoutPanel pnlMobile;
        private System.Windows.Forms.Label lblMobile1;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.FlowLayoutPanel pnlAddress;
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.FlowLayoutPanel pnlAmount;
        private System.Windows.Forms.Label lblAmount1;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.FlowLayoutPanel pnlDocument;
        private System.Windows.Forms.Label lblDocDetail;
        private System.Windows.Forms.FlowLayoutPanel pnlPincode;
        private System.Windows.Forms.Label lblPincode1;
        private System.Windows.Forms.Label lblPincode;
        private System.Windows.Forms.FlowLayoutPanel pnlTaluka;
        private System.Windows.Forms.Label lblTaluka1;
        private System.Windows.Forms.Label lblTaluka;
        private FlowLayoutPanel pnlState;
        private Label lblState1;
        private Label lblState;
        private FlowLayoutPanel pnlAmtWord;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblDistrict1;
        private Label lblDistrict;
        private Label lblDengiHead1;
        private FlowLayoutPanel pnlGotra;
        private Label lblGotra1;
        private Label lblGotra;
        private Label lblDengiHead;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label lblAmtWords;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label label1;
        private Label lblValue;
        private FlowLayoutPanel pnlMode;
        private Label label2;
        private Label lblMode;
        private Label lblDocType;
        private DataSet.DengiReceiptDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
    }
}