using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_OpenGL
{
    public partial class Form1 : Form
    {
        GLGraphics glGraphics = new GLGraphics();

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glGraphics.Resize(glControl1.Width, glControl1.Height);
            Application.Idle += Application_Idle;
            int texID = glGraphics.LoadTexture("..\\..\\Sample.png");
            glGraphics.texturesIDs.Add(texID);

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            int i = 0;
            if (radioButton1.Checked)
                i = 1;
            else if (radioButton2.Checked)
                i = 2;
            else
                i = 3;

            glGraphics.Update(i);
            glControl1.SwapBuffers();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float widthCoef = (e.X - glControl1.Width * 0.5f) / (float)glControl1.Width;
            float heightCoef = (-e.Y + glControl1.Height * 0.5f) / (float)glControl1.Height;
            glGraphics.latitude = heightCoef * 180;
            glGraphics.longitude = widthCoef * 360;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
                glControl1.Refresh();
        }
    }
}
