﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab05
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Factory.IAbstractFactory factoryCircle;
        private Factory.IAbstractFactory factoryRectangle;
        public Color currentColor = Color.Black;
        public int size = 5;
        
        public Color CurrentColor()
        {
            return currentColor;
        }

        public int CurrentSize()
        {
            return size;
        }

        

        private void InitCanvas(Factory.IAbstractFactory factory)
        {
            Canvas.ICanvas canvas = factory.CreateCanvas();
            DrawWindow dw = new DrawWindow();
            dw.drawType = canvas.GetCanvasType();
            dw.DrawBox = canvas.SetDrawBox(dw.DrawBox);
            dw.MdiParent = this;

            /*if (dw.drawType == "Circle")
            {
                dw.iscircle = true;
            }
            else
            {
                dw.iscircle = false;
            }*/

            dw.Show();
        }

        private void ButtonColor_Click(object sender, EventArgs e)
        {
            DialogResult D = ColorPickerDialog.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
            {
                currentColor = ColorPickerDialog.Color;
                ButtonColor.BackColor = currentColor;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SizeBar.Value = size;
            ButtonColor.BackColor = currentColor;
            factoryCircle = new Factory.ConcreteFactoryCircle();
            factoryRectangle = new Factory.ConcreteFactoryRectangle();
        }

        private void SizeBar_ValueChanged(object sender, EventArgs e)
        {
            size = SizeBar.Value;
        }

        private void прямоугольныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCanvas(factoryRectangle);
        }

        private void круглыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCanvas(factoryCircle);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            size = Convert.ToInt32(textBox1.Text);
        }
    }
}
