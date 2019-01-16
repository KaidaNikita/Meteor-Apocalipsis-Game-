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
        int coins = 0;
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(Timer_tick);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();

            met_tr.Tick += new EventHandler(Meteor_tick);
            met_tr.Interval = new TimeSpan(0, 0, 0, 0, 5);
            met_tr.Start();

            label.Content = "HP: " + hp.ToString();
        }
        private void Timer_tick(object sender, EventArgs e)
        {
            int r = rnd.Next(0, 2);
            int rnd_x = rnd.Next(5, 70);
            int hp_of_mt = 1;
            Ellipse el = new Ellipse()
            {
                Width = rnd_x,
                Height = rnd_x
            };

            if (r == 0)
            {
                int x = rnd.Next(0, 200);
                int y = rnd.Next(0, 200);
                Canvas.SetTop(el, x);
                Canvas.SetLeft(el, y);
            }
            else
            {
                int x = rnd.Next(500, 1000);
                int y = rnd.Next(500, 1000);
                Canvas.SetTop(el, x);
                Canvas.SetLeft(el, y);
            }

            if (rnd_x < 15)
            {
                hp_of_mt = 1;
            }
            else
            if (rnd_x > 15 && rnd_x < 25)
            {
                hp_of_mt = 2;
            }
            else
            if (rnd_x > 25 && rnd_x < 35)
            {
                hp_of_mt = 3;
            }
            else
            if (rnd_x > 35 && rnd_x < 45)
            {
                hp_of_mt = 4;
            }
            else
            if (rnd_x > 45)
            {
                hp_of_mt = 5;
            }

            ImageBrush temp = (ImageBrush)this.FindResource("meteor");
            temp.Stretch = Stretch.Uniform;
            el.Fill = temp;

            el.MouseLeftButtonDown += Meteor_Click;
            el.Tag = "Meteor";
            if (true)
            {

            }
            Cnvs.Children.Add(el);
        }

        bool IsCrush()
        {
            bool temp = false;
            foreach (var el in Cnvs.Children)
            {
                if (el is Ellipse)
                    if ((el as Ellipse).Tag == "Meteor")
                        try
                        {
                            Ellipse my_eliplse = (Ellipse)el;
                            GeneralTransform t1 = my_eliplse.TransformToVisual(this);
                            GeneralTransform t2 = earth.TransformToVisual(this);
                            Rect r1 = t1.TransformBounds(new Rect() { X = 0, Y = 0, Width = my_eliplse.ActualWidth, Height = my_eliplse.ActualHeight });
                            Rect r2 = t2.TransformBounds(new Rect() { X = 0, Y = 0, Width = earth.ActualWidth, Height = earth.ActualHeight });
                            bool result = r1.IntersectsWith(r2);
                            if (result)
                            {
                                return true;
                            }
                        }

                        catch { }
            }
            return temp;
        }

        public void Meteor_tick(object sender, EventArgs e)
        {

            double y = earth.Width / 2 + Canvas.GetTop(earth);
            double x = earth.Width / 2 + Canvas.GetLeft(earth);

            for (int i = 0; i < Cnvs.Children.Count; i++)
            {
                if (Cnvs.Children[i] is Ellipse && (Cnvs.Children[i] as Ellipse).Tag == "Meteor")
                {

                    double x_m = Canvas.GetLeft(Cnvs.Children[i]);
                    double y_m = Canvas.GetTop(Cnvs.Children[i]);
                    int v = 1;


                    if (x_m < x)
                    {
                        Canvas.SetLeft(Cnvs.Children[i], x_m += v);
                    }
                    if (x_m > x)
                    {
                        Canvas.SetLeft(Cnvs.Children[i], x_m -= v);
                    }

                    if (y_m < y)
                    {
                        Canvas.SetTop(Cnvs.Children[i], y_m += v);
                    }
                    if (y_m > y)
                    {
                        Canvas.SetTop(Cnvs.Children[i], y_m -= v);
                    }
                    if (IsCrush())
                    {
                        hp--;
                        label.Content = "HP: " + hp.ToString();
                        Cnvs.Children.Remove(Cnvs.Children[i]);
                    }
                }
            }

        }

        private void Meteor_Click(object sender, MouseButtonEventArgs e)
        {
            coins++;
            Cnvs.Children.Remove((sender as Ellipse));
            label1.Content = "Coins: " + coins.ToString();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            coins++;
            label1.Content = "Coins: " + coins.ToString();
        }
    }
}
