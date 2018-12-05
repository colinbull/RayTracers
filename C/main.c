#include <stdio.h>
#include <stdlib.h>
#include "ppmfile.h"
#include "vector3.h"

int main() {
    int width = 200;
    int height = 100;

    createPpmFile("output.ppm", width, height);

    for(int j = height - 1; j >= 0; j--) {
        for(int i = 0; i < width; i++) {
            vector3 vec = newVector3(
                    ((float)i) / ((float)width),
                    ((float)j) / ((float)height),
                    0.2);
            writeLine(vec.x,vec.y,vec.z);

            free(&vec);
        }
    }

    printf("Done");
    return 0;
}