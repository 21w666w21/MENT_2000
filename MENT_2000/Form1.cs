using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MENT_2000
{
    public partial class Form1 : Form
    {
        List<Shape> Shapes = new List<Shape>();
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
                Shapes.Add(new Cross(e.Location));
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
                    Shapes.Add(new Line(p1, p2));
                }
                klik = !klik;
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape count in this.Shapes)
            {
                count.ReDraw(e.Graphics);
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            klik = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("Save.txt");
            foreach (Shape count in this.Shapes)
            {
                count.SaveTo(sw);
            }
            sw.Close();
        }
    }
    public abstract class Shape
    {
        public abstract void ReDraw(Graphics g);
        public abstract void SaveTo(StreamWriter sw);
    }

    public class Cross : Shape
    {
        Point a;

        Pen p = new Pen(Color.Black);

        public Cross(Point a)
        {
            this.a = a;
        }

        public override void ReDraw(Graphics g)
        {
            g.DrawLine(p, a.X - 2, a.Y - 2, a.X + 2, a.Y + 2);
            g.DrawLine(p, a.X - 2, a.Y + 2, a.X + 2, a.Y - 2);

        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("cross");
            sw.WriteLine(Convert.ToString(a.X) + " " + Convert.ToString(a.Y));
        }
    }
    public class Line : Shape
    {
        Point s, f;

        Pen w = new Pen(Color.Black);

        public Line(Point s, Point f)
        {
            this.s = s;
            this.f = f;
        }

        public override void ReDraw(Graphics g)
        {
            g.DrawLine(w, s, f);
        }

        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("line");
            sw.WriteLine(Convert.ToString(s.X) + " " + Convert.ToString(f.Y));
            sw.WriteLine(Convert.ToString(s.X) + " " + Convert.ToString(f.Y));
        }
    }
    
}
