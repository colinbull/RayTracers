//
// Created by colin on 04/12/2018.
//
#include <stdlib.h>
#include <math.h>
#include "vector3.h"

vector3 newVector3(float x, float y, float z) {
    vector3* newVec = malloc(sizeof(vector3));
    newVec->x = x;
    newVec->y = y;
    newVec->z = z;
    return *newVec;
}

float vlength(const vector3 a) {
    return sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
}



float vlengthSquared(const vector3 a) {
    return sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
}

vector3 vPlusv(const vector3 a, const vector3 b) {
    return newVector3(a.x + b.x, a.y + b.y, a.z + b.z);
}

vector3 vMinusv(const vector3 a, const vector3 b) {
    return newVector3(a.x - b.x, a.y - b.y, a.z - b.z);
}

vector3 vMulv(const vector3 a, const vector3 b) {
    return newVector3(a.x * b.x, a.y * b.y, a.z * b.z);
}

vector3 vDivv(const vector3 a, const vector3 b) {
    return newVector3(a.x / b.x, a.y / b.y, a.z / b.z);
}

vector3 vPlusf(const vector3 a, const float b) {
    return newVector3(a.x + b, a.y + b, a.z + b);
}

vector3 vMinusf(const vector3 a, const float b) {
    return newVector3(a.x - b, a.y - b, a.z - b);
}

vector3 vMulf(const vector3 a, const float b) {
    return newVector3(a.x * b, a.y * b, a.z * b);
}

vector3 vDivf(const vector3 a, const float b) {
    return newVector3(a.x / b, a.y / b, a.z / b);
}

vector3 makeUnit(const vector3 a) {
    float k = 1 / vlength(a);
    return newVector3(a.x / k, a.y / k, a.z / k);
}

float dot(const vector3 a, const vector3 b) {
    return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
}

vector3 cross(const vector3 a, const vector3 b) {
    return newVector3(
            (a.y * b.z) - (a.z * b.y),
            -((a.x * b.z) - (a.z * b.x)),
            (a.x * b.y) - (a.y * b.x)
            );
}

