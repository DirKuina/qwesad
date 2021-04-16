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
using ClassLibrary1;

namespace qwesad
{
    public partial class Form1 : Form
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        private string currentFolderPath;
        private string newPath;
        
        public Form1()
        {
            this.BackColor = Color.FromArgb(5, 163, 255);
            InitializeComponent();
            timer1.Enabled = false;
            timer1.Interval = 3000;
            timer1.Tick += timer1_Tick;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
        }

        void Copyfile(string source, string des)
        {
            
            FileStream fsout = new FileStream(des, FileMode.Create);
            FileStream fsin = new FileStream(source, FileMode.Open);
            byte[] bt = new byte[1048756];
            int Readbyte;
            while ((Readbyte = fsin.Read(bt, 0, bt.Length)) > 0)
            {
                fsout.Write(bt, 0, Readbyte);
                backgroundWorker1.ReportProgress((int)(fsin.Position * 100 / fsin.Length));
            }
            
            fsin.Close();
            fsout.Close();
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

       

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                currentFolderPath = label1.Text;
                newPath = textBox4.Text;
                File.Move(newPath, currentFolderPath);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            currentFolderPath = label1.Text;
            newPath = textBox4.Text;
            Copyfile(currentFolderPath, newPath);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClassLibrary1.Class1 j = new ClassLibrary1.Class1();

            label3.Text = j.Full_name+"-"+j.Number_of_group+ "-"+j.Number_of_lab.ToString()+"-" + j.Number_of_project.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
            
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            string Button_condition= " ";
            
            switch (e.Button)
            {
                case MouseButtons.Right:
                    Button_condition = "Right";
                    break;
                case MouseButtons.Left:
                    Button_condition = "Left";
                    break;
                case MouseButtons.None:
                    Button_condition = "Najmi";
                    break;
            }
            label4.Text = "He clicked:"+ Button_condition + "Mouse Button";
        }

    }
}
