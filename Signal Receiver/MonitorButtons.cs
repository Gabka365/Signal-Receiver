using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Receiver
{
    public partial class Form1 : Form
    {
        private bool _IsASCII = true;
        private bool _IsHEX = false;
        private bool _IsBinary = false;


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _IsASCII = !_IsASCII;
            
            if (!_IsASCII) 
            { 
                toolStripButton1.BackColor = Color.Transparent;
                textBox1.Visible = false;
            }
            else
            {
                toolStripButton1.BackColor = Color.LightGreen;
                textBox1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;
                toolStripButton2.BackColor = Color.Transparent;
                toolStripButton18.BackColor = Color.Transparent;
                _IsHEX = false;
                _IsBinary = false;
            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _IsHEX = !_IsHEX;

            if (!_IsHEX)
            {
                toolStripButton2.BackColor = Color.Transparent;
                textBox2.Visible= false;
            }
            else
            {
                toolStripButton2.BackColor = Color.LightGreen;
                textBox1.Visible = false;
                textBox2.Visible = true;
                textBox3.Visible = false;
                toolStripButton1.BackColor = Color.Transparent;
                toolStripButton18.BackColor = Color.Transparent;
                _IsASCII = false;
                _IsBinary = false;
            }

            
        }


        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            _IsBinary = !_IsBinary;

            if (!_IsBinary)
            {
                toolStripButton18.BackColor = Color.Transparent;
                textBox3.Visible= false;
            }
            else
            {
                toolStripButton18.BackColor = Color.LightGreen;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = true;
                toolStripButton2.BackColor = Color.Transparent;
                toolStripButton1.BackColor = Color.Transparent;
                _IsHEX = false;
                _IsASCII = false;
            }

            
        }

        
    }
}
