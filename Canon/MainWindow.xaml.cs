using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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

        public MainWindow()
        {
            InitializeComponent();
            Schietbaan = new ShootingRange(paperCanvas);
            Schietbaan.Initialise();
        }

        //private double distanceMeter = 0;
        //public double DistanceMeter
        //{
        //    get { return distanceMeter; }
        //    set { distanceMeter = value; }
        //}

        //private double distanceHeight = 0;
        //public double DistanceHeight
        //{
        //    get { return distanceHeight; }
        //    set { distanceHeight = value; }
        //}

        //private double speedValue = 0;
        //public double SpeedValue
        //{
        //    get { return speedValue; }
        //    set { speedValue = value; }
        //}

        //private double angleValue;
        //public double AngleValue
        //{
        //    get { return angleValue; }
        //    set 
        //    {
        //        angleValue = value;
        //    }
        //}

        private void btnShoot_Click(object sender, RoutedEventArgs e)
        {
            Schietbaan.Shoot();
            // TODO: callback
        }
    }
}
