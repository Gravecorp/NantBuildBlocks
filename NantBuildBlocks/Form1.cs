using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace NantBuildBlocks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                    listBox1.Items.Add(element);
                }
            }
            else
            {
                BasicElement projectNode = new BasicElement("Project");
                BasicProperty prop = new BasicProperty("name", "Octgn test build");
                projectNode.Properties.Add(prop);
                prop = new BasicProperty("default", "Build");
                projectNode.Properties.Add(prop);
                listBox1.Items.Add(projectNode);
            }
        }

        private void SaveBlocks()
        {
            BasicElement[] array = new BasicElement[listBox1.Items.Count];
            listBox1.Items.CopyTo(array, 0);
            Utility.SaveBlocks(array);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveBlocks();
        }
    }
}
