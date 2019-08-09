using ExportWordFileFromTemplate.Models;
using Novacode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    public partial class ListFile : Form
    {
        private ExportFile ExportFiles;
        private FormMain FormMains;
        private string DefaultFileName;
        public ListFile(string defaultFileName, FormMain formMains)
        {
            InitializeComponent();
            DefaultFileName = defaultFileName;
            FormMains = formMains;
        }

        private void ListFile_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            // Set the file dialog to filter for graphics files.
            FD.Filter =
                "Word (*.docx)|*.docx|" +
        "All files (*.*)|*.*";
            // Allow the user to select multiple images.
            FD.Multiselect = true;
            FD.Title = "My Template Browser";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (String file in FD.FileNames)
                {
                    if (File.Exists(file))
                    {

                        //txtFileMau.Text = fileToOpen;
                        var characters = file.Split('.');
                        var index = characters.Length;
                        var templateFile = characters[index - 2];
                        var templateNames = templateFile.Split('\\');
                        var templateName = templateNames[templateNames.Length - 1];
                        var fileName = templateName + "-" + DefaultFileName;
                        var dateToString = DateTime.Now.ToString("dd-MM-yyyy");
                        var newFile = string.Format(Constants.NewFile, dateToString, fileName);
                        //if (txtFileMau.Text != string.Empty && txtFileMau.Text != ""
                        //    && txtnewFile.Text != string.Empty && txtnewFile.Text != "")
                        //{
                        //    btThemFile.Enabled = true;
                        //}
                        var item = new ListViewItem();
                        item.Text = file;
                        item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = newFile });
                        lvItem.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("File không tồn tại");
                        return;
                    }
                }


            }
            //if (ExportFiles == null || ExportFiles == default(ExportFile) || ExportFiles.IsDisposed)
            //{
            //    ExportFiles = new ExportFile(DefaultFileName, this);
            //}

            //ExportFiles.Show();
        }

        //internal void AddListView(List<ListViewCustom> items)
        //{
        //    foreach(var i in items)
        //    {
        //        ListViewItem item = new ListViewItem();
        //        item.Text = i.TemplateFile;
        //        item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.NewFile });
        //        item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = i.Action });

        //        lvItem.Items.Add(item);
        //    }

        //}

        internal void ClearListView()
        {
            lvItem.Items.Clear();
        }

        internal List<ListViewCustom> GetListView()
        {
            var result = new List<ListViewCustom>();
            var items = lvItem.Items;
            var length = items.Count;
            for (int i = 0; i < length; i++)
            {
                result.Add(new ListViewCustom
                {
                    TemplateFile = items[i].SubItems[0].Text,
                    NewFile = items[i].SubItems[1].Text
                });
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var items = GetListView();
                DocX gDoc;
                var pathString = "";
                //Process p = new Process();
                foreach (var item in items)
                {
                    var subFolder = item.NewFile.Split('\\');
                    var length = subFolder.Count();
                    pathString = "";

                    for (var i = 0; i < length - 1; i++)
                    {
                        pathString += subFolder[i] + '\\';
                    }
                    //pathString = pathString.Substring(pathString.Length - 1);
                    bool exists = System.IO.Directory.Exists(pathString);

                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(pathString);
                    }

                    gDoc = FormMains.CreateInvoiceFromTemplate(DocX.Load(item.TemplateFile));
                    gDoc.SaveAs(item.NewFile);

                    //Open a doc file.
                    var application = new Microsoft.Office.Interop.Word.Application();
                    var document = application.Documents.Open(item.NewFile);

                    // Get the page count.
                    var numberOfPages = document.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages, false);
                    document.Close();
                    application.Quit();

                    gDoc = FormMains.NumberOfPage(gDoc, numberOfPages);
                    gDoc.SaveAs(item.NewFile);
                    // disable component
                    //if(item.Action == "Lưu và mở")
                    //{
                    //    p.StartInfo.FileName = item.NewFile;
                    //}
                }

                MessageBox.Show("Đã lưu file thành công!");
                this.Visible = false;
                FormMains.ClearData();
                ClearListView();
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = pathString,
                    UseShellExecute = true,
                    Verb = "open"
                });
                //p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("File mẫu đang được mở bởi chương trình khác");
                throw;
            }

            //DocX gDoc;
            //try
            //{
            //    BackupData();
            //    // Open a doc file.
            //    var application = new Microsoft.Office.Interop.Word.Application();
            //    var document = application.Documents.Open(txtTemplatePath.Text);

            //    // Get the page count.
            //    var numberOfPages = document.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages, false);
            //    document.Close();
            //    application.Quit();
            //    gDoc = CreateInvoiceFromTemplate(DocX.Load(txtTemplatePath.Text), numberOfPages);
            //    gDoc.SaveAs(txtSaveFile.Text);
            //    MessageBox.Show("Đã lưu file thành công!");
            //    ClearData();
            //    // disable component
            //    // Open in word:
            //    Process p = new Process();
            //    p.StartInfo.FileName = txtSaveFile.Text;
            //    p.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("File mẫu đang được mở bởi chương trình khác");
            //    throw;
            //}
        }
    }
}
