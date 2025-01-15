namespace wfaGraphicSinCos
{
    internal class MyGraphic
    {
        public int CountWave { get; }

        private const int DOT = 4;
        private readonly Bitmap b;
        private readonly Graphics g;
        private readonly int grShiftY;
        private readonly double grWidthPI;

        public int Width { get => b.Width; }
        public int Height { get => b.Height; }
        public Bitmap Picture { get => b; }

        public MyGraphic(int width, int height, int countWave) 
        {
            CountWave = countWave;

            b = new Bitmap(width, height);
            g = Graphics.FromImage(b);

            grShiftY = Height / 2;
            grWidthPI = Math.PI / (Width - 1);

        }

        public void DrawAxes(Color color)
        {
            // Рисуем ось X
            g.DrawLine(new Pen(color), 0, grShiftY, Width, grShiftY);
            // Рисуем ось Y
            g.DrawLine(new Pen(color), 0, 0, 0, Height);
        }

        public void DrawSin(Color color)
        {
            for (double x = 0; x < Width; x++)
            {
                double y = grShiftY + -Math.Sin(x * CountWave * grWidthPI) * grShiftY;
                g.FillEllipse(new SolidBrush(color), (int)(x - DOT / 2), (int)(y - DOT / 2), DOT, DOT);
            }
        }

        public void DrawCos(Color color)
        {
            for (double x = 0; x < Width; x++)
            {
                double y = grShiftY + -Math.Cos(x * CountWave * grWidthPI) * grShiftY;
                g.FillEllipse(new SolidBrush(color), (int)(x - DOT / 2), (int)(y - DOT / 2), DOT, DOT);
            }
        }

        public void DrawTan(Color color)
        {
            for (double x = 0; x < Width; x++)
            {
                double y = grShiftY + -Math.Tan(x * CountWave * grWidthPI) * grShiftY;
                if (y > 0 && y < Height)
                    g.FillEllipse(new SolidBrush(color), (int)(x - DOT / 2), (int)(y - DOT / 2), DOT, DOT);
            }
        }



    }
}