using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;

namespace VChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Checker.pushNvdFromXmlToDb(openDialog("файл данных для загрузки (*.xml)", "xml"), openDialog("базы данных", "mdf"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string str in Checker.getInstalledSoftware())
            {
                listBox1.Items.Add(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (string str in Checker.searchCVEforSoftware(new HashSet<string> { "juniper junos", "google chrome" }))
            {
                listBox2.Items.Add(str);
            }
        }

        public static string openDialog(string textMB, string fileType)
        {
            MessageBox.Show(String.Format("Выберите файл {0}", textMB));
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName;
            openFileDialog.Filter = String.Format("{0}|*.{0}", fileType);
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            return filePath;
        }

    }
}
