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
        DispatcherTimer met_tr = new DispatcherTimer();
        int hp = 100;
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(Timer_tick);
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Start();

            met_tr.Tick += new EventHandler(Meteor_tick);
            met_tr.Interval = new TimeSpan(0, 0, 0, 0,5); 
            met_tr.Start();

            label.Content = "HP: " + hp.ToString();
        }
        private void Timer_tick(object sender, EventArgs e)
        {
            int rnd_x = rnd.Next(5, 70);
            Ellipse el = new Ellipse()
            {
                Width = rnd_x,
                Height= rnd_x
            };
            el.Fill =Brushes.Green;
            Cnvs.Children.Add(el);
            el.MouseLeftButtonDown += Meteor_Click;
            el.Tag = "Meteor";
            Canvas.SetTop(el, rnd.Next(20, 500));
            Canvas.SetLeft(el,rnd.Next(20, 500));
        }

        private void Meteor_tick(object sender, EventArgs e)
        {

            double y = Canvas.GetTop(earth);
            double x = Canvas.GetLeft(earth);

            for (int i = 0; i < Cnvs.Children.Count; i++)
            {
                if (Cnvs.Children[i] is Ellipse&& (Cnvs.Children[i] as Ellipse).Tag=="Meteor")
                {
                   
                  double  x_m = Canvas.GetLeft(Cnvs.Children[i]);
                    double y_m = Canvas.GetTop(Cnvs.Children[i]);
                  //  Cnvs.Children.Remove(Cnvs.Children[i]);
                    if (x_m < x)
                    {
                        Canvas.SetLeft(Cnvs.Children[i], x_m += 1);
                    }
                    if (x_m > x)
                    {
                        Canvas.SetLeft(Cnvs.Children[i], x_m -= 1);
                    }

                    if (y_m < y)
                    {
                        Canvas.SetTop(Cnvs.Children[i], y_m+=1);
                    }
                    if (y_m > y)
                    {
                        Canvas.SetTop(Cnvs.Children[i], y_m -= 1);
                    }
                }
            }

        }

        private void Meteor_Click(object sender, MouseButtonEventArgs e)
        {
            hp--;
            Cnvs.Children.Remove((sender as Ellipse));
            label.Content = "HP: " + hp.ToString();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
