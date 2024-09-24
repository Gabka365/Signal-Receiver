using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Receiver
{
    public partial class Form1 : Form
    {
        // Do with generics


        private string _ASCII_path = @"C:\proga\course\Signal Receiver\Saves\ASCII-files";
        private string _HEX_path = @"C:\proga\course\Signal Receiver\Saves\HEX-files";
        private string _Binary_path = @"C:\proga\course\Signal Receiver\Saves\Binary-files";

        private void saveHEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupportSave(_HEX_path, out DialogResult result, out FolderBrowserDialog dialog, out string Now);

            if (result == DialogResult.OK)
            {
                _HEX_path = dialog.SelectedPath;
                _HEX_path = _HEX_path + @"\" + "HEX_" + Now + ".txt";
                try
                {
                    File.WriteAllText(_HEX_path, textBox2.Text);
                }
                finally
                {
                    MessageBox.Show($"HEX-data saved successfully by path {_HEX_path}");
                }
            }
        }

        private void saveASCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupportSave(_ASCII_path, out DialogResult result, out FolderBrowserDialog dialog, out string Now);

            if (result == DialogResult.OK)
            {
                _ASCII_path = dialog.SelectedPath;
                _ASCII_path = _ASCII_path + @"\" + "ASCII_" + Now + ".txt";
                try
                {
                    File.WriteAllText(_ASCII_path, textBox1.Text);
                }
                finally
                {
                    MessageBox.Show($"ASCII-data saved successfully by path {_ASCII_path}");
                }
            }
        }


        private void saToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupportSave(_Binary_path, out DialogResult result, out FolderBrowserDialog dialog, out string Now);

            if (result == DialogResult.OK)
            {
                _Binary_path = dialog.SelectedPath;
                _Binary_path = _Binary_path + @"\" + "Binary_" + Now + ".txt";
                try
                {
                    File.WriteAllText(_Binary_path, textBox3.Text);
                }
                finally
                {
                    MessageBox.Show($"Binary-data saved successfully by path {_Binary_path}");
                }
            }
        }

        private void SupportSave(string path, out DialogResult result, out FolderBrowserDialog dialog, out string Now)
        {
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString();
            Now = new string(dateTimeString.Where(char.IsDigit).ToArray());
            dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = path;
            result = dialog.ShowDialog();
        }

    }
}
