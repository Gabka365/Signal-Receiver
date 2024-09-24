using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Receiver
{
    public partial class Form1 : Form
    {
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (panel2.Visible || panel3.Visible)
            {
                panel5.Visible = false;
                toolStripSeparator7.Visible = true;
                panel4.Location = new Point(12, 37);
                toolStripButton6.Visible = true;


                if (panel2.Visible && panel3.Visible)
                {
                    Size = new System.Drawing.Size(1377, 676);
                }
                else if (!panel2.Visible)
                {
                    Size = new System.Drawing.Size(870, 696);
                }
                else if (!panel3.Visible)
                {
                    Size = new System.Drawing.Size(590, 750);
                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton6.Visible = false;
            toolStripSeparator7.Visible = false;
            panel5.Visible = true;
            panel4.Location = new Point(348, 37);

            if (panel2.Visible && panel3.Visible)
            {
                Size = new System.Drawing.Size(1751, 676);
            }
            else if (!panel2.Visible && panel3.Visible)
            {
                Size = new System.Drawing.Size(1253, 676);
            }
            else if (panel2.Visible && !panel3.Visible)
            {
                Size = new System.Drawing.Size(953, 716);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (panel3.Visible)
            {
                panel2.Visible = false;
                toolStripSeparator8.Visible = true;
                panel3.Location = new Point(25, 18);
                panel4.Size = new System.Drawing.Size(824, 573);
                toolStripButton8.Visible = true;

                button3.Location = new Point(383, 521);
                button4.Location = new Point(589, 521);

                if (panel5.Visible && panel3.Visible)
                {
                    Size = new System.Drawing.Size(1253, 676);
                }
                else if (!panel5.Visible)
                {
                    Size = new System.Drawing.Size(900, 700);
                }
                else if (!panel3.Visible)
                {
                    Size = new System.Drawing.Size(590, 750);
                }
            }

            

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            toolStripButton8.Visible = false;
            toolStripSeparator8.Visible = false;
            panel2.Visible = true;
            panel2.Location = new Point(25, 18);
            panel3.Location = new Point(539, 18);
            panel4.Size = new System.Drawing.Size(1353, 573);
            toolStripButton7.Visible = true;


            button3.Location = new Point(920, 521);
            button4.Location = new Point(1126, 521);

            if (panel5.Visible && panel3.Visible)
            {
                Size = new System.Drawing.Size(1751, 676);
            }
            else if (panel5.Visible && !panel3.Visible)
            {
                Size = new System.Drawing.Size(953, 716);
            }
            else if (!panel5.Visible && panel3.Visible)
            {
                Size = new System.Drawing.Size(1377, 676);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                panel3.Visible = false;
                panel4.Size = new System.Drawing.Size(555, 628);
                toolStripButton9.Visible = true;
                toolStripSeparator5.Visible = true;

                button3.Location = new Point(100, 571);
                button4.Location = new Point(310, 571);


                if (panel2.Visible && panel5.Visible)
                {
                    Size = new System.Drawing.Size(953, 716);
                }
                else if (!panel2.Visible)
                {
                    Size = new System.Drawing.Size(953, 716);
                }
                else if (!panel5.Visible)
                {
                    Size = new System.Drawing.Size(590, 750);
                }
            }

            
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel4.Size = new System.Drawing.Size(1353, 573);
            panel3.Location = new Point(539, 18);
            button3.Location = new Point(920, 521);
            button4.Location = new Point(1130, 521);
            toolStripButton9.Visible = false;
            toolStripSeparator5.Visible = false;

            if (panel5.Visible && panel2.Visible)
            {
                Size = new System.Drawing.Size(1751, 676);
            }
            else if (!panel5.Visible && panel2.Visible)
            {
                Size = new System.Drawing.Size(1377, 676);
            }
            else if (panel5.Visible && !panel2.Visible)
            {
                Size = new System.Drawing.Size(1253, 676);
            }
        }
    }
}
