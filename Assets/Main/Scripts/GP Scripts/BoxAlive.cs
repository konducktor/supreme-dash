using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAlive : MonoBehaviour
{
    private Vector3 savedPosition;
    private Rigidbody2D rb;

    public pController player;

    public void ResetPosition()
    {
        transform.position = savedPosition;
        transform.eulerAngles = Vector3.zero;

        rb.MoveRotation(0f);
        rb.velocity = Vector3.zero;
    }

    public void Checkpoint()
    {
        savedPosition = transform.position;
    }

    private void Awake()
    {
        savedPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Spike")
        {
            player.ResetPosition();
        }
    }
}
