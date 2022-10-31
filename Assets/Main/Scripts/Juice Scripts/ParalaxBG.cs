using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParalaxBG : MonoBehaviour
{
    [Range(0f, 100f)]
    public float speed = 50f;

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(rt.sizeDelta.x * 3f, rt.sizeDelta.y);
    }

    void Update()
    {
        if (rt.localPosition.x >= rt.sizeDelta.x / 3f)
        {
            rt.localPosition *= -1f;
        }

        transform.localPosition += new Vector3(Time.deltaTime * speed, 0, 0);
    }
}
