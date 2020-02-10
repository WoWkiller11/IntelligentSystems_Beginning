#pragma once

struct neuron
{
    double output = 0;
    double weight_sum = 0;
    double delta = 0;
    double* weights = nullptr;
};