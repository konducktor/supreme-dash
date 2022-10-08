using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chunk") ChangeChildren(collision, true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Chunk") ChangeChildren(collision, false);
    }

    void ChangeChildren(Collider2D collision, bool value)
    {
        for (int i = 0; i < collision.transform.childCount; i++)
        {
            collision.transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}
