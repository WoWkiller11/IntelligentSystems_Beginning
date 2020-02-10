#include "./../headers/perceptron.h"

double* perceptron::count_result(double* input)
{
    int i, j;
    
    for (i = 0; i < input_layer_count; i++)
    {
        input_layer[i].output = input[i];
    }


    for (i = 0; i < first_hidden_count; i++)
    {
        first_hidden_layer[i].weight_sum = 0;
        for (j = 0; j < input_layer_count; j++)
        {
            first_hidden_layer[i].weight_sum += input_layer[j].output * first_hidden_layer[i].weights[j];
        }
        first_hidden_layer[i].output = sigmoid(first_hidden_layer[i].weight_sum, alpha);
    }


    for (i = 0; i < output_layer_count; i++)
    {
        output_layer[i].weight_sum = 0;
        for (j = 0; j < first_hidden_count; j++)
        {
            output_layer[i].weight_sum += first_hidden_layer[j].output * output_layer[i].weights[j];
        }
        output_layer[i].output = sigmoid(output_layer[i].weight_sum, alpha);
    }


    double* result = new double[output_layer_count];
    for (i = 0; i < output_layer_count; i++)
    {
        result[i] = output_layer[i].output;
    }
    return result;
}


double* perceptron::back_propagation(double* input, double* ideal)
{
    int i, j;

    double* answer = count_result(input);

    // Count deltas
    /*********************************************************/
    for (i = 0; i < output_layer_count; i++)
    {
        output_layer[i].delta = ideal[i] - answer[i];
    }


    for (i = 0; i < first_hidden_count; i++)
    {
        first_hidden_layer[i].delta = 0;
        for (j = 0; j < output_layer_count; j++)
        {
            first_hidden_layer[i].delta += output_layer[j].delta * output_layer[j].weights[i];
        }
    }


    for (i = 0; i < input_layer_count; i++)
    {
        input_layer[i].delta = 0;
        for (j = 0; j < first_hidden_count; j++)
        {
            input_layer[i].delta += first_hidden_layer[j].delta * first_hidden_layer[j].weights[i];
        }
    }
    /*********************************************************/


    // Change weights
    /*********************************************************/
    for (i = 0; i < first_hidden_count; i++)
    {
        for (j = 0; j < input_layer_count; j++)
        {
            first_hidden_layer[i].weights[j] += first_hidden_layer[i].delta * 
                sigmoid_differential(first_hidden_layer[i].weight_sum, alpha) * input_layer[j].output * learning_rate;
        }
    }

    for (i = 0; i < output_layer_count; i++)
    {
        for (j = 0; j < first_hidden_count; j++)
        {
            output_layer[i].weights[j] += output_layer[i].delta * 
                sigmoid_differential(output_layer[i].weight_sum, alpha) * first_hidden_layer[j].output * learning_rate;
        }
    }
    /*********************************************************/

    return answer;
}


double perceptron::sigmoid(double param, double alpha)
{
    return 1.0 / (1.0 + exp(-alpha * param));
}

double perceptron::sigmoid_differential(double param, double alpha)
{
    return sigmoid(param, alpha) * (1.0 - sigmoid(param, alpha));
}


void perceptron::set_alpha(double alpha)
{
    this->alpha = alpha;
}

void perceptron::set_learning_rate(double learning_rate)
{
    this->learning_rate = learning_rate;
}
