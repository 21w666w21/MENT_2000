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


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);
            Points.Add(new Cross(e.X, e.Y));
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Cross count in this.Points)
            {
                count.ReDraw(e.Graphics);
            }
        }
    }


    public class Cross
    {
        int x, y;

        Pen p = new Pen(Color.Black);

        public Cross(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void ReDraw(Graphics a)
        {
            a.DrawLine(p, this.x - 2, this.y - 2, this.x + 2, this.y + 2);
            a.DrawLine(p, this.x - 2, this.y + 2, this.x + 2, this.y - 2);
        }
    }
    
}
