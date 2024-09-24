using System.Runtime.Serialization;
using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using System.Configuration;
using ScottPlot.Styles;
using System.Runtime.InteropServices;
using ScottPlot;



namespace Signal_Receiver
{

    public partial class Form1 : Form
    {

        private SerialPort _currentPort = new SerialPort();
        private int[] _availableBaudrates = { 4800, 9600, 19200, 31250, 115200 };
        private string[] _availableParities = { "None", "Odd", "Even", "Mark", "Space" };
        private int[] _availableDataBits = { 5, 6, 7, 8, 9 };
        private string[] _availableStopBits = { "None", "One", "Two", "OnePointFive" };
        private object _receiveDataLock = new object();
        private ManualResetEvent _reading = new ManualResetEvent(true);
        private Thread _receiveData;
        private string? _txt;

        public Form1()
        {
            InitializeComponent();


            toolStripMenuItem11.BackColor = Color.LightGreen;
            toolStripMenuItem9.BackColor = Color.LightGreen;

            DoubleBuffered = true;

            Text = "Signal Receiver";

            label1.Text = "COM-port:";
            label2.Text = "Bandwidth (baud):";
            label3.Text = "Parity:";
            label4.Text = "Data bits:";
            label5.Text = "Stop bits:";

            FrameColor frame = new FrameColor();
            frame.CustomWindow(Color.Black, Color.White, Color.Red, Handle);

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;

            panel4.Enabled = false;
            button4.Visible = false;

            toolStrip1.BackColor = Color.FromArgb(150, Color.IndianRed);
            toolStrip2.BackColor = Color.FromArgb(100, Color.White);
            toolStrip3.BackColor = Color.FromArgb(100, Color.White);
            toolStrip4.BackColor = Color.FromArgb(150, Color.IndianRed);


            toolStrip1.ForeColor = Color.White;
            toolStrip2.ForeColor = Color.White;
            toolStrip3.ForeColor = Color.White;
            toolStrip4.ForeColor = Color.White;


            panel5.BackColor = Color.FromArgb(150, Color.Gray);
            panel4.BackColor = Color.FromArgb(150, Color.Gray);


            textBox1.BackColor = Color.LightGreen;
            textBox2.BackColor = Color.LightGreen;
            textBox3.BackColor = Color.LightGreen;
            textBox1.ForeColor = Color.Black;
            textBox2.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;

            textBox4.Visible = false;

            textBox1.Visible = true;
            textBox2.Visible = false;
            textBox3.Visible = false;

            SetRoundedView();

            toolStripButton1.BackColor = Color.LightGreen;



            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (int Baudrate in _availableBaudrates)
            {
                comboBox2.Items.Add(Baudrate);
            }
            comboBox2.SelectedItem = 9600;

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (string Parity in _availableParities)
            {
                comboBox3.Items.Add(Parity);
            }
            comboBox3.SelectedItem = "None";

            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (int DataBits in _availableDataBits)
            {
                comboBox4.Items.Add(DataBits);
            }
            comboBox4.SelectedItem = 8;


            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (string StopBits in _availableStopBits)
            {
                comboBox6.Items.Add(StopBits);
            }
            comboBox6.SelectedItem = "None";

            button1.Text = "Connect";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox2.ScrollBars = ScrollBars.Both;
            textBox3.ScrollBars = ScrollBars.Both;
            toolStripButton6.Visible = false;
            toolStripButton8.Visible = false;
            toolStripButton9.Visible = false;
            toolStripSeparator7.Visible = false;
            toolStripSeparator8.Visible = false;

            formsPlot1.ForeColor = Color.White;
            formsPlot1.Plot.Style(style: Style.Black);


            button4.Visible = true;
            toolStripButton11.Visible = false;
            InitializeChart();
        }



        // for rounded shapes

