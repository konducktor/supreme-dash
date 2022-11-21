using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLogic : MonoBehaviour
{
    [SerializeField] private Camera followCamera;
    [SerializeField] private float offset;

    private Vector3 position;
    private SpriteRenderer sr;
    private Vector2 spriteSize;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(followCamera.orthographicSize * 12f, followCamera.orthographicSize * 12f);
        position = transform.position;
    }

    private void Update()
    {
        sr.color = followCamera.backgroundColor;

        if (followCamera.orthographicSize > 13f)
        {
            sr.color = new Color(1f, 1f, 1f, 0f);
        }
        spriteSize = sr.size;

        position.y = GlobalControl.Round(followCamera.transform.position.y / (sr.size.y / 3f), 0) * (sr.size.y / 3f) + offset;
        position.x = GlobalControl.Round(followCamera.transform.position.x / (sr.size.y / 3f), 0) * (sr.size.y / 3f) + offset;

        transform.position = position;
    }
}
