using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExportWordFileFromTemplate.Models;
using Novacode;
//using Application = Microsoft.Office.Interop.Word.Application;
using CheckBox = System.Windows.Forms.CheckBox;
using CustomProperty = Novacode.CustomProperty;

namespace ExportWordFileFromTemplate
{
    public partial class FormMain : Form
    {
        // ben A
        private List<ListUserCustom> userListA;
        //ben B
        private List<ListUserCustom> userListB;

        private UyQuyen UyQuyenA { get; set; }
        private UyQuyen UyQuyenB { get; set; }

        private MiniForm MiniForms { get; set; }
        private ListFile ListFiles { get; set; }
        
        private HandoverDemo HandoverDemo1 { get; set; }
        private HandoverDemo HandoverDemo2 { get; set; }

        private string QSDD2 { get; set; }
        private string SoQSDD2 { get; set; }
        private string SoVaoSo2 { get; set; }
        private string UBND2 { get; set; }
        private string NgayCapQSDD2 { get; set; }
        private string ThuaDatSo2 { get; set; }
        private string ToBanDoSo2 { get; set; }
        private string DiaChiThuaDat2 { get; set; }
        private string DienTich2 { get; set; }
        private string DienTichRieng2 { get; set; }
        private string DienTichChung2 { get; set; }
        private string MucDichSuDung2 { get; set; }
        private string ThoiHan2 { get; set; }
        private string NguonGocSuDung2 { get; set; }
        private string HanCheQuyenSuDung2 { get; set; }
        private string QuyenSoHuuNhaO2 { get; set; }
        private string QuyenSoHuuTaiSanKhac2 { get; set; }
        
        public FormMain()
        {
            InitializeComponent();

            userListA = new List<ListUserCustom>();
            userListB = new List<ListUserCustom>();

            UyQuyenA = new UyQuyen();
            UyQuyenB = new UyQuyen();

            UyQuyenA.Users = new List<ListUserCustom>();
            UyQuyenB.Users = new List<ListUserCustom>();

            // default value
            txtTienKieuChu.Visible = false;
            lbToDay.Text = DateTime.Now.ToString("dd/MM/yyyy");


            lbCuTruOng.Visible = false;
            lbCuTruBa.Visible = false;
            cboCuTruA2.Visible = false;
            cboCuTruA1.Location = new System.Drawing.Point(75, 95);
            cboCuTruA1.Size = new System.Drawing.Size(838, 26);
            
            lbCuTruOngB.Visible = false;
            lbCuTruBaB.Visible = false;
            cboCuTruB2.Visible = false;
            cboCuTruB1.Location = new System.Drawing.Point(75, 95);
            cboCuTruB1.Size = new System.Drawing.Size(838, 26);
            
            btSuaDongCQA.Visible = false;
            btSuaDongCQB.Visible = false;

            //1
            cboSenderCall1.SelectedIndex = 0;
            cboSenderCall2.SelectedIndex = 1;

            cboSenderNationalCall1.SelectedIndex = 2;
            cboSenderNationalCall2.SelectedIndex = 2;

            cboSenderNationalCreator1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            cboSenderNationalCreator2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";


            //2
            cboReceiverCall1.SelectedIndex = 0;
            cboReceiverCall2.SelectedIndex = 1;

            cboReceiverNationalCall1.SelectedIndex = 2;
            cboReceiverNationalCall2.SelectedIndex = 2;

            cboReceiverNationalCreator1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            cboReceiverNationalCreator2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";

            //3
            cboQSDD.SelectedIndex = 0;
            cboUBND.SelectedIndex = 0;
            //cboMucDichSd.SelectedIndex = 0;
            //cboNguonGocSd.SelectedIndex = 0;
            cboHinhThucThanhToan.SelectedIndex = 0;
            cboBenAGiaoDat.SelectedIndex = 0;
            cboNopThue.SelectedIndex = 0;
            cboTinhTrang.SelectedIndex = 0;

            //btUyQuyen1.Text = "\u00bb";
            //btUyQuyen2.Text = "\u00bb";

            btUyQuyen1.Text = "Ủy Quyền";
            btUyQuyen2.Text = "Ủy Quyền";

            button3.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            
        }

        


        private void btnProcess_Click(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                BackupData();

                var defaultFileName = string.Format(Constants.FileName, txtSoVaoSo.Text, txtSenderFullName1.Text,
                        txtSenderNationalId1.Text, txtReceiverFullName1.Text, txtReceiverNationalId1.Text);
                if (ListFiles == null || ListFiles == default(ListFile) || ListFiles.IsDisposed)
                {
                    ListFiles = new ListFile(defaultFileName, this);
                }

                ListFiles.Show();
            }
        }

        internal DocX NumberOfPage(DocX template, int numberOfPages)
        {
            template.AddCustomProperty(new CustomProperty("TongSoTrang", numberOfPages));
            return template;
        }

        private string GetUserInfor(List<ListUserCustom> users)
        {
            var output = string.Empty;
            foreach (var u in users)
            {
                if (u.HoVaTen1 != string.Empty)
                {
                    output += "@" + u.DanhXung1 + ": " + u.HoVaTen1 + "; ";

                    if (u.NgaySinh1 != string.Empty)
                    {
                        output += "Năm sinh: " + u.NgaySinh1 + ";#";
                    }
                    var giayto = string.Empty;

                    if (u.SoGiayTo1 != string.Empty)
                    {
                        giayto += "@" + u.LoaiGiayTo1 + ": " + u.SoGiayTo1;
                    }
                    if (u.NoiCap1 != string.Empty)
                    {
                        giayto += ",  cấp tại: " + u.NoiCap1;
                    }
                    if (u.NgayCap1 != string.Empty)
                    {
                        giayto += ", ngày cấp: " + u.NgayCap1;
                    }
                    if (giayto != string.Empty)
                    {
                        output += giayto + ";#";
                    }
                    if (u.CuTru1 != string.Empty && u.CuTru1 != u.CuTru2)
                    {
                        output += "@" + "Cư trú: " + u.CuTru1 + ";#";
                    }
                }
                

                // nguoi 2
                if (u.HoVaTen2 != string.Empty)
                {
                    output += "@" + u.DanhXung2 + ": " + u.HoVaTen2 + "; ";
                    if (u.NgaySinh2 != string.Empty)
                    {
                        output += "Năm sinh: " + u.NgaySinh2 + ";#";
                    }
                    var giayto = string.Empty;

                    if (u.SoGiayTo2 != string.Empty)
                    {
                        giayto += "@" + u.LoaiGiayTo2 + ": " + u.SoGiayTo2;
                    }
                    if (u.NoiCap2 != string.Empty)
                    {
                        giayto += ",  cấp tại: " + u.NoiCap2;
                    }
                    if (u.NgayCap2 != string.Empty)
                    {
                        giayto += ", ngày cấp: " + u.NgayCap2;
                    }
                    if (giayto != string.Empty)
                    {
                        output += giayto + ";#";
                    }
                    if (u.CuTru2 != string.Empty && u.CuTru1 != u.CuTru2)
                    {
                        output += "@" + "Cư trú: " + u.CuTru2 + ";#";
                    }
                }

                if(u.CuTru1 == u.CuTru2 && u.CuTru1 != string.Empty)
                {
                    output += "@" + "Cùng cư trú: " + u.CuTru1 + ";#";
                }
                
            }
            return output;
        }
        internal DocX CreateInvoiceFromTemplate(DocX template)
        {
            if (userListA.Count <= 1 && cbDongChuyenQuyenA.Checked == false) { LoadDefaultUserA(); }
            if (userListB.Count <= 1 && cbDongNhanChuyenQuyen.Checked == false) { LoadDefaultUserB(); }

            var userA = GetUserInfor(userListA);
            var userB = GetUserInfor(userListB);
            if(userA.Length > 0 && userA.Substring(userA.Length-1) == "#")
            {
                userA = userA.Substring(0, userA.Length - 2);
            }
            
            if (userB.Length > 0 && userB.Substring(userB.Length - 1) == "#")
            {
                userB = userB.Substring(0, userB.Length - 1);
            }


            var uqA = string.Empty;
            var uqB = string.Empty;
            if (UyQuyenA != null)
            {
                if (UyQuyenA.Users.Any())
                {
                    var newList = new List<ListUserCustom>();
                    for (int i = 0; i < UyQuyenA.Users.Count; i++)
                    {
                        var x = UyQuyenA.Users.ElementAt(i);
                        if (!(x.HoVaTen1 == string.Empty && x.HoVaTen2 == string.Empty))
                        {
                            newList.Add(x);
                        }
                    }
                    UyQuyenA.Users = newList;
                }

                if (UyQuyenA.Users.Any())
                {

                    var BenChinh = CamketUpdateBenChinh(userListA);
                    var uq = CamketUpdate(UyQuyenA.Users);

                    UyQuyenA.CamKet = "@" + string.Format(Constants.CamKet, BenChinh == string.Empty ? "Ông ... và bà ..." : BenChinh,
               uq == string.Empty ? "ông ... và bà ..." : uq, "...", "...", "...", "...",
               uq == string.Empty ? "ông ... và bà ..." : uq);


                    //uqA += "#*NGƯỜI ĐẠI DIỆN THEO ỦY QUYỀN LÀ#";
                    uqA += GetUserInfor(UyQuyenA.Users);
                    if (uqA.Substring(uqA.Length - 1) == "#")
                    {
                        uqA = uqA.Substring(0, uqA.Length - 1);
                    }

                }
                else
                {
                    UyQuyenA.CamKet = string.Empty;
                }
            }

            if (UyQuyenB != null)
            {
                if (UyQuyenB.Users.Any())
                {
                    var newList = new List<ListUserCustom>();
                    for (int i = 0; i < UyQuyenB.Users.Count; i++)
                    {
                        var x = UyQuyenB.Users.ElementAt(i);
                        if (!(x.HoVaTen1 == string.Empty && x.HoVaTen2 == string.Empty))
                        {
                            newList.Add(x);
                        }
                    }
                    UyQuyenB.Users = newList;
                }
                if (UyQuyenB.Users.Any())
                {
                    var BenChinh = CamketUpdateBenChinh(userListB);
                    var uq = CamketUpdate(UyQuyenB.Users);

                    UyQuyenB.CamKet = "@" + string.Format(Constants.CamKet, BenChinh == string.Empty ? "Ông ... và bà ..." : BenChinh,
               uq == string.Empty ? "ông ... và bà ..." : uq, "...", "...", "...", "...",
               uq == string.Empty ? "ông ... và bà ..." : uq);
                    // uqB += "#*NGƯỜI ĐẠI DIỆN THEO ỦY QUYỀN LÀ#";
                    uqB += GetUserInfor(UyQuyenB.Users);
                    if (uqB.Substring(uqB.Length - 1) == "#")
                    {
                        uqB = uqB.Substring(0, uqB.Length - 1);
                    }

                }
                else
                {
                    UyQuyenB.CamKet = string.Empty;
                }
            }
            if (userA != null && userA != string.Empty)
            {
                template.AddCustomProperty(new CustomProperty("BenA", userA));
                
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("BenA", "$"));

            }

