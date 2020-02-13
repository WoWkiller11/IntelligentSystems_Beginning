using System;
using System.IO;
using System.Globalization;

namespace lab1.network
{
    class Program
    {
        static void Main(string[] args)
        {
            Perceptron p = new Perceptron(3, 17, 2);
            Random rnd = new Random(DateTime.Now.Millisecond);
            double[][] red_set;
            double[][] blue_set;

            string[] lines = File.ReadAllLines("./red_set.txt");
            red_set = new double[lines.Length][];
            string[] nums;

            for(int i = 0; i < lines.Length; i++)
            {
                nums = lines[i].Split(", ");
                red_set[i] = new double[] 
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
                nums = lines[i].Split(", ");
                blue_set[i] = new double[]
                {
                    1.0d,
                    double.Parse(nums[0], CultureInfo.InvariantCulture),
                    double.Parse(nums[1], CultureInfo.InvariantCulture)
                };
            }

            double[] ideal_red = new double[] { 1.0d, 0 };
            double[] ideal_blue = new double[] { 0, 1.0d };
            int choice, elem;


            for (int i = 0; i < 110000; i++)
            {
                choice = rnd.Next(3);

                if (choice > 1)
                {
                    elem = rnd.Next(blue_set.Length);
                    p.Backpropagation(ref blue_set[elem], ref ideal_blue);
                }
                else
                {
                    elem = rnd.Next(red_set.Length);
                    p.Backpropagation(ref red_set[elem], ref ideal_red);
                }
            }

            double[] test = new double[] { 1.0d, 0.21111d, 0.6112d };
            double[] ans = p.CountResult(ref red_set[33]);
            Console.WriteLine(ans[0] + "    " + ans[1]);
            ans = p.CountResult(ref red_set[28]);
            Console.WriteLine(ans[0] + "    " + ans[1]);

            ans = p.CountResult(ref test);
            Console.WriteLine(ans[0] + "    " + ans[1]);
        }
    }
}
