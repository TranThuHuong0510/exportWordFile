using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var oldPassword = ReadData();
            if(oldPassword != txtOldPassword.Text)
            {
                MessageBox.Show("Sai mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(oldPassword == txtOldPassword.Text && txtNewPassword.Text != " " && txtNewPassword.Text != string.Empty){
                var macAddr =
                    (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()
                    ).FirstOrDefault();

                var data = new List<string>();
                data.Add(txtNewPassword.Text);
                data.Add(macAddr);
                SaveData(data);
                //SaveData(txtNewPassword.Text);
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Sai định dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ReadData()
        {
            try
            {
                var data = new List<string>();
                FileStream fs = new FileStream("Key.txt", FileMode.Open);
                StreamReader rd = new StreamReader(fs, Encoding.UTF8);
                int counter = 0;
                string ln;
                ln = rd.ReadLine();
                rd.Close();
                return ln;
            }
            catch (Exception)
            {
                return "AF01715B-895C-4724-91E6-0C9D7DE8CB17";
                throw;
            }
            
        }

        //private void SaveData(string data)
        //{
        //    String filepath = "Key.txt";// đường dẫn của file muốn tạo
        //    FileStream fs = new FileStream(filepath, FileMode.Create);//Tạo file mới tên là test.txt            
        //    StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
            
        //        sWriter.WriteLine(data);
        //    // Ghi và đóng file
        //    sWriter.Flush();
        //    fs.Close();
        //}
        private void SaveData(List<string> data)
        {
            String filepath = "Key.txt";// đường dẫn của file muốn tạo
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
    }
}
