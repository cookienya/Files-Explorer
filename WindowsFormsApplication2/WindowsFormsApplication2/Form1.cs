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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            TreeNode rootNode1 = this.treeView1.Nodes[0];
            TreeNode rootNode2 = this.treeView1.Nodes[1];
            TreeNode rootNode3 = this.treeView1.Nodes[3];
            rootNode1.Expand();
            rootNode2.Expand();
            //rootNode3.Expand();
            TreeNode ndComputer = null;
            foreach (TreeNode nd in treeView1.Nodes) {
                if (nd.Name == "ndComputer") {
                    ndComputer = nd;
                }
            }
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo driver in drivers) {
                TreeNode node = new TreeNode(driver.Name);
                if (node.Name == "C:\\")
                {
                    node.ImageKey = "C_Drive.ico";
                    node.SelectedImageKey = "C_Drive.ico";
                }
                else {
                    node.ImageKey = "D_Drive.ico";
                    node.SelectedImageKey = "D_Drive.ico";
                }
                ndComputer.Nodes.Add(node);
            }
        }
    }
}
