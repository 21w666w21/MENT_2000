using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MENT_2000
{
    public partial class Form1 : Form
    {
        List<Cross> Points = new List<Cross>();
        bool klik = true;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);
            }
            else if (radioButton2.Checked)
            {
                if (klik)
                {
                    this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y) + " четный";
                }
                else
                {
                    this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y) + " нечетный";
                }
                klik = !klik;
            }
            Points.Add(new Cross(e.Location));
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Cross count in this.Points)
            {
                count.ReDraw(e.Graphics);
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            klik = true;
        }
    }


    public class Cross
    {
        Point a;

        Pen p = new Pen(Color.Black);

        public Cross(Point a)
        {
            this.a = a;
        }

        public void ReDraw(Graphics g)
        {
            g.DrawLine(p, a.X - 2, a.Y - 2, a.X + 2, a.Y + 2);
            g.DrawLine(p, a.X - 2, a.Y + 2, a.X + 2, a.Y - 2);
        }
    }
    
}
