using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG
{
    class BinarFilter : Filters
    {
        protected override Color calculaterNewPixelColor(Bitmap sourceImage, int i, int j)
        {
            Color sourceColor = sourceImage.GetPixel(i, j);

            int intense = Clamp((sourceColor.R + sourceColor.B + sourceColor.G) / 3, 0, 255);

            if (intense < 128)
                intense = 0;
            else
                intense = 255;

            Color resultColor = Color.FromArgb(intense, intense, intense);

            return resultColor;
        }
    }
}
