# Eigen
Experiment with [Eigen]("http://eigen.tuxfamily.org/index.php?title=Main_Page#Download").

## Build
* Download [Eigen]("http://eigen.tuxfamily.org/index.php?title=Main_Page#Download") github repository.
* Copy and paste "Eigen/" subdirectory to this directory.
* Type `g++ -std=c++11 <src> -o <bin>`
* Type `.\<bin>` 

## Usage
```cpp
#include <iostream>
#include "Eigen/Dense"

using Eigen::MatrixXd;

int main() {
    MatrixXd m(2, 2);
    m(0, 0) = 3;
    m(1, 0) = 2.5;
    m(0, 1) = -1;
    m(1, 1) = m(1, 0) + m(0, 1);
    std::cout << m << std::endl;
    return 0;
}
```