using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace NantBuildBlocks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadBlocks();
        }

        private void LoadBlocks()
        {
            BasicElement[] blocks = Utility.LoadBlocks();
            if (blocks.Length > 0)
            {
                foreach (BasicElement element in blocks)
                {
                    BlocksListBox.Items.Add(element);
                }
            }
            else
            {
                BasicElement projectNode = new BasicElement("Project");
                BasicProperty prop = new BasicProperty("name", "Octgn test build");
                projectNode.Properties.Add(prop);
                prop = new BasicProperty("default", "Build");
                projectNode.Properties.Add(prop);
                BlocksListBox.Items.Add(projectNode);
            }
        }

        private void SaveBlocks()
        {
            BasicElement[] array = new BasicElement[BlocksListBox.Items.Count];
            BlocksListBox.Items.CopyTo(array, 0);
            Utility.SaveBlocks(array);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveBlocks();
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicElement block = (BasicElement)BlocksListBox.SelectedItem;
            DialogResult result = MessageBox.Show(string.Format("Are you sure you want to delete block: {0}", block), "Delete block", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                BlocksListBox.Items.Remove(block);
                if (Utility.DeleteBlock(block))
                {
                    MessageBox.Show("Block deleted", "Deleted", MessageBoxButtons.OK);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                BasicElement ret = Utility.LoadBuildFile(openFileDialog1.FileName);
                TreeNode root = LoadTree(ret);
                treeView1.Nodes.Add(root);
            }
        }

        private TreeNode LoadTree(BasicElement rootElement)
        {
            TreeNode rootNode = new TreeNode(rootElement.ElementName);
            if (rootElement.Properties.Count > 0)
            {
                foreach (BasicProperty property in rootElement.Properties)
                {
                    TreeNode propertyNode = new TreeNode();
                    propertyNode.Text = property.ToString();
                    rootNode.Nodes.Add(propertyNode);
                }
            }
            if (rootElement.ChildElements.Count > 0)
            {
                foreach (BasicElement element in rootElement.ChildElements)
                {
                    rootNode.Nodes.Add(LoadNode(element, rootNode));
                }
            }
            return (rootNode);
        }

        private TreeNode LoadNode(BasicElement element, TreeNode parent)
        {
            string elementName = (string)Type.GetType(element.InherentType).InvokeMember("ElementName", BindingFlags.GetProperty, null, element, null);
            TreeNode node = new TreeNode(elementName);

            if (element.Properties.Count > 0)
            {
                foreach (BasicProperty property in element.Properties)
                {
                    string text = (string)Type.GetType(property.InherentType).InvokeMember("ToString", BindingFlags.InvokeMethod, null, property, null);
                    TreeNode propertyNode = new TreeNode();
                    propertyNode.Text = text;
                    node.Nodes.Add(propertyNode);
                }
            }

            if (element.ChildElements.Count > 0)
            {
                foreach (BasicElement child in element.ChildElements)
                {
                    node.Nodes.Add(LoadNode(child, node));
                }
            }
            return (node);
        }
    }
}
