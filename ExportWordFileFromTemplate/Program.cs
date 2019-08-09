using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportWordFileFromTemplate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(DateTime.Now <= new DateTime(2019,8,15))
            {
                Application.Run(new LoginUser());
                //Application.Run(new FormMain());
            }
            else
            {
                MessageBox.Show("Đã quá hạn dùng thử");
            }
        }
    }
}
