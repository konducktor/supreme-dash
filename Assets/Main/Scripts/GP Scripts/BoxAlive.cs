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

        rb.MoveRotation(0f);
        rb.velocity = Vector3.zero;
    }

    private void Awake()
    {
        savedPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Spike")
        {
            player.ResetPosition();
        }
    }
}
