//
// Created by colin on 04/12/2018.
//

#ifndef C_VECTOR3_H
#define C_VECTOR3_H

typedef struct {
    float x, y, z;
} vector3;

vector3 newVector3(float x, float y, float z);

vector3 vPlusv(const vector3 a, const vector3 b);
vector3 vMinusv(const vector3 a, const vector3 b);
vector3 vMulv(const vector3 a, const vector3 b);
vector3 vDivv(const vector3 a, const vector3 b);
vector3 vPlusf(const vector3 a, const float b);
vector3 vMinusf(const vector3 a, const float b);
vector3 vMulf(const vector3 a, const float b);
vector3 vDivf(const vector3 a, const float b);

float vlengthSquared(const vector3 a);
float vlength(const vector3 a);
vector3 makeUnit(const vector3 a);

float dot(const vector3 a, const vector3 b);
vector3 cross(const vector3 a, const vector3 b);

#endif //C_VECTOR3_H
