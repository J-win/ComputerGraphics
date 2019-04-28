using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG
{
    class Average : Filters
    {
        protected override Color calculaterNewPixelColor(Bitmap sourceImage, int i, int j)
        {
            Color sourceColor = sourceImage.GetPixel(i, j);
            int grey = (sourceColor.R + sourceColor.G + sourceColor.B) / 3;
            Color resultColor = Color.FromArgb(grey, grey, grey);
            return resultColor;
        }
    }
}
