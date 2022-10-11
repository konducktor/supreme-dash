using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalData
{
    public int selectedCube;
    public int selectedBall;
    public bool[] unlockedCubes;
    string selectedColor;

    public LocalData(int cube, int ball, bool[] cubes, string color)
    {
        this.selectedCube = cube;
        this.selectedBall = ball;
        this.unlockedCubes = cubes;
        this.selectedColor = color;
    }
}
