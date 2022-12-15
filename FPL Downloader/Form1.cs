using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace FPL_Downloader
{
    public partial class Form1 : Form
    {
        private AsyncCompletedEventHandler client_DownloadFileCompleted;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        WebClient client = new WebClient();
        public static string url = "https://github.com/PastaLaPate/Flat/releases/download/v0.3.0-alpha/FPL_IDE-0.3.0-alpha.jar";
        public static string name = "FPL_IDE-0.3.0-alpha.jar";
        public static string path = "C:\\Users\\" + Environment.UserName + "\\Desktop\\" + name;

        private void button1_Click(object sender, EventArgs e)
        {
            using (client)
            {
                try
                {
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    client.DownloadFileAsync(new Uri(url), path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Flat Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }  
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Status : " + progressBar1.Value + "%";
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {  
            if(progressBar1.Value == 100)
            {
                MessageBox.Show("The Ide was succesfully downloaded !", "Flat Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The download has been cancelled", "Flat Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.CancelAsync();

            if(File.Exists("C:\\Users\\" + Environment.UserName + "\\Desktop\\" + name))
            {
                File.Delete("C:\\Users\\" + Environment.UserName + "\\Desktop\\" + name);
            }

            MessageBox.Show("The download has been cancelled", "Flat Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