        private void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string SerialPort in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(SerialPort);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem is not null && comboBox2.SelectedItem is not null)
            {

                if (_currentPort.IsOpen)
                {
                    _currentPort.Close();
                }

                _currentPort.PortName = comboBox1.SelectedItem.ToString();
                _currentPort.BaudRate = (int)comboBox2.SelectedItem;

                switch (comboBox3.SelectedItem)
                {
                    case "None":
                        _currentPort.Parity = Parity.None;
                        break;
                    case "Odd":
                        _currentPort.Parity = Parity.Odd;
                        break;
                    case "Even":
                        _currentPort.Parity = Parity.Even;
                        break;
                    case "Mark":
                        _currentPort.Parity = Parity.Mark;
                        break;
                    case "Space":
                        _currentPort.Parity = Parity.Space;
                        break;
                }


                switch (comboBox6.SelectedItem)
                {
                    case "One":
                        _currentPort.StopBits = StopBits.One;
                        break;
                    case "Two":
                        _currentPort.StopBits = StopBits.Two;
                        break;
                    case "OnePointFive":
                        _currentPort.StopBits = StopBits.OnePointFive;
                        break;
                }

                panel4.Enabled = true;


                _receiveData = new Thread(Receive)
                {
                    Name = "Receive",
                    IsBackground = true,
                };

                _receiveData.Start();

                DropChartParam();
                formsPlot1.Reset();
                formsPlot1.Plot.Style(style: Style.Black);

                button1.Enabled = false;
                button3.Enabled = true;


                textBox1.Visible = true;
                textBox2.Visible = true;

            }

        }

        private void Receive()
        {
            if (!_currentPort.IsOpen)
            {
                try
                {
                    _currentPort.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            _currentPort.DataReceived += new SerialDataReceivedEventHandler(HandleReceipt);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Monitor.Enter(_receiveDataLock);
            try
            {
                if (_currentPort.IsOpen)
                {
                    Monitor.Wait(_receiveDataLock);
                    _currentPort.DiscardInBuffer();
                    _currentPort.DiscardOutBuffer();
                    _currentPort.Close();
                }
            }
            finally
            {
                Monitor.Exit(_receiveDataLock);
            }

            Close();
            panel4.Enabled = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button1.Enabled = true;
              
            
            Monitor.Enter(_receiveDataLock);
            try
            {
                if (_currentPort.IsOpen)
                {
                    Monitor.Wait(_receiveDataLock);
                    _currentPort.DiscardInBuffer();
                    _currentPort.DiscardOutBuffer();
                    _currentPort.Close();
                }
            }
            finally
            {
                Monitor.Exit(_receiveDataLock);
            }


            textBox1.BeginInvoke(textBox1.Clear);
            textBox2.BeginInvoke(textBox2.Clear);


            panel4.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Pause")
            {
                Monitor.Enter(_receiveDataLock);
                try
                {
                    if (_currentPort.IsOpen)
                    {
                        Monitor.Wait(_receiveDataLock);
                        _currentPort.DiscardInBuffer();
                        _currentPort.DiscardOutBuffer();
                        _currentPort.Close();
                    }
                }
                finally
                {
                    Monitor.Exit(_receiveDataLock);
                }


                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;

                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;

                toolStripButton12.Visible = false;

                button4.Text = "Start";
            }
            else
            {
                try
                {
                    _currentPort.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message);
                }



                label1.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;

                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;

                toolStripButton12.Visible = true;

                button4.Text = "Pause";
            }


        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

            toolStrip1.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton7.Visible = false;
            toolStripButton11.Visible = true;
            toolStripButton10.Visible = false;
            panel2.Visible = false;
            panel5.Visible = false;
            WindowState = FormWindowState.Maximized;
            panel4.Size = new Size(1860, 1050);
            panel4.Location = new Point(30, 30);
            panel3.Location = new Point(0, 0);
            panel3.Size = new Size(1860, 1050);
            formsPlot1.Size = new Size(1700, 800);
            formsPlot1.Location = new Point(80, 80);


            button4.Location = new Point(1520, 900);
            textBox4.Location = new Point(1314, 900);

            button4.BackColor = Color.Black;
            button4.ForeColor = Color.White;

            button3.Visible = false;

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = true;
            toolStripButton5.Visible = true;
            toolStripButton7.Visible = true;
            toolStripButton11.Visible = false;
            toolStripButton10.Visible = true;
            panel2.Visible = true;
            panel5.Visible = true;
            WindowState = FormWindowState.Normal;
            panel4.Size = new Size(1353, 573);
            panel4.Location = new Point(348, 37);
            panel3.Location = new Point(539, 18);
            panel3.Size = new Size(788, 497);
            formsPlot1.Size = new Size(744, 460);
            formsPlot1.Location = new Point(18, 31);
            button3.Location = new Point(920, 521);

            button3.Visible = true;

            button4.BackColor = Color.Transparent;
            button4.ForeColor = Color.Black;

            button4.Location = new Point(1126, 521);

            textBox4.Location = new Point(714, 521);

        }

        private void toolStripButton12_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("For more options press Pause button.");
        }


        private void SetRoundedView()
        {
            SetRoundedShape(panel1, 30);

            SetRoundedShape(button1, 30);
            SetRoundedShape(button3, 30);
            SetRoundedShape(button4, 30);
            SetRoundedShape(textBox1, 30);
            SetRoundedShape(textBox2, 30);
            SetRoundedShape(textBox3, 30);
            SetRoundedShape(textBox4, 30);

            toolStrip4.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
        }

        
    }
}
