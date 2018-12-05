cmake_minimum_required(VERSION 3.5)
project(C C)

set(CMAKE_C_STANDARD 99)

add_executable(C main.c vector3.c vector3.h ppmfile.c ppmfile.h ray.c ray.h)