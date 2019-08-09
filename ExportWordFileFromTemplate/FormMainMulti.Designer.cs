﻿using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    partial class FormMainMulti : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainMulti));
            this.lvUser = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkSenderResidentYN = new System.Windows.Forms.CheckBox();
            this.cboDanhXungUQ2 = new System.Windows.Forms.ComboBox();
            this.txtHoVaTenUQ2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNgaySinhUQ2 = new System.Windows.Forms.TextBox();
            this.cboLoaiGiayToUQ2 = new System.Windows.Forms.ComboBox();
            this.txtSoGiayToUQ2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboNoiCapUQ2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNgayCapUQ2 = new System.Windows.Forms.TextBox();
            this.cboDanhXungUQ1 = new System.Windows.Forms.ComboBox();
            this.txtHoVaTenUQ1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNgaySinhUQ1 = new System.Windows.Forms.TextBox();
            this.cboLoaiGiayToUQ1 = new System.Windows.Forms.ComboBox();
            this.txtSoGiayToUQ1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboNoiCapUQ1 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNgayCapUQ1 = new System.Windows.Forms.TextBox();
            this.lbCuTruOng = new System.Windows.Forms.Label();
            this.cboCuTruUQ1 = new System.Windows.Forms.ComboBox();
            this.lbCuTruBa = new System.Windows.Forms.Label();
            this.cboCuTruUQ2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lbDaiDien = new System.Windows.Forms.Label();
            this.btnXoa1 = new System.Windows.Forms.Button();
            this.btnXoa2 = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvUser
            // 
            this.lvUser.AllowDrop = true;
            this.lvUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.Id,
            this.columnHeader15});
            this.lvUser.Location = new System.Drawing.Point(10, 252);
            this.lvUser.Name = "lvUser";
            this.lvUser.Size = new System.Drawing.Size(1539, 356);
            this.lvUser.TabIndex = 0;
            this.lvUser.UseCompatibleStateImageBehavior = false;
            this.lvUser.View = System.Windows.Forms.View.Details;
            this.lvUser.SelectedIndexChanged += new System.EventHandler(this.lvUser_SelectedIndexChanged_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Danh Xưng 1";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Họ Và Tên 1";
            this.columnHeader2.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Năm Sinh 1";
            this.columnHeader3.Width = 101;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Loại Giấy Tờ 1";
            this.columnHeader4.Width = 115;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Số Giấy Tờ 1";
            this.columnHeader5.Width = 108;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Nơi Cấp 1";
            this.columnHeader6.Width = 84;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Ngày Cấp 1";
            this.columnHeader7.Width = 106;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Danh Xưng 2";
            this.columnHeader8.Width = 112;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Họ Và Tên 2";
            this.columnHeader9.Width = 109;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Năm Sinh 2";
            this.columnHeader10.Width = 110;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Loại Giấy Tờ 2";
            this.columnHeader11.Width = 122;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Số Giấy Tờ 2";
            this.columnHeader12.Width = 106;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Nơi Cấp 2";
            this.columnHeader13.Width = 83;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Ngày Cấp 2";
            this.columnHeader14.Width = 118;
            // 
            // Id
            // 
            this.Id.Width = 0;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Tài Sản Riêng";
            // 
            // chkSenderResidentYN
            // 
            this.chkSenderResidentYN.AutoSize = true;
            this.chkSenderResidentYN.Location = new System.Drawing.Point(10, 144);
            this.chkSenderResidentYN.Name = "chkSenderResidentYN";
            this.chkSenderResidentYN.Size = new System.Drawing.Size(91, 24);
            this.chkSenderResidentYN.TabIndex = 14;
            this.chkSenderResidentYN.Text = "2 Cư trú";
            this.chkSenderResidentYN.UseVisualStyleBackColor = true;
            this.chkSenderResidentYN.CheckedChanged += new System.EventHandler(this.chkSenderResidentYN_CheckedChanged);
            // 
            // cboDanhXungUQ2
            // 
            this.cboDanhXungUQ2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhXungUQ2.FormattingEnabled = true;
            this.cboDanhXungUQ2.Items.AddRange(new object[] {
            "Ông",
            "Bà",
            "Cùng vợ là bà",
            "Cùng chồng là ông",
            "Tài sản riêng"});
            this.cboDanhXungUQ2.Location = new System.Drawing.Point(10, 81);
            this.cboDanhXungUQ2.Name = "cboDanhXungUQ2";
            this.cboDanhXungUQ2.Size = new System.Drawing.Size(84, 28);
            this.cboDanhXungUQ2.TabIndex = 8;
            this.cboDanhXungUQ2.SelectedIndexChanged += new System.EventHandler(this.cboDanhXungUQ2_SelectedIndexChanged);
            // 
            // txtHoVaTenUQ2
            // 
            this.txtHoVaTenUQ2.Location = new System.Drawing.Point(100, 83);
            this.txtHoVaTenUQ2.Name = "txtHoVaTenUQ2";
            this.txtHoVaTenUQ2.Size = new System.Drawing.Size(180, 26);
            this.txtHoVaTenUQ2.TabIndex = 9;
            this.txtHoVaTenUQ2.TextChanged += new System.EventHandler(this.txtHoVaTenUQ2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(308, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Năm sinh";
            // 
            // txtNgaySinhUQ2
            // 
            this.txtNgaySinhUQ2.Location = new System.Drawing.Point(389, 86);
            this.txtNgaySinhUQ2.Name = "txtNgaySinhUQ2";
            this.txtNgaySinhUQ2.Size = new System.Drawing.Size(136, 26);
            this.txtNgaySinhUQ2.TabIndex = 10;
            this.txtNgaySinhUQ2.TextChanged += new System.EventHandler(this.txtNgaySinhUQ2_TextChanged);
            // 
            // cboLoaiGiayToUQ2
            // 
            this.cboLoaiGiayToUQ2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGiayToUQ2.FormattingEnabled = true;
            this.cboLoaiGiayToUQ2.Items.AddRange(new object[] {
            "CMND",
            "Hộ Chiếu",
            "CCCD"});
            this.cboLoaiGiayToUQ2.Location = new System.Drawing.Point(543, 86);
            this.cboLoaiGiayToUQ2.Name = "cboLoaiGiayToUQ2";
            this.cboLoaiGiayToUQ2.Size = new System.Drawing.Size(97, 28);
            this.cboLoaiGiayToUQ2.TabIndex = 24;
            this.cboLoaiGiayToUQ2.SelectedIndexChanged += new System.EventHandler(this.cboLoaiGiayToUQ2_SelectedIndexChanged);
            // 
            // txtSoGiayToUQ2
            // 
            this.txtSoGiayToUQ2.Location = new System.Drawing.Point(646, 86);
            this.txtSoGiayToUQ2.Name = "txtSoGiayToUQ2";
            this.txtSoGiayToUQ2.Size = new System.Drawing.Size(132, 26);
            this.txtSoGiayToUQ2.TabIndex = 11;
            this.txtSoGiayToUQ2.TextChanged += new System.EventHandler(this.txtSoGiayToUQ2_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(820, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.TabIndex = 28;
            this.label7.Text = "Cấp tại";
            // 
            // cboNoiCapUQ2
            // 
            this.cboNoiCapUQ2.FormattingEnabled = true;
            this.cboNoiCapUQ2.Items.AddRange(new object[] {
            "Công an tỉnh Tây Ninh",
            "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư",
            "Phòng quản lý Xuất nhập cảnh Tây Ninh",
            "Cục quản lý Xuất nhập cảnh",
            "Công an Thành phố Hồ Chí Minh",
            "Công An tỉnh Long An",
            "Công an tỉnh Bình Dương",
            "Công an tỉnh Bình Phước",
            "Công an tỉnh Đồng Nai",
            "Công an tỉnh Bà Rịa - Vũng Tàu",
            "Công an tỉnh Bình Phước"});
            this.cboNoiCapUQ2.Location = new System.Drawing.Point(885, 83);
            this.cboNoiCapUQ2.Name = "cboNoiCapUQ2";
            this.cboNoiCapUQ2.Size = new System.Drawing.Size(248, 28);
            this.cboNoiCapUQ2.TabIndex = 12;
            this.cboNoiCapUQ2.SelectedIndexChanged += new System.EventHandler(this.cboNoiCapUQ2_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1174, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "Ngày cấp";
            // 
            // txtNgayCapUQ2
            // 
            this.txtNgayCapUQ2.Location = new System.Drawing.Point(1255, 83);
            this.txtNgayCapUQ2.Name = "txtNgayCapUQ2";
            this.txtNgayCapUQ2.Size = new System.Drawing.Size(132, 26);
            this.txtNgayCapUQ2.TabIndex = 13;
            this.txtNgayCapUQ2.TextChanged += new System.EventHandler(this.txtNgayCapUQ2_TextChanged);
            // 
            // cboDanhXungUQ1
            // 
            this.cboDanhXungUQ1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhXungUQ1.FormattingEnabled = true;
            this.cboDanhXungUQ1.Items.AddRange(new object[] {
            "Ông",
            "Bà"});
            this.cboDanhXungUQ1.Location = new System.Drawing.Point(10, 37);
            this.cboDanhXungUQ1.Name = "cboDanhXungUQ1";
            this.cboDanhXungUQ1.Size = new System.Drawing.Size(84, 28);
            this.cboDanhXungUQ1.TabIndex = 1;
            this.cboDanhXungUQ1.SelectedIndexChanged += new System.EventHandler(this.cboDanhXungUQ1_SelectedIndexChanged);
            // 
            // txtHoVaTenUQ1
            // 
            this.txtHoVaTenUQ1.Location = new System.Drawing.Point(100, 39);
            this.txtHoVaTenUQ1.Name = "txtHoVaTenUQ1";
            this.txtHoVaTenUQ1.Size = new System.Drawing.Size(180, 26);
            this.txtHoVaTenUQ1.TabIndex = 2;
            this.txtHoVaTenUQ1.TextChanged += new System.EventHandler(this.txtHoVaTenUQ1_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 20);
            this.label10.TabIndex = 5;
            this.label10.Text = "Năm sinh";
            // 
            // txtNgaySinhUQ1
            // 
            this.txtNgaySinhUQ1.Location = new System.Drawing.Point(389, 42);
            this.txtNgaySinhUQ1.Name = "txtNgaySinhUQ1";
            this.txtNgaySinhUQ1.Size = new System.Drawing.Size(136, 26);
            this.txtNgaySinhUQ1.TabIndex = 3;
            this.txtNgaySinhUQ1.TextChanged += new System.EventHandler(this.txtNgaySinhUQ1_TextChanged);
            // 
            // cboLoaiGiayToUQ1
            // 
            this.cboLoaiGiayToUQ1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGiayToUQ1.FormattingEnabled = true;
            this.cboLoaiGiayToUQ1.Items.AddRange(new object[] {
            "CMND",
            "Hộ Chiếu",
            "CCCD"});
            this.cboLoaiGiayToUQ1.Location = new System.Drawing.Point(543, 42);
            this.cboLoaiGiayToUQ1.Name = "cboLoaiGiayToUQ1";
            this.cboLoaiGiayToUQ1.Size = new System.Drawing.Size(97, 28);
            this.cboLoaiGiayToUQ1.TabIndex = 4;
            this.cboLoaiGiayToUQ1.SelectedIndexChanged += new System.EventHandler(this.cboLoaiGiayToUQ1_SelectedIndexChanged);
            // 
            // txtSoGiayToUQ1
            // 
            this.txtSoGiayToUQ1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoGiayToUQ1.Location = new System.Drawing.Point(646, 45);
            this.txtSoGiayToUQ1.Name = "txtSoGiayToUQ1";
            this.txtSoGiayToUQ1.Size = new System.Drawing.Size(132, 26);
            this.txtSoGiayToUQ1.TabIndex = 5;
            this.txtSoGiayToUQ1.TextChanged += new System.EventHandler(this.txtSoGiayToUQ1_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(820, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 20);
            this.label11.TabIndex = 28;
            this.label11.Text = "Cấp tại";
            // 
            // cboNoiCapUQ1
            // 
            this.cboNoiCapUQ1.FormattingEnabled = true;
            this.cboNoiCapUQ1.Items.AddRange(new object[] {
            "Công an tỉnh Tây Ninh",
            "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư",
            "Phòng quản lý Xuất nhập cảnh Tây Ninh",
            "Cục quản lý Xuất nhập cảnh",
            "Công an Thành phố Hồ Chí Minh",
            "Công An tỉnh Long An",
            "Công an tỉnh Bình Dương",
            "Công an tỉnh Bình Phước",
            "Công an tỉnh Đồng Nai",
            "Công an tỉnh Bà Rịa - Vũng Tàu",
            "Công an tỉnh Bình Phước"});
            this.cboNoiCapUQ1.Location = new System.Drawing.Point(885, 42);
            this.cboNoiCapUQ1.Name = "cboNoiCapUQ1";
            this.cboNoiCapUQ1.Size = new System.Drawing.Size(248, 28);
            this.cboNoiCapUQ1.TabIndex = 6;
            this.cboNoiCapUQ1.SelectedIndexChanged += new System.EventHandler(this.cboNoiCapUQ1_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1174, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 20);
            this.label12.TabIndex = 32;
            this.label12.Text = "Ngày cấp";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // txtNgayCapUQ1
            // 
            this.txtNgayCapUQ1.Location = new System.Drawing.Point(1255, 42);
            this.txtNgayCapUQ1.Name = "txtNgayCapUQ1";
            this.txtNgayCapUQ1.Size = new System.Drawing.Size(132, 26);
            this.txtNgayCapUQ1.TabIndex = 7;
            this.txtNgayCapUQ1.TextChanged += new System.EventHandler(this.txtNgayCapUQ1_TextChanged);
            // 
            // lbCuTruOng
            // 
            this.lbCuTruOng.AutoSize = true;
            this.lbCuTruOng.Location = new System.Drawing.Point(104, 148);
            this.lbCuTruOng.Name = "lbCuTruOng";
            this.lbCuTruOng.Size = new System.Drawing.Size(63, 20);
            this.lbCuTruOng.TabIndex = 36;
            this.lbCuTruOng.Text = "Người 1";
            // 
            // cboCuTruUQ1
            // 
            this.cboCuTruUQ1.FormattingEnabled = true;
            this.cboCuTruUQ1.Items.AddRange(new object[] {
            ", huyện Châu Thành, tỉnh Tây Ninh",
            ", huyện Hòa Thành, tỉnh Tây Ninh",
            ", Thành phố Tây Ninh, tỉnh Tây Ninh",
            ", huyện Tân Châu, tỉnh Tây Ninh",
            ", huyện Tân Biên, tỉnh Tây Ninh",
            ", huyện Dương Minh Châu, tỉnh Tây Ninh",
            ", huyện Gò Dầu, tỉnh Tây Ninh",
            ", huyện Trảng Bàng, tỉnh Tây Ninh",
            ", huyện Bến Cầu, tỉnh Tây Ninh"});
            this.cboCuTruUQ1.Location = new System.Drawing.Point(179, 142);
            this.cboCuTruUQ1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboCuTruUQ1.Name = "cboCuTruUQ1";
            this.cboCuTruUQ1.Size = new System.Drawing.Size(350, 28);
            this.cboCuTruUQ1.TabIndex = 15;
            // 
            // lbCuTruBa
            // 
            this.lbCuTruBa.AutoSize = true;
            this.lbCuTruBa.Location = new System.Drawing.Point(690, 144);
            this.lbCuTruBa.Name = "lbCuTruBa";
            this.lbCuTruBa.Size = new System.Drawing.Size(63, 20);
            this.lbCuTruBa.TabIndex = 38;
            this.lbCuTruBa.Text = "Người 2";
            // 
            // cboCuTruUQ2
            // 
            this.cboCuTruUQ2.FormattingEnabled = true;
            this.cboCuTruUQ2.Items.AddRange(new object[] {
            ", huyện Châu Thành, tỉnh Tây Ninh",
            ", huyện Hòa Thành, tỉnh Tây Ninh",
            ", Thành phố Tây Ninh, tỉnh Tây Ninh",
            ", huyện Tân Châu, tỉnh Tây Ninh",
            ", huyện Tân Biên, tỉnh Tây Ninh",
            ", huyện Dương Minh Châu, tỉnh Tây Ninh",
            ", huyện Gò Dầu, tỉnh Tây Ninh",
            ", huyện Trảng Bàng, tỉnh Tây Ninh",
            ", huyện Bến Cầu, tỉnh Tây Ninh"});
            this.cboCuTruUQ2.Location = new System.Drawing.Point(815, 136);
            this.cboCuTruUQ2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboCuTruUQ2.Name = "cboCuTruUQ2";
            this.cboCuTruUQ2.Size = new System.Drawing.Size(362, 28);
            this.cboCuTruUQ2.TabIndex = 16;
            this.cboCuTruUQ2.SelectedIndexChanged += new System.EventHandler(this.cboCuTruUQ2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(694, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 40);
            this.button1.TabIndex = 17;
            this.button1.Text = "Thêm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(393, 195);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 40);
            this.button2.TabIndex = 41;
            this.button2.Text = "Sửa";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(578, 653);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 45);
            this.button3.TabIndex = 18;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbDaiDien
            // 
            this.lbDaiDien.AutoSize = true;
            this.lbDaiDien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDaiDien.Location = new System.Drawing.Point(12, 9);
            this.lbDaiDien.Name = "lbDaiDien";
            this.lbDaiDien.Size = new System.Drawing.Size(185, 25);
            this.lbDaiDien.TabIndex = 45;
            this.lbDaiDien.Text = "Đại diện ủy quyền";
            // 
            // btnXoa1
            // 
            this.btnXoa1.Location = new System.Drawing.Point(1411, 41);
            this.btnXoa1.Name = "btnXoa1";
            this.btnXoa1.Size = new System.Drawing.Size(61, 32);
            this.btnXoa1.TabIndex = 46;
            this.btnXoa1.Text = "Xóa";
            this.btnXoa1.UseVisualStyleBackColor = true;
            this.btnXoa1.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnXoa2
            // 
            this.btnXoa2.Location = new System.Drawing.Point(1411, 82);
            this.btnXoa2.Name = "btnXoa2";
            this.btnXoa2.Size = new System.Drawing.Size(61, 32);
            this.btnXoa2.TabIndex = 47;
            this.btnXoa2.Text = "Xóa";
            this.btnXoa2.UseVisualStyleBackColor = true;
            this.btnXoa2.Click += new System.EventHandler(this.btnXoa2_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(1003, 195);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(174, 40);
            this.btnXoa.TabIndex = 53;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // FormMainMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1484, 711);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnXoa2);
            this.Controls.Add(this.btnXoa1);
            this.Controls.Add(this.lbDaiDien);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboCuTruUQ2);
            this.Controls.Add(this.lbCuTruBa);
            this.Controls.Add(this.cboCuTruUQ1);
            this.Controls.Add(this.lbCuTruOng);
            this.Controls.Add(this.chkSenderResidentYN);
            this.Controls.Add(this.txtNgayCapUQ2);
            this.Controls.Add(this.txtNgayCapUQ1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboNoiCapUQ2);
            this.Controls.Add(this.cboNoiCapUQ1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSoGiayToUQ2);
            this.Controls.Add(this.txtSoGiayToUQ1);
            this.Controls.Add(this.cboLoaiGiayToUQ2);
            this.Controls.Add(this.cboLoaiGiayToUQ1);
            this.Controls.Add(this.txtNgaySinhUQ2);
            this.Controls.Add(this.txtNgaySinhUQ1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtHoVaTenUQ2);
            this.Controls.Add(this.txtHoVaTenUQ1);
            this.Controls.Add(this.cboDanhXungUQ2);
            this.Controls.Add(this.cboDanhXungUQ1);
            this.Controls.Add(this.lvUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMainMulti";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HandoverDemo";
            this.Load += new System.EventHandler(this.HandoverDemo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvUser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.CheckBox chkSenderResidentYN;
        private System.Windows.Forms.ComboBox cboDanhXungUQ2;
        private System.Windows.Forms.TextBox txtHoVaTenUQ2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNgaySinhUQ2;
        private System.Windows.Forms.ComboBox cboLoaiGiayToUQ2;
        private System.Windows.Forms.TextBox txtSoGiayToUQ2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboNoiCapUQ2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNgayCapUQ2;
        private System.Windows.Forms.ComboBox cboDanhXungUQ1;
        private System.Windows.Forms.TextBox txtHoVaTenUQ1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNgaySinhUQ1;
        private System.Windows.Forms.ComboBox cboLoaiGiayToUQ1;
        private System.Windows.Forms.TextBox txtSoGiayToUQ1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboNoiCapUQ1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNgayCapUQ1;
        private System.Windows.Forms.Label lbCuTruOng;
        private System.Windows.Forms.ComboBox cboCuTruUQ1;
        private System.Windows.Forms.Label lbCuTruBa;
        private System.Windows.Forms.ComboBox cboCuTruUQ2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lbDaiDien;
        private ColumnHeader columnHeader15;
        private Button btnXoa1;
        private Button btnXoa2;
        private Button btnXoa;
    }
}