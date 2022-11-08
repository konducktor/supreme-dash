using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float speed = 2f;

    private Vector3 startAngle;

    private float elapsedTime = 0f;

    void Start()
    {
        startAngle = transform.eulerAngles;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * speed;
        if (elapsedTime >= 360f) elapsedTime -= 360f;

        transform.eulerAngles = startAngle + new Vector3(0, 0, (float)(range * Math.Sin(elapsedTime)));
    }
}
