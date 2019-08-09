using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    public partial class ExportFile : Form
    {
        private string Defaultfilename;
        private string TemplateName;
        private ListFile ListFiles;
        public ExportFile(string defaultfilename, ListFile lf)
        {
            InitializeComponent();
            Defaultfilename = defaultfilename;
            ListFiles = lf;
            btThemFile.Enabled = false;
        }

        private void ExportFile_Load(object sender, EventArgs e)
        {

        }

        private void btnChooseTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileToOpen = FD.FileName;
                    if (File.Exists(fileToOpen))
                    {
                        txtFileMau.Text = fileToOpen;
                        var characters = FD.SafeFileName.Split('.');
                        var index = characters.Length;
                        TemplateName = characters[index-2];
                        if(txtFileMau.Text != string.Empty && txtFileMau.Text != ""
                            && txtnewFile.Text != string.Empty && txtnewFile.Text != "")
                        {
                            btThemFile.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("File không tồn tại");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng đóng file mẫu trước khi sử dụng");
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog savefile = new SaveFileDialog();
                // set a defaultfilename
                savefile.FileName = TemplateName + "-" + Defaultfilename;
                // set filters - this can be done in properties as well
                savefile.Filter = "Text files (*.docx)|*.docx|All files (*.*)|*.*";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    txtnewFile.Text = savefile.FileName;
                }
                if (txtFileMau.Text != string.Empty && txtFileMau.Text != ""
                            && txtnewFile.Text != string.Empty && txtnewFile.Text != "")
                {
                    btThemFile.Enabled = true;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("File mẫu đang được mở bởi chương trình khác");
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        //    var action = "Lưu";
        //    if (chbOpenFile.Checked)
        //    {
        //        var mess = string.Format("Chỉ có thể mở 1 file sau  khi xuất file.{0}Bạn chắc chắn muốn mở file này?", Environment.NewLine);
        //        DialogResult result = MessageBox.Show(mess, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //        if (result == DialogResult.Yes)
        //        {
        //            var items = ListFiles.GetListView();
        //            if (items.Any())
        //            {
        //                foreach (var item in items)
        //                {
        //                    item.Action = "Lưu";
        //                }
        //            }
                    
        //            items.Add(new ListViewCustom
        //            {
        //                TemplateFile = txtFileMau.Text,
        //                NewFile = txtnewFile.Text,
        //                Action = "Lưu và mở"
        //            });

        //            ListFiles.ClearListView();
        //           // ListFiles.AddListView(items);
                    
        //        }
        //        else if (result == DialogResult.No)
        //        {
        //            var items = new List<ListViewCustom>();
        //            items.Add(new ListViewCustom
        //            {
        //                TemplateFile = txtFileMau.Text,
        //                NewFile = txtnewFile.Text,
        //                Action = "Lưu"
        //            });
        //            //ListFiles.AddListView(items);
        //        }
        //    }

        //    txtnewFile.Text = string.Empty;
        //    txtFileMau.Text = string.Empty;
        //    this.Visible = false;
        }
    }
}
