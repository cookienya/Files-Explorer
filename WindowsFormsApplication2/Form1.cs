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
using System.Diagnostics;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private string currentPath = ""; //当前路径
        //private string[] sources = new string[100];   //复制文件的源路径
        private void ListDrivers()
        {
            treeView1.Nodes.Clear();
            listView1.Items.Clear();
            currentPath = "";
            tsbaddress.Text = currentPath;
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo driver in drivers)
            {
                TreeNode node = treeView1.Nodes.Add(driver.Name);

                //判断驱动器类型，用不同图标显示
                if (node.Text == "C:\\")
                {
                    node.ImageKey = "C_Drive.ico";
                    node.SelectedImageKey = "C_Drive.ico";
                }
                else if (node.Text == "D:\\")
                {
                    node.ImageKey = "D_Drive.ico";
                    node.SelectedImageKey = "D_Drive.ico";
                }
                else if (node.Text == "E:\\")
                {
                    node.ImageKey = "E_Drive.ico";
                    node.SelectedImageKey = "E_Drive.ico";
                }
                else
                {
                    node.ImageKey = "F_Drive.ico";
                    node.SelectedImageKey = "F_Drive.ico";
                }
            }

            foreach (TreeNode node in treeView1.Nodes)
            {
                NodeUpdate(node);
            }
        }

        //更新结点(列出当前目录下的子目录)
        private void NodeUpdate(TreeNode node)
        {
            try
            {
                node.Nodes.Clear();
                DirectoryInfo dir = new DirectoryInfo(node.FullPath);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo d in dirs)
                {
                    node.Nodes.Add(d.Name);
                }
            }
            catch (Exception) { }
        }

        //更新列表(列出当前目录下的目录和文件)
        private void ListUpdate(string newPath)
        {
            if (newPath == "")
                ListDrivers();
            else
            {
                try
                {
                    DirectoryInfo currentDir = new DirectoryInfo(newPath);
                    DirectoryInfo[] dirs = currentDir.GetDirectories(); //获取目录
                    FileInfo[] files = currentDir.GetFiles();   //获取文件
                    listView1.Items.Clear();
                    //列出文件夹
                    foreach (DirectoryInfo dir in dirs)
                    {
                        ListViewItem dirItem = listView1.Items.Add(dir.Name, "Folder.ico");
                        dirItem.Name = dir.FullName;
                        dirItem.SubItems.Add(dir.LastWriteTimeUtc.ToString());
                        dirItem.SubItems.Add("文件夹");
                        dirItem.SubItems.Add("");
                    }
                    //列出文件
                    foreach (FileInfo file in files)
                    {
                        ListViewItem fileItem = listView1.Items.Add(file.Name,"file.ico");
                        fileItem.Name = file.FullName;
                        fileItem.SubItems.Add(file.LastWriteTimeUtc.ToString());
                        fileItem.SubItems.Add(file.Extension);
                        fileItem.SubItems.Add(GetFileSize(file));
                    }
                    currentPath = newPath;
                    tsbaddress.Text = currentPath;   //更新地址栏
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string GetFileSize(FileInfo file)
        {
            string result = "";
            long fileSize = file.Length;
            if (fileSize >= 1024 * 1024 * 1024)
            {
                result = string.Format("{0:#####0.00} GB", ((double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                result = string.Format("{0:####0.00} MB", ((double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                result = string.Format("{0:####0.00} KB", ((double)fileSize) / 1024);
            }
            else
            {
                result = string.Format("{0} B", fileSize);
            }
            return result;
        }
        //打开文件夹或文件
        private void Open()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string newPath = listView1.SelectedItems[0].Name;
                try
                {
                    //判断是目录还是文件
                    if (Directory.Exists(newPath))
                        ListUpdate(newPath);
                    else
                        Process.Start(newPath); //打开文件
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //新建文件夹
        private void CreateFolder()
        {
            try
            {
                string path = Path.Combine(currentPath, "新建文件夹");
                int i = 1;
                string newPath = path;
                while (Directory.Exists(newPath))
                {
                    newPath = path + i;
                    i++;
                }
                Directory.CreateDirectory(newPath);
                listView1.Items.Add(newPath, "新建文件夹" + (i - 1 == 0 ? "" : (i - 1).ToString()), "Folder.ico");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tsbaddress.Width = this.toolStrip1.Width - 245;
            this.toolStripTextBox1.Width = this.toolStrip1.Width - this.tsbaddress.Width - 120;
            //this.toolStrip2.Width = this.toolStrip1.Width - 400;
            //toolStrip2.Location.X = menuStrip1.Location.X + 300;
        } 
        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            e.Node.Expand();
            ListUpdate(e.Node.FullPath);
        }

        //获取文件大小
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            ListDrivers();
        } 
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            Open();
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            NodeUpdate(e.Node); //更新当前结点
            foreach (TreeNode node in e.Node.Nodes) //更新所有子结点
            {
                NodeUpdate(node);
            }
        }

        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolder();
        }

        private void tsbaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string newPath = tsbaddress.Text;
                if (newPath == "")
                    return;
                ListUpdate(newPath);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deletenode();
        }
        private void Deletenode() {                //删除函数
            if (listView1.SelectedItems.Count == 0)
                return;
            DialogResult result = MessageBox.Show("确定要删除吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
                return;
            try
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    string path = item.Name;
                    if (File.Exists(path)) 
                        File.Delete(path);
                    else if (Directory.Exists(path)) 
                        Directory.Delete(path, true);
                    listView1.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void 新建文件夹ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateFolder();
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Deletenode();
        }
    }
}
