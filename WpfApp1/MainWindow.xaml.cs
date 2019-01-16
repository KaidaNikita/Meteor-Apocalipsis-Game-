using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(Timer_tick);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();

        }
        private void Timer_tick(object sender, EventArgs e)
        {
            int rnd_x = rnd.Next(5, 70);
            Ellipse el = new Ellipse()
            {
                Width = rnd_x,
                Height= rnd_x
            };
            el.Fill = Brushes.Green;
            Cnvs.Children.Add(el);
            Canvas.SetTop(el, rnd.Next(20, 500));
            Canvas.SetLeft(el,rnd.Next(20, 500));
        }
    }
}
