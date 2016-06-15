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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tsbaddress.Width = this.toolStrip1.Width - 270;
            //this.toolStrip2.Width = this.toolStrip1.Width - 400;
            //toolStrip2.Location.X = menuStrip1.Location.X + 300;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            //显示当前信息的地址
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard) {
                if (e.Node.Tag == null) {
                    this.tsbaddress.Text = e.Node.Text;
                }
                else {
                    this.tsbaddress.Text = e.Node.Tag.ToString();
                }
                //ListView中显示文件信息
                this.listView1.Items.Clear();
                if (e.Node.Tag != null) {
                    string folderPath = e.Node.Tag.ToString();
                    DirectoryInfo folder = new DirectoryInfo(folderPath);

                    DirectoryInfo[] subfolders = null;
                    try { subfolders = folder.GetDirectories(); }
                    catch (UnauthorizedAccessException) { }

                    foreach (DirectoryInfo subfolder in subfolders) {
                        listView1.Items.Add(new ListViewItem(new string[]{
                            subfolder.Name,"folder","",""
                        },"Folder.ico"));
                    }

                    FileInfo[] files = folder.GetFiles();
                    foreach (FileInfo file in files) {
                        listView1.Items.Add(new ListViewItem(new string[]{
                            file.Name,file.LastWriteTime.ToString(),"file",GetFileSize(file)
                        },"File.ico"));
                    }

                }
            }
        }

        //获取文件大小
        private string GetFileSize(FileInfo file) {
            string result = "";
            long fileSize = file.Length;
            if (fileSize >= 1024 * 1024 * 1024) {
                result = string.Format("{0:#####0.00} GB", ((double)fileSize) / (1024 * 1024 * 1024));
            }
            else if(fileSize >= 1024 * 1024 ) {
                result = string.Format("{0:####0.00} MB", ((double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                result = string.Format("{0:####0.00} KB", ((double)fileSize) / 1024);
            }
            else {
                result = string.Format("{0} B", fileSize);
            }
            return result;
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //获取根节点，并展开
            TreeNode rootNode1 = this.treeView1.Nodes[0];
            TreeNode rootNode2 = this.treeView1.Nodes[1];
            TreeNode rootNode3 = this.treeView1.Nodes[3];
            rootNode1.Expand();
            rootNode2.Expand();
            //rootNode3.Expand();
            
            //遍历第一层节点，找到计算机节点
            TreeNode ndComputer = null;
            foreach (TreeNode nd in treeView1.Nodes) {
                if (nd.Name == "ndComputer") {
                    ndComputer = nd;
                }
            }
            //遍历磁盘并把每个磁盘连接到节点上
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo driver in drivers) {
                TreeNode node = new TreeNode(driver.Name);
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
                else {
                    node.ImageKey = "F_Drive.ico";
                    node.SelectedImageKey = "F_Drive.ico";
                }
                //获取当前节点的地址
                node.Tag = driver.RootDirectory.FullName;
                ndComputer.Nodes.Add(node);
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //遍历每一层的子节点，并将地址作为新的父节点，继续遍历
            
            //if (e.Node.Parent != null) { return; }
            foreach (TreeNode node in e.Node.Nodes)
            {
                if (node.Tag == null) { continue; }
                DirectoryInfo folder = new DirectoryInfo(node.Tag.ToString());
                DirectoryInfo[] subFolders = null;
                try { subFolders = folder.GetDirectories(); }
                catch (Exception) { }
                if (subFolders != null)
                {
                    foreach (DirectoryInfo subFolder in subFolders)
                    {
                        TreeNode subNode = new TreeNode(subFolder.Name);
                        subNode.Tag = subFolder.FullName;
                        node.Nodes.Add(subNode);
                    }
                }
            }
        }
    }
}
