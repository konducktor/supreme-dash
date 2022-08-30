using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public static int level;
    public static GameObject currentLevel;
    public static GameObject[] actualLevels;

    public GameObject[] levels;

    private int levelID;
    void Start()
    {
        actualLevels = levels;
        currentLevel = Instantiate(actualLevels[level]);
    }
}
