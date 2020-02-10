#pragma once
#include "stdlib.h"
#include <cmath>

double rand_double()
{
    return ((double)rand()/(double)RAND_MAX);
}