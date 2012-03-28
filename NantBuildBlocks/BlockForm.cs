using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NantBuildBlocks
{
    public partial class BlockForm : Form
    {
        public BlockForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public BasicElement Element
        {
            get;
            set;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BasicElement selectedElement = (BasicElement)comboBox2.SelectedItem;
            foreach (BasicProperty property in selectedElement.Properties)
            {
                comboBox1.Items.Add(property);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BasicProperty property = (BasicProperty)comboBox1.SelectedItem;
            NameTextBox.Text = property.Name;
            ValueTextBox.Text = property.Value;
        }

        private void BlockForm_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox1.Items.Clear();
            if (Element == null)
            {
                Element = new BasicElement("New Element");
                Element.Properties.Add(new BasicProperty("Argument", "somethingcool"));
                Element.Properties.Add(new BasicProperty("Argument", "somethingcrappy"));
            }
            comboBox2.Items.Add(Element);
            LoadAllChildElements(Element);
            comboBox2.SelectedItem = Element;
        }

        private void LoadAllChildElements(BasicElement element)
        {
            if (element.ChildElements.Count > 0)
            {
                foreach (BasicElement child in element.ChildElements)
                {
                    comboBox2.Items.Add(child);
                    if (child.ChildElements.Count > 0)
                    {
                        LoadAllChildElements(child);
                    }
                }
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            BasicProperty property = (BasicProperty)comboBox1.SelectedItem;
            property.Name = NameTextBox.Text;
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            BasicProperty property = (BasicProperty)comboBox1.SelectedItem;
            property.Value = ValueTextBox.Text;
        }
    }
}
