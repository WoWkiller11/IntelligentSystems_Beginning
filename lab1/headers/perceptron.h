#pragma once
#include "stdlib.h"
#include "./helpers.h"
#include "./neuron.h"

class perceptron
{
    private:
        neuron* input_layer = nullptr;
        neuron* first_hidden_layer = nullptr;
        neuron* output_layer = nullptr;

        int input_layer_count = 0;
        int first_hidden_count = 0;
        int output_layer_count = 0;

        double alpha = 1.0;
        double learning_rate = 0.5;

        double sigmoid(double param, double alpha);
        double sigmoid_differential(double param, double alpha);

    public:

        perceptron() {}

        perceptron(int input_count, int hidden_count, int output_count)
        {
            input_layer = new neuron[input_count];
            first_hidden_layer = new neuron[hidden_count];
            output_layer = new neuron[output_count];

            this->input_layer_count = input_count;
            this->first_hidden_count = hidden_count;
            this->output_layer_count = output_count;

            int i, j;

            for (i = 0; i < hidden_count; i++)
            {
                first_hidden_layer[i].weights = new double[input_count];
                for (j = 0; j < input_count; j++)
                {
                    first_hidden_layer[i].weights[j] = rand_double();
                }
            }


            for (i = 0; i < output_count; i++)
            {
                output_layer[i].weights = new double[hidden_count];
                for (j = 0; j < 0; j++)
                {
                    output_layer[i].weights[j] = rand_double();
                }
            }
        }

        ~perceptron()
        {
            int i, j;

            for (i = 0; i < input_layer_count; i++)
            {
                delete[] input_layer[i].weights;
            }
            delete[] input_layer;


            for (i = 0; i < first_hidden_count; i++)
            {
                delete[] first_hidden_layer[i].weights;
            }
            delete[] first_hidden_layer;


            for (i = 0; i < output_layer_count; i++)
            {
                delete[] output_layer[i].weights;
            }
            delete[] output_layer;
        }


        void set_alpha(double alpha);
        void set_learning_rate(double learning_rate);

        double* count_result(double* input);
        double* back_propagation(double* input, double* ideal);
};