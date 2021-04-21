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

            sldSpeed.ValueChanged += SlidersUpdate;
            sldAngle.ValueChanged += SlidersUpdate;
        }

        private void btnShoot_Click(object sender, RoutedEventArgs e)
        {
            Bullet billy = new Bullet(Schietbaan);
            BulletTimer = billy.Launch(Convert.ToDouble(lblAngle.Content), Convert.ToDouble(lblSpeed.Content));
            BulletTimer.Tick += UpdateWPFView; // hook WPF cleanup
            BulletTimer.Start();
            EnableUI(false);
        }

        #region UI_Update
        private void UpdateWPFView(object sender, EventArgs e)
        {
            if (!BulletTimer.IsEnabled)
                ShotsFired();
            lblHeight.Content = Schietbaan.ActiveBullet.Sy.ToString("0.00") + " meter";
            lblDistance.Content = Schietbaan.ActiveBullet.Sx.ToString("0.00") + " meter";
        }

        private void ShotsFired()
        {
            EnableUI(true);
            //Schietbaan.ActiveBullet.EraseBullet(); // idk
        }

        private void EnableUI(bool status = true)
        {
            sldSpeed.IsEnabled = status;
            sldAngle.IsEnabled = status;
            btnShoot.IsEnabled = status;
        }

        private void SlidersUpdate(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblAngle.Content = sldAngle.Value;
            lblSpeed.Content = sldSpeed.Value;
        }

        #endregion
    }
}
