using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Canon
{
    public class ShootingRange
    {
        public int WorldWidth;
        public int WorldHeight;
        public Canvas World;
        public Bullet ActiveBullet;

        public ShootingRange(Canvas canvas, int worldWidth = 300, int wordHeight = 120)
        {
            World = canvas;
            WorldWidth = worldWidth;
            WorldHeight = wordHeight;
        }

        public void Initialise()
        {
            World.Children.Clear();
            DrawMetricLines();
        }

        private void DrawMetricLines()
        {
            // draw lines
            var lineColor = new SolidColorBrush(Colors.Black); // dont shoot them plz
            for (int i = 1; i < WorldWidth / 10; i++)
            {
                int lineHeight = 5;
                if(i % 10 == 0)
                {
                    //lineHeight = 8;
                    Label label = new Label();
                    string content = $"{10*i}";
                    int labelWith = content.Length * 11;

                    label.Content = content;
                    label.Margin = new Thickness()
                    {
                        Left = CalcPixelX((10 * i) - (labelWith / 2)),
                        Right = CalcPixelX((10 * i) + (labelWith / 2)),
                        Top = CalcPixelY(lineHeight + 8)
                    };
                    World.Children.Add(label);
                }
                Line line = new Line() { Y1 = CalcPixelY(0), Y2 = CalcPixelY(lineHeight), SnapsToDevicePixels = true };
                line.X1 = CalcPixelX(10 * i);
                line.X2 = line.X1;
                line.Stroke = lineColor;
                World.Children.Add(line);
            }
        }

        #region HELPERS
        public int CalcPixelX(int width)
        {
            double f = (World.Width / WorldWidth);
            return (int)(width * f);
        }

        public int CalcPixelY(int height)
        {
            double f = (World.Height / WorldHeight);
            return (int)((WorldHeight - height) * f);
        }

        public Point WorldToScreen(Point worldP)
        {
            return new Point()
            {
                X = CalcPixelX((int)worldP.X),
                Y = CalcPixelY((int)worldP.Y)
            };
        }
        #endregion

    }
}
