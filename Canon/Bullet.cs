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

        public double Sx = 0;
        public double Sy = 0;

        private double AngleGrade;
        private double Velocitie;
        private double TimerTicks;

        public Bullet(ShootingRange schietbaan)
        {
            Schietbaan = schietbaan;
        }

        public DispatcherTimer Launch(double angleGrade, double initialVelocitie)
        {
            AngleGrade = angleGrade;
            Velocitie = initialVelocitie;
            TimerTicks = 0;
            Initialise();
            Timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(1000/60) // 30 fps?
            };
            Timer.Tick += Tick;
            return Timer;

            void Tick(object sender, EventArgs args)
            {
                // billy goes brrrrr
                TimerTicks++;
                QuickMafs();
                if (CheckCollision())
                {
                    Timer.Stop();
                    if (Sx < Schietbaan.WorldWidth)
                        Sy = 0;
                    EraseBullet();
                    return;
                }
                UpdateBullet();
            }
        }

        private void QuickMafs()
        {
            //Sx = 0; // horizontaal afgelegde afstand (in meter)
            //Sy = 0; // verticaal afgelegde afstand (in meter)
            //double v = 1; // snelheid (m/s) for billy
            double a = AngleGrade * Math.PI / 180; // Angle (radialen)
            double g = 9.81; // valversnelling = 9,81 m/s2
            double t = (TimerTicks * Timer.Interval.TotalMilliseconds) / 360;
            Sx = Velocitie * Math.Cos(a) * t;
            Sy = Velocitie * Math.Sin(a) * t - (g * 0.5) * (t * t);
            Sy += 8; // ground offset
            BulletLocationX = (int)Sx;
            BulletLocationY = (int)Sy;
        }
        private bool CheckCollision()
        {
            // De kogel mag hoger vliegen dan de hoogte van de wereld om dan vervolgens terug in de “wereld” te vallen
            return BulletLocationX < 0 /* Left */ || BulletLocationX > Schietbaan.WorldWidth /* Right */ || BulletLocationY < 0 /* Bottom */;
        }

        private void UpdateBullet()
        {
            BulletBil.Margin = new Thickness(Schietbaan.CalcPixelX(BulletLocationX - BillyRadius), Schietbaan.CalcPixelY(BulletLocationY + BillyRadius), 0, 0);
        }

        public void EraseBullet()
        {
            Schietbaan.World.Children.Remove(BulletBil);
        }

        private void Initialise()
        {
            BulletBil = new Ellipse(); // create Billy
            BulletBil.Margin = new Thickness(Schietbaan.CalcPixelX(-(BillyRadius*2)), Schietbaan.CalcPixelY(8), 0, 0);
            BulletBil.Stroke = new SolidColorBrush(Colors.Red);
            BulletBil.StrokeThickness = BillyRadius;
            BulletBil.Height = BillyRadius * 2;
            BulletBil.Width = BillyRadius * 2;
            Schietbaan.World.Children.Add(BulletBil);
            Schietbaan.ActiveBullet = this;
        }

    }
}
