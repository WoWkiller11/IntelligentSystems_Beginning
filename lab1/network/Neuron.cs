namespace lab1.network
{
    public struct Neuron
    {
        public double Output { get; set; }
        public double Sum { get; set; }

        public double Delta { get; set; }

        public double[] Weights;
    }
}