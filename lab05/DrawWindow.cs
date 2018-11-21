using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace lab05
{
    public partial class DrawWindow : Form
    {
        private Factory.IAbstractFactory factoryCircle;
        private Factory.IAbstractFactory factoryRectangle;
        private bool ispressed = false;
        private Color currentColor = Color.Black;
        private int size = 5;
        public string drawType;
        Graphics g;
        Point prevPoint;
        Point currentPoint;
        public bool iscircle;
        Bitmap bitmap;

        

        public DrawWindow()
        {
            InitializeComponent();
            //g = DrawBox.CreateGraphics();

            bitmap = new Bitmap(DrawBox.Width, DrawBox.Height);
            DrawBox.Image = bitmap;
            g = Graphics.FromImage(DrawBox.Image);

        }

        private void DrawWindow_Load(object sender, EventArgs e)
        {
            factoryCircle = new Factory.ConcreteFactoryCircle();
            factoryRectangle = new Factory.ConcreteFactoryRectangle();

            MessageBox.Show(drawType);
            if (drawType == "Circle")
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, 231, 231);
                Region region = new Region(path);
                DrawBox.Region = region;

                g.SetClip(region, System.Drawing.Drawing2D.CombineMode.Replace);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.White);
            }
        }

        private void DrawLine(Factory.IAbstractFactory factory)
        {
            Brush.IBrush brush = factory.CreateBrush();
            brush.SetGraph(g);
            brush.SetColor(currentColor);
            brush.SetSize(size);
            brush.Draw(g, prevPoint, currentPoint);
            DrawBox.Invalidate();
        }

        private void DrawBox_MouseDown(object sender, MouseEventArgs e)
        {
            ispressed = true;
            var parent = this.MdiParent as MainWindow;
            currentColor = parent.CurrentColor();
            size = parent.CurrentSize();
            currentPoint = e.Location;
        }

        private void DrawBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (ispressed)
            {
                prevPoint = currentPoint;
                currentPoint = e.Location;
                switch (drawType)
                {
                    case "Circle":
                        DrawLine(factoryCircle);
                        break;
                    case "Rectangle":
                        DrawLine(factoryRectangle);
                        break;
                }
            }
        }

        private void DrawBox_MouseUp(object sender, MouseEventArgs e)
        {
            ispressed = false;
        }

        private void DrawWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image files(*.jpg)|*.jpg";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        bitmap.Save(sfd.FileName);
                        
                    }
                    catch
                    {
                        MessageBox.Show("Cannot save the image", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
