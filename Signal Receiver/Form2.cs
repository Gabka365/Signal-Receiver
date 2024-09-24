using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Signal_Receiver
{
    public partial class Form2 : Form
    {
        private double _x;
        private double _y;
        private string? _text;

        public double? x = null;
        public double? y = null;
        public string? text = null;

        public Form2(string expected_data)
        {
            InitializeComponent();

            if (expected_data == " vertical spans")
            {
                label1.Text = "X1";
                label2.Text = "X2";
                button2.Visible = false;
            }
            if (expected_data == " horizontal spans")
            {
                label1.Text = "Y1";
                label2.Text = "Y2";
                button2.Visible = false;
            }


            Text = "Input" + expected_data;

            textBox1.MaxLength = 10;
            textBox2.MaxLength = 10;

            if (expected_data == " inscription")
            {
                label1.Visible = false;
                label2.Visible = false;
                textBox2.Visible = false;
                textBox1.MaxLength = 100;
                Size = new Size(268, 178);
                button1.Visible = false;
                textBox1.Size = new Size(226, 27);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                try
                {

                    _x = Convert.ToDouble(textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("Incorrect format");
                }
            }
            else
            {
                _x = 0;
            }
            if (textBox2.Text.Length != 0)
            {
                try
                {
                    _y = Convert.ToDouble(textBox2.Text);
                }
                catch
                {
                    MessageBox.Show("Incorrect format");
                }
            }
            else
            {
                _y = 0;
            }

            x = _x;
            y = _y;
            Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                _text = textBox1.Text;
                text = _text;
            }
            else
            {
                _text = "";
                text = _text;
            }

            Close();
        }
    }
}
