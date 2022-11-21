using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl
{
    public static float Round(float num, int divide)
    {
        return decimal.ToSingle(Math.Round((decimal)num, divide));
    }

    public static Vector3 RoundVector(Vector3 vec, int amount)
    {
        vec.x = Round(vec.x, amount);
        vec.y = Round(vec.y, amount);

        return vec;
    }
}
