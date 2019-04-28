using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG
{
    class Erosion : Filters
    {
        protected override Color calculaterNewPixelColor(Bitmap sourceImage, int i, int j)
        {
            int X = 1;
            int Y = 1;

            int resultR = 0;
            int resultG = 0;
            int resultB = 0;

            int p = 0;
            int intence = 0;

            for (int l = -Y; l <= Y; l++)
            {
                for (int k = -X; k <= X; k++)
                {
                    int idX = Clamp(i + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(j + l, 0, sourceImage.Height - 1);

                    Color nColor = sourceImage.GetPixel(idX, idY);

                    intence = Clamp((nColor.R + nColor.B + nColor.G) / 3, 0, 255);

                    if (intence < 125)
                    {
                        resultR += nColor.R;
                        resultG += nColor.G;
                        resultB += nColor.B;

                        p++;
                    }
                }
            }

            if (p > 0)
            {
                return Color.FromArgb(Clamp(resultR / p, 0, 255), Clamp(resultG / p, 0, 255), Clamp(resultB / p, 0, 255));
            }
            else
            {
                //return sourceImage.GetPixel(X, Y);
                return Color.FromArgb(255, 255, 255);
            }
        }
    }
}
