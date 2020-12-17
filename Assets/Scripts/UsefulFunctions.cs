using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;


public static class UsefulFunctions
{
    static System.Random r = new System.Random();
    public static double NextGaussian(double mu = 0, double sigma = 1)
    {
        double u1 = r.NextDouble();
        double u2 = r.NextDouble();

        double rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

        double rand_normal = (mu + sigma * rand_std_normal);

        //rand_normal = Mathf.Clamp(rand_normal, 0, 10);
        //Debug.Log(Math.Round(rand_normal));

        return rand_normal;
    }

}
