using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    public partial class MiniForm : Form
    {
        private FormMain Main;
        public MiniForm()
        {
            InitializeComponent();
            //cboQSDD2.SelectedIndex = 0;
            //cboMucDichSd2.SelectedIndex  = 0;
            //cboNguonGocSd2.SelectedIndex = 0;
            //cboUBND2.SelectedIndex = 0;
        }

        public MiniForm(FormMain main)
        {
            InitializeComponent();
            this.Main = main;
            cboQSDD2.SelectedIndex = 0;
            //cboMucDichSd2.SelectedIndex = 0;
            //cboNguonGocSd2.SelectedIndex = 0;
            cboUBND2.SelectedIndex = 0;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            UpdateForMain();
            Main.BackupData();
            MessageBox.Show("Bổ sung thông tin thành công!");
            this.Visible = false;
        }

        public void UpdateForMain()
        {
            Main.UpdateData(cboQSDD2.Text, txtSoQuyenSuDungDat2.Text, txtSoVaoSo2.Text,
                cboUBND2.Text, txtNgayCapQSDD2.Text, txtThuaDatSo2.Text, txtToBanDoSo2.Text,
                cboDiaChiThuaDat.Text, txtDienTich2.Text, txtDienTichSdRieng2.Text, txtDienTichSdChung2.Text,
                cboMucDichSd2.Text, cboThoiHan.Text, cboNguonGocSd2.Text, txtHanCheQuyenSd2.Text,
                txtQuyenSoHuuNhaO.Text, txtQuyenSoHuuTaiSanKhac.Text);

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

        private void button1_Click(object sender, EventArgs e)
        {
            CopyAllData(true);
        }

        public void CopyAllData(bool fromMain = false)
        {
            cboQSDD2.Text = Main.GetQSDD2(fromMain);
            txtSoQuyenSuDungDat2.Text = Main.GetSoQSDD2(fromMain);
            txtSoVaoSo2.Text = Main.GetSoVaoSo2(fromMain);
            cboUBND2.Text = Main.GetUBND2(fromMain);
            txtNgayCapQSDD2.Text = Main.GetNgayCapQSDD2(fromMain);
            txtThuaDatSo2.Text = Main.GetThuaDatSo2(fromMain);
            txtToBanDoSo2.Text = Main.GetToBanDoSo2(fromMain);
            cboDiaChiThuaDat.Text = Main.GetDiaChiThuaDat2(fromMain);
            txtDienTich2.Text = Main.GetDienTich2(fromMain);
            txtDienTichSdRieng2.Text = Main.GetDienTichRieng2(fromMain);
            txtDienTichSdChung2.Text = Main.GetDienTichChung2(fromMain);
            cboMucDichSd2.Text = Main.GetMucDichSuDung2(fromMain);
            cboThoiHan.Text = Main.GetThoiHan2(fromMain);
            cboNguonGocSd2.Text = Main.GetNguonGocSuDung2(fromMain);
            txtHanCheQuyenSd2.Text = Main.GetHanCheQuyenSuDung2(fromMain);
            txtQuyenSoHuuNhaO.Text = Main.GetQuyenSuDungDat2(fromMain);
            txtQuyenSoHuuTaiSanKhac.Text = Main.GetQuyenSoHuuTaiSanKhac2(fromMain);
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

        private void cboThoiHan_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void cboNguonGocSd2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void ckbQDSDD2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if(ckb.Checked == true){
                cboQSDD2.Text = Main.GetQSDD2(true);
            }
            else
            {
                cboQSDD2.Text = string.Empty;
            }
        }

        private void ckbSo2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtSoQuyenSuDungDat2.Text = Main.GetSoQSDD2(true);
            }
            else
            {
                txtSoQuyenSuDungDat2.Text = String.Empty;
            }
        }

        private void ckbSoVaoSo2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtSoVaoSo2.Text = Main.GetSoVaoSo2(true);
            }
            else
            {
                txtSoVaoSo2.Text = String.Empty;
            }
        }

        private void ckbUBND2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                cboUBND2.SelectedItem = Main.GetUBND2(true);
            }
            else
            {
                cboUBND2.SelectedItem = null;
            }
        }

        private void ckbNgayCap2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtNgayCapQSDD2.Text = Main.GetNgayCapQSDD2(true);
            }
            else
            {
                txtNgayCapQSDD2.Text = String.Empty;
            }
        }

        private void ckbThuaDatSo2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtThuaDatSo2.Text = Main.GetThuaDatSo2(true);
            }
            else
            {
                txtThuaDatSo2.Text = String.Empty;
            }
        }

        private void cbkToBanDoSo2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtToBanDoSo2.Text = Main.GetToBanDoSo2(true);
            }
            else
            {
                txtToBanDoSo2.Text = String.Empty;
            }
        }

        private void ckbDiaChiThuaDat2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                cboDiaChiThuaDat.Text = Main.GetDiaChiThuaDat2(true);
            }
            else
            {
                cboDiaChiThuaDat.Text = null;
            }
        }

        private void ckbDienTich2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtDienTich2.Text = Main.GetDienTich2(true);
            }
            else
            {
                txtDienTich2.Text = String.Empty;
            }
        }

        private void ckbSuDungRieng2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtDienTichSdRieng2.Text = Main.GetDienTichRieng2(true);
            }
            else
            {
                txtDienTichSdRieng2.Text = String.Empty;
            }
        }

        private void ckbSuDungChung_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtDienTichSdChung2.Text = Main.GetDienTichChung2(true);
            }
            else
            {
                txtDienTichSdChung2.Text = null;
            }
        }

        private void ckbMucDichSd2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                cboMucDichSd2.Text = Main.GetMucDichSuDung2(true);
            }
            else
            {
                cboMucDichSd2.Text = String.Empty;
            }
        }

        private void ckbThoiHan2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                cboThoiHan.Text = Main.GetThoiHan2(true);
            }
            else
            {
                cboThoiHan.SelectedItem = null;
            }
        }

        private void ckbNguonGocSd2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                cboNguonGocSd2.Text = Main.GetNguonGocSuDung2(true);
            }
            else
            {
                cboNguonGocSd2.Text = null;
            }
        }

        private void ckbHanCheQuyenSd_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked == true)
            {
                txtHanCheQuyenSd2.Text = Main.GetHanCheQuyenSuDung2(true);
            }
            else
            {
                txtHanCheQuyenSd2.Text = String.Empty;
            }
        }

        private void cboUBND2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboUBND2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void cboQSDD2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void cboQSDD2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboUBND2.SelectedItem = "Sở Tài nguyên và Môi trường";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboUBND2.SelectedItem = "UBND huyện";
            }
        }

        private void txtDienTich2_TextChanged(object sender, EventArgs e)
        {
            txtDienTichSdRieng2.Text = txtDienTich2.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk.Checked)
            {
                txtQuyenSoHuuNhaO.Text = Main.GetQuyenSuDungDat2(true);
            }
            else
            {
                txtQuyenSoHuuNhaO.Text = "Không có";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk.Checked)
            {
                txtQuyenSoHuuTaiSanKhac.Text = Main.GetQuyenSoHuuTaiSanKhac2(true);
            }
            else
            {
                txtQuyenSoHuuTaiSanKhac.Text = "Không có";
            }
            
        }
    }
}
        
        
       
      
