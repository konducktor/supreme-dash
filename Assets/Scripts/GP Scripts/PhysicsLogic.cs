using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsLogic : MonoBehaviour
{
    public bool isOnScreen = false;

    Rigidbody2D rb;
    Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!rb.isKinematic && !isOnScreen && rb.velocity == Vector2.zero && lastVelocity == Vector2.zero)
        {
            rb.isKinematic = true;
            FindChildWithTag("ChunkLoader").SetActive(false);

            return;
        }

        lastVelocity = rb.velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            rb.isKinematic = false;
            FindChildWithTag("ChunkLoader").SetActive(true);
            isOnScreen = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            isOnScreen = false;
        }
    }

    public GameObject FindChildWithTag(string tag)
    {
        GameObject child = null;

        foreach (Transform childTransform in transform)
        {
            if (childTransform.CompareTag(tag))
            {
                child = childTransform.gameObject;
                break;
            }
        }

        return child;
    }
}
