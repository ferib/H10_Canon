using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;

namespace Canon
{
    public class Bullet
    {
        private const int BillyRadius = 5; // billy thicc
        private ShootingRange Schietbaan;
        private int BulletLocationX = 0;
        private int BulletLocationY = 0;
        private Ellipse BulletBil; // from Mario ;)

        private DispatcherTimer Timer;

        public Bullet(ShootingRange schietbaan)
        {
            Schietbaan = schietbaan;
        }

        public DispatcherTimer Launch()
        {
            Initialise();
            Timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            Timer.Tick += Tick;
            return Timer;

            void Tick(object sender, EventArgs args)
            {
                // billy goes brrrrr
                BulletLocationX += 3;
                BulletLocationY += 1;
                //callback();
                if (CheckCollision())
                {
                    Timer.Stop();
                    return;
                }
                UpdateBullet();
            }
        }

        private void QuickMafs(double angleGrade)
        {
            double Sx = 0; // horizontaal afgelegde afstand (in meter)
            double Sy = 0; // verticaal afgelegde afstand (in meter)
            double v = 1; // snelheid (m/s) for billy
            double a = 10; // Angle (radialen)
            double t = 0; // time
            double g = 0; // valversnelling = 9,81 m/s2

            Sx = v * Math.Cos(a) * t;
            Sy = v * Math.Sin(a) * t - (g * 0.5) * (t * t);
        }
        private bool CheckCollision()
        {
            // De kogel mag hoger vliegen dan de hoogte van de wereld om dan vervolgens terug in de “wereld” te vallen
            return BulletLocationX < 0 /* Left */ || BulletLocationX > Schietbaan.WorldWidth /* Right */ || BulletLocationY > Schietbaan.WorldHeight /* Bottom */;
        }

        private void UpdateBullet()
        {
            BulletBil.Margin = new Thickness(Schietbaan.CalcPixelX(BulletLocationX - BillyRadius), Schietbaan.CalcPixelY(BulletLocationY + BillyRadius), 0, 0);
        }

        private void EraseBullet()
        {
            Schietbaan.World.Children.Remove(BulletBil);
        }

        private void Initialise()
        {
            BulletBil = new Ellipse(); // create Billy
            BulletBil.Margin = new Thickness(Schietbaan.CalcPixelX(0), Schietbaan.CalcPixelY(8), 0, 0);
            BulletBil.Stroke = new SolidColorBrush(Colors.Red);
            BulletBil.StrokeThickness = BillyRadius;
            BulletBil.Height = BillyRadius * 2;
            BulletBil.Width = BillyRadius * 2;
            Schietbaan.World.Children.Add(BulletBil);
        }

    }
}
