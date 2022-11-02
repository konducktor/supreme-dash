using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLogic : MonoBehaviour
{
    private int chunkLoaders = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ChunkLoader" || collision.tag == "MainCamera")
        {
            if (chunkLoaders == 0)
            {
                ChangeChildren(true);
            }

            chunkLoaders++;
            return;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ChunkLoader" || collision.tag == "MainCamera")
        {
            chunkLoaders--;

            if (chunkLoaders == 0)
            {
                ChangeChildren(false);
            }
            return;
        }
    }


    void ChangeChildren(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}
