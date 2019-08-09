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
    public partial class LoginUser : Form
    {
        public LoginUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var password = ReadData();
            var macAddr =
                    (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()
                    ).FirstOrDefault();

            if (password.Count() == 1)
            {
                if (textKey.Text ==
                    "AF01715B-895C-4724-91E6-0C9D7DE8CB17")
                {
                    var data = new List<string>();
                    data.Add(textKey.Text);
                    data.Add(macAddr);
                    SaveData(data);
                    this.Visible = false;
                    FormMain main = new FormMain();
                    main.Show();
                }
                else if(textKey.Text == password.ElementAt(0))
                {
                    MessageBox.Show("Lỗi cài đặt phần mềm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
                else
                {
                    MessageBox.Show("Sai mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else if (textKey.Text == password.ElementAt(0))
            {
                if(macAddr == password.ElementAt(1))
                {
                    this.Visible = false;
                    FormMain main = new FormMain();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Lỗi cài đặt phần mềm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }

            }
            else
            {
                MessageBox.Show("Sai mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private List<string> ReadData()
        {
            var data = new List<string>();
            try
            {
                FileStream fs = new FileStream("Key.txt", FileMode.Open);
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
                data.Add("AF01715B-895C-4724-91E6-0C9D7DE8CB17");
                return data;
                throw;
            }

        }

        private void SaveData(List<string> data)
        {
            String filepath = "Key.txt";// đường dẫn của file muốn tạo
            FileStream fs = new FileStream(filepath, FileMode.Create);//Tạo file mới tên là test.txt            
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 

            foreach(var s in data)
            {
                sWriter.WriteLine(s);
            }
            
            // Ghi và đóng file
            sWriter.Flush();
            fs.Close();
        }
    }

}
