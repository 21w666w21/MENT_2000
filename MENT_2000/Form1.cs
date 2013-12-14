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
        string filename = null;


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
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;

                StreamWriter sw = new StreamWriter(filename);
                foreach (Shape count in this.Shapes)
                {
                    count.SaveTo(sw);
                }
                sw.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;


                string textLine;
                StreamReader sr = new StreamReader(filename);
                Shapes.Clear();
                textLine = sr.ReadLine();
                while (textLine != null)
                {

                    if (textLine == "cross")
                    {
                        Shapes.Add(new Cross(sr));
                    }
                    if (textLine == "line")
                    {
                        Shapes.Add(new Line(sr));
                    }
                    textLine = sr.ReadLine();
                }
                sr.Close();
                Invalidate();
            }
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

        public Cross(StreamReader sr)
        {
            string textline = sr.ReadLine();
            string[] ln = textline.Split(' ');
            a.X = int.Parse(ln[0]);
            a.Y = int.Parse(ln[1]);
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

        public Line(StreamReader sr)
        {
            string textline = sr.ReadLine();
            string[] ln = textline.Split(' ');
            s.X = int.Parse(ln[0]);
            s.Y = int.Parse(ln[1]);

            textline = sr.ReadLine();
            ln = textline.Split(' ');
            f.X = int.Parse(ln[0]);
            f.Y = int.Parse(ln[1]);
        }

        public override void ReDraw(Graphics g)
        {
            g.DrawLine(w, s, f);
        }

        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("line");
            sw.WriteLine(Convert.ToString(s.X) + " " + Convert.ToString(s.Y));
            sw.WriteLine(Convert.ToString(f.X) + " " + Convert.ToString(f.Y));
        }
    }
    
}
