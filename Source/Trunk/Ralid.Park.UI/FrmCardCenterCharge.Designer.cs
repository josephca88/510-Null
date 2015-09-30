﻿namespace Ralid.Park.UI
{
    partial class FrmCardCenterCharge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardCenterCharge));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.paymentPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRepay = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnCoupon = new System.Windows.Forms.Button();
            this.btnPos = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.btnYCT = new System.Windows.Forms.Button();
            this.carTypePanel1 = new Ralid.Park.UserControls.CarTypePanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.lblOwnerName = new System.Windows.Forms.Label();
            this.lblCarNum = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.lblExitDateTime = new System.Windows.Forms.Label();
            this.lblParkingTime = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblTariffType = new System.Windows.Forms.Label();
            this.lblLastTotalPaid = new System.Windows.Forms.Label();
            this.lblAccounts = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblLastWorkstation = new System.Windows.Forms.Label();
            this.txtPaid = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.comPark = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrDiscountHour = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblDiscountMemo = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCarPlateList = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.ucapmMonitor1 = new Ralid.Park.UserControls.UCAPMMonitor();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.GridView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntranceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitTop = new System.Windows.Forms.Splitter();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.picIn = new Ralid.Park.UserControls.UCPictureGrid();
            this.spliterLeft = new System.Windows.Forms.Splitter();
            this.paymentPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.videoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentPanel
            // 
            this.paymentPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.paymentPanel.Controls.Add(this.panel1);
            this.paymentPanel.Controls.Add(this.tableLayoutPanel1);
            this.paymentPanel.Controls.Add(this.panel5);
            resources.ApplyResources(this.paymentPanel, "paymentPanel");
            this.paymentPanel.Name = "paymentPanel";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.carTypePanel1);
            this.panel1.Name = "panel1";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnCancel);
            this.panel7.Controls.Add(this.btnRepay);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            this.panel7.Resize += new System.EventHandler(this.panel7_Resize);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRepay
            // 
            this.btnRepay.BackColor = System.Drawing.SystemColors.Control;
            this.btnRepay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnRepay, "btnRepay");
            this.btnRepay.Name = "btnRepay";
            this.btnRepay.UseVisualStyleBackColor = false;
            this.btnRepay.Click += new System.EventHandler(this.btnRepay_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnCoupon);
            this.panel6.Controls.Add(this.btnPos);
            this.panel6.Controls.Add(this.btnCash);
            this.panel6.Controls.Add(this.btnYCT);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            this.panel6.Resize += new System.EventHandler(this.panel6_Resize);
            // 
            // btnCoupon
            // 
            this.btnCoupon.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnCoupon, "btnCoupon");
            this.btnCoupon.Name = "btnCoupon";
            this.btnCoupon.UseVisualStyleBackColor = false;
            this.btnCoupon.Click += new System.EventHandler(this.btnCoupon_Click);
            // 
            // btnPos
            // 
            this.btnPos.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnPos, "btnPos");
            this.btnPos.Name = "btnPos";
            this.btnPos.UseVisualStyleBackColor = false;
            this.btnPos.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnCash, "btnCash");
            this.btnCash.Name = "btnCash";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.EnabledChanged += new System.EventHandler(this.btnCash_EnabledChanged);
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // btnYCT
            // 
            this.btnYCT.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnYCT, "btnYCT");
            this.btnYCT.Name = "btnYCT";
            this.btnYCT.UseVisualStyleBackColor = false;
            this.btnYCT.Click += new System.EventHandler(this.btnYCT_Click);
            // 
            // carTypePanel1
            // 
            this.carTypePanel1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.carTypePanel1, "carTypePanel1");
            this.carTypePanel1.Name = "carTypePanel1";
            this.carTypePanel1.CarTypeSelectedChanged += new System.EventHandler(this.CarType_Selected);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.Controls.Add(this.txtCertificate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtCardID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOwnerName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCarNum, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterDateTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblExitDateTime, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblParkingTime, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblCardType, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblTariffType, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblLastTotalPaid, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblAccounts, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscount, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.txtMemo, 1, 17);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblLastWorkstation, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtPaid, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 17);
            this.tableLayoutPanel1.Controls.Add(this.comPark, 1, 18);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 18);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrDiscountHour, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 16);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscountMemo, 1, 16);
            this.tableLayoutPanel1.Controls.Add(this.label20, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // txtCertificate
            // 
            this.txtCertificate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtCertificate, "txtCertificate");
            this.txtCertificate.Name = "txtCertificate";
            this.txtCertificate.TextChanged += new System.EventHandler(this.txtCertificate_TextChanged);
            this.txtCertificate.Enter += new System.EventHandler(this.txt_Enter);
            this.txtCertificate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtCardID
            // 
            this.txtCardID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            this.txtCardID.Enter += new System.EventHandler(this.txt_Enter);
            this.txtCardID.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblOwnerName
            // 
            resources.ApplyResources(this.lblOwnerName, "lblOwnerName");
            this.lblOwnerName.Name = "lblOwnerName";
            // 
            // lblCarNum
            // 
            resources.ApplyResources(this.lblCarNum, "lblCarNum");
            this.lblCarNum.Name = "lblCarNum";
            // 
            // lblEnterDateTime
            // 
            resources.ApplyResources(this.lblEnterDateTime, "lblEnterDateTime");
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            // 
            // lblExitDateTime
            // 
            resources.ApplyResources(this.lblExitDateTime, "lblExitDateTime");
            this.lblExitDateTime.Name = "lblExitDateTime";
            // 
            // lblParkingTime
            // 
            resources.ApplyResources(this.lblParkingTime, "lblParkingTime");
            this.lblParkingTime.ForeColor = System.Drawing.Color.Blue;
            this.lblParkingTime.Name = "lblParkingTime";
            // 
            // lblCardType
            // 
            resources.ApplyResources(this.lblCardType, "lblCardType");
            this.lblCardType.Name = "lblCardType";
            // 
            // lblTariffType
            // 
            resources.ApplyResources(this.lblTariffType, "lblTariffType");
            this.lblTariffType.Name = "lblTariffType";
            // 
            // lblLastTotalPaid
            // 
            resources.ApplyResources(this.lblLastTotalPaid, "lblLastTotalPaid");
            this.lblLastTotalPaid.Name = "lblLastTotalPaid";
            // 
            // lblAccounts
            // 
            resources.ApplyResources(this.lblAccounts, "lblAccounts");
            this.lblAccounts.ForeColor = System.Drawing.Color.Blue;
            this.lblAccounts.Name = "lblAccounts";
            // 
            // lblDiscount
            // 
            resources.ApplyResources(this.lblDiscount, "lblDiscount");
            this.lblDiscount.ForeColor = System.Drawing.Color.Red;
            this.lblDiscount.Name = "lblDiscount";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.FormattingEnabled = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Enter += new System.EventHandler(this.txt_Enter);
            this.txtMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMemo_KeyDown);
            this.txtMemo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // lblLastWorkstation
            // 
            resources.ApplyResources(this.lblLastWorkstation, "lblLastWorkstation");
            this.lblLastWorkstation.Name = "lblLastWorkstation";
            // 
            // txtPaid
            // 
            resources.ApplyResources(this.txtPaid, "txtPaid");
            this.txtPaid.ForeColor = System.Drawing.Color.Blue;
            this.txtPaid.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtPaid.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.NumberWithCommas = true;
            this.txtPaid.PointCount = 2;
            this.txtPaid.TextChanged += new System.EventHandler(this.txtPaid_TextChanged);
            this.txtPaid.Enter += new System.EventHandler(this.txt_Enter);
            this.txtPaid.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // comPark
            // 
            resources.ApplyResources(this.comPark, "comPark");
            this.comPark.FormattingEnabled = true;
            this.comPark.Name = "comPark";
            this.comPark.SelectedIndexChanged += new System.EventHandler(this.parkCombobox1_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblCurrDiscountHour
            // 
            resources.ApplyResources(this.lblCurrDiscountHour, "lblCurrDiscountHour");
            this.lblCurrDiscountHour.Name = "lblCurrDiscountHour";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // lblDiscountMemo
            // 
            resources.ApplyResources(this.lblDiscountMemo, "lblDiscountMemo");
            this.lblDiscountMemo.Name = "lblDiscountMemo";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel5.Controls.Add(this.btnCarPlateList);
            this.panel5.Controls.Add(this.label15);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // btnCarPlateList
            // 
            resources.ApplyResources(this.btnCarPlateList, "btnCarPlateList");
            this.btnCarPlateList.Name = "btnCarPlateList";
            this.btnCarPlateList.UseVisualStyleBackColor = true;
            this.btnCarPlateList.Click += new System.EventHandler(this.btnCarPlateList_Click);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.splitTop);
            this.panel4.Controls.Add(this.videoPanel);
            this.panel4.Controls.Add(this.spliterLeft);
            this.panel4.Controls.Add(this.paymentPanel);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.eventList);
            this.panel3.Controls.Add(this.ucapmMonitor1);
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.GridView);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // eventList
            // 
            resources.ApplyResources(this.eventList, "eventList");
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.FormattingEnabled = true;
            this.eventList.Name = "eventList";
            // 
            // ucapmMonitor1
            // 
            this.ucapmMonitor1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.ucapmMonitor1, "ucapmMonitor1");
            this.ucapmMonitor1.Name = "ucapmMonitor1";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.AllowUserToResizeRows = false;
            this.GridView.BackgroundColor = System.Drawing.Color.White;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colCardType,
            this.colEventDateTime,
            this.colEntranceName,
            this.colAccounts,
            this.colLastCarPlate,
            this.colCarPlate,
            this.colOperatorID});
            resources.ApplyResources(this.GridView, "GridView");
            this.GridView.Name = "GridView";
            this.GridView.RowHeadersVisible = false;
            this.GridView.RowTemplate.Height = 23;
            this.GridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_CellDoubleClick);
            // 
            // colCardID
            // 
            this.colCardID.DataPropertyName = "CardID";
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            this.colCardID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCardType
            // 
            this.colCardType.DataPropertyName = "CardType";
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            this.colCardType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colEventDateTime
            // 
            this.colEventDateTime.DataPropertyName = "EventDateTime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colEventDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colEventDateTime, "colEventDateTime");
            this.colEventDateTime.Name = "colEventDateTime";
            this.colEventDateTime.ReadOnly = true;
            this.colEventDateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colEntranceName
            // 
            this.colEntranceName.DataPropertyName = "EventAddress";
            resources.ApplyResources(this.colEntranceName, "colEntranceName");
            this.colEntranceName.Name = "colEntranceName";
            this.colEntranceName.ReadOnly = true;
            this.colEntranceName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAccounts
            // 
            resources.ApplyResources(this.colAccounts, "colAccounts");
            this.colAccounts.Name = "colAccounts";
            this.colAccounts.ReadOnly = true;
            this.colAccounts.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLastCarPlate
            // 
            resources.ApplyResources(this.colLastCarPlate, "colLastCarPlate");
            this.colLastCarPlate.Name = "colLastCarPlate";
            this.colLastCarPlate.ReadOnly = true;
            this.colLastCarPlate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            this.colCarPlate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colOperatorID
            // 
            this.colOperatorID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOperatorID.DataPropertyName = "OperatorNum";
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            this.colOperatorID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitTop
            // 
            this.splitTop.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.splitTop, "splitTop");
            this.splitTop.Name = "splitTop";
            this.splitTop.TabStop = false;
            // 
            // videoPanel
            // 
            this.videoPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.videoPanel.Controls.Add(this.picIn);
            resources.ApplyResources(this.videoPanel, "videoPanel");
            this.videoPanel.Name = "videoPanel";
            // 
            // picIn
            // 
            resources.ApplyResources(this.picIn, "picIn");
            this.picIn.Name = "picIn";
            // 
            // spliterLeft
            // 
            this.spliterLeft.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.spliterLeft, "spliterLeft");
            this.spliterLeft.Name = "spliterLeft";
            this.spliterLeft.TabStop = false;
            // 
            // FrmCardCenterCharge
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.panel4);
            this.KeyPreview = true;
            this.Name = "FrmCardCenterCharge";
            this.Activated += new System.EventHandler(this.FrmCardPaying_Activated);
            this.Deactivate += new System.EventHandler(this.FrmCardPaying_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardCenterCharge_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCardCenterCharge_FormClosed);
            this.Load += new System.EventHandler(this.FrmCardCenterCharge_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCardCenterCharge_KeyDown);
            this.paymentPanel.ResumeLayout(false);
            this.paymentPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.videoPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paymentPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitTop;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.Splitter spliterLeft;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private Ralid.Park.UserControls.CustomDataGridView GridView;
        private Ralid.Park.UserControls.UCPictureGrid picIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRepay;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.Button btnYCT;
        private UserControls.CarTypePanel carTypePanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label lblOwnerName;
        private System.Windows.Forms.Label lblCarNum;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblExitDateTime;
        private System.Windows.Forms.Label lblParkingTime;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblTariffType;
        private System.Windows.Forms.Label lblLastTotalPaid;
        private System.Windows.Forms.Label lblAccounts;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.ComboBox txtMemo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblLastWorkstation;
        private GeneralLibrary.WinformControl.DecimalTextBox txtPaid;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private UserControls.EventReportListBox eventList;
        private UserControls.UCAPMMonitor ucapmMonitor1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.Panel panel1;
        private UserControls.ParkCombobox comPark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCurrDiscountHour;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblDiscountMemo;
        private System.Windows.Forms.Button btnCarPlateList;
        private System.Windows.Forms.Button btnPos;
        private System.Windows.Forms.Button btnCoupon;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label20;
    }
}