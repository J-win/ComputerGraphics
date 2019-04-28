using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG
{
    class MedianFilter : Filters
    {
        protected override Color calculaterNewPixelColor(Bitmap sourceImage, int i, int j)
        {
            int X = 1;
            int Y = 1;

            int[] resultR = new int[9];
            int[] resultG = new int[9];
            int[] resultB = new int[9];

            int p = 0;

            for (int l = -Y; l <= Y; l++)
            {
                for (int k = -X; k <= X; k++)
                {
                    int idX = Clamp(i + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(j + l, 0, sourceImage.Height - 1);

                    Color nColor = sourceImage.GetPixel(idX, idY);

                    resultR[p] = nColor.R;
                    resultG[p] = nColor.G;
                    resultB[p] = nColor.B;

                    p++;
                }
            }

            Array.Sort(resultR);
            Array.Sort(resultG);
            Array.Sort(resultB);

            return Color.FromArgb(resultR[5], resultG[5], resultB[5]);
        }
    }
}
