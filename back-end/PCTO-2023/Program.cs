using container;
using ship;
using Test;
using computing;

Test.Program example = new Test.Program(2, 2, 1, 20);

float[] pesi = { 10, 11, 9, 8 };
int[] scarico = { 5, 5, 5, 5 };
int [] id = {0, 1, 2, 3};

example.ex1(pesi, scarico, id);