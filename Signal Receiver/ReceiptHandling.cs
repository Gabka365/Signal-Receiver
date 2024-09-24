using ScottPlot.Drawing.Colormaps;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Signal_Receiver
{
    public partial class Form1 : Form
    {
        private float _number;
        private byte[] _bytes;
        private string? _hex_number;
        private int _counterLines = 0;


        private void HandleReceipt(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (_currentPort.IsOpen)
            {
                _reading.WaitOne();

                Monitor.Enter(_receiveDataLock);
                try
                {
                    _txt += _currentPort.ReadLine() + "\n";

                    if (InvokeRequired)
                    {
                        textBox1.BeginInvoke(SetAsciiText, _txt.ToString());
                        textBox2.BeginInvoke(SetHexText, _txt.ToString());
                        textBox3.BeginInvoke(SetBooleanText, _txt.ToString());
                        formsPlot1.BeginInvoke(UpdateChart, _txt.ToString());

                    }
                    else
                    {
                        SetAsciiText(_txt.ToString());
                        SetHexText(_txt.ToString());
                    }

                    _txt = null;
                }
                finally
                {
                    Monitor.Pulse(_receiveDataLock);
                    Monitor.Exit(_receiveDataLock);
                }
            }

        }



        private void SetAsciiText(string? text)
        {
            textBox1.AppendText(text);
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            _counterLines++;
            
        }



        private void SetHexText(string? text)
        {
            if (text != null)
            {
                text = text.Replace("\n", "").Replace("\r", "");
                _number = float.Parse(text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);
                _bytes = BitConverter.GetBytes(_number);
                _hex_number = BitConverter.ToString(_bytes).Replace("-", "");
                textBox2.AppendText(_hex_number + "\r\n");
                textBox2.SelectionStart = textBox2.Text.Length;
                textBox2.ScrollToCaret();

            }
        }


        private void SetBooleanText(string? text)
        {
            text = text.Replace("\n", "").Replace("\r", "");
            _number = float.Parse(text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);
            _bytes = BitConverter.GetBytes(_number);

            text = Convert.ToString(_bytes[0], 2).PadLeft(8, '0') +
                        Convert.ToString(_bytes[1], 2).PadLeft(8, '0') +
                        Convert.ToString(_bytes[2], 2).PadLeft(8, '0') +
                        Convert.ToString(_bytes[3], 2).PadLeft(8, '0');


            textBox3.AppendText(text + "\r\n");
            textBox3.ScrollToCaret();


            if (_counterLines > 300)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                _counterLines = 0;
            }

        }

    }


}

