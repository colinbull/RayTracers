//
// Created by colin on 04/12/2018.
//

#include <stdio.h>
#include "ppmfile.h"

FILE* fp;

void createPpmFile(const char* fileName, int width, int height) {
    fopen_s(&fp, fileName, "w");
    fprintf(fp, "P3\n%d %d\n255\n", width, height);

}

void writeLine(float x, float y, float z) {
    fprintf(fp, "%d %d %d\n", (int)(255.99 * x), (int)(255.99 * y), (int)(255.99 * x));
}
