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
        private const int WorldWidth = 300;
        private const int WorldHeight = 120;
        private const int BillyRadius = 3; // billy thicc

        private int BulletLocationX = -1;
        private int BulletLocationY = -1;
        private Ellipse BulletBil; // from Mario ;)

        private DispatcherTimer Timer;
        private Canvas World;
        private Action Callback;

        public ShootingRange(Canvas canvas)
        {
            World = canvas;
        }

        public void Initialise()
        {
            World.Children.Clear();
            DrawMetricLines();
            InitBullet();
            BulletLocationX = 0;
            BulletLocationY = WorldHeight;
        }


        private int CalcPixelX(int height)
        {
            double f = (World.Height / WorldHeight);
            return (int)(height * f);
        }

        private int CalcPixelY(int width)
        {
            double f = (World.Width / WorldWidth);
            return (int)(width * f);
        }

        public void Shoot()
        {
            Timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(50),
            };
            Timer.Tick += Tick;
            Timer.Start();

            void Tick(object sender, EventArgs args)
            {
                BulletLocationX += 3;
                BulletLocationY -= 1;
                //callback();
                if (CheckCollision())
                {
                    Timer.Stop();
                    return;
                }
                UpdateBullet();
            }
        }

        private bool CheckCollision()
        {
            // De kogel mag hoger vliegen dan de hoogte van de wereld om dan vervolgens terug in de “wereld” te vallen
            return BulletLocationX < 0 /* Left */ || BulletLocationX > WorldWidth /* Right */ || BulletLocationY > WorldHeight /* Bottom */;
        }

        private void UpdateBullet()
        {
            BulletBil.Margin = new Thickness(CalcPixelX(BulletLocationX - BillyRadius), CalcPixelY(BulletLocationY + BillyRadius), 0, 0);
        }

        private void InitBullet()
        {
            BulletBil = new Ellipse(); // create Billy
            BulletBil.Margin = new Thickness(BulletLocationX, BulletLocationY, 0, 0);
            BulletBil.Stroke = new SolidColorBrush(Colors.Red);
            BulletBil.StrokeThickness = BillyRadius;
            BulletBil.Height = BillyRadius*2;
            BulletBil.Width = BillyRadius*2;
            World.Children.Add(BulletBil);
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
                    lineHeight = 8;
                    // TODO: replace with label
                }
                Line line = new Line() { Y1 = CalcPixelX(WorldHeight), Y2 = CalcPixelX(WorldHeight - lineHeight), SnapsToDevicePixels = true };
                line.X1 = CalcPixelY(10 * i);
                line.X2 = line.X1;
                line.Stroke = lineColor;
                World.Children.Add(line);
            }
        }
    }
}
