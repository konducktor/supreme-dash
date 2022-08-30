using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCam : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 dir;
    private Vector3 mouseStart;
    Vector3 pointer;
    private float extraSpeed = 1f;

    private void Update()
    {
        pointer = GameInput.Pointer();
        extraSpeed = 1f;
        if (Input.GetButton("ExtraSpeed"))
        {
            extraSpeed = 10f;
        }

        if (GameInput.StartMove())
        {
            mouseStart = pointer;
        }
        else if (GameInput.ContinueMove())
        {
            transform.position += mouseStart - pointer;
        }
        else
        {
            Movement();
        }

        Scroll();
    }

    void Movement()
    {
        dir = Vector3.zero;

        if (Input.GetButton("Horizontal"))
        {
            dir += new Vector3(1f, 0, 0) * Input.GetAxis("Horizontal");
        }

        if (Input.GetButton("Vertical"))
        {
            dir += new Vector3(0, 1f, 0) * Input.GetAxis("Vertical");
        }

        dir.Normalize();

        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + dir,
            speed * Time.deltaTime * extraSpeed
        );
    }

    void Scroll()
    {
        float axis = GameInput.ScrollAxis() / 100;
        if (axis != 0 && Camera.main.orthographicSize >= 3 && Camera.main.orthographicSize <= 100)
        {
            Camera.main.orthographicSize -= axis * extraSpeed;
        }

        if (Camera.main.orthographicSize < 3) Camera.main.orthographicSize = 3;
        if (Camera.main.orthographicSize > 100) Camera.main.orthographicSize = 100;
    }
}
