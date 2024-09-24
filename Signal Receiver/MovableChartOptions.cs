using ScottPlot.Styles;
using ScottPlot;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Receiver
{
    public partial class Form1 : Form
    {

        private bool _IsFilling = false;
        private bool _IsAutoScale = true;
        private bool _IsMousing = false;  
        private bool _IsAddingHorizontal = false;
        private bool _IsAddingVertical = false;
        private bool _IsPointAdding = false;
        private bool _IsTextAdding = false;

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (_IsAutoScale)
            {
                _IsAutoScale = false;
                toolStripMenuItem9.BackColor = Color.Transparent;
            }
            else
            {
                _IsAutoScale = true;
                toolStripMenuItem9.BackColor = Color.LightGreen;
            }
        }


        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            _IsFilling = !_IsFilling;
            if (_IsFilling)
            {
                toolStripMenuItem10.BackColor = Color.LightGreen;
            }
            else
            {
                toolStripMenuItem10.BackColor = Color.Transparent;
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem11.BackColor == Color.LightGreen)
            {
                _marker = 0;
                toolStripMenuItem11.BackColor = Color.Transparent;
            }
            else
            {
                _marker = 5;
                toolStripMenuItem11.BackColor = Color.LightGreen;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 spans_input = new Form2(" vertical spans");
            spans_input.ShowDialog();

            if (spans_input.x is not null && spans_input.y is not null)
            {
                var hSpan = formsPlot1.Plot.AddHorizontalSpan((double)spans_input.x, (double)spans_input.y);
                hSpan.DragEnabled = true;
                hSpan.DragFixedSize = true;
            }
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 spans_input = new Form2(" horizontal spans");
            spans_input.ShowDialog();

            if (spans_input.x is not null && spans_input.y is not null)
            {
                var hSpan = formsPlot1.Plot.AddVerticalSpan((double)spans_input.x, (double)spans_input.y);
                hSpan.DragEnabled = true;
                hSpan.DragFixedSize = true;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Style(style: Style.Black);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Style(style: Style.Pink);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Style(style: Style.Default);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Style(style: Style.Blue2);
        }



        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            _IsAddingVertical = !_IsAddingVertical;
            if (_IsAddingVertical)
            {
                formsPlot1.MouseMove += FormsPlot1_MouseMove;
                formsPlot1.MouseDown += (s, e) =>
                {
                    if (_IsAddingVertical)
                    {
                        toolStripMenuItem4.BackColor = Color.LightGreen;
                        (double x, double y) = formsPlot1.GetMouseCoordinates();
                        Text = $"X={x:N3}, Y={y:N3} (mouse down)";
                        var vline = formsPlot1.Plot.AddVerticalLine(x);
                        vline.LineWidth = 2;
                        vline.PositionLabel = true;
                        vline.PositionLabelBackground = vline.Color;
                        vline.DragEnabled = true;

                    }
                };
            }
            else
            {
                toolStripMenuItem4.BackColor = Color.Transparent;
                formsPlot1.MouseMove -= FormsPlot1_MouseMove;
                formsPlot1.MouseDown -= FormsPlot1_MouseDown;
                Text = "Signal Receiver";
            }

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

            _IsAddingHorizontal = !_IsAddingHorizontal;
            if (_IsAddingHorizontal)
            {
                formsPlot1.MouseMove += FormsPlot1_MouseMove;
                formsPlot1.MouseDown += (s, e) =>
                {
                    if (_IsAddingHorizontal)
                    {
                        toolStripMenuItem8.BackColor = Color.LightGreen;
                        (double x, double y) = formsPlot1.GetMouseCoordinates();
                        Text = $"X={x:N3}, Y={y:N3} (mouse down)";
                        var vline = formsPlot1.Plot.AddHorizontalLine((double)y);
                        vline.LineWidth = 2;
                        vline.PositionLabel = true;
                        vline.PositionLabelBackground = vline.Color;
                        vline.DragEnabled = true;

                    }
                };
            }
            else
            {
                toolStripMenuItem8.BackColor = Color.Transparent;
                formsPlot1.MouseMove -= FormsPlot1_MouseMove;
                formsPlot1.MouseDown -= FormsPlot1_MouseDown;
                Text = "Signal Receiver";
            }

            
        }




        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _IsMousing = !_IsMousing;
            if (_IsMousing)
            {
                toolStripButton3.BackColor = Color.LightGreen;
                formsPlot1.MouseMove += FormsPlot1_MouseMove;
                formsPlot1.MouseDown += FormsPlot1_MouseDown;
            }
            else
            {
                toolStripButton3.BackColor = Color.Transparent;
                formsPlot1.MouseMove -= FormsPlot1_MouseMove;
                formsPlot1.MouseDown -= FormsPlot1_MouseDown;
                Text = "Signal Receiver";
            }
        }





        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            _IsPointAdding = !_IsPointAdding;
            if (_IsPointAdding)
            {
                formsPlot1.MouseMove += FormsPlot1_MouseMove;
                formsPlot1.MouseDown += (s, e) =>
                {
                    if (_IsPointAdding)
                    {
                        toolStripMenuItem12.BackColor = Color.LightGreen;
                        (double x, double y) = formsPlot1.GetMouseCoordinates();
                        Text = $"X={x:N3}, Y={y:N3} (mouse down)";
                        formsPlot1.Plot.AddPoint(x,y);
                        formsPlot1.Plot.AddText($"x:{Math.Round(x, 3)}\ny:{Math.Round(y, 3)}", x, y, size: 15, color: Color.White);
                    }
                };
            }
            else
            {
                toolStripMenuItem12.BackColor = Color.Transparent;
                formsPlot1.MouseMove -= FormsPlot1_MouseMove;
                formsPlot1.MouseDown -= FormsPlot1_MouseDown;
                Text = "Signal Receiver";
            }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            _IsTextAdding = !_IsTextAdding;



            if (_IsTextAdding)
            {
                textBox4.Visible = true;
                formsPlot1.MouseMove += FormsPlot1_MouseMove;
                formsPlot1.MouseDown += (s, e) =>
                {
                    if (_IsTextAdding && textBox4.Text != "")
                    {
                        toolStripMenuItem13.BackColor = Color.LightGreen;
                        (double x, double y) = formsPlot1.GetMouseCoordinates();
                        Text = $"X={x:N3}, Y={y:N3} (mouse down)";
                        formsPlot1.Plot.AddPoint(x, y);
                        formsPlot1.Plot.AddText(textBox4.Text, x, y, size: 15, color: Color.White);
                    }
                };
            }
            else
            {
                toolStripMenuItem13.BackColor = Color.Transparent;
                textBox4.Visible = false;
                formsPlot1.MouseMove -= FormsPlot1_MouseMove;
                formsPlot1.MouseDown -= FormsPlot1_MouseDown;
                Text = "Signal Receiver";
            }
        }


        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            (double x, double y) = formsPlot1.GetMouseCoordinates();
            Text = $"X={x:N3}, Y={y:N3}";
        }

        private void FormsPlot1_MouseDown(object sender, MouseEventArgs e)
        {
            (double x, double y) = formsPlot1.GetMouseCoordinates();
            Text = $"X={x:N3}, Y={y:N3} (mouse down)";
        }


       


    }
}
