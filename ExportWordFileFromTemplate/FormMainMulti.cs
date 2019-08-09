using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    public partial class FormMainMulti : Form
    {
        private List<ListUserCustom> userList;
        private FormMain Main;
        private Guid SelectedId;
        private int ben;

        public FormMainMulti(FormMain main, List<ListUserCustom> users, int ben)
        {
            InitializeComponent();

            //CheckATachCuTru();

            this.Main = main;
            userList = users;
            this.ben = ben;
            if(this.ben == 1)
            {
                lbDaiDien.Text = "1. BÊN CHUYỂN QUYỀN (Bên A)";
            }
            else
            {
                lbDaiDien.Text = "1. BÊN NHẬN CHUYỂN QUYỀN (Bên B)";
            }

            lvUser.MultiSelect = false;
            lvUser.FullRowSelect = true;
            lvUser.GridLines = true;

           
            LoadDefaultData();
            AddUser(userList, true);
        }
        private void LoadDefaultData()
        {
            
            cboDanhXungUQ1.SelectedIndex = 0;
            cboDanhXungUQ2.SelectedIndex = 1;

            txtHoVaTenUQ1.Text = string.Empty;
            txtHoVaTenUQ2.Text = string.Empty;

            txtNgaySinhUQ1.Text = string.Empty;
            txtNgaySinhUQ2.Text = string.Empty;

            cboLoaiGiayToUQ1.SelectedIndex = 2;
            cboLoaiGiayToUQ2.SelectedIndex = 2;

            txtSoGiayToUQ1.Text = string.Empty;
            txtSoGiayToUQ2.Text = string.Empty;

            cboNoiCapUQ1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            cboNoiCapUQ2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";

            txtNgayCapUQ1.Text = string.Empty;
            txtNgayCapUQ2.Text = string.Empty;

            cboCuTruUQ1.Text = string.Empty;
            cboCuTruUQ2.Text = string.Empty;
            
                button1.Visible = true;
                button2.Visible = true;
                button1.Enabled = true;
                button2.Enabled = false;
                lvUser.Visible = true;
            //button3.Location = new System.Drawing.Point(450, 530);
            //this.Size = new System.Drawing.Size(1506, 950);
            //btnXoa.Location = new System.Drawing.Point(510, 126);


            btnXoa1.Enabled = false;
            btnXoa2.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                var user = new ListUserCustom
                {
                    Id = Guid.NewGuid(),
                    DanhXung1 = cboDanhXungUQ1.Text,
                    HoVaTen1 = txtHoVaTenUQ1.Text,
                    NgaySinh1 = txtNgaySinhUQ1.Text,
                    LoaiGiayTo1 = cboLoaiGiayToUQ1.Text,
                    SoGiayTo1 = txtSoGiayToUQ1.Text,
                    NoiCap1 = cboNoiCapUQ1.Text,
                    NgayCap1 = txtNgayCapUQ1.Text,
                    CuTru1 = cboCuTruUQ1.Text,

                    DanhXung2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text,
                    HoVaTen2 = cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty : txtHoVaTenUQ2.Text,
                    NgaySinh2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty : txtNgaySinhUQ2.Text,
                    LoaiGiayTo2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty : cboLoaiGiayToUQ2.Text,
                    SoGiayTo2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty : txtSoGiayToUQ2.Text,
                    NoiCap2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty : cboNoiCapUQ2.Text,
                    NgayCap2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty : txtNgayCapUQ2.Text,
                    CuTru2 = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? string.Empty :
                        (cboCuTruUQ2.Text != null && cboCuTruUQ2.Text != string.Empty && cboCuTruUQ2.Text != "") ?
                        cboCuTruUQ2.Text : cboCuTruUQ1.Text,
                    TaiSanRieng = txtHoVaTenUQ2.Text == null || txtHoVaTenUQ2.Text == string.Empty ? string.Empty : cboDanhXungUQ2.Text == "Tài sản riêng" ? txtHoVaTenUQ2.Text : string.Empty
                };
                userList.Add(user);

                var users = new List<ListUserCustom> { user };
                AddUser(users, false);
                MessageBox.Show("Thêm thành công!");
                LoadDefaultData();
            }
        }

        internal void AddUser(List<ListUserCustom> custom, bool clear)
        {
            if (clear) lvUser.Items.Clear();
            foreach (var i in custom)
            {
                var item = new ListViewItem();
                item.Text = i.DanhXung1;
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.HoVaTen1 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NgaySinh1 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.LoaiGiayTo1 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.SoGiayTo1 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NoiCap1 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NgayCap1 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.CuTru1 });

                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.DanhXung2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.HoVaTen2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NgaySinh2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.LoaiGiayTo2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.SoGiayTo2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NoiCap2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NgayCap2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.CuTru2 });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.Id.ToString() });


                item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.TaiSanRieng });

                lvUser.Items.Add(item);
            }

        }

        private void lvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lv = sender as ListView;
            var m = lv.SelectedItems;
            if (m.Count > 0)
            {
                var item = lv.SelectedItems[0];
                cboDanhXungUQ1.Text = item.Text;
                txtHoVaTenUQ1.Text = item.SubItems[1].Text;
                txtNgaySinhUQ1.Text = item.SubItems[2].Text;
                cboLoaiGiayToUQ1.Text = item.SubItems[3].Text;
                txtSoGiayToUQ1.Text = item.SubItems[4].Text;
                cboNoiCapUQ1.Text = item.SubItems[5].Text;
                txtNgayCapUQ1.Text = item.SubItems[6].Text;
                cboCuTruUQ1.Text = item.SubItems[7].Text;

                cboDanhXungUQ2.Text = item.SubItems[8].Text;
                txtHoVaTenUQ2.Text = item.SubItems[9].Text;
                txtNgaySinhUQ2.Text = item.SubItems[10].Text;
                cboLoaiGiayToUQ2.Text = item.SubItems[11].Text;
                txtSoGiayToUQ2.Text = item.SubItems[12].Text;
                cboNoiCapUQ2.Text = item.SubItems[13].Text;
                txtNgayCapUQ2.Text = item.SubItems[14].Text;
                cboCuTruUQ2.Text = item.SubItems[15].Text;

                SelectedId = Guid.Parse(item.SubItems[16].Text);

                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                SelectedId = Guid.Empty;
            }
            // lv.SelectedItems.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                if (SelectedId != Guid.Empty)
                {
                    var newList = new List<ListUserCustom>();

                    foreach (var item in userList)
                    {
                        if (item.Id != SelectedId)
                        {
                            newList.Add(item);
                        }
                        else
                        {
                            var editedItem = item;
                            editedItem.DanhXung1 = cboDanhXungUQ1.Text;
                            editedItem.HoVaTen1 = txtHoVaTenUQ1.Text;
                            editedItem.NgaySinh1 = txtNgaySinhUQ1.Text;
                            editedItem.LoaiGiayTo1 = cboLoaiGiayToUQ1.Text;
                            editedItem.SoGiayTo1 = txtSoGiayToUQ1.Text;
                            editedItem.NoiCap1 = cboNoiCapUQ1.Text;
                            editedItem.NgayCap1 = txtNgayCapUQ1.Text;
                            editedItem.CuTru1 = cboCuTruUQ1.Text;
                            editedItem.DanhXung2 = cboDanhXungUQ2.Text;
                            editedItem.HoVaTen2 = txtHoVaTenUQ2.Text;
                            editedItem.NgaySinh2 = txtNgaySinhUQ2.Text;
                            editedItem.LoaiGiayTo2 = cboLoaiGiayToUQ2.Text;
                            editedItem.SoGiayTo2 = txtSoGiayToUQ2.Text;
                            editedItem.NoiCap2 = cboNoiCapUQ2.Text;
                            editedItem.NgayCap2 = txtNgayCapUQ2.Text;
                            editedItem.CuTru2 = (cboCuTruUQ2.Text != null && cboCuTruUQ2.Text != string.Empty && cboCuTruUQ2.Text != "") ?
                        cboCuTruUQ2.Text : cboCuTruUQ1.Text;

                            newList.Add(editedItem);
                        }
                    }

                    AddUser(newList, true);

                }
                MessageBox.Show("Sửa thành công!");
                LoadDefaultData();
            }
        }

        private void cboLoaiGiayToUQ1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboNoiCapUQ1.SelectedItem = "Công an tỉnh Tây Ninh";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboNoiCapUQ1.SelectedItem = "Cục quản lý Xuất nhập cảnh";
            }
            else if (cb.SelectedIndex == 2)
            {
                cboNoiCapUQ1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            }
        }

        private void cboLoaiGiayToUQ2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                cboNoiCapUQ2.SelectedItem = "Công an tỉnh Tây Ninh";
            }
            else if (cb.SelectedIndex == 1)
            {
                cboNoiCapUQ2.SelectedItem = "Cục quản lý Xuất nhập cảnh";
            }
            else if (cb.SelectedIndex == 2)
            {
                cboNoiCapUQ2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            }
        }

        private void HandoverDemo_Load(object sender, EventArgs e)
        {

        }

        private void cboDanhXungUQ2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == "Tài sản riêng")
            {
                // cu tru
                lbCuTruOng.Text = "Cư trú";
                lbCuTruBa.Visible = false;
                cboCuTruUQ2.Visible = false;
                chkSenderResidentYN.Visible = false;
                lbCuTruOng.Location = new System.Drawing.Point(30, 90);
                cboCuTruUQ1.Location = new System.Drawing.Point(68, 90);
                cboCuTruUQ1.Size = new System.Drawing.Size(865, 26);
                cboCuTruUQ1.TabIndex = 4;

                // tai san rieng
                txtHoVaTenUQ2.Size = new System.Drawing.Size(865, 26);
                txtHoVaTenUQ2.Text = "(Theo giấy xác nhận tình trạng hôn nhân số ... do UBND ... xác nhận ngày .../.../...)";
                txtNgaySinhUQ2.Visible = false;
                txtSoGiayToUQ2.Visible = false;
                cboLoaiGiayToUQ2.Visible = false;
                cboNoiCapUQ2.Visible = false;
                txtNgayCapUQ2.Visible = false;

                txtNgaySinhUQ2.Text = string.Empty;
                txtSoGiayToUQ2.Text = string.Empty;
                cboLoaiGiayToUQ2.Text = string.Empty;
                cboNoiCapUQ2.Text = string.Empty;
                txtNgayCapUQ2.Text = string.Empty;

                label5.Visible = false;
                label7.Visible = false;
                label9.Visible = false;
            }
            else
            {
                chkSenderResidentYN.Visible = true;
                CheckATachCuTru();

                txtHoVaTenUQ2.Size = new System.Drawing.Size(121, 26);
                txtHoVaTenUQ2.Text = string.Empty;
                txtNgaySinhUQ2.Visible = true;
                txtSoGiayToUQ2.Visible = true;
                cboLoaiGiayToUQ2.Visible = true;
                cboNoiCapUQ2.Visible = true;
                txtNgayCapUQ2.Visible = true;

                txtNgaySinhUQ2.Text = string.Empty;
                txtSoGiayToUQ2.Text = string.Empty;
                cboLoaiGiayToUQ2.Text = string.Empty;
                cboNoiCapUQ2.Text = string.Empty;
                txtNgayCapUQ2.Text = string.Empty;

                label5.Visible = true;
                label7.Visible = true;
                label9.Visible = true;
            }
        }

        private void chkSenderResidentYN_CheckedChanged(object sender, EventArgs e)
        {
            var ckb = sender as CheckBox;
            if (ckb.Checked)
            {
                CheckATachCuTru();
            }
            else
            {
                lbCuTruOng.Text = "Người 1";
                lbCuTruBa.Visible = false;
                cboCuTruUQ2.Visible = false;
                cboCuTruUQ1.Location = new System.Drawing.Point(73, 90);
                cboCuTruUQ1.Size = new System.Drawing.Size(910, 26);
                cboCuTruUQ1.TabIndex = 4;
            }
        }

        private void CheckATachCuTru(int? backup = null)
        {
            chkSenderResidentYN.Checked = true;
            lbCuTruOng.Visible = true;
            lbCuTruBa.Visible = true;
            cboCuTruUQ2.Visible = true;

            lbCuTruOng.Location = new System.Drawing.Point(73, 97);
            lbCuTruOng.TabIndex = 5;
            lbCuTruOng.Text = "Người 1";

            cboCuTruUQ1.Location = new System.Drawing.Point(120, 90);
            cboCuTruUQ1.Size = new System.Drawing.Size(250, 26);
            cboCuTruUQ1.TabIndex = 4;
            
            lbCuTruBa.Location = new System.Drawing.Point(570, 97);
            lbCuTruBa.Name = "lbCuTruBa";
            lbCuTruBa.TabIndex = 6;
            lbCuTruBa.Text = "Người 2";

            cboCuTruUQ2.Location = new System.Drawing.Point(642, 93);
            cboCuTruUQ2.Size = new System.Drawing.Size(250, 26);
            cboCuTruUQ2.TabIndex = 4;
        }
        

        private void lbCuTruBa_Click(object sender, EventArgs e)
        {

        }

        private void cboCuTruUQ2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cboNoiCapUQ1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnXoa1.Enabled = true;
            if (txtHoVaTenUQ2.Text != string.Empty | txtNgaySinhUQ2.Text != string.Empty
                || txtSoGiayToUQ2.Text != string.Empty || txtNgayCapUQ2.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void cboNoiCapUQ2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnXoa2.Enabled = true;
            if (txtHoVaTenUQ1.Text != string.Empty | txtNgaySinhUQ1.Text != string.Empty
                || txtSoGiayToUQ1.Text != string.Empty || txtNgayCapUQ1.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtNgayCapUQ1_TextChanged(object sender, EventArgs e)
        {
            btnXoa1.Enabled = true;
            if (txtHoVaTenUQ2.Text != string.Empty | txtNgaySinhUQ2.Text != string.Empty
                || txtSoGiayToUQ2.Text != string.Empty || txtNgayCapUQ2.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void cboDanhXungUQ1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //userList.Clear();
            //var user = new ListUserCustom
            //{
            //    Id = Guid.NewGuid(),
            //    DanhXung1 = cboDanhXungUQ1.Text,
            //    HoVaTen1 = txtHoVaTenUQ1.Text,
            //    NgaySinh1 = txtNgaySinhUQ1.Text,
            //    LoaiGiayTo1 = cboLoaiGiayToUQ1.Text,
            //    SoGiayTo1 = txtSoGiayToUQ1.Text,
            //    NoiCap1 = cboNoiCapUQ1.Text,
            //    NgayCap1 = txtNgayCapUQ1.Text,
            //    CuTru1 = cboCuTruUQ1.Text,
            //    DanhXung2 = cboDanhXungUQ2.Text,
            //    HoVaTen2 = txtHoVaTenUQ2.Text,
            //    NgaySinh2 = txtNgaySinhUQ2.Text,
            //    LoaiGiayTo2 = cboLoaiGiayToUQ2.Text,
            //    SoGiayTo2 = txtSoGiayToUQ2.Text,
            //    NoiCap2 = cboNoiCapUQ2.Text,
            //    NgayCap2 = txtNgayCapUQ2.Text,
            //    CuTru2 = (cboCuTruUQ2.Text != null && cboCuTruUQ2.Text != string.Empty && cboCuTruUQ2.Text != "") ?
            //    cboCuTruUQ2.Text : cboCuTruUQ1.Text
            //};
            //userList.Add(user);

            //var users = new List<ListUserCustom> { user };
            //AddUser(users, true);
            var error = Validate();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
            }
            else
            {
                MessageBox.Show("Thêm thành công!");
                Main.SetUsers(ben, userList);
                this.Visible = false;
            }

        }

        private void lvUser_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var lv = sender as ListView;
            var m = lv.SelectedItems;
            if (m.Count > 0)
            {
                var item = lv.SelectedItems[0];
                cboDanhXungUQ1.Text = item.Text;
                txtHoVaTenUQ1.Text = item.SubItems[1].Text;
                txtNgaySinhUQ1.Text = item.SubItems[2].Text;
                cboLoaiGiayToUQ1.Text = item.SubItems[3].Text;
                txtSoGiayToUQ1.Text = item.SubItems[4].Text;
                cboNoiCapUQ1.Text = item.SubItems[5].Text;
                txtNgayCapUQ1.Text = item.SubItems[6].Text;
                cboCuTruUQ1.Text = item.SubItems[7].Text;

                cboDanhXungUQ2.Text = item.SubItems[8].Text;
                txtHoVaTenUQ2.Text = item.SubItems[9].Text;
                txtNgaySinhUQ2.Text = item.SubItems[10].Text;
                cboLoaiGiayToUQ2.Text = item.SubItems[11].Text;
                txtSoGiayToUQ2.Text = item.SubItems[12].Text;
                cboNoiCapUQ2.Text = item.SubItems[13].Text;
                txtNgayCapUQ2.Text = item.SubItems[14].Text;
                cboCuTruUQ2.Text = item.SubItems[15].Text;

                SelectedId = Guid.Parse(item.SubItems[16].Text);

                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                SelectedId = Guid.Empty;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cboDanhXungUQ1.SelectedIndex = 0;
            txtHoVaTenUQ1.Text = string.Empty;
            txtNgaySinhUQ1.Text = string.Empty;
            cboLoaiGiayToUQ1.SelectedIndex = 2;
            txtSoGiayToUQ1.Text = string.Empty;
            //cboNoiCapUQ1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapUQ1.Text = string.Empty;
            cboCuTruUQ1.Text = string.Empty;
            button1.Enabled = true;
            btnXoa1.Enabled = false;
            btnXoa.Enabled = false;

            if (SelectedId != Guid.Empty && txtHoVaTenUQ1.Text == string.Empty && txtHoVaTenUQ2.Text == string.Empty)
            {
                if (userList != null)
                {
                    var deletedUser = userList.FirstOrDefault(x => x.Id == SelectedId);


                    if (deletedUser != null)
                    {
                        userList.Remove(deletedUser);

                        AddUser(userList, true);

                    }

                }
            }
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            cboDanhXungUQ2.SelectedIndex = 0;
            txtHoVaTenUQ2.Text = string.Empty;
            txtNgaySinhUQ2.Text = string.Empty;
            cboLoaiGiayToUQ2.SelectedIndex = 2;
            txtSoGiayToUQ2.Text = string.Empty;
            //cboNoiCapUQ2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapUQ2.Text = string.Empty;
            chkSenderResidentYN.Checked = false;
            button1.Enabled = true;
            btnXoa2.Enabled = false;
            btnXoa.Enabled = false;
            cboCuTruUQ2.Text = string.Empty;

            if (SelectedId != Guid.Empty && txtHoVaTenUQ1.Text == string.Empty && txtHoVaTenUQ2.Text == string.Empty)
            {
                if (userList != null)
                {
                    var deletedUser = userList.FirstOrDefault(x => x.Id == SelectedId);


                    if (deletedUser != null)
                    {
                        userList.Remove(deletedUser);

                        AddUser(userList, true);

                    }

                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            cboDanhXungUQ1.SelectedIndex = 0;
            txtHoVaTenUQ1.Text = string.Empty;
            txtNgaySinhUQ1.Text = string.Empty;
            cboLoaiGiayToUQ1.SelectedIndex = 2;
            txtSoGiayToUQ1.Text = string.Empty;
            //cboNoiCapUQ1.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapUQ1.Text = string.Empty;

            cboDanhXungUQ2.SelectedIndex = 0;
            txtHoVaTenUQ2.Text = string.Empty;
            txtNgaySinhUQ2.Text = string.Empty;
            cboLoaiGiayToUQ2.SelectedIndex = 2;
            txtSoGiayToUQ2.Text = string.Empty;
            //cboNoiCapUQ2.SelectedItem = "Cục Cảnh sát ĐKQL Cư trú và DLQG về Dân cư";
            txtNgayCapUQ2.Text = string.Empty;
            chkSenderResidentYN.Checked = false;


            btnXoa.Enabled = false;
            btnXoa1.Enabled = false;
            btnXoa2.Enabled = false;
            button1.Enabled = true;
            //button6.Enabled = false;

            if (SelectedId != Guid.Empty)
            {
                if (userList != null)
                {
                    var deletedUser = userList.FirstOrDefault(x => x.Id == SelectedId);
                    
                    if (deletedUser != null)
                    {
                        userList.Remove(deletedUser);

                        AddUser(userList, true);

                    }

                }
            }
        }

        private void txtSoGiayToUQ1_TextChanged(object sender, EventArgs e)
        {
            btnXoa1.Enabled = true;
            if (txtHoVaTenUQ2.Text != string.Empty | txtNgaySinhUQ2.Text != string.Empty
                || txtSoGiayToUQ2.Text != string.Empty || txtNgayCapUQ2.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void txtNgaySinhUQ1_TextChanged(object sender, EventArgs e)
        {
            btnXoa1.Enabled = true;
            if (txtHoVaTenUQ2.Text != string.Empty | txtNgaySinhUQ2.Text != string.Empty
                || txtSoGiayToUQ2.Text != string.Empty || txtNgayCapUQ2.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void txtHoVaTenUQ1_TextChanged(object sender, EventArgs e)
        {
            btnXoa1.Enabled = true;
            if (txtHoVaTenUQ2.Text != string.Empty | txtNgaySinhUQ2.Text != string.Empty
                || txtSoGiayToUQ2.Text != string.Empty || txtNgayCapUQ2.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void txtHoVaTenUQ2_TextChanged(object sender, EventArgs e)
        {
            btnXoa2.Enabled = true;
            if (txtHoVaTenUQ1.Text != string.Empty | txtNgaySinhUQ1.Text != string.Empty
                || txtSoGiayToUQ1.Text != string.Empty || txtNgayCapUQ1.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void txtNgaySinhUQ2_TextChanged(object sender, EventArgs e)
        {
            btnXoa2.Enabled = true;
            if (txtHoVaTenUQ1.Text != string.Empty | txtNgaySinhUQ1.Text != string.Empty
                || txtSoGiayToUQ1.Text != string.Empty || txtNgayCapUQ1.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void txtSoGiayToUQ2_TextChanged(object sender, EventArgs e)
        {
            btnXoa2.Enabled = true;
            if (txtHoVaTenUQ1.Text != string.Empty | txtNgaySinhUQ1.Text != string.Empty
                || txtSoGiayToUQ1.Text != string.Empty || txtNgayCapUQ1.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
        }

        private void txtNgayCapUQ2_TextChanged(object sender, EventArgs e)
        {
            btnXoa2.Enabled = true;
            if (txtHoVaTenUQ1.Text != string.Empty | txtNgaySinhUQ1.Text != string.Empty
                || txtSoGiayToUQ1.Text != string.Empty || txtNgayCapUQ1.Text != string.Empty)
            {
                btnXoa.Enabled = true;
            }
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
            if (txtNgaySinhUQ1.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtNgaySinhUQ1.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng ngày sinh người thứ nhất" + Environment.NewLine;
                }
                else
                {
                    if (DateTime.Now.Year - number < 18)
                    {
                        error += "Ngày sinh người thứ nhất không hợp lệ (Phải lớn hơn 18 tuổi)" + Environment.NewLine;
                    }
                    number = 0;
                }
            }

            if (txtNgaySinhUQ2.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtNgaySinhUQ2.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng ngày sinh người thứ hai" + Environment.NewLine;
                }
                else
                {
                    if (DateTime.Now.Year - number < 18)
                    {
                        error += "Ngày sinh người thứ hai không hợp lệ (Phải lớn hơn 18 tuổi)" + Environment.NewLine;
                    }
                    number = 0;
                }
            }

            // Ngay cap A
            if (txtNgayCapUQ1.Text != string.Empty)
            {
                var parse = DateTime.TryParseExact(txtNgayCapUQ1.Text, formats,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!parse)
                {
                    error += "Sai định dạng ngày cấp giấy tờ người thứ nhất" + Environment.NewLine;
                }
                else
                {
                   // txtNgayCapUQ1.Text = dateTime.ToString("dd/MM/yyyy");
                    if (dateTime > DateTime.Now)
                    {
                        error += "Ngày cấp giấy tờ người thứ nhất không hợp lệ (Phải nhỏ hơn năm hiện tại)" + Environment.NewLine;
                    }
                }
            }

            if (txtNgayCapUQ2.Text != string.Empty)
            {
                var parse = DateTime.TryParseExact(txtNgayCapUQ1.Text, formats,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!parse)
                {
                    error += "Sai định dạng ngày cấp giấy tờ người thứ hai" + Environment.NewLine;
                }
                else
                {
                    //txtNgayCapUQ2.Text = dateTime.ToString("dd/MM/yyyy");
                    if (dateTime > DateTime.Now)
                    {
                        error += "Ngày cấp giấy tờ người thứ hai không hợp lệ (Phải nhỏ hơn năm hiện tại)" + Environment.NewLine;
                    }
                    number = 0;
                }



            }

            // So giay to A
            if (txtSoGiayToUQ1.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtSoGiayToUQ1.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng số CMND/ Hộ chiếu/CCCD người thứ nhất" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            if (txtSoGiayToUQ2.Text != string.Empty)
            {
                var parse = Int64.TryParse(txtSoGiayToUQ2.Text, out number);
                if (!parse)
                {
                    error += "Sai định dạng số CMND/ Hộ chiếu/CCCD người thứ hai" + Environment.NewLine;
                }
                else
                {
                    number = 0;
                }
            }

            return error;
        }

    }
}
