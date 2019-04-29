using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Form1 : Form
    {
        /// <summary> Прямоугольник -- границы игрового поля. </summary>
        private Rectangle areaRect = new Rectangle(20, 20, 600, 600);

        /// <summary> Прямоугольник -- подставка для мячика. </summary>
        private Rectangle blockRect = new Rectangle(300, 595, 100, 22);

        /// <summary> Изображение -- футбольный мяч. </summary>
        private Bitmap ballImage = new Bitmap("..\\..\\football.gif");

        /// <summary> Текущее положение мячика. </summary>
        private Point ballPosition = new Point(300, 200);

        /// <summary> Текущее направление мячика. </summary>
        private Point ballVelocity = new Point(5, 4);

        /// <summary> Текущий кадр анимации футбольного мячика. </summary>
        private int ballFrame = 0;

        /// <summary> Шрифт для рисования надписей. </summary>
        private Font font = new Font("Arial", 48, FontStyle.Bold);

        /// <summary> Параметры рисования надписей. </summary>
        private StringFormat stringFormat = new StringFormat();

        public Form1()
        {
            InitializeComponent();

            // устанавливаем параметры рисования надписей (выравнивание по центру)
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        /// <summary> Отрисовываем фон игры. </summary>
        private void DrawBackground (Graphics graphics)
        {
            // заливаем игровое поле
            HatchBrush brush = new HatchBrush(HatchStyle.DottedDiamond, Color.LightGray, Color.White);
            graphics.FillRectangle(brush, areaRect);

            // рисуем его границу
            Pen pen = new Pen(Color.DarkGray, 2);
            graphics.DrawRectangle(pen, areaRect);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBackground(e.Graphics);
            DrawBlock(e.Graphics);
            PaintBall(e.Graphics);
        }

        /// <summary> Отрисовываем подставку. </summary>
        private void DrawBlock(Graphics graphics)
        {
            // заливаем подставку
            Brush brush = new LinearGradientBrush(blockRect,
                Color.LightBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            graphics.FillRectangle(brush, blockRect);

            // рисуем ее границу
            Pen pen = new Pen(Color.RoyalBlue, 2);
            graphics.DrawRectangle(pen, blockRect);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // если указатель мыши находится в игровом поле...
            if (e.X >= areaRect.Left)
            {
                if (e.X <= areaRect.Right - blockRect.Width)
                {
                    // ...устанавливаем новое положение подставки
                    blockRect.X = e.X;
                    //Refresh();
                }
            }
        }

        /// <summary> Возвращает отраженное направление для заданного направления и нормали. </summary>
        private Point Reflect(Point impactPoint, Point normal)
        {
            // вычисляем скалярное произведение
            double dot = normal.X * impactPoint.X + normal.Y * impactPoint.Y;

            // вычисляем отраженное направление
            return new Point((int)(impactPoint.X - 2.0 * dot * normal.X),
                (int)(impactPoint.Y - 2.0 * dot * normal.Y));
        }

        private void PaintBall(Graphics graphics)
        {
            // мяч улетел?
            if (!areaRect.Contains(ballPosition))
            {
                // отрисовываем надпись с помощью градиентной кисти
                LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle,
                    Color.Green, Color.Red, LinearGradientMode.BackwardDiagonal);
                graphics.DrawString("Вы проиграли!", font, brush, ClientRectangle, stringFormat);
                return;
            }
                // меняем положение мячика в соответствии с направлением движения
                ballPosition.X += ballVelocity.X;
                ballPosition.Y += ballVelocity.Y;

                // мяч долетел до правой стенки?
                if (ballPosition.X + ballImage.Width >= areaRect.Right)
                    ballVelocity = Reflect(ballVelocity, new Point(-1, 0));

                // мяч долетел до левой стенки?
                if (ballPosition.X <= areaRect.Left)
                    ballVelocity = Reflect(ballVelocity, new Point(1, 0));

                // мяч долетел до потолка?
                if (ballPosition.Y <= areaRect.Top)
                    ballVelocity = Reflect(ballVelocity, new Point(0, 1));

                // мяч долетел до подставки?
                if (ballPosition.Y + ballImage.Height >= blockRect.Top)
                {
                    // а подставка оказалась рядом?
                    if (ballPosition.X + ballImage.Width / 2 >= blockRect.Left)
                    {
                        if (ballPosition.X + ballImage.Width / 2 <= blockRect.Right)
                        {
                            ballVelocity = Reflect(ballVelocity, new Point(0, -1));
                        }
                    }
                }

                if (ballImage != null)
                {
                    // находим следующий кадр анимации (с учетом зацикливания)
                    ballFrame = (ballFrame + 1) % ballImage.GetFrameCount(FrameDimension.Time);

                    // устанавливаем новый кадр анимации
                    ballImage.SelectActiveFrame(FrameDimension.Time, ballFrame);

                    // отрисовываем изображение мячика
                    graphics.DrawImage(ballImage, ballPosition.X, ballPosition.Y, ballImage.Width, ballImage.Height);
                }
                else
                {
                    graphics.FillEllipse(new SolidBrush(Color.Red), ballPosition.X,
                        ballPosition.Y, ballImage.Width, ballImage.Height);
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
