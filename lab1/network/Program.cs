using System;
using System.Collections.Generic;

namespace lab1.network
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example logic function (A ^ B) -> C
            /********************************************/
            Perceptron p = new Perceptron(3, 6, 1);
            Random rnd = new Random(DateTime.Now.Millisecond);

            double[][] right = new double[8][];
            right[0] = new double[] { 1 };
            right[1] = new double[] { 1 };
            right[2] = new double[] { 0 };
            right[3] = new double[] { 1 };
            right[4] = new double[] { 0 };
            right[5] = new double[] { 1 };
            right[6] = new double[] { 1 };
            right[7] = new double[] { 1 };


            double[][] input = new double[8][];
            input[0] = new double[] { 0, 0, 0 };
            input[1] = new double[] { 0, 0, 1 };
            input[2] = new double[] { 0, 1, 0 };
            input[3] = new double[] { 0, 1, 1 };
            input[4] = new double[] { 1, 0, 0 };
            input[5] = new double[] { 1, 0, 1 };
            input[6] = new double[] { 1, 1, 0 };
            input[7] = new double[] { 1, 1, 1 };


            double[] result;
            int index;

            for (int i = 0; i < 10000; i++)
            {
                index = rnd.Next(8);
                result = p.Backpropagation(ref input[index], ref right[index]);
            }


            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(p.CountResult(ref input[i])[0]);
            }
            /********************************************/
        }
    }
}
