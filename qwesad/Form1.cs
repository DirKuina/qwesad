using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Input;
using System.Windows;

namespace qwesad
{
    public partial class Form1 : Form
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        private string currentFolderPath;
        private string newPath;
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = false;
            timer1.Interval = 3000;
            timer1.Tick += timer1_Tick;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            foreach (DriveInfo d in allDrives)
            {
                textBox1.Text += $"Drive {d.Name} Drive type: {d.DriveType}";
                if (d.IsReady == true)
                {
                    textBox1.Text += $", DriveFormat : {d.DriveFormat} , DriveTotalSize : {d.TotalSize / 1024 / 1024 / 1024} GB , DriveFreeSpace : {d.AvailableFreeSpace / 1024 / 1024 / 1024} GB" + Environment.NewLine;
                }
                else
                {
                    textBox1.Text += Environment.NewLine;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
            else
            {
                timer1.Start();
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Clear();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    textBox2.Text += $"Drive {d.Name} DriveTotalSize : {d.TotalSize} Bytes , DriveFreeSpace : {d.AvailableFreeSpace} Bytes" + Environment.NewLine;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentFolderPath = label1.Text;
            newPath = textBox4.Text;
            try
            {

                File.Copy(currentFolderPath, newPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удается переместить файл из-за исключения: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                currentFolderPath = label1.Text;
                newPath = textBox4.Text;
                File.Move(currentFolderPath, newPath);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentFolderPath = label1.Text;

            try
            {
                File.Delete(currentFolderPath);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();


            foreach (System.IO.DriveInfo myDrives in System.IO.DriveInfo.GetDrives())
            {
                TreeNode myDrivesNode = treeView1.Nodes.Add(myDrives.Name);

                myDrivesNode.Nodes.Add("Expand123");
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode MyExistNode = e.Node;

            MyExistNode.Nodes.Clear();

            try
            {
                string mypath = MyExistNode.FullPath;


                foreach (string myFolders in System.IO.Directory.GetDirectories(mypath))
                {
                    TreeNode FldrNode = MyExistNode.Nodes.Add(System.IO.Path.GetFileName(myFolders));

                    FldrNode.Nodes.Add("Expand123");
                }


                foreach (string MyFiles in System.IO.Directory.GetFiles(mypath))
                {

                    TreeNode FLNode = MyExistNode.Nodes.Add(System.IO.Path.GetFileName(MyFiles));

                }

            }
            catch (Exception FlErr)
            {
                MessageBox.Show(FlErr.ToString());
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode MySelectedNode = e.Node;

            label1.Text = MySelectedNode.FullPath;
        }
    }
}
