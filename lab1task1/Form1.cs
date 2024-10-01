using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tyshyk
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox;
        private double c = 100; 
        private double p = 0;   

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(this.pictureBox);

            Button btnCase1 = new Button { Text = "Випадок 1: p = c² / 4", Dock = DockStyle.Top };
            btnCase1.Click += (s, e) => { p = Math.Pow(c, 2) / 4; DrawGraph(); };

            Button btnCase2 = new Button { Text = "Випадок 2: p < c² / 4", Dock = DockStyle.Top };
            btnCase2.Click += (s, e) => { p = Math.Pow(c, 2) / 8; DrawGraph(); };

            Button btnCase3 = new Button { Text = "Випадок 3: c² / 4 < p < c² / 2", Dock = DockStyle.Top };
            btnCase3.Click += (s, e) => { p = Math.Pow(c, 2) / 3; DrawGraph(); };

            Button btnCase4 = new Button { Text = "Випадок 4: p > c² / 4", Dock = DockStyle.Top };
            btnCase4.Click += (s, e) => { p = Math.Pow(c, 2) / 2 + 10; DrawGraph(); };

            this.Controls.Add(btnCase1);
            this.Controls.Add(btnCase2);
            this.Controls.Add(btnCase3);
            this.Controls.Add(btnCase4);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "Візуалізація множини точок";
        }

        private void DrawGraph()
        {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            double x1 = pictureBox.Width / 2 - c / 2;
            double x2 = pictureBox.Width / 2 + c / 2;
            double y = pictureBox.Height / 2;

            Pen axisPen = new Pen(Color.Black, 1);
            g.DrawLine(axisPen, 0, (int)y, pictureBox.Width, (int)y); 
            g.FillEllipse(Brushes.Blue, (int)x1 - 5, (int)y - 5, 10, 10); 
            g.FillEllipse(Brushes.Red, (int)x2 - 5, (int)y - 5, 10, 10);  

            Pen pointPen = new Pen(Color.Green, 2);

            for (double angle = 0; angle < 2 * Math.PI; angle += 0.01)
            {
                for (double r = 0; r < pictureBox.Width / 2; r += 1)
                {
                    double x = pictureBox.Width / 2 + r * Math.Cos(angle);
                    double yDraw = pictureBox.Height / 2 + r * Math.Sin(angle);

                    double d1 = Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(yDraw - y, 2));
                    double d2 = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(yDraw - y, 2));

                    if (Math.Abs(d1 * d2 - p) < 1) 
                    {
                        g.DrawEllipse(pointPen, (float)x, (float)yDraw, 1, 1);
                    }
                }
            }

            pictureBox.Image = bitmap;
            g.Dispose();
        }
    }
}
