using System;

namespace lab1.network
{
    public class Perceptron
    {
        private Neuron[] InputLayer = null;
        private Neuron[] FirstHidden = null;
        private Neuron[] OutputLayer = null;

        private double Alpha = 1.0d;
        private double LearningRate = 0.5d;

        public Perceptron(int inputLayer, int firstHiddenLayer, int outputLayer)
        {
            this.InputLayer = new Neuron[inputLayer];
            this.FirstHidden = new Neuron[firstHiddenLayer];
            this.OutputLayer = new Neuron[outputLayer];

            int i, j;


            Random rnd = new Random(DateTime.Now.Millisecond);

            for (i = 0; i < FirstHidden.Length; i++)
            {
                FirstHidden[i].Weights = new double[InputLayer.Length];
                for (j = 0; j < InputLayer.Length; j++)
                {
                    FirstHidden[i].Weights[j] = rnd.NextDouble() * Math.Pow(-1, (rnd.Next(2) + 1));
                }
            }



            for (i = 0; i < OutputLayer.Length; i++)
            {
                OutputLayer[i].Weights = new double[FirstHidden.Length];
                for (j = 0; j < FirstHidden.Length; j++)
                {
                    OutputLayer[i].Weights[j] = rnd.NextDouble() * Math.Pow(-1, (rnd.Next(2) + 1));
                }
            }
        }


        public double[] CountResult(ref double[] input)
        {
            int i, j;
            for (i = 0; i < input.Length; i++)
                InputLayer[i].Output = input[i];
            

            for (i = 0; i < FirstHidden.Length; i++)
            {
                FirstHidden[i].Sum = 0;
                for (j = 0; j < InputLayer.Length; j++)
                {
                    FirstHidden[i].Sum += InputLayer[j].Output * FirstHidden[i].Weights[j];
                }
                FirstHidden[i].Output = Sigmoid(FirstHidden[i].Sum);
            }



            for (i = 0; i < OutputLayer.Length; i++)
            {
                OutputLayer[i].Sum = 0;
                for (j = 0; j < FirstHidden.Length; j++)
                {
                    OutputLayer[i].Sum += FirstHidden[j].Output * OutputLayer[i].Weights[j];
                }
                OutputLayer[i].Output = Sigmoid(OutputLayer[i].Sum);
            }
            double[] result = new double[OutputLayer.Length];
            for (i = 0; i < OutputLayer.Length; i++)
            {
                result[i] = OutputLayer[i].Output;
            }
            return result;
        }


        public double[] Backpropagation(ref double[] Input, ref double[] Ideal)
        {
            int i, j;
            double[] answer = CountResult(ref Input);

            for (i = 0; i < OutputLayer.Length; i++)
            {
                OutputLayer[i].Delta = Ideal[i] - answer[i];
            }


            for (i = 0; i < FirstHidden.Length; i++)
            {
                FirstHidden[i].Delta = 0;
                for (j = 0; j < OutputLayer.Length; j++)
                {
                    FirstHidden[i].Delta += OutputLayer[j].Delta * OutputLayer[j].Weights[i];
                }
            }


            for (i = 0; i < InputLayer.Length; i++)
            {
                InputLayer[i].Delta = 0;
                for (j = 0; j < FirstHidden.Length; j++)
                {
                    InputLayer[i].Delta += FirstHidden[j].Delta * FirstHidden[j].Weights[i];
                }
            }

            //////////////////////////




            for (i = 0; i < FirstHidden.Length; i++)
            {
                for (j = 0; j < InputLayer.Length; j++)
                {
                    FirstHidden[i].Weights[j] += FirstHidden[i].Delta * 
                        SigmoidDifferential(FirstHidden[i].Sum) * 
                        InputLayer[j].Output * LearningRate;
                }
            }


            for (i = 0; i < OutputLayer.Length; i++)
            {
                for (j = 0; j < FirstHidden.Length; j++)
                {
                    OutputLayer[i].Weights[j] += OutputLayer[i].Delta * 
                        SigmoidDifferential(OutputLayer[i].Sum) * 
                        FirstHidden[j].Output * LearningRate;
                }
            }

            return answer;
        }


        protected double Sigmoid(double X)
        {
            return 1.0 / (1.0 + Math.Exp(-Alpha * X));
        }

        protected double SigmoidDifferential(double X)
        {
            return Sigmoid(X) * (1.0 - Sigmoid(X));
        }

        private void SetAlpha(double Alpha)
        {
            this.Alpha = Alpha;
        }

        private void SetLearningRate(double lr)
        {
            LearningRate = lr;
        }

    }
}