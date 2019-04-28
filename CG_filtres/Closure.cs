using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace KG
{
    class Closure : Filters
    {
        protected override Color calculaterNewPixelColor(Bitmap sourceImage, int i, int j)
        {
            return Color.FromArgb(0, 0, 0);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            Filters filter1 = new Buildup();
            Filters filter2 = new Erosion();

            resultImage = filter1.processImage(sourceImage, worker);
            resultImage = filter2.processImage(resultImage, worker);

            return resultImage;
        }
    }
}
