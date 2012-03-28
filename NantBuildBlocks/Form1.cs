using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            BasicElement projectNode = new BasicElement("Project");
            BasicProperty prop = new BasicProperty("name", "Octgn test build");
            projectNode.Properties.Add(prop);
            prop = new BasicProperty("default", "Build");
            projectNode.Properties.Add(prop);
            listBox1.Items.Add(projectNode);
        }
    }
}
