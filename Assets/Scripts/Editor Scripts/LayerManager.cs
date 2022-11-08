using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerManager : MonoBehaviour
{
    private InputField number;

    public static int currentLayer;

    void Start()
    {
        currentLayer = 100;
        number = transform.GetComponentInChildren<InputField>();
        number.text = (currentLayer - 100).ToString();

        SetOpacity();
    }

    public void Change(int value)
    {
        currentLayer += value;
        number.text = (currentLayer - 100).ToString();

        SetOpacity();
    }

    public void Set()
    {
        currentLayer = (int)Convert.ToDecimal(number.text) + 100;

        SetOpacity();
    }

    public static void SetOpacity()
    {
        for (int i = 0; i < Convert.ToInt32(EditorLogic.objects.Count); i++)
        {
            Color col = EditorLogic.level[i].GetComponent<SpriteRenderer>().color;

            col.a = 0.3f;
            if (EditorLogic.objects[i].layer == currentLayer)
            {
                col.a = EditorLogic.objects[i].col.a;
            }

            EditorLogic.level[i].GetComponent<SpriteRenderer>().color = col;
        }
    }
}
