using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG
{
    public partial class Form1 : Form
    {
        Bitmap image;

        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage + 1;
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void averageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Average();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Sobel();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void наращиваниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Buildup();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void эрозияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Erosion();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void замыканиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Closure();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размыканиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Disjunction();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void бинаризацияПоПорогуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BinarFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
