using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float speed = 2f;

    private Vector3 startScale;
    private float elapsedTime = 0f;
    private float scaleValue;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * speed;
        if (elapsedTime >= 360f) elapsedTime -= 360f;

        scaleValue = (float)(range * Math.Sin(elapsedTime));
        transform.localScale = startScale + new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
