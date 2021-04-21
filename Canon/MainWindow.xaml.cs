using System;
using System.Collections.Generic;
using System.Timers;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShootingRange Schietbaan;
        private DispatcherTimer BulletTimer;

        public MainWindow()
        {
            InitializeComponent();
            Schietbaan = new ShootingRange(paperCanvas);
            Schietbaan.Initialise();
        }

        private void btnShoot_Click(object sender, RoutedEventArgs e)
        {
            Bullet billy = new Bullet(Schietbaan);
            BulletTimer = billy.Launch();
            BulletTimer.Tick += UpdateWPFView; // hook WPF cleanup
            BulletTimer.Start();
            sldSpeed.IsEnabled = false;
            sldAngle.IsEnabled = false;
            btnShoot.IsEnabled = false;
        }

        private void UpdateWPFView(object sender, EventArgs e)
        {
            // TODO: update WPF UI
            if (!BulletTimer.IsEnabled)
                ShotsFired();

        }

        private void ShotsFired()
        {
            // TODO: unlock WPF UI
            sldSpeed.IsEnabled = true;
            sldAngle.IsEnabled = true;
            btnShoot.IsEnabled = true;
        }
    }
}
