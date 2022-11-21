using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSaved : MonoBehaviour
{
    private static LevelSaved instance;
    private static Text text;

    void Start()
    {
        instance = this;
        text = GetComponentInChildren<Text>();
    }


    public static void LevelSavedAnimation()
    {
        instance.StartCoroutine(instance.Animation());
    }

    IEnumerator Animation()
    {
        Color color = text.color;
        color.a = 1f;

        text.color = color;

        yield return new WaitForSeconds(2f);

        color.a = 0f;
        text.color = color;
    }
}