            if (userB != null && userB != string.Empty)
            {
                template.AddCustomProperty(new CustomProperty("BenB", userB));
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("BenB", "$"));

            }

            if (uqA != null && uqA != string.Empty)
            {

                template.AddCustomProperty(new CustomProperty("DaiDienPhapLuatA", "*NGƯỜI ĐẠI DIỆN THEO ỦY QUYỀN LÀ"));
                
                template.AddCustomProperty(new CustomProperty("UyQuyenA", uqA));
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("DaiDienPhapLuatA", "$"));

                template.AddCustomProperty(new CustomProperty("UyQuyenA", "$"));

            }

            

            if (uqB != null && uqB != string.Empty)
            {
                template.AddCustomProperty(new CustomProperty("DaiDienPhapLuatB", "*NGƯỜI ĐẠI DIỆN THEO ỦY QUYỀN LÀ"));

                template.AddCustomProperty(new CustomProperty("UyQuyenB", uqB));
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("DaiDienPhapLuatB", "$"));

                template.AddCustomProperty(new CustomProperty("UyQuyenB", "$"));
            }
            template = PrintChuyenNhuong(template);
            template = PrintNhanChuyenNhuong(template);
            template = PrintUyQuyenChuyenNhuong(template);
            template = PrintUyQuyenNhanChuyenNhuong(template);

            if (cbDongChuyenQuyenA.Checked)
            {
                //if (userListA.Count > 1)
                //{
                    template.AddCustomProperty(new CustomProperty("DongQuyenA", "*ĐỒNG QUYỀN SỬ DỤNG, QUYỀN SỞ HỮU"));
                //}
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("DongQuyenA", "$"));
                
            }

            if (cbDongNhanChuyenQuyen.Checked)
            {
                //if (userListB.Count > 1)
                //{
                    template.AddCustomProperty(new CustomProperty("DongQuyenB", "*ĐỒNG QUYỀN SỬ DỤNG, QUYỀN SỞ HỮU"));
                //}
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("DongQuyenB", "$"));

            }

            /////3
            template.AddCustomProperty(new CustomProperty("QSDD", cboQSDD.Text));
            template.AddCustomProperty(new CustomProperty("SoQSDD", txtSoQuyenSuDungDat.Text));
            template.AddCustomProperty(new CustomProperty("SoVaoSo", txtSoVaoSo.Text));
            template.AddCustomProperty(new CustomProperty("UBND", cboUBND.Text));
            template.AddCustomProperty(new CustomProperty("NgayCapQSDD", txtNgayCapQSDD1.Text));
            template.AddCustomProperty(new CustomProperty("ThuaDatSo", txtThuaDatSo.Text));
            template.AddCustomProperty(new CustomProperty("ToBanDoSo", txtToBanDoSo.Text));
            template.AddCustomProperty(new CustomProperty("DiaChiThuaDat", cboDiaChiThuaDat.Text));
            template.AddCustomProperty(new CustomProperty("DienTichKieuSo", txtDienTich.Text));
            template.AddCustomProperty(new CustomProperty("DienTichKieuChu", txtDienTichChu.Text));
            template.AddCustomProperty(new CustomProperty("DienTichRieng", txtDienTichSdRieng.Text));
            template.AddCustomProperty(new CustomProperty("DienTichChung", txtDienTichSdChung.Text));
            template.AddCustomProperty(new CustomProperty("MucDichSuDung", cboMucDichSd.Text));
            template.AddCustomProperty(new CustomProperty("ThoiHan", cboThoiHan.Text));
            template.AddCustomProperty(new CustomProperty("NguonGocSuDung", cboNguonGocSd.Text));
            template.AddCustomProperty(new CustomProperty("HanCheQuyenSuDung", txtHanCheQuyenSd.Text));
            template.AddCustomProperty(new CustomProperty("GiaKieuSo", txtGia.Text));
            template.AddCustomProperty(new CustomProperty("GiaKieuChu", txtTienKieuChu.Text));
            template.AddCustomProperty(new CustomProperty("HinhThucThanhToan", cboHinhThucThanhToan.Text));
            template.AddCustomProperty(new CustomProperty("BenAGiaoDat", cboBenAGiaoDat.Text));
            template.AddCustomProperty(new CustomProperty("BenNopThue", cboNopThue.Text));
            template.AddCustomProperty(new CustomProperty("TinhTrangHopDong", cboTinhTrang.Text));
            template.AddCustomProperty(new CustomProperty("NghiaVuTaiChinh", txtNghiaVuTaiChinh.Text));
            template.AddCustomProperty(new CustomProperty("QSHNO", txtQuyenSoHuuNhaO.Text));
            template.AddCustomProperty(new CustomProperty("QSHTSKGLVD", txtQuyenSoHuutaiSanKhac.Text));

            /// 4 (if any)
            template.AddCustomProperty(new CustomProperty("QSDD2", QSDD2));
            template.AddCustomProperty(new CustomProperty("SoQSDD2", SoQSDD2));
            template.AddCustomProperty(new CustomProperty("SoVaoSo2", SoVaoSo2));
            template.AddCustomProperty(new CustomProperty("UBND2", UBND2));
            template.AddCustomProperty(new CustomProperty("NgayCapQSDD2", NgayCapQSDD2));
            template.AddCustomProperty(new CustomProperty("ThuaDatSo2", ThuaDatSo2));
            template.AddCustomProperty(new CustomProperty("ToBanDoSo2", ToBanDoSo2));
            template.AddCustomProperty(new CustomProperty("DiaChiThuaDat2", DiaChiThuaDat2));
            template.AddCustomProperty(new CustomProperty("DienTich2", DienTich2));
            template.AddCustomProperty(new CustomProperty("DienTichRieng2", DienTichRieng2));
            template.AddCustomProperty(new CustomProperty("DienTichChung2", DienTichChung2));
            template.AddCustomProperty(new CustomProperty("MucDichSuDung2", MucDichSuDung2));
            template.AddCustomProperty(new CustomProperty("ThoiHan2", ThoiHan2));
            template.AddCustomProperty(new CustomProperty("NguonGocSuDung2", NguonGocSuDung2));
            template.AddCustomProperty(new CustomProperty("HanCheQuyenSuDung2", HanCheQuyenSuDung2));
            template.AddCustomProperty(new CustomProperty("QSHNO2", QuyenSoHuuNhaO2));
            template.AddCustomProperty(new CustomProperty("QSHTSKGLVD2", QuyenSoHuuTaiSanKhac2));


            // Tham so bo sung
            template.AddCustomProperty(new CustomProperty("DauPhay", ","));
            var parags = template.Paragraphs;
            foreach (var pa in parags)
            {
                var text = pa.Text.ToCharArray();
                if (text.Any())
                {
                    if (text.All(x => x.Equals('$') || x.Equals(' ')
                 || x.Equals(',') || x.Equals('.') || x.Equals(':') || x.Equals('\t'))
                    && text.Any(x => x.Equals('$')))
                    {
                        pa.Remove(false);
                    }
                    else
                    {
                        pa.ReplaceText("$", "");
                        pa.ReplaceText("#", Environment.NewLine);
                        pa.ReplaceText("@", "\t");
                        //pa.ReplaceText(Environment.NewLine, "\u00B6");
                        pa.ReplaceText("m2", "m\u00b2");
                    }

                }

            }
           
            //template.CustomProperties.Clear();
            return template;
        }


        private void chkSenderResidentYN_CheckedChanged_1(object sender, EventArgs e)
        {
            var ckb = sender as CheckBox;
            if (ckb.Checked)
            {
                CheckATachCuTru();
            }
            else
            {
                lbCuTruOng.Visible = false;
                lbCuTruBa.Visible = false;
                cboCuTruA2.Visible = false;
                cboCuTruA1.Location = new System.Drawing.Point(75, 95);
                cboCuTruA1.Size = new System.Drawing.Size(838, 26);
            }
        }

        private void CheckATachCuTru(int? backup = null)
        {
            chkSenderResidentYN.Checked = true;
            lbCuTruOng.Visible = true;
            lbCuTruBa.Visible = true;
            cboCuTruA2.Visible = true;
            if (backup == null)
            {
                cboCuTruA2.Text = string.Empty;
            }

            // 
            // lbCuTruBa
            // 
            lbCuTruBa.AutoSize = true;
            lbCuTruBa.Location = new System.Drawing.Point(490, 98);
            // 
            // txtCuTru2
            // 
            cboCuTruA2.Location = new System.Drawing.Point(535, 95);
            cboCuTruA2.Size = new System.Drawing.Size(250, 26);
            // 
            // lbCuTruOng
            // 
            // lbCuTruOng.AutoSize = true;
            lbCuTruOng.Location = new System.Drawing.Point(75, 98);
            // 
            // txtCuTru
            // 
            cboCuTruA1.Location = new System.Drawing.Point(120, 95);
            cboCuTruA1.Size = new System.Drawing.Size(250, 26);
            
        }

        private void chkReceiver_CheckedChanged(object sender, EventArgs e)
        {
            var ckb = sender as CheckBox;
            if (ckb.Checked)
            {
                CheckBHaiChuTru();
            }
            else
            {
                lbCuTruOngB.Visible = false;
                lbCuTruBaB.Visible = false;
                cboCuTruB2.Visible = false;
                cboCuTruB1.Location = new System.Drawing.Point(75, 95);
                cboCuTruB1.Size = new System.Drawing.Size(838, 26);
            }
        }

        private void CheckBHaiChuTru(int? backup = null)
        {
            chkReceiver.Checked = true;
            lbCuTruOngB.Visible = true;
            lbCuTruBaB.Visible = true;
            cboCuTruB2.Visible = true;
            if (backup == null)
            {
                cboCuTruB2.Text = string.Empty;
            }
            
            // 
            // lbCuTruOng
            // 
            lbCuTruOngB.AutoSize = true;
            lbCuTruOngB.Location = new System.Drawing.Point(75, 98);
            lbCuTruOngB.Size = new System.Drawing.Size(39, 20);
            // 
            // txtCuTru
            // 
            cboCuTruB1.Location = new System.Drawing.Point(120, 95);
            cboCuTruB1.Size = new System.Drawing.Size(250, 28);



            // 
            // lbCuTruBa
            // 
            lbCuTruBaB.AutoSize = true;
            lbCuTruBaB.Location = new System.Drawing.Point(490, 98);
            lbCuTruBaB.Size = new System.Drawing.Size(29, 20);

            // 
            // txtCuTru2
            // 
            cboCuTruB2.Location = new System.Drawing.Point(535, 95);
            cboCuTruB2.Size = new System.Drawing.Size(250, 26);
        }
        private void cboMucDichSd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cboMucDich = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cboMucDich.Text;
                if (!cboMucDich.Items.Contains(x))
                {
                    cboMucDich.Items.Add(cboMucDich.Text);
                    cboMucDich.SelectedItem = cboMucDich.Text;
                }
            }
        }
        private string ConvertDecimalToString(decimal number)
        {
            string s = number.ToString();
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "Lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "ấm " + str;
            return str;
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var des = decimal.Parse(txtGia.Text);
                txtTienKieuChu.Visible = true;
                txtTienKieuChu.Text = ConvertDecimalToString(des);

            }
            catch (Exception ex)
            {
                txtTienKieuChu.Visible = true;
                txtTienKieuChu.Text = "Sai định dạng tiền tệ";
            }

        }

        private void btBienDong_Click(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                if (MiniForms == null || MiniForms == default(MiniForm) || MiniForms.IsDisposed)
                {
                    MiniForms = new MiniForm(this);
                    if (SoQSDD2 != null || SoVaoSo2 != null || ToBanDoSo2 != null ||
                        NgayCapQSDD2 != null || DiaChiThuaDat2 != null || ThuaDatSo2 != null
                        || NguonGocSuDung2 != null || MucDichSuDung2 != null)
                    {
                        MiniForms.CopyAllData();
                    }
                }

                MiniForms.Show();
            }
        }
        internal void UpdateData(string qSDD2, string soQSDD2, string soVaoSo2, string uBND2, string ngayCapQSDD2,
            string thuaDatSo2, string toBanDoSo2, string diaChiThuaDat2, string dienTich2, string dienTichRieng2, string dienTichChung2,
            string mucDichSuDung2, string thoiHan2, string nguonGocSuDung2, string hanCheQuyenSuDung2, string quyenSoHuuNhaO2,
            string quyenSoHuuTaiSanKhac2)
        {
            QSDD2 = qSDD2;
            SoQSDD2 = soQSDD2;
            SoVaoSo2 = soVaoSo2;
            UBND2 = uBND2;
            NgayCapQSDD2 = ngayCapQSDD2;
            ThuaDatSo2 = thuaDatSo2;
            ToBanDoSo2 = toBanDoSo2;
            DiaChiThuaDat2 = diaChiThuaDat2;
            DienTich2 = dienTich2;
            DienTichRieng2 = dienTichRieng2;
            DienTichChung2 = dienTichChung2;
            MucDichSuDung2 = mucDichSuDung2;
            ThoiHan2 = thoiHan2;
            NguonGocSuDung2 = nguonGocSuDung2;
            HanCheQuyenSuDung2 = hanCheQuyenSuDung2;
            QuyenSoHuuNhaO2 = quyenSoHuuNhaO2;
            QuyenSoHuuTaiSanKhac2 = quyenSoHuuTaiSanKhac2;
        }

        

        internal void UpdateUyQuyen2(UyQuyen uyQuyen, int ben)
        {
            if (ben == 1)
            {
                UyQuyenA = uyQuyen;
            }
            else
            {
                UyQuyenB = uyQuyen;
            }

        }

        internal string GetQSDD2(bool fromMain = false)
        {
            return (QSDD2 != null && QSDD2 != string.Empty && !fromMain) ? QSDD2 : cboQSDD.Text == null ? string.Empty :
                cboQSDD.Text;
        }

        internal string GetSoQSDD2(bool fromMain = false)
        {
            return (SoQSDD2 != null && SoQSDD2 != string.Empty && !fromMain) ? SoQSDD2 : txtSoQuyenSuDungDat.Text;
        }

        internal string GetSoVaoSo2(bool fromMain = false)
        {
            return (SoVaoSo2 != null && SoVaoSo2 != string.Empty && !fromMain) ? SoVaoSo2 : txtSoVaoSo.Text;
        }
        internal string GetUBND2(bool fromMain = false)
        {
            return (UBND2 != null && UBND2 != string.Empty && !fromMain) ? UBND2 : cboUBND.Text == null ? string.Empty :
                cboUBND.Text;
        }

        internal string GetNgayCapQSDD2(bool fromMain = false)
        {
            return (NgayCapQSDD2 != null && NgayCapQSDD2 != string.Empty && !fromMain) ? NgayCapQSDD2 : txtNgayCapQSDD1.Text;
        }
        internal string GetThuaDatSo2(bool fromMain = false)
        {
            return (ThuaDatSo2 != null && ThuaDatSo2 != string.Empty && !fromMain) ? ThuaDatSo2 : txtThuaDatSo.Text;
        }
        internal string GetToBanDoSo2(bool fromMain = false)
        {
            return (ToBanDoSo2 != null && ToBanDoSo2 != string.Empty && !fromMain) ? ToBanDoSo2 : txtToBanDoSo.Text;
        }
        internal string GetDiaChiThuaDat2(bool fromMain = false)
        {
            return (DiaChiThuaDat2 != null && DiaChiThuaDat2 != string.Empty && !fromMain) ? DiaChiThuaDat2 : cboDiaChiThuaDat.Text == null ? string.Empty :
                cboDiaChiThuaDat.Text;
        }
        internal string GetDienTich2(bool fromMain = false)
        {
            return (DienTich2 != null && DienTich2 != string.Empty && !fromMain) ? DienTich2 : txtDienTich.Text;
        }
        internal string GetDienTichRieng2(bool fromMain = false)
        {
            return (DienTichRieng2 != null && DienTich2 != string.Empty && !fromMain) ? DienTichRieng2 : txtDienTichSdRieng.Text;
        }
        internal string GetDienTichChung2(bool fromMain = false)
        {
            return (DienTichChung2 != null && DienTichChung2 != string.Empty && !fromMain) ? DienTichChung2 : txtDienTichSdChung.Text; ;
        }
        internal string GetMucDichSuDung2(bool fromMain = false)
        {
            return (MucDichSuDung2 != null && MucDichSuDung2 != string.Empty && !fromMain) ? MucDichSuDung2 : cboMucDichSd.Text == null ? string.Empty :
                cboMucDichSd.Text;
        }
        internal string GetThoiHan2(bool fromMain = false)
        {
            return (ThoiHan2 != null && ThoiHan2 != string.Empty && !fromMain) ? ThoiHan2 : cboThoiHan.Text == null ? string.Empty :
                cboThoiHan.Text;
        }
        internal string GetNguonGocSuDung2(bool fromMain = false)
        {
            return (NguonGocSuDung2 != null && NguonGocSuDung2 != string.Empty && !fromMain) ? NguonGocSuDung2 : cboNguonGocSd.Text == null ? string.Empty :
                cboNguonGocSd.Text;
        }

        internal string GetHanCheQuyenSuDung2(bool fromMain = false)
        {
            return (HanCheQuyenSuDung2 != null && HanCheQuyenSuDung2 != string.Empty && !fromMain) ? HanCheQuyenSuDung2 : txtHanCheQuyenSd.Text;
        }

        internal string GetQuyenSuDungDat2(bool fromMain = false)
        {
            return (QuyenSoHuuNhaO2 != null && QuyenSoHuuNhaO2 != string.Empty && !fromMain) ? QuyenSoHuuNhaO2 : txtQuyenSoHuuNhaO.Text;
        }

        internal string GetQuyenSoHuuTaiSanKhac2(bool fromMain = false)
        {
            return (QuyenSoHuuTaiSanKhac2 != null && QuyenSoHuuTaiSanKhac2 != string.Empty && !fromMain) ? QuyenSoHuuTaiSanKhac2 : txtQuyenSoHuutaiSanKhac.Text;
        }

        private void cboSenderNationalCreator1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cboSenderNationalCreator1 = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cboSenderNationalCreator1.Text;
                if (!cboSenderNationalCreator1.Items.Contains(x))
                {
                    cboSenderNationalCreator1.Items.Add(cboSenderNationalCreator1.Text);
                    cboSenderNationalCreator1.SelectedItem = cboSenderNationalCreator1.Text;
                }
            }
        }

        private void cboSenderNationalCreator2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboCuTruA2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboCuTruA1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboSenderNationalCreator1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName1.Text != string.Empty || txtNgaySinhChuyenNhuong1.Text != string.Empty
                || txtSenderNationalId1.Text != string.Empty || txtNgayCapChuyenNhuong1.Text != string.Empty
               )
            {
                button1.Enabled = true;
            }
        }

        private void cboReceiverNationalCreator1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboReceiverNationalCreator2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboCuTruB1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboCuTruB2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void cboDiaChiThuaDat_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void comboBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }

        private void txtDienTich_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDienTichSdRieng.Text = txtDienTich.Text;
                var des = decimal.Parse(txtDienTich.Text);
                txtDienTichChu.Visible = true;
                txtDienTichChu.Text = ConvertDecimalToString(des);

            }
            catch (Exception ex)
            {
                txtDienTichChu.Visible = true;
                txtDienTichChu.Text = "Sai định dạng số";
            }
        }

        private void cboNguonGocSd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbobox = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbobox.Text;
                if (!cbobox.Items.Contains(x))
                {
                    cbobox.Items.Add(cbobox.Text);
                    cbobox.SelectedItem = cbobox.Text;
                }
            }
        }


        private void cboSenderNationalCall1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboSenderNationalCreator1.SelectedItem = "Công an tỉnh Tây Ninh";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboSenderNationalCreator1.SelectedItem = "Cục quản lý Xuất nhập cảnh";
            }
            else if (cb.SelectedIndex == 2)
            {
                cboSenderNationalCreator1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            }
        }


        private void cboSenderNationalCall2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboSenderNationalCreator2.SelectedItem = "Công an tỉnh Tây Ninh";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboSenderNationalCreator2.SelectedItem = "Cục quản lý Xuất nhập cảnh";
            }
            else if (cb.SelectedIndex == 2)
            {
                cboSenderNationalCreator2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            }
        }

        private void cboReceiverNationalCall1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboReceiverNationalCreator1.SelectedItem = "Công an tỉnh Tây Ninh";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboReceiverNationalCreator1.SelectedItem = "Cục quản lý Xuất nhập cảnh";
            }
            else if (cb.SelectedIndex == 2)
            {
                cboReceiverNationalCreator1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            }
        }

        private void cboReceiverNationalCall2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboReceiverNationalCreator2.SelectedItem = "Công an tỉnh Tây Ninh";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboReceiverNationalCreator2.SelectedItem = "Cục quản lý Xuất nhập cảnh";
            }
            else if (cb.SelectedIndex == 2)
            {
                cboReceiverNationalCreator2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            }
        }

        private void cboQSDD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbo = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbo.Text;
                if (!cbo.Items.Contains(x))
                {
                    cbo.Items.Add(cbo.Text);
                    cbo.SelectedItem = cbo.Text;
                }
            }
        }

        private void cboUBND_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var cbo = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                var x = cbo.Text;
                if (!cbo.Items.Contains(x))
                {
                    cbo.Items.Add(cbo.Text);
                    cbo.SelectedItem = cbo.Text;
                }
            }
        }

        internal void BackupData()
        {
            var data = GetAllData();
            String filepath = "Backup.txt";// đường dẫn của file muốn tạo
            FileStream fs = new FileStream(filepath, FileMode.Create);//Tạo file mới tên là test.txt            
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 

            foreach (var s in data)
            {
                sWriter.WriteLine(s);
            }
            // Ghi và đóng file
            sWriter.Flush();
            fs.Close();
        }

        private List<string> GetAllData()
        {
            var data = new List<string>();

            // Ben A
            data.Add(cboSenderCall1.Text);
            data.Add(txtSenderFullName1.Text);
            data.Add(txtNgaySinhChuyenNhuong1.Text);
            data.Add(cboSenderNationalCall1.Text);
            data.Add(txtSenderNationalId1.Text);
            data.Add(cboSenderNationalCreator1.Text);
            data.Add(txtNgayCapChuyenNhuong1.Text);
            data.Add(cboCuTruA1.Text);

            data.Add(cboSenderCall2.Text);
            data.Add(txtSenderFullName2.Text);
            data.Add(txtNgaySinhChuyenNhuong2.Text);
            data.Add(cboSenderNationalCall2.Text);
            data.Add(txtSenderNationalId2.Text);
            data.Add(cboSenderNationalCreator2.Text);
            data.Add(txtNgayCapChuyenNhuong2.Text);
            data.Add(cboCuTruA2.Text);

            // Ben B
            data.Add(cboReceiverCall1.Text);
            data.Add(txtReceiverFullName1.Text);
            data.Add(txtNgaySinhNhanChuyenNhuong1.Text);
            data.Add(cboReceiverNationalCall1.Text);
            data.Add(txtReceiverNationalId1.Text);
            data.Add(cboReceiverNationalCreator1.Text);
            data.Add(txtNgayCapNhanChuyenNhuong1.Text);
            data.Add(cboCuTruB1.Text);

            data.Add(cboReceiverCall2.Text);
            data.Add(txtReceiverFullName2.Text);
            data.Add(txtNgaySinhNhanChuyenNhuong2.Text);
            data.Add(cboReceiverNationalCall2.Text);
            data.Add(txtReceiverNationalId2.Text);
            data.Add(cboReceiverNationalCreator2.Text);
            data.Add(txtNgayCapNhanChuyenNhuong2.Text);
            data.Add(cboCuTruB2.Text);

            // Phần 3
            data.Add(cboQSDD.Text);
            data.Add(txtSoQuyenSuDungDat.Text);
            data.Add(txtSoVaoSo.Text);
            data.Add(cboUBND.Text);
            data.Add(txtNgayCapQSDD1.Text);
            data.Add(txtThuaDatSo.Text);
            data.Add(txtToBanDoSo.Text);
            data.Add(cboDiaChiThuaDat.Text);
            data.Add(txtDienTich.Text);
            data.Add(txtDienTichSdChung.Text);
            data.Add(txtDienTichSdRieng.Text);
            data.Add(cboMucDichSd.Text);
            data.Add(cboNguonGocSd.Text);
            data.Add(cboThoiHan.Text);
            data.Add(txtHanCheQuyenSd.Text);
            data.Add(txtGia.Text);
            data.Add(cboHinhThucThanhToan.Text);
            data.Add(cboBenAGiaoDat.Text);
            data.Add(cboNopThue.Text);
            data.Add(cboTinhTrang.Text);
            data.Add(txtNghiaVuTaiChinh.Text);

            // Phan 4
            if (MiniForms == null || MiniForms == default(MiniForm) || MiniForms.IsDisposed)
            {
                MiniForms = new MiniForm(this);
                MiniForms.UpdateForMain();
            }
            data.Add(QSDD2);
            data.Add(SoQSDD2);
            data.Add(SoVaoSo2);
            data.Add(UBND2);
            data.Add(NgayCapQSDD2);
            data.Add(ThuaDatSo2);
            data.Add(ToBanDoSo2);
            data.Add(DiaChiThuaDat2);
            data.Add(DienTich2);
            data.Add(DienTichRieng2);
            data.Add(DienTichChung2);
            data.Add(MucDichSuDung2);
            data.Add(ThoiHan2);
            data.Add(NguonGocSuDung2);
            data.Add(HanCheQuyenSuDung2);

            // bosung taisanganlienvoidat
            data.Add(txtQuyenSoHuuNhaO.Text);
            data.Add(txtQuyenSoHuutaiSanKhac.Text);
            data.Add(QuyenSoHuuNhaO2);
            data.Add(QuyenSoHuuTaiSanKhac2);



            // bosung uy quyen
            if (UyQuyenA != null && UyQuyenA.Users != null && UyQuyenA.Users.Any())
            {
                var firstUserA = UyQuyenA.Users.ElementAt(0);

                data.Add(firstUserA.DanhXung1);
                data.Add(firstUserA.HoVaTen1);
                data.Add(firstUserA.NgaySinh1);
                data.Add(firstUserA.LoaiGiayTo1);
                data.Add(firstUserA.SoGiayTo1);
                data.Add(firstUserA.NoiCap1);
                data.Add(firstUserA.NgayCap1);
                data.Add(firstUserA.CuTru1);

                data.Add(firstUserA.DanhXung2);
                data.Add(firstUserA.HoVaTen2);
                data.Add(firstUserA.NgaySinh2);
                data.Add(firstUserA.LoaiGiayTo2);
                data.Add(firstUserA.SoGiayTo2);
                data.Add(firstUserA.NoiCap2);
                data.Add(firstUserA.NgayCap2);
                data.Add(firstUserA.CuTru2);

                data.Add(UyQuyenA.CamKet);
            }
            if(UyQuyenB != null && UyQuyenB.Users != null && UyQuyenB.Users.Any())
            {
                var firstUserB = UyQuyenB.Users.ElementAt(0);

                data.Add(firstUserB.DanhXung1);
                data.Add(firstUserB.HoVaTen1);
                data.Add(firstUserB.NgaySinh1);
                data.Add(firstUserB.LoaiGiayTo1);
                data.Add(firstUserB.SoGiayTo1);
                data.Add(firstUserB.NoiCap1);
                data.Add(firstUserB.NgayCap1);
                data.Add(firstUserB.CuTru1);

                data.Add(firstUserB.DanhXung2);
                data.Add(firstUserB.HoVaTen2);
                data.Add(firstUserB.NgaySinh2);
                data.Add(firstUserB.LoaiGiayTo2);
                data.Add(firstUserB.SoGiayTo2);
                data.Add(firstUserB.NoiCap2);
                data.Add(firstUserB.NgayCap2);
                data.Add(firstUserB.CuTru2);

                data.Add(UyQuyenB.CamKet);
            }
            
            return data;
        }

        private void RestoreData()
        {
            var data = ReadData();
            if (data.Count() == 0)
            {
                MessageBox.Show("Không có phiên cuối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var index = data.Count() - 1;
                // Ben A
                cboSenderCall1.Text = index < 0 ? string.Empty : data.ElementAt(0);
                txtSenderFullName1.Text = index < 1 ? string.Empty : data.ElementAt(1);
                txtNgaySinhChuyenNhuong1.Text = index < 2 ? string.Empty : data.ElementAt(2);
                cboSenderNationalCall1.Text = index < 3 ? string.Empty : data.ElementAt(3);
                txtSenderNationalId1.Text = index < 4 ? string.Empty : data.ElementAt(4);
                cboSenderNationalCreator1.Text = index < 5 ? string.Empty : data.ElementAt(5);
                txtNgayCapChuyenNhuong1.Text = index < 6 ? string.Empty : data.ElementAt(6);
                cboCuTruA1.Text = index < 7 ? string.Empty : data.ElementAt(7);

                cboSenderCall2.Text = index < 8 ? string.Empty : data.ElementAt(8);
                txtSenderFullName2.Text = index < 9 ? string.Empty : data.ElementAt(9);
                txtNgaySinhChuyenNhuong2.Text = index < 10 ? string.Empty : data.ElementAt(10);
                cboSenderNationalCall2.Text = index < 11 ? string.Empty : data.ElementAt(11);
                txtSenderNationalId2.Text = index < 12 ? string.Empty : data.ElementAt(12);
                cboSenderNationalCreator2.Text = index < 13 ? string.Empty : data.ElementAt(13);
                txtNgayCapChuyenNhuong2.Text = index < 14 ? string.Empty : data.ElementAt(14);
                cboCuTruA2.Text = index < 15 ? string.Empty : data.ElementAt(15);

                if (cboCuTruA2.Text != string.Empty && cboCuTruA2.Text != "")
                {
                    CheckATachCuTru(1);
                }

                // Ben B
                cboReceiverCall1.Text = index < 16 ? string.Empty : data.ElementAt(16);
                txtReceiverFullName1.Text = index < 17 ? string.Empty : data.ElementAt(17);
                txtNgaySinhNhanChuyenNhuong1.Text = index < 18 ? string.Empty : data.ElementAt(18);
                cboReceiverNationalCall1.Text = index < 19 ? string.Empty : data.ElementAt(19);
                txtReceiverNationalId1.Text = index < 20 ? string.Empty : data.ElementAt(20);
                cboReceiverNationalCreator1.Text = index < 21 ? string.Empty : data.ElementAt(21);
                txtNgayCapNhanChuyenNhuong1.Text = index < 22 ? string.Empty : data.ElementAt(22);
                cboCuTruB1.Text = index < 23 ? string.Empty : data.ElementAt(23);

                cboReceiverCall2.Text = index < 24 ? string.Empty : data.ElementAt(24);
                txtReceiverFullName2.Text = index < 25 ? string.Empty : data.ElementAt(25);
                txtNgaySinhNhanChuyenNhuong2.Text = index < 26 ? string.Empty : data.ElementAt(26);
                cboReceiverNationalCall2.Text = index < 27 ? string.Empty : data.ElementAt(27);
                txtReceiverNationalId2.Text = index < 28 ? string.Empty : data.ElementAt(28);
                cboReceiverNationalCreator2.Text = index < 29 ? string.Empty : data.ElementAt(29);
                txtNgayCapNhanChuyenNhuong2.Text = index < 30 ? string.Empty : data.ElementAt(30);
                cboCuTruB2.Text = index < 31 ? string.Empty : data.ElementAt(31);
                if (cboCuTruB2.Text != string.Empty && cboCuTruB2.Text != "")
                {
                    CheckBHaiChuTru(1);
                }

                // Phần 3
                cboQSDD.Text = index < 32 ? string.Empty : data.ElementAt(32);
                txtSoQuyenSuDungDat.Text = index < 33 ? string.Empty : data.ElementAt(33);
                txtSoVaoSo.Text = index < 34 ? string.Empty : data.ElementAt(34);
                cboUBND.Text = index < 35 ? string.Empty : data.ElementAt(35);
                txtNgayCapQSDD1.Text = index < 36 ? string.Empty : data.ElementAt(36);
                txtThuaDatSo.Text = index < 37 ? string.Empty : data.ElementAt(37);
                txtToBanDoSo.Text = index < 38 ? string.Empty : data.ElementAt(38);
                cboDiaChiThuaDat.Text = index < 39 ? string.Empty : data.ElementAt(39);
                txtDienTich.Text = index < 40 ? string.Empty : data.ElementAt(40);
                txtDienTichSdChung.Text = index < 41 ? string.Empty : data.ElementAt(41);
                txtDienTichSdRieng.Text = index < 42 ? string.Empty : data.ElementAt(42);
                cboMucDichSd.Text = index < 43 ? string.Empty : data.ElementAt(43);
                cboNguonGocSd.Text = index < 44 ? string.Empty : data.ElementAt(44);
                cboThoiHan.Text = index < 45 ? string.Empty : data.ElementAt(45);
                txtHanCheQuyenSd.Text = index < 46 ? string.Empty : data.ElementAt(46);
                txtGia.Text = index < 47 ? string.Empty : data.ElementAt(47);
                cboHinhThucThanhToan.Text = index < 48 ? string.Empty : data.ElementAt(48);
                cboBenAGiaoDat.Text = index < 49 ? string.Empty : data.ElementAt(49);
                cboNopThue.Text = index < 50 ? string.Empty : data.ElementAt(50);
                cboTinhTrang.Text = index < 51 ? string.Empty : data.ElementAt(51);
                txtNghiaVuTaiChinh.Text = index < 52 ? string.Empty : data.ElementAt(52);


                QSDD2 = index < 53 ? string.Empty : data.ElementAt(53);
                SoQSDD2 = index < 54 ? string.Empty : data.ElementAt(54);
                SoVaoSo2 = index < 55 ? string.Empty : data.ElementAt(55);
                UBND2 = index < 56 ? string.Empty : data.ElementAt(56);
                NgayCapQSDD2 = index < 57 ? string.Empty : data.ElementAt(57);
                ThuaDatSo2 = index < 58 ? string.Empty : data.ElementAt(58);
                ToBanDoSo2 = index < 59 ? string.Empty : data.ElementAt(59);
                DiaChiThuaDat2 = index < 60 ? string.Empty : data.ElementAt(60);
                DienTich2 = index < 61 ? string.Empty : data.ElementAt(61);
                DienTichRieng2 = index < 62 ? string.Empty : data.ElementAt(62);
                DienTichChung2 = index < 63 ? string.Empty : data.ElementAt(63);
                MucDichSuDung2 = index < 64 ? string.Empty : data.ElementAt(64);
                ThoiHan2 = index < 65 ? string.Empty : data.ElementAt(65);
                NguonGocSuDung2 = index < 66 ? string.Empty : data.ElementAt(66);
                HanCheQuyenSuDung2 = index < 67 ? string.Empty : data.ElementAt(67);

                // Phan 4
                if (MiniForms == null || MiniForms == default(MiniForm) || MiniForms.IsDisposed)
                {
                    MiniForms = new MiniForm(this);
                    MiniForms.CopyAllData();
                }

                // PhanAdd them
                
                txtQuyenSoHuuNhaO.Text = index < 68 ? string.Empty : data.ElementAt(68);
                txtQuyenSoHuutaiSanKhac.Text = index < 69 ? string.Empty : data.ElementAt(69);
                QuyenSoHuuNhaO2 = index < 70 ? string.Empty : data.ElementAt(70);
                QuyenSoHuuTaiSanKhac2 = index < 71 ? string.Empty : data.ElementAt(71);

                // Uy quyen
                    UyQuyenA = new UyQuyen();
                    UyQuyenA.Users = new List<ListUserCustom>();
                
                    UyQuyenB = new UyQuyen();
                    UyQuyenB.Users = new List<ListUserCustom>();
                

                UyQuyenA.Users.Add(new ListUserCustom
                {
                    DanhXung1 = index < 72 ? string.Empty : data.ElementAt(72),
                    HoVaTen1 = index < 73 ? string.Empty : data.ElementAt(73),
                    NgaySinh1 = index < 74 ? string.Empty : data.ElementAt(74),
                    LoaiGiayTo1 = index < 75 ? string.Empty : data.ElementAt(75),
                    SoGiayTo1 = index < 76 ? string.Empty : data.ElementAt(76),
                    NoiCap1 = index < 77 ? string.Empty : data.ElementAt(77),
                    NgayCap1 = index < 78 ? string.Empty : data.ElementAt(78),
                    CuTru1 = index < 79 ? string.Empty : data.ElementAt(79),

                    DanhXung2 = index < 80 ? string.Empty : data.ElementAt(80),
                    HoVaTen2 = index < 81 ? string.Empty : data.ElementAt(81),
                    NgaySinh2 = index < 82 ? string.Empty : data.ElementAt(82),
                    LoaiGiayTo2 = index < 83 ? string.Empty : data.ElementAt(83),
                    SoGiayTo2 = index < 84 ? string.Empty : data.ElementAt(84),
                    NoiCap2 = index < 85 ? string.Empty : data.ElementAt(85),
                    NgayCap2 = index < 86 ? string.Empty : data.ElementAt(86),
                    CuTru2 = index < 87 ? string.Empty : data.ElementAt(87),

                });
               UyQuyenA.CamKet = index < 88 ? string.Empty : data.ElementAt(88);

                //if (Handover1 == null || Handover1 == default(Handover) || Handover1.IsDisposed)
                //{
                //    Handover1 = new Handover(this, 1);
                //    Handover1.CopyAllData(DanhXungUyQuyenA, TenUyQuyenA, NgaySinhUyQuyenA, LoaiGiayToUyQuyenA,
                //        SoGiayToA, NoiCapGiayToUyQuyenA, NgayCapUyQuyenA, CuTruUyQuyenA, CamKetUyQuyenA);
                //}

                UyQuyenB.Users.Add(new ListUserCustom
                {
                    DanhXung1 = index < 89 ? string.Empty : data.ElementAt(89),
                    HoVaTen1 = index < 90 ? string.Empty : data.ElementAt(90),
                    NgaySinh1 = index < 91 ? string.Empty : data.ElementAt(91),
                    LoaiGiayTo1 = index < 92 ? string.Empty : data.ElementAt(92),
                    SoGiayTo1 = index < 93 ? string.Empty : data.ElementAt(93),
                    NoiCap1 = index < 94 ? string.Empty : data.ElementAt(94),
                    NgayCap1 = index < 95 ? string.Empty : data.ElementAt(95),
                    CuTru1 = index < 96 ? string.Empty : data.ElementAt(96),

                    DanhXung2 = index < 97 ? string.Empty : data.ElementAt(97),
                    HoVaTen2 = index < 98 ? string.Empty : data.ElementAt(98),
                    NgaySinh2 = index < 99 ? string.Empty : data.ElementAt(99),
                    LoaiGiayTo2 = index < 100 ? string.Empty : data.ElementAt(100),
                    SoGiayTo2 = index < 101 ? string.Empty : data.ElementAt(101),
                    NoiCap2 = index < 102 ? string.Empty : data.ElementAt(102),
                    NgayCap2 = index < 103 ? string.Empty : data.ElementAt(103),
                    CuTru2 = index < 104 ? string.Empty : data.ElementAt(104),

                });
                UyQuyenB.CamKet = index < 105 ? string.Empty : data.ElementAt(105);

                //if (Handover2 == null || Handover2 == default(Handover) || Handover2.IsDisposed)
                //{
                //    Handover2 = new Handover(this, 2);
                //    Handover2.CopyAllData(DanhXungUyQuyenB, TenUyQuyenB, NgaySinhUyQuyenB, LoaiGiayToUyQuyenB,
                //        SoGiayToB, NoiCapGiayToUyQuyenB, NgayCapUyQuyenB, CuTruUyQuyenB, CamKetUyQuyenB);
                //}
            }

        }
        private List<string> ReadData()
        {
            try
            {
                var data = new List<string>();
                FileStream fs = new FileStream("Backup.txt", FileMode.Open);
                StreamReader rd = new StreamReader(fs, Encoding.UTF8);
                int counter = 0;
                string ln;

                while ((ln = rd.ReadLine()) != null)
                {
                    data.Add(ln);
                    counter++;
                }
                rd.Close();
                return data;
            }
            catch (Exception)
            {
                return new List<string>();
                throw;
            }

        }


        internal void ClearData()
        {
            //// Ben A
            cboSenderCall1.SelectedIndex = 0;
            txtSenderFullName1.Text = string.Empty;
            txtNgaySinhChuyenNhuong1.Text = string.Empty;
            cboSenderNationalCall1.SelectedIndex = 0;
            txtSenderNationalId1.Text = string.Empty;
            cboSenderNationalCreator1.SelectedIndex = 0;
            txtNgayCapChuyenNhuong1.Text = string.Empty;
            cboCuTruA1.Text = string.Empty;

            cboSenderCall2.SelectedIndex = 1;
            txtSenderFullName2.Text = string.Empty;
            txtNgaySinhChuyenNhuong2.Text = string.Empty;
            cboSenderNationalCall2.SelectedIndex = 0;
            txtSenderNationalId2.Text = string.Empty;
            cboSenderNationalCreator2.SelectedIndex = 0;
            txtNgayCapChuyenNhuong2.Text = string.Empty;
            cboCuTruA2.Text = string.Empty;

            //// Ben B
            cboReceiverCall1.SelectedIndex = 0;
            txtReceiverFullName1.Text = string.Empty;
            txtNgaySinhNhanChuyenNhuong1.Text = string.Empty;
            cboReceiverNationalCall1.SelectedIndex = 0;
            txtReceiverNationalId1.Text = string.Empty;
            cboReceiverNationalCreator1.SelectedIndex = 0;
            txtNgayCapNhanChuyenNhuong1.Text = string.Empty;
            cboCuTruB1.Text = string.Empty;

            cboReceiverCall2.SelectedIndex = 0;
            txtReceiverFullName2.Text = string.Empty;
            txtNgaySinhNhanChuyenNhuong2.Text = string.Empty;
            cboReceiverNationalCall2.SelectedIndex = 0;
            txtReceiverNationalId2.Text = string.Empty;
            cboReceiverNationalCreator2.SelectedIndex = 0;
            txtNgayCapNhanChuyenNhuong2.Text = string.Empty;
            cboCuTruB2.Text = string.Empty;

            //// Phần 3
            cboQSDD.SelectedIndex = 0;
            txtSoQuyenSuDungDat.Text = string.Empty;
            txtSoVaoSo.Text = string.Empty;
            cboUBND.SelectedIndex = 0;
            txtNgayCapQSDD1.Text = string.Empty;
            txtThuaDatSo.Text = string.Empty;
            txtToBanDoSo.Text = string.Empty;
            cboDiaChiThuaDat.Text = string.Empty;
            txtDienTich.Text = string.Empty;
            txtDienTichSdChung.Text = string.Empty;
            txtDienTichSdRieng.Text = string.Empty;
            cboMucDichSd.Text = string.Empty;
            cboNguonGocSd.Text = string.Empty;
            cboThoiHan.Text = string.Empty;
            txtHanCheQuyenSd.Text = string.Empty;
            txtGia.Text = string.Empty;
            cboHinhThucThanhToan.SelectedIndex = 0;
            cboBenAGiaoDat.SelectedIndex = 0;
            cboNopThue.SelectedIndex = 0;
            cboTinhTrang.SelectedIndex = 0;
            txtNghiaVuTaiChinh.Text = string.Empty;


            QSDD2 = cboQSDD.Text;
            SoQSDD2 = string.Empty;
            SoVaoSo2 = string.Empty;
            UBND2 = cboUBND.Text;
            NgayCapQSDD2 = string.Empty;
            ThuaDatSo2 = string.Empty;
            ToBanDoSo2 = string.Empty;
            DiaChiThuaDat2 = string.Empty;
            DienTich2 = string.Empty;
            DienTichRieng2 = string.Empty;
            DienTichChung2 = string.Empty;
            MucDichSuDung2 = string.Empty;
            ThoiHan2 = string.Empty;
            NguonGocSuDung2 = string.Empty;
            HanCheQuyenSuDung2 = string.Empty;

            // Phan 4
            if (MiniForms == null || MiniForms == default(MiniForm) || MiniForms.IsDisposed)
            {
                MiniForms = new MiniForm(this);
                MiniForms.CopyAllData();
            }

            userListA.Clear();
            userListB.Clear();
            UyQuyenA.CamKet = string.Empty;
            UyQuyenA.Users.Clear();
            UyQuyenB.CamKet = string.Empty;
            UyQuyenB.Users.Clear();
        }

        private void mởPhiênCuốiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreData();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Password p = new Password();
            p.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboQSDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboUBND.SelectedItem = "Sở Tài nguyên và Môi trường";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboUBND.SelectedItem = "UBND huyện";
            }

        }

        private void cboSenderCall2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == "Tài sản riêng")
            {
                txtSenderFullName2.Size = new System.Drawing.Size(838, 26);
                txtSenderFullName2.Text = "(Theo giấy xác nhận tình trạng hôn nhân số ... do UBND ... xác nhận ngày .../.../...)";
                txtNgaySinhChuyenNhuong2.Visible = false;
                txtSenderNationalId2.Visible = false;
                cboSenderNationalCreator2.Visible = false;
                cboSenderNationalCall2.Visible = false;
                txtNgayCapChuyenNhuong2.Visible = false;

                txtNgaySinhChuyenNhuong2.Text = string.Empty;
                txtSenderNationalId2.Text = string.Empty;
                cboSenderNationalCreator2.Text = string.Empty;
                cboSenderNationalCall2.Text = string.Empty;
                txtNgayCapChuyenNhuong2.Text = string.Empty;

                chkSenderResidentYN.Checked = false;
                chkSenderResidentYN.Enabled = false;
                label5.Visible = false;
                label7.Visible = false;
                label9.Visible = false;
            }
            else
            {
                txtSenderFullName2.Size = new System.Drawing.Size(121, 26);
                txtSenderFullName2.Text = string.Empty;
                txtNgaySinhChuyenNhuong2.Visible = true;
                txtSenderNationalId2.Visible = true;
                cboSenderNationalCreator2.Visible = true;
                cboSenderNationalCall2.Visible = true;
                txtNgayCapChuyenNhuong2.Visible = true;
                chkSenderResidentYN.Enabled = true;
                label5.Visible = true;
                label7.Visible = true;
                label9.Visible = true;
            }
        }

        private void cboReceiverCall2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == "Tài sản riêng")
            {
                txtReceiverFullName2.Size = new System.Drawing.Size(838, 26);
                txtReceiverFullName2.Text = "(Theo giấy xác nhận tình trạng hôn nhân số ... do UBND ... xác nhận ngày .../.../...)";
                txtNgaySinhNhanChuyenNhuong2.Visible = false;
                txtReceiverNationalId2.Visible = false;
                cboReceiverNationalCreator2.Visible = false;
                cboReceiverNationalCall2.Visible = false;
                txtNgayCapNhanChuyenNhuong2.Visible = false;

                txtNgaySinhNhanChuyenNhuong2.Text = string.Empty;
                txtReceiverNationalId2.Text = string.Empty;
                cboReceiverNationalCreator2.Text = string.Empty;
                cboReceiverNationalCall2.Text = string.Empty;
                txtNgayCapNhanChuyenNhuong2.Text = string.Empty;
                chkReceiver.Checked = false;
                chkReceiver.Enabled = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
            }
            else
            {
                chkReceiver.Enabled = true;
                txtReceiverFullName2.Size = new System.Drawing.Size(121, 26);
                txtReceiverFullName2.Text = string.Empty;
                txtNgaySinhNhanChuyenNhuong2.Visible = true;
                txtReceiverNationalId2.Visible = true;
                cboReceiverNationalCreator2.Visible = true;
                cboReceiverNationalCall2.Visible = true;
                txtNgayCapNhanChuyenNhuong2.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
            }
        }

        private void btUyQuyen1_Click(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                if (HandoverDemo1 == null || HandoverDemo1 == default(HandoverDemo) || HandoverDemo1.IsDisposed)
                {
                    var newList = new List<ListUserCustom>();
                    if (!cbDongChuyenQuyenA.Checked)
                    {
                        var editedItem = new ListUserCustom();
                        editedItem.DanhXung1 = cboSenderCall1.Text;
                        editedItem.HoVaTen1 = txtSenderFullName1.Text;

                        editedItem.DanhXung2 = cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : cboSenderCall2.Text;
                        editedItem.HoVaTen2 = cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : txtSenderFullName2.Text;

                        newList.Add(editedItem);
                    }
                    else
                    {
                        newList.AddRange(userListA);
                    }
                    var benChinh = CamketUpdateBenChinh(newList);
                    HandoverDemo1 = new HandoverDemo(this, 1, UyQuyenA, benChinh);
                    //if (TenUyQuyenA != null)
                    //{
                    //    Handover1.CopyAllData(DanhXungUyQuyenA, TenUyQuyenA, NgaySinhUyQuyenA, LoaiGiayToUyQuyenA,
                    //        SoGiayToA, NoiCapGiayToUyQuyenA, NgayCapUyQuyenA, CuTruUyQuyenA, CamKetUyQuyenA);
                    //}
                }
                HandoverDemo1.Show();
            }
        }

        private void btUyQuyen2_Click(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                if (HandoverDemo2 == null || HandoverDemo2 == default(HandoverDemo) || HandoverDemo2.IsDisposed)
                {
                    var newList = new List<ListUserCustom>();
                    if (!cbDongNhanChuyenQuyen.Checked)
                    {
                        var editedItem = new ListUserCustom();
                        editedItem.DanhXung1 = cboReceiverCall1.Text;
                        editedItem.HoVaTen1 = txtReceiverFullName1.Text;

                        editedItem.DanhXung2 = cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : cboReceiverCall2.Text;
                        editedItem.HoVaTen2 = cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : txtReceiverFullName2.Text;

                        newList.Add(editedItem);
                    }
                    else
                    {
                        newList.AddRange(userListB);
                    }
                    var benChinh = CamketUpdateBenChinh(newList);
                    HandoverDemo2 = new HandoverDemo(this, 2, UyQuyenB, benChinh);

                }
                HandoverDemo2.Show();
            }
        }

        private void cbDongChuyenQuyenA_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb.Checked)
            {
                var error = Validate();
                if(error != string.Empty)
                {
                    MessageBox.Show(error);
                    cb.Checked = false;
                }
                else
                {
                    userListA.Clear();
                    // if (userListB.Count() > 1) { userListB.Clear(); }
                    if (userListA.Count() <= 1 && txtSenderFullName1.Text != string.Empty
                        && txtSenderFullName1.Text != null)
                    {
                        LoadDefaultUserA();

                    }

                    cboSenderCall1.Enabled = false;
                    txtSenderFullName1.Enabled = false;
                    txtNgaySinhChuyenNhuong1.Enabled = false;
                    cboSenderNationalCall1.Enabled = false;
                    txtSenderNationalId1.Enabled = false;
                    cboSenderNationalCreator1.Enabled = false;
                    txtNgayCapChuyenNhuong1.Enabled = false;
                    cboCuTruA1.Enabled = false;

                    cboSenderCall2.Enabled = false;
                    txtSenderFullName2.Enabled = false;
                    txtNgaySinhChuyenNhuong2.Enabled = false;
                    cboSenderNationalCall2.Enabled = false;
                    txtSenderNationalId2.Enabled = false;
                    cboSenderNationalCreator2.Enabled = false;
                    txtNgayCapChuyenNhuong2.Enabled = false;
                    cboCuTruA2.Enabled = false;

                    btSuaDongCQA.Visible = true;
                    FormMainMulti multi = new FormMainMulti(this, userListA, 1);
                    multi.AddUser(userListA, true);
                    multi.Show();
                }
                
            }
            else
            {
                btSuaDongCQB.Visible = false;
                cboSenderCall1.Enabled = true;
                txtSenderFullName1.Enabled = true;
                txtNgaySinhChuyenNhuong1.Enabled = true;
                cboSenderNationalCall1.Enabled = true;
                txtSenderNationalId1.Enabled = true;
                cboSenderNationalCreator1.Enabled = true;
                txtNgayCapChuyenNhuong1.Enabled = true;
                cboCuTruA1.Enabled = true;

                cboSenderCall2.Enabled = true;
                txtSenderFullName2.Enabled = true;
                txtNgaySinhChuyenNhuong2.Enabled = true;
                cboSenderNationalCall2.Enabled = true;
                txtSenderNationalId2.Enabled = true;
                cboSenderNationalCreator2.Enabled = true;
                txtNgayCapChuyenNhuong2.Enabled = true;
                cboCuTruA2.Enabled = true;

            }
        }

        internal void SetUsers(int ben, List<ListUserCustom> users)
        {
            if (ben == 1)
            {
                userListA = users;
                if (userListA.Count > 0)
                {
                    var item = userListA.ElementAt(0);

                    cboSenderCall1.Text = item.DanhXung1;
                    txtSenderFullName1.Text = item.HoVaTen1;
                    txtNgaySinhChuyenNhuong1.Text = item.NgaySinh1;
                    cboSenderNationalCall1.Text = item.LoaiGiayTo1;
                    txtSenderNationalId1.Text = item.SoGiayTo1;
                    cboSenderNationalCreator1.Text = item.NoiCap1;
                    txtNgayCapChuyenNhuong1.Text = item.NgayCap1;
                    cboCuTruA1.Text = item.CuTru1;

                    cboSenderCall2.Text = item.DanhXung2;
                    txtSenderFullName2.Text = item.HoVaTen2;
                    txtNgaySinhChuyenNhuong2.Text = item.NgaySinh2;
                    cboSenderNationalCall2.Text = item.LoaiGiayTo2;
                    txtSenderNationalId2.Text = item.SoGiayTo2;
                    cboSenderNationalCreator2.Text = item.NoiCap2;
                    txtNgayCapChuyenNhuong2.Text = item.NgayCap2;
                    cboCuTruA2.Text = item.CuTru2;

                }

            }
            else
            {
                userListB = users;
                if (userListB.Count > 0)
                {
                    var item = userListB.ElementAt(0);

                    cboReceiverCall1.Text = item.DanhXung1;
                    txtReceiverFullName1.Text = item.HoVaTen1;
                    txtNgaySinhNhanChuyenNhuong1.Text = item.NgaySinh1;
                    cboReceiverNationalCall1.Text = item.LoaiGiayTo1;
                    txtReceiverNationalId1.Text = item.SoGiayTo1;
                    cboReceiverNationalCreator1.Text = item.NoiCap1;
                    txtNgayCapNhanChuyenNhuong1.Text = item.NgayCap1;
                    cboCuTruB1.Text = item.CuTru1;

                    cboReceiverCall2.Text = item.DanhXung2;
                    txtReceiverFullName2.Text = item.HoVaTen2;
                    txtNgaySinhNhanChuyenNhuong2.Text = item.NgaySinh2;
                    cboReceiverNationalCall2.Text = item.LoaiGiayTo2;
                    txtReceiverNationalId2.Text = item.SoGiayTo2;
                    cboReceiverNationalCreator2.Text = item.NoiCap2;
                    txtNgayCapNhanChuyenNhuong2.Text = item.NgayCap2;
                    cboCuTruB2.Text = item.CuTru2;

                }
            }
        }

        private void cbDongNhanChuyenQuyen_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb.Checked)
            {
                var error = Validate();
                if (error != string.Empty)
                {
                    MessageBox.Show(error);
                    cb.Checked = false;
                }
                else
                {
                    userListB.Clear();
                    // if (userListB.Count() > 1) { userListB.Clear(); }
                    if (userListB.Count() <= 1 && txtReceiverFullName1.Text != string.Empty
                        && txtReceiverFullName1.Text != null)
                    {
                        LoadDefaultUserB();

                    }
                    cboReceiverCall1.Enabled = false;
                    txtReceiverFullName1.Enabled = false;
                    txtNgaySinhNhanChuyenNhuong1.Enabled = false;
                    cboReceiverNationalCall1.Enabled = false;
                    txtReceiverNationalId1.Enabled = false;
                    cboReceiverNationalCreator1.Enabled = false;
                    txtNgayCapNhanChuyenNhuong1.Enabled = false;
                    cboCuTruB1.Enabled = false;

                    cboReceiverCall2.Enabled = false;
                    txtReceiverFullName2.Enabled = false;
                    txtNgaySinhNhanChuyenNhuong2.Enabled = false;
                    cboReceiverNationalCall2.Enabled = false;
                    txtReceiverNationalId2.Enabled = false;
                    cboReceiverNationalCreator2.Enabled = false;
                    txtNgayCapNhanChuyenNhuong2.Enabled = false;
                    cboCuTruB2.Enabled = false;

                    btSuaDongCQB.Visible = true;

                    FormMainMulti multi = new FormMainMulti(this, userListB, 2);
                    multi.AddUser(userListB, true);
                    multi.Show();
                }
            }
            else
            {
                btSuaDongCQB.Visible = false;
                cboReceiverCall1.Enabled = true;
                txtReceiverFullName1.Enabled = true;
                txtNgaySinhNhanChuyenNhuong1.Enabled = true;
                cboReceiverNationalCall1.Enabled = true;
                txtReceiverNationalId1.Enabled = true;
                cboReceiverNationalCreator1.Enabled = true;
                txtNgayCapNhanChuyenNhuong1.Enabled = true;
                cboCuTruB1.Enabled = true;

                cboReceiverCall2.Enabled = true;
                txtReceiverFullName2.Enabled = true;
                txtNgaySinhNhanChuyenNhuong2.Enabled = true;
                cboReceiverNationalCall2.Enabled = true;
                txtReceiverNationalId2.Enabled = true;
                cboReceiverNationalCreator2.Enabled = true;
                txtNgayCapNhanChuyenNhuong2.Enabled = true;
                cboCuTruB2.Enabled = true;

            }
        }

        private void LoadDefaultUserB()
        {
            var user = new ListUserCustom
            {
                Id = Guid.NewGuid(),
                DanhXung1 = cboReceiverCall1.Text,
                HoVaTen1 = txtReceiverFullName1.Text,
                NgaySinh1 = txtNgaySinhNhanChuyenNhuong1.Text,
                LoaiGiayTo1 = cboReceiverNationalCall1.Text,
                SoGiayTo1 = txtReceiverNationalId1.Text,
                NoiCap1 = cboReceiverNationalCreator1.Text,
                NgayCap1 = txtNgayCapNhanChuyenNhuong1.Text,
                CuTru1 = cboCuTruB1.Text,

                TaiSanRieng = cboReceiverCall2.Text == "Tài sản riêng" ? txtReceiverFullName2.Text : string.Empty,

                DanhXung2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text,
                HoVaTen2 = cboReceiverCall2.Text == "Tài sản riêng" ?  string.Empty : txtReceiverFullName2.Text,
                NgaySinh2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : txtNgaySinhNhanChuyenNhuong2.Text,
                LoaiGiayTo2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : cboReceiverNationalCall2.Text,
                SoGiayTo2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : txtReceiverNationalId2.Text,
                NoiCap2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : cboReceiverNationalCreator2.Text,
                NgayCap2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : txtNgayCapNhanChuyenNhuong2.Text,
                CuTru2 = txtReceiverFullName2.Text == null || txtReceiverFullName2.Text == string.Empty ? string.Empty : cboReceiverCall2.Text == "Tài sản riêng" ? string.Empty : (cboCuTruB2.Text != null && cboCuTruB2.Text != string.Empty && cboCuTruB2.Text != "") ?
        cboCuTruB2.Text : cboCuTruB1.Text
            };
            userListB.Add(user);

        }

        private void LoadDefaultUserA()
        {
            var user = new ListUserCustom
            {
                Id = Guid.NewGuid(),
                DanhXung1 = cboSenderCall1.Text,
                HoVaTen1 = txtSenderFullName1.Text,
                NgaySinh1 = txtNgaySinhChuyenNhuong1.Text,
                LoaiGiayTo1 = cboSenderNationalCall1.Text,
                SoGiayTo1 = txtSenderNationalId1.Text,
                NoiCap1 = cboSenderNationalCreator1.Text,
                NgayCap1 = txtNgayCapChuyenNhuong1.Text,
                CuTru1 = cboCuTruA1.Text,

                TaiSanRieng = cboSenderCall2.Text == "Tài sản riêng" ? txtSenderFullName2.Text : string.Empty,
                DanhXung2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text,
                HoVaTen2 = cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : txtSenderFullName2.Text,
                NgaySinh2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : txtNgaySinhChuyenNhuong2.Text,
                LoaiGiayTo2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : cboSenderNationalCall2.Text,
                SoGiayTo2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : txtSenderNationalId2.Text,
                NoiCap2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : cboSenderNationalCreator2.Text,
                NgayCap2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : txtNgayCapChuyenNhuong2.Text,
                CuTru2 = txtSenderFullName2.Text == null || txtSenderFullName2.Text == string.Empty ? string.Empty : cboSenderCall2.Text == "Tài sản riêng" ? string.Empty : (cboCuTruA2.Text != null && cboCuTruA2.Text != string.Empty && cboCuTruA2.Text != "") ?
            cboCuTruA2.Text : cboCuTruA1.Text
            };
            userListA.Add(user);
        }

        private DocX PrintUyQuyenChuyenNhuong(DocX template)
        {
            int index = 0;
            foreach (var user in UyQuyenA.Users)
            {
                if (user != null)
                {
                    index = index + 1;
                    //check data cua A1
                    // ho va ten
                    if (user.HoVaTen1 == null || user.HoVaTen1 == ""
                        || user.HoVaTen1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQA" + index, user.DanhXung1));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQA" + index, user.HoVaTen1));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDA" + index, user.DanhXung1
                            + ": " + user.HoVaTen1));
                    }

                    // ngay sinh
                    if (user.NgaySinh1 == null || user.NgaySinh1 == ""
                        || user.NgaySinh1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhuUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhuUQA" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQA" + index, user.NgaySinh1));
                        template.AddCustomProperty(new CustomProperty("NamSinhuUQDA" + index, "Năm sinh: " + user.NgaySinh1));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo1 == null || (user.SoGiayTo1 == "")
                       || (user.SoGiayTo1 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQA" + index, user.LoaiGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQA" + index, user.SoGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDA" + index, user.LoaiGiayTo1 + ": " + user.SoGiayTo1));

                    }

                    // noi cap giay to
                    if (user.NoiCap1 == null || user.NoiCap1 == ""
                        || user.NoiCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQA" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQA" + index, user.NoiCap1));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDA" + index, "Cấp tại: " + user.NoiCap1));
                    }

                    // ngay cap
                    if (user.NgayCap1 == null || user.NgayCap1 == ""
                        || user.NgayCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQA" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQA" + index, user.NgayCap1));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDA" + index, "Ngày cấp: " + user.NgayCap1));
                    }

                    // cu tru A1
                    if (user.CuTru1 == null || user.CuTru1 == ""
                        || user.CuTru1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQA" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQA" + index, user.CuTru1));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDA" + index, "Cư trú: " + user.CuTru1));

                    }

                    index = index + 1;
                    if (user.TaiSanRieng != string.Empty && user.TaiSanRieng != null)
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuUQA" + index, user.TaiSanRieng));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQA" + index, "Tài sản riêng"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQA" + index + "TaiSanRiengNhapLieuUQA" + index, "Tài sản riêng: " + user.TaiSanRieng));

                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQA" + index + "TaiSanRiengNhapLieuUQA" + index, "$"));

                    }

                    if (user.HoVaTen2 == null || user.HoVaTen2 == ""
                        || user.HoVaTen2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQA" + index, user.DanhXung2));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQA" + index, user.HoVaTen2));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDA" + index, user.DanhXung2
                            + ": " + user.HoVaTen2));
                    }

                    // ngay sinh
                    if (user.NgaySinh2 == null || user.NgaySinh2 == ""
                        || user.NgaySinh2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhUQA" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQA" + index, user.NgaySinh2));
                        template.AddCustomProperty(new CustomProperty("NamSinhUQDA" + index, "Năm sinh: " + user.NgaySinh2));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo2 == null || (user.SoGiayTo2 == "")
                       || (user.SoGiayTo2 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQA" + index, user.LoaiGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQA" + index, user.SoGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDA" + index, user.LoaiGiayTo2 + ": " + user.SoGiayTo2));

                    }

                    // noi cap giay to
                    if (user.NoiCap2 == null || user.NoiCap2 == ""
                        || user.NoiCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQA" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQA" + index, user.NoiCap2));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDA" + index, "Cấp tại: " + user.NoiCap2));
                    }

                    // ngay cap
                    if (user.NgayCap2 == null || user.NgayCap2 == ""
                        || user.NgayCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQA" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQA" + index, user.NgayCap2));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDA" + index, "Ngày cấp: " + user.NgayCap2));
                    }

                    // cu tru A1
                    if (user.CuTru2 == null || user.CuTru2 == ""
                        || user.CuTru2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQA" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQA" + index, user.CuTru2));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDA" + index, "Cư trú: " + user.CuTru2));

                    }
                }
            }
            // camket uy quyen A
            if (UyQuyenA.CamKet == null || UyQuyenA.CamKet == ""
                || UyQuyenA.CamKet == string.Empty)
            {
                template.AddCustomProperty(new CustomProperty("CamKetUyQuyenA", "$"));
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("CamKetUyQuyenA", UyQuyenA.CamKet));

            }
            return template;
        }
    
        private DocX PrintUyQuyenNhanChuyenNhuong(DocX template)
        {
            int index = 0;
            foreach (var user in UyQuyenB.Users)
            {
                if (user != null)
                {
                    index = index + 1;
                    //check data cua A1
                    // ho va ten
                    if (user.HoVaTen1 == null || user.HoVaTen1 == ""
                        || user.HoVaTen1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQB" + index, user.DanhXung1));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQB" + index, user.HoVaTen1));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDB" + index, user.DanhXung1
                            + ": " + user.HoVaTen1));
                    }

                    // ngay sinh
                    if (user.NgaySinh1 == null || user.NgaySinh1 == ""
                        || user.NgaySinh1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhUQB" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQB" + index, user.NgaySinh1));
                        template.AddCustomProperty(new CustomProperty("NamSinhUQDB" + index, "Năm sinh: " + user.NgaySinh1));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo1 == null || (user.SoGiayTo1 == "")
                       || (user.SoGiayTo1 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQB" + index, user.LoaiGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQB" + index, user.SoGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDB" + index, user.LoaiGiayTo1 + ": " + user.SoGiayTo1));

                    }

                    // noi cap giay to
                    if (user.NoiCap1 == null || user.NoiCap1 == ""
                        || user.NoiCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQB" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQB" + index, user.NoiCap1));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDB" + index, "Cấp tại: " + user.NoiCap1));
                    }

                    // ngay cap
                    if (user.NgayCap1 == null || user.NgayCap1 == ""
                        || user.NgayCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQB" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQB" + index, user.NgayCap1));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDB" + index, "Ngày cấp: " + user.NgayCap1));
                    }

                    // cu tru A1
                    if (user.CuTru1 == null || user.CuTru1 == ""
                        || user.CuTru1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDBB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQB" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQB" + index, user.CuTru1));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDB" + index, "Cư trú: " + user.CuTru1));

                    }

                    index = index + 1;
                    if (user.TaiSanRieng != string.Empty && user.TaiSanRieng != null)
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuUQB" + index, user.TaiSanRieng));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQB" + index, "Tài sản riêng"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQB" + index + "TaiSanRiengNhapLieuUQB" + index, "Tài sản riêng: " + user.TaiSanRieng));

                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengKieuChuUQB" + index + "TaiSanRiengNhapLieuUQB" + index, "$"));

                    }

                    if (user.HoVaTen2 == null || user.HoVaTen2 == ""
                        || user.HoVaTen2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungUQB" + index, user.DanhXung2));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQB" + index, user.HoVaTen2));
                        template.AddCustomProperty(new CustomProperty("HoVaTenUQDB" + index, user.DanhXung2
                            + ": " + user.HoVaTen2));
                    }

                    // ngay sinh
                    if (user.NgaySinh2 == null || user.NgaySinh2 == ""
                        || user.NgaySinh2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhUQB" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuUQB" + index, user.NgaySinh2));
                        template.AddCustomProperty(new CustomProperty("NamSinhUQDB" + index, "Năm sinh: " + user.NgaySinh2));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo2 == null || (user.SoGiayTo2 == "")
                       || (user.SoGiayTo2 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToUQB" + index, user.LoaiGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQB" + index, user.SoGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToUQDB" + index, user.LoaiGiayTo2 + ": " + user.SoGiayTo2));

                    }

                    // noi cap giay to
                    if (user.NoiCap2 == null || user.NoiCap2 == ""
                        || user.NoiCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToUQB" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQB" + index, user.NoiCap2));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToUQDB" + index, "Cấp tại: " + user.NoiCap2));
                    }

                    // ngay cap
                    if (user.NgayCap2 == null || user.NgayCap2 == ""
                        || user.NgayCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapUQB" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuUQB" + index, user.NgayCap2));
                        template.AddCustomProperty(new CustomProperty("NgayCapUQDB" + index, "Ngày cấp: " + user.NgayCap2));
                    }

                    // cu tru A1
                    if (user.CuTru2 == null || user.CuTru2 == ""
                        || user.CuTru2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruUQB" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuUQB" + index, user.CuTru2));
                        template.AddCustomProperty(new CustomProperty("CuTruUQDB" + index, "Cư trú: " + user.CuTru2));

                    }
                }
            }
            // camket uy quyen A
            if (UyQuyenB.CamKet == null || UyQuyenB.CamKet == ""
                || UyQuyenB.CamKet == string.Empty)
            {
                template.AddCustomProperty(new CustomProperty("CamKetUyQuyenB", "$"));
            }
            else
            {
                template.AddCustomProperty(new CustomProperty("CamKetUyQuyenB", UyQuyenB.CamKet));

            }
            return template;
        }

        private DocX PrintChuyenNhuong(DocX template)
        {

            // 1. Ben A
            int index = 0;
            foreach (var user in userListA)
            {
                if (user != null)
                {
                    index = index + 1;
                    //check data cua A1
                    // ho va ten 
                    if (user.HoVaTen1 == null || user.HoVaTen1 == ""
                        || user.HoVaTen1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungA" +index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungA" + index, user.DanhXung1));
                        template.AddCustomProperty(new CustomProperty("HoVaTenA" + index, user.HoVaTen1));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenA" + index, user.DanhXung1
                            + ": " + user.HoVaTen1));
                    }

                    // ngay sinh
                    if (user.NgaySinh1 == null || user.NgaySinh1 == ""
                        || user.NgaySinh1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhA" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuA" + index, user.NgaySinh1));
                        template.AddCustomProperty(new CustomProperty("NamSinhDA" + index, "Năm sinh: " + user.NgaySinh1));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo1 == null || (user.SoGiayTo1 == "")
                       || (user.SoGiayTo1 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToA" + index, user.LoaiGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToA" + index, user.SoGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDA" + index, user.LoaiGiayTo1 + ": " + user.SoGiayTo1));

                    }

                    // noi cap giay to
                    if (user.NoiCap1 == null || user.NoiCap1 == ""
                        || user.NoiCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToA" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToA" + index, user.NoiCap1));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDA" + index, "Cấp tại: " + user.NoiCap1));
                    }

                    // ngay cap
                    if (user.NgayCap1 == null || user.NgayCap1 == ""
                        || user.NgayCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapA" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuA" + index, user.NgayCap1));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDA" + index, "Ngày cấp: " + user.NgayCap1));
                    }

                    // cu tru A1
                    if (user.CuTru1 == null || user.CuTru1 == ""
                        || user.CuTru1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruA" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuA" + index, user.CuTru1));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDA" + index, "Cư trú: " + user.CuTru1));

                    }

                    index = index + 1;
                    if (user.TaiSanRieng != string.Empty && user.TaiSanRieng != null)
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuA" + index, user.TaiSanRieng));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengA" + index, "Tài sản riêng"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuDA" + index, "Tài sản riêng: " + user.TaiSanRieng));

                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuDA" + index, "$"));

                    }

                    if (user.HoVaTen2 == null || user.HoVaTen2 == ""
                        || user.HoVaTen2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungA" + index, user.DanhXung2));
                        template.AddCustomProperty(new CustomProperty("HoVaTenA" + index, user.HoVaTen2));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenA" + index, user.DanhXung2
                            + ": " + user.HoVaTen2));
                    }

                    // ngay sinh
                    if (user.NgaySinh2 == null || user.NgaySinh2 == ""
                        || user.NgaySinh2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhA" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuA" + index, user.NgaySinh2));
                        template.AddCustomProperty(new CustomProperty("NamSinhDA" + index, "Năm sinh: " + user.NgaySinh2));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo2 == null || (user.SoGiayTo2 == "")
                       || (user.SoGiayTo2 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToA" + index, user.LoaiGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToA" + index, user.SoGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDA" + index, user.LoaiGiayTo2 + ": " + user.SoGiayTo2));

                    }

                    // noi cap giay to
                    if (user.NoiCap2 == null || user.NoiCap2 == ""
                        || user.NoiCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToA" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToA" + index, user.NoiCap2));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDA" + index, "Cấp tại: " + user.NoiCap2));
                    }

                    // ngay cap
                    if (user.NgayCap2 == null || user.NgayCap2 == ""
                        || user.NgayCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapA" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuA" + index, user.NgayCap2));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDA" + index, "Ngày cấp: " + user.NgayCap2));
                    }

                    // cu tru A1
                    if (user.CuTru2 == null || user.CuTru2 == ""
                        || user.CuTru2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuA" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDA" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruA" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuA" + index, user.CuTru2));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDA" + index, "Cư trú: " + user.CuTru2));

                    }
                }
            }

            return template;
        }

        private DocX PrintNhanChuyenNhuong(DocX template)
        {
            
            int index = 0;
            foreach (var user in userListB)
            {
                if (user != null)
                {
                    index = index + 1;
                    //check data cua A1
                    // ho va ten
                    if (user.HoVaTen1 == null || user.HoVaTen1 == ""
                        || user.HoVaTen1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungB" + index, user.DanhXung1));
                        template.AddCustomProperty(new CustomProperty("HoVaTenB" + index, user.HoVaTen1));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenB" + index, user.DanhXung1
                            + ": " + user.HoVaTen1));
                    }

                    // ngay sinh
                    if (user.NgaySinh1 == null || user.NgaySinh1 == ""
                        || user.NgaySinh1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhB" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuB" + index, user.NgaySinh1));
                        template.AddCustomProperty(new CustomProperty("NamSinhDB" + index, "Năm sinh: " + user.NgaySinh1));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo1 == null || (user.SoGiayTo1 == "")
                       || (user.SoGiayTo1 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToB" + index, user.LoaiGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToB" + index, user.SoGiayTo1));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDB" + index, user.LoaiGiayTo1 + ": " + user.SoGiayTo1));

                    }

                    // noi cap giay to
                    if (user.NoiCap1 == null || user.NoiCap1 == ""
                        || user.NoiCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToB" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToB" + index, user.NoiCap1));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDB" + index, "Cấp tại: " + user.NoiCap1));
                    }

                    // ngay cap
                    if (user.NgayCap1 == null || user.NgayCap1 == ""
                        || user.NgayCap1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapB" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuB" + index, user.NgayCap1));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDB" + index, "Ngày cấp: " + user.NgayCap1));
                    }

                    // cu tru A1
                    if (user.CuTru1 == null || user.CuTru1 == ""
                        || user.CuTru1 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruB" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuB" + index, user.CuTru1));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDB" + index, "Cư trú: " + user.CuTru1));

                    }

                    index = index + 1;
                    if (user.TaiSanRieng != string.Empty && user.TaiSanRieng != null)
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuB" + index, user.TaiSanRieng));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengB" + index, "Tài sản riêng"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuDB" + index, "Tài sản riêng: " + user.TaiSanRieng));

                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TaiSanRiengNhapLieuDB" + index, "$"));

                    }

                    if (user.HoVaTen2 == null || user.HoVaTen2 == ""
                        || user.HoVaTen2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("HoVaTenB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("DanhXungB" + index, user.DanhXung2));
                        template.AddCustomProperty(new CustomProperty("HoVaTenB" + index, user.HoVaTen2));
                        template.AddCustomProperty(new CustomProperty("DXHoVaTenB" + index, user.DanhXung2
                            + ": " + user.HoVaTen2));
                    }

                    // ngay sinh
                    if (user.NgaySinh2 == null || user.NgaySinh2 == ""
                        || user.NgaySinh2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NamSinhDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NamSinhB" + index, "Năm sinh: "));
                        template.AddCustomProperty(new CustomProperty("NamSinhNhapLieuB" + index, user.NgaySinh2));
                        template.AddCustomProperty(new CustomProperty("NamSinhDB" + index, "Năm sinh: " + user.NgaySinh2));
                    }


                    // loai giay to A1
                    //var loaiGiayToA1 = cboSenderNationalCall1.Text == null ? string.Empty : cboSenderNationalCall1.Text;
                    if (user.SoGiayTo2 == null || (user.SoGiayTo2 == "")
                       || (user.SoGiayTo2 == string.Empty))
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("LoaiGiayToB" + index, user.LoaiGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToB" + index, user.SoGiayTo2));
                        template.AddCustomProperty(new CustomProperty("SoGiayToDB" + index, user.LoaiGiayTo2 + ": " + user.SoGiayTo2));

                    }

                    // noi cap giay to
                    if (user.NoiCap2 == null || user.NoiCap2 == ""
                        || user.NoiCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NoiCapGiayToB" + index, "Cấp tại:"));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToB" + index, user.NoiCap2));
                        template.AddCustomProperty(new CustomProperty("TenNoiCapGiayToDB" + index, "Cấp tại: " + user.NoiCap2));
                    }

                    // ngay cap
                    if (user.NgayCap2 == null || user.NgayCap2 == ""
                        || user.NgayCap2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("NgayCapB" + index, "Ngày cấp: "));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuB" + index, user.NgayCap2));
                        template.AddCustomProperty(new CustomProperty("NgayCapNhapLieuDB" + index, "Ngày cấp: " + user.NgayCap2));
                    }

                    // cu tru A1
                    if (user.CuTru2 == null || user.CuTru2 == ""
                        || user.CuTru2 == string.Empty)
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuB" + index, "$"));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDB" + index, "$"));
                    }
                    else
                    {
                        template.AddCustomProperty(new CustomProperty("CuTruB" + index, "Cư trú: "));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuB" + index, user.CuTru2));
                        template.AddCustomProperty(new CustomProperty("CuTruNhapLieuDB" + index, "Cư trú: " + user.CuTru2));

                    }
                }
            }

            return template;
        }

        private void btSuaDongCQA_Click(object sender, EventArgs e)
        {
            FormMainMulti multi = new FormMainMulti(this, userListA, 1);
           // multi.AddUser(userListA, true);
            multi.Show();
        }

        private void btSuaDongCQB_Click(object sender, EventArgs e)
        {
            FormMainMulti multi = new FormMainMulti(this, userListB, 2);
            multi.AddUser(userListB, true);
            multi.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cboSenderCall1.SelectedIndex = 0;
            txtSenderFullName1.Text = string.Empty;
            txtNgaySinhChuyenNhuong1.Text = string.Empty;
            cboSenderNationalCall1.SelectedIndex = 2;
            txtSenderNationalId1.Text = string.Empty;
            cboSenderNationalCreator1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapChuyenNhuong1.Text = string.Empty;
            cboCuTruA1.Text = string.Empty;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cboSenderCall2.SelectedIndex = 0;
            txtSenderFullName2.Text = string.Empty;
            txtNgaySinhChuyenNhuong2.Text = string.Empty;
            cboSenderNationalCall2.SelectedIndex = 2;
            txtSenderNationalId2.Text = string.Empty;
            cboSenderNationalCreator2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapChuyenNhuong2.Text = string.Empty;
            chkSenderResidentYN.Checked = false;
            button2.Enabled = false;
            cboCuTruA2.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cboReceiverCall1.SelectedIndex = 0;
            txtReceiverFullName1.Text = string.Empty;
            txtNgaySinhNhanChuyenNhuong1.Text = string.Empty;
            cboReceiverNationalCall1.SelectedIndex = 2;
            txtReceiverNationalId1.Text = string.Empty;
            cboReceiverNationalCreator1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapNhanChuyenNhuong1.Text = string.Empty;
            button3.Enabled = false;
            cboCuTruB1.Text = string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cboReceiverCall2.SelectedIndex = 0;
            txtReceiverFullName2.Text = string.Empty;
            txtNgaySinhNhanChuyenNhuong2.Text = string.Empty;
            cboReceiverNationalCall2.SelectedIndex = 2;
            txtReceiverNationalId2.Text = string.Empty;
            cboReceiverNationalCreator2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapNhanChuyenNhuong2.Text = string.Empty;
            chkReceiver.Checked = false;
            button4.Enabled = false;
            cboCuTruB2.Text = string.Empty;
        }

        private void txtSenderFullName1_TextChanged(object sender, EventArgs e)
        {
            if(txtSenderFullName1.Text != string.Empty || txtNgaySinhChuyenNhuong1.Text != string.Empty
                || txtSenderNationalId1.Text != string.Empty || txtNgayCapChuyenNhuong1.Text != string.Empty
               )
            {
                button1.Enabled = true;
            }
        }

        private void txtNgaySinhChuyenNhuong1_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName1.Text != string.Empty || txtNgaySinhChuyenNhuong1.Text != string.Empty
                || txtSenderNationalId1.Text != string.Empty || txtNgayCapChuyenNhuong1.Text != string.Empty
               )
            {
                button1.Enabled = true;
            }
        }

        private void txtSenderNationalId1_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName1.Text != string.Empty || txtNgaySinhChuyenNhuong1.Text != string.Empty
                || txtSenderNationalId1.Text != string.Empty || txtNgayCapChuyenNhuong1.Text != string.Empty
               )
            {
                button1.Enabled = true;
            }
        }

        private void txtNgayCapChuyenNhuong1_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName1.Text != string.Empty || txtNgaySinhChuyenNhuong1.Text != string.Empty
                || txtSenderNationalId1.Text != string.Empty || txtNgayCapChuyenNhuong1.Text != string.Empty
               )
            {
                button1.Enabled = true;
            }
        }

        private void txtNgayCapChuyenNhuong2_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName2.Text != string.Empty || txtNgaySinhChuyenNhuong2.Text != string.Empty
                || txtSenderNationalId2.Text != string.Empty || txtNgayCapChuyenNhuong2.Text != string.Empty
               )
            {
                button2.Enabled = true;
            }
        }

        private void cboSenderNationalCreator2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName2.Text != string.Empty || txtNgaySinhChuyenNhuong2.Text != string.Empty
                || txtSenderNationalId2.Text != string.Empty || txtNgayCapChuyenNhuong2.Text != string.Empty
               )
            {
                button2.Enabled = true;
            }
        }

        private void txtSenderNationalId2_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName2.Text != string.Empty || txtNgaySinhChuyenNhuong2.Text != string.Empty
                || txtSenderNationalId2.Text != string.Empty || txtNgayCapChuyenNhuong2.Text != string.Empty
               )
            {
                button2.Enabled = true;
            }
        }

        private void txtNgaySinhChuyenNhuong2_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName2.Text != string.Empty || txtNgaySinhChuyenNhuong2.Text != string.Empty
                || txtSenderNationalId2.Text != string.Empty || txtNgayCapChuyenNhuong2.Text != string.Empty
               )
            {
                button2.Enabled = true;
            }
        }

        private void txtSenderFullName2_TextChanged(object sender, EventArgs e)
        {
            if (txtSenderFullName2.Text != string.Empty || txtNgaySinhChuyenNhuong2.Text != string.Empty
                || txtSenderNationalId2.Text != string.Empty || txtNgayCapChuyenNhuong2.Text != string.Empty
               )
            {
                button2.Enabled = true;
            }
        }

        private void txtNgayCapNhanChuyenNhuong2_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName2.Text != string.Empty || txtNgaySinhNhanChuyenNhuong2.Text != string.Empty
                || txtReceiverNationalId2.Text != string.Empty || txtNgayCapNhanChuyenNhuong2.Text != string.Empty
               )
            {
                button4.Enabled = true;
            }
        }

        private void cboReceiverNationalCreator2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName2.Text != string.Empty || txtNgaySinhNhanChuyenNhuong2.Text != string.Empty
                || txtReceiverNationalId2.Text != string.Empty || txtNgayCapNhanChuyenNhuong2.Text != string.Empty
               )
            {
                button4.Enabled = true;
            }
        }

        private void txtReceiverNationalId2_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName2.Text != string.Empty || txtNgaySinhNhanChuyenNhuong2.Text != string.Empty
                || txtReceiverNationalId2.Text != string.Empty || txtNgayCapNhanChuyenNhuong2.Text != string.Empty
               )
            {
                button4.Enabled = true;
            }
        }

        private void txtNgaySinhNhanChuyenNhuong2_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName2.Text != string.Empty || txtNgaySinhNhanChuyenNhuong2.Text != string.Empty
                || txtReceiverNationalId2.Text != string.Empty || txtNgayCapNhanChuyenNhuong2.Text != string.Empty
               )
            {
                button4.Enabled = true;
            }
        }

        private void txtReceiverFullName2_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName2.Text != string.Empty || txtNgaySinhNhanChuyenNhuong2.Text != string.Empty
                || txtReceiverNationalId2.Text != string.Empty || txtNgayCapNhanChuyenNhuong2.Text != string.Empty
               )
            {
                button4.Enabled = true;
            }
        }

        private void txtReceiverFullName1_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName1.Text != string.Empty || txtNgaySinhNhanChuyenNhuong1.Text != string.Empty
                || txtReceiverNationalId1.Text != string.Empty || txtNgayCapNhanChuyenNhuong1.Text != string.Empty
               )
            {
                button3.Enabled = true;
            }
        }

        private void txtNgaySinhNhanChuyenNhuong1_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName1.Text != string.Empty || txtNgaySinhNhanChuyenNhuong1.Text != string.Empty
                || txtReceiverNationalId1.Text != string.Empty || txtNgayCapNhanChuyenNhuong1.Text != string.Empty
               )
            {
                button3.Enabled = true;
            }
        }

        private void txtReceiverNationalId1_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName1.Text != string.Empty || txtNgaySinhNhanChuyenNhuong1.Text != string.Empty
                || txtReceiverNationalId1.Text != string.Empty || txtNgayCapNhanChuyenNhuong1.Text != string.Empty
               )
            {
                button3.Enabled = true;
            }
        }

        private void cboReceiverNationalCreator1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName1.Text != string.Empty || txtNgaySinhNhanChuyenNhuong1.Text != string.Empty
                || txtReceiverNationalId1.Text != string.Empty || txtNgayCapNhanChuyenNhuong1.Text != string.Empty
               )
            {
                button3.Enabled = true;
            }
        }

        private void txtNgayCapNhanChuyenNhuong1_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiverFullName1.Text != string.Empty || txtNgaySinhNhanChuyenNhuong1.Text != string.Empty
                || txtReceiverNationalId1.Text != string.Empty || txtNgayCapNhanChuyenNhuong1.Text != string.Empty
               )
            {
                button3.Enabled = true;
            }
        }

        private string CamketUpdateBenChinh(List<ListUserCustom> users)
        {
            var lastIndex = users.Count - 1;
            var index = -1;

            var uq = string.Empty;

            foreach (var u in users)
            {
                index = index + 1;
                if (u.HoVaTen1 != string.Empty && u.HoVaTen1 != null)
                {
                    if (index == 0)
                    {
                        uq += u.DanhXung1 + " " + u.HoVaTen1;
                    }
                    else if (index != 0 && u.HoVaTen2 != string.Empty && u.HoVaTen2 != null)
                    {
                        uq += ", " + u.DanhXung1.ToLower() + " " + u.HoVaTen1;

                    }
                    else if (index != 0 && (u.HoVaTen2 == string.Empty || u.HoVaTen2 == null))
                    {
                        uq += " và " + u.DanhXung1.ToLower() + " " + u.HoVaTen1;

                    }

                }
                if (u.HoVaTen2 != string.Empty && u.HoVaTen2 != null)
                {
                    if (index == lastIndex)
                    {
                        uq += " và " + u.DanhXung2.ToLower() + " " + u.HoVaTen2;

                    }
                    else
                    {
                        uq += ", " + u.DanhXung2.ToLower() + " " + u.HoVaTen2;
                    }
                }
            }
            return uq;
        }

        private string CamketUpdate(List<ListUserCustom> users)
        {
            var lastIndex = users.Count - 1;
            var index = -1;

            var uq = string.Empty;

            foreach (var u in users)
            {
                index = index + 1;
                if (u.HoVaTen1 != string.Empty && u.HoVaTen1 != null)
                {
                    if (index == 0)
                    {
                        uq += u.DanhXung1.ToLower() + " " + u.HoVaTen1;
                    }
                    else if (index != 0 && u.HoVaTen2 != string.Empty && u.HoVaTen2 != null)
                    {
                        uq += ", " + u.DanhXung1.ToLower() + " " + u.HoVaTen1;

                    }
                    else if (index != 0 && (u.HoVaTen2 == string.Empty || u.HoVaTen2 == null))
                    {
                        uq += " và " + u.DanhXung1.ToLower() + " " + u.HoVaTen1;

                    }

                }
                if (u.HoVaTen2 != string.Empty && u.HoVaTen2 != null)
                {
                    if (index == lastIndex)
                    {
                        uq += " và " + u.DanhXung2.ToLower() + " " + u.HoVaTen2;

                    }
                    else
                    {
                        uq += ", " + u.DanhXung2.ToLower() + " " + u.HoVaTen2;
                    }
                }
            }
            return uq;
        }

        private string Validate()
        {
            var error = string.Empty;
            Int64 number;
            DateTime dateTime;
            string[] formats = {"dd/MM/yyyy", "dd-MM-yyyy",
                                "d/MM/yyyy", "dd/M/yyyy",
                                "d-MM-yyyy", "dd-M-yyyy",
                                "d/M/yyyy", "d-M-yyyy"};

            // ngay sinh A
            if (txtNgaySinhChuyenNhuong1.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtNgaySinhChuyenNhuong1.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng năm sinh người thứ nhất bên A" + Environment.NewLine;
                }
                else
                {
                    if (DateTime.Now.Year - number < 18)
                    {
                        error += "Năm sinh người thứ nhất bên A không hợp lệ (Phải lớn hơn 18 tuổi)" + Environment.NewLine;
                    }
                    number = 0;
                }
            }

            if (txtNgaySinhChuyenNhuong2.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtNgaySinhChuyenNhuong2.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng năm sinh người thứ hai bên A" + Environment.NewLine;
                }
                else
                {
                    if (DateTime.Now.Year - number < 18)
                    {
                        error += "Năm sinh người thứ hai bên A không hợp lệ (Phải lớn hơn 18 tuổi)" + Environment.NewLine;
                    }
                    number = 0;
                }
            }

            // Ngay cap A
            if (txtNgayCapChuyenNhuong1.Text != string.Empty)
            {
                var parse = DateTime.TryParseExact(txtNgayCapChuyenNhuong1.Text, formats, 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!parse)
                {
                    error += "Sai định dạng ngày cấp giấy tờ người thứ nhất bên A" + Environment.NewLine;
                }
                else
                {
                    //txtNgayCapChuyenNhuong1.Text = dateTime.ToString("dd/MM/yyyy");
                    if (dateTime > DateTime.Now)
                    {
                        error += "Ngày cấp giấy tờ người thứ nhất bên A không hợp lệ (Phải nhỏ hơn năm hiện tại)" + Environment.NewLine;
                    }
                }
            }

            if (txtNgayCapChuyenNhuong2.Text != string.Empty)
            {
                var parse = DateTime.TryParseExact(txtNgayCapChuyenNhuong2.Text, formats,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!parse)
                {
                    error += "Sai định dạng ngày cấp giấy tờ người thứ hai bên A" + Environment.NewLine;
                }
                else
                {
                    //txtNgayCapChuyenNhuong2.Text = dateTime.ToString("dd/MM/yyyy");
                    if (dateTime > DateTime.Now)
                    {
                        error += "Ngày cấp giấy tờ người thứ hai bên A không hợp lệ (Phải nhỏ hơn thời gian hiện tại)" + Environment.NewLine;
                    }
                    number = 0;
                }



            }

            // So giay to A
            if (txtSenderNationalId1.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtSenderNationalId1.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng số CMND/ Hộ chiếu/CCCD người thứ nhất bên A" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtSenderNationalId2.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtSenderNationalId2.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng số CMND/ Hộ chiếu/CCCD người thứ hai bên A" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            // NGAY SINH B
            if (txtNgaySinhNhanChuyenNhuong1.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtNgaySinhNhanChuyenNhuong1.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng năm sinh người thứ nhất bên B" + Environment.NewLine;
                }
                else
                {
                    if (DateTime.Now.Year - number < 18)
                    {
                        error += "Năm sinh người thứ nhất bên B không hợp lệ (Phải lớn hơn 18 tuổi)" + Environment.NewLine;
                    }
                    number = 0;
                }
            }

            if (txtNgaySinhNhanChuyenNhuong2.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtNgaySinhNhanChuyenNhuong2.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng năm sinh người thứ hai bên B" + Environment.NewLine;
                }
                else
                {
                    if (DateTime.Now.Year - number < 18)
                    {
                        error += "Năm sinh người thứ hai bên B không hợp lệ (Phải lớn hơn 18 tuổi)" + Environment.NewLine;
                    }
                    number = 0;
                }
                
            }

            // NGÀY CẤP B
            if (txtNgayCapNhanChuyenNhuong1.Text != string.Empty)
            {
                var parse = DateTime.TryParseExact(txtNgayCapNhanChuyenNhuong1.Text, formats,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!parse)
                {
                    error += "Sai định dạng ngày cấp giấy tờ người thứ nhất bên B" + Environment.NewLine;
                }
                else
                {
                   // txtNgayCapNhanChuyenNhuong1.Text = dateTime.ToString("dd/MM/yyyy");
                    if (dateTime > DateTime.Now)
                    {
                        error += "Ngày cấp giấy tờ người thứ nhất bên B không hợp lệ (Phải nhỏ hơn thời gian hiện tại)" + Environment.NewLine;
                    }
                }
            }

            if (txtNgayCapNhanChuyenNhuong2.Text != string.Empty)
            {
                var parse = DateTime.TryParseExact(txtNgayCapNhanChuyenNhuong2.Text, formats,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!parse)
                {
                    error += "Sai định dạng ngày cấp giấy tờ người thứ hai bên B" + Environment.NewLine;
                }
                else
                {
                    //txtNgayCapNhanChuyenNhuong2.Text = dateTime.ToString("dd/MM/yyyy");
                    if (dateTime > DateTime.Now)
                    {
                        error += "Ngày cấp giấy tờ người thứ hai bên B không hợp lệ (Phải nhỏ hơn thời gian hiện tại)" + Environment.NewLine;
                    }
                    number = 0;
                }
            }

            // SO CMND BÊN B
            if (txtReceiverNationalId1.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtReceiverNationalId1.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng số CMND/ Hộ chiếu/CCCD người thứ nhất bên B" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtSenderNationalId2.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtSenderNationalId2.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng số CMND/ Hộ chiếu/CCCD người thứ hai bên B" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtThuaDatSo.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtThuaDatSo.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng thửa đất số" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtToBanDoSo.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtThuaDatSo.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng bản đồ số" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtDienTich.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtThuaDatSo.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng diện tích" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtDienTichSdRieng.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtThuaDatSo.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng diện tích sử dụng riêng" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtDienTichSdChung.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtThuaDatSo.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng diện tích sử dụng chung" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtGia.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtThuaDatSo.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng giá tiền" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }
            return error;
        }

        private void txtSenderFullName1_TabStopChanged(object sender, EventArgs e)
        {
            
        }

        private void txtNgaySinhChuyenNhuong1_TabIndexChanged(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
        }

        private void txtSoVaoSo_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public static class Constants
    {
        public const string User1 = "{0}: {1}, \nsinh năm: {2};{3}: {4} cấp tại {5} ngày {6};\nĐịa chỉ thường trú: {7};";

        public const string User2 = "\r\n\r\nCùng với {0} là {1}: {2}, sinh năm: {3};\n{4}: {5} cấp tại {6} ngày {7};\nĐịa chỉ thường trú: {8};";

        public const string FileName = "{0}-{1}-{2} Và {3}-{4}.docx";
        public static string NewFile = "D:\\HS ĐẤT ĐAI\\{0}\\{1}";
        public const string User = "{0}{1}{2}#{3}{4}{5}{6}#{7}#";

        public const string CamKet = "({0} có người đại diện theo ủy quyền là {1}, theo hợp đồng ủy quyền số: {2}, quyển số 01 TP/CC-SCC/HĐGD do UBND xã {3} chứng thực hoặc Văn phòng công chứng {4} ký ngày {5}, {6} cam kết hợp đồng ủy quyền nêu trên vẫn còn hiệu lực theo luật định).";
    }
}
