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
       // List<Cross> Points = new List<Cross>();
        List<Line> Points = new List<Line>();
        bool klik = true;
        Point p1, p2;


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
                //Points.Add(new Cross(e.Location));

            }
            else if (radioButton2.Checked)
            {
                if (klik)
                {
                    this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y) + " первый";
                    p1 = e.Location;
                }
                else
                {
                    this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y) + " второй";
                    p2 = e.Location;
                    Points.Add(new Line(p1, p2));
                }
                klik = !klik;
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Line count in this.Points)
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
    public class Line
    {
        Point s, f;

        Pen w = new Pen(Color.Black);

        public Line(Point s, Point f)
        {
            this.s = s;
            this.f = f;
        }

        public void ReDraw(Graphics g)
        {
            g.DrawLine(w, s, f);
        }
    }
    
}
