using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chunk")
        {
            ChangeChildren(collision, true);
            return;
        }

        if (collision.tag == "DynamicObject")
        {
            ChangePhysics(collision, false);
            return;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Chunk")
        {
            ChangeChildren(collision, false);
            return;
        }

        if (collision.tag == "DynamicObject")
        {
            ChangePhysics(collision, true);
            return;
        }
    }

    void ChangeChildren(Collider2D collision, bool value)
    {
        for (int i = 0; i < collision.transform.childCount; i++)
        {
            collision.transform.GetChild(i).gameObject.SetActive(value);
        }
    }

    void ChangePhysics(Collider2D collision, bool value)
    {
        Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = value;
    }
}
