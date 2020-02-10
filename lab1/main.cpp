#include <vector>
#include <time.h>
#include <iostream>
#include "stdlib.h"
#include "./headers/perceptron.h"
#include "./source/perceptron.cpp"

int main()
{
    srand(time(NULL));

    perceptron a(3, 4, 2);
    a.set_alpha(1.0);
    a.set_learning_rate(0.5);

    double* input = new double[3];
    double* output = nullptr;

    input[0] = 0.3;
    input[1] = 0.9;
    input[2] = 0.5221;

    output = a.count_result(input);

    for (int i = 0; i < 2; i++)
    {
        std::cout << output[i] << std::endl;
    }

    delete[] input;
    delete[] output;
    system("pause");
    return 0;
}