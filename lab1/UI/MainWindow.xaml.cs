using SkiaSharp;
using System.Windows;
using lab1.network;
using System;
using System.IO;
using System.Globalization;
using System.Windows.Controls;
using System.Collections.Generic;

namespace UI
{
    public partial class MainWindow : Window
    {
        SKPaint red = new SKPaint
        {
            Color = SKColors.Red,
            StrokeWidth = 1.0f,
            IsAntialias = true
        };

        SKPaint blue = new SKPaint
        {
            Color = SKColors.Blue,
            StrokeWidth = 1.0f,
            IsAntialias = true
        };


        SKPaint black = new SKPaint
        {
            Color = SKColors.Black,
            StrokeWidth = 3.0f,
            TextSize = 30,
            IsAntialias = true
        };

        Perceptron p = new Perceptron(3, 17, 3);
        Random rnd;
        LinkedList<SKPoint> red_points = new LinkedList<SKPoint>();
        LinkedList<SKPoint> blue_points = new LinkedList<SKPoint>();

        double[][] red_set_top;
        double[][] red_set_bot;
        double[][] blue_set;
        double[] ideal_red_top = new double[] { 1.0d, 0, 1.0d };
        double[] ideal_red_bot = new double[] { 0, 0, 1.0d };
        double[] ideal_blue = new double[] { 0, 1.0d, 0 };
        int choice, elem;
        bool enabled = false;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            rnd = new Random(DateTime.Now.Millisecond);
        }

        private void surface_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            if (!enabled)
            {
                return;
            }
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();
            float width = e.Info.Width;
            float height = e.Info.Height;

            // Draw axis
            canvas.DrawColor(SKColors.LightGray);
            canvas.DrawLine(0, 0, 0, height, black);
            canvas.DrawLine(0, height - 1.5f, width, height - 1.5f, black);
            canvas.DrawText("X2", 28.0f, 35.0f, black);
            canvas.DrawText("X1", width - 45.0f, height - 35.0f, black);


            // Draw dots
            foreach (SKPoint pt in red_points)
            {
                canvas.DrawCircle(pt, 4, red);
            }

            foreach (SKPoint pt in blue_points)
            {
                canvas.DrawCircle(pt, 4, blue);
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            enabled = false;
            int iter;
            if (!int.TryParse(iter_input.Text, out iter))
            {
                iter = 50000;
            }

            for (int i = 0; i < iter; i++)
            {
                choice = rnd.Next(3);

                switch (choice)
                {
                    case 0:
                        {
                            elem = rnd.Next(red_set_top.Length);
                            p.Backpropagation(ref red_set_top[elem], ref ideal_red_top);
                            break;
                        }
                    case 1:
                        {
                            elem = rnd.Next(blue_set.Length);
                            p.Backpropagation(ref blue_set[elem], ref ideal_blue);
                            break;
                        }
                    case 2:
                        {
                            elem = rnd.Next(red_set_bot.Length);
                            p.Backpropagation(ref red_set_bot[elem], ref ideal_red_bot);
                            break;
                        }
                }
            }
            (sender as Button).IsEnabled = true;
            enabled = true;
            surface.InvalidateVisual();
        }





        private void surface_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!enabled)
            {
                return;
            }

            Point pt = e.GetPosition(sender as SkiaSharp.Views.WPF.SKElement);
            double[] tmpData = new double[] { 1.0d, pt.X / (surface.ActualWidth), 1.0f - pt.Y / (surface.ActualHeight) };
            double[] answer = p.CountResult(ref tmpData);

            if (answer[1] > answer[0] && answer[1] > answer[2])
            {
                blue_points.AddLast(new SKPoint((float)pt.X, (float)pt.Y));
            }
            else
            {
                red_points.AddLast(new SKPoint((float)pt.X, (float)pt.Y));
            }

            surface.InvalidateVisual();
        }






        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            enabled = false;
            p.ResetWeights();
            enabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Init();
        }

        private void Init()
        {
            string[] lines = File.ReadAllLines("./red_set_top.txt");
            red_set_top = new double[lines.Length][];
            string[] nums;

            for (int i = 0; i < lines.Length; i++)
            {
                nums = lines[i].Split(' ');
                red_set_top[i] = new double[]
                {
                    1.0d,
                    double.Parse(nums[0], CultureInfo.InvariantCulture),
                    double.Parse(nums[1], CultureInfo.InvariantCulture)
                };
            }




            lines = File.ReadAllLines("./red_set_bot.txt");
            red_set_bot = new double[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                nums = lines[i].Split(' ');
                red_set_bot[i] = new double[]
                {
                    1.0d,
                    double.Parse(nums[0], CultureInfo.InvariantCulture),
                    double.Parse(nums[1], CultureInfo.InvariantCulture)
                };
            }





            lines = File.ReadAllLines("./blue_set.txt");
            blue_set = new double[lines.Length][];

            for (int i = 0; i < blue_set.Length; i++)
            {
                nums = lines[i].Split(' ');
                blue_set[i] = new double[]
                {
                    1.0d,
                    double.Parse(nums[0], CultureInfo.InvariantCulture),
                    double.Parse(nums[1], CultureInfo.InvariantCulture)
                };
            }



            double w = surface.ActualWidth;
            double h = surface.ActualHeight;
            foreach (double[] tmp in red_set_bot)
            {
                red_points.AddLast(new SKPoint((float)(tmp[1] * w), (float)(h - tmp[2] * h)));
            }

            foreach (double[] tmp in red_set_top)
            {
                red_points.AddLast(new SKPoint((float)(tmp[1] * w), (float)(h - tmp[2] * h)));
            }

            foreach (double[] tmp in blue_set)
            {
                blue_points.AddLast(new SKPoint((float)(tmp[1] * w), (float)(h - tmp[2] * h)));
            }
        }
    }
}
