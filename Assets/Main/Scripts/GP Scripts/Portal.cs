using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform portal2;

    private BoxCollider2D p2Collider;

    void Awake()
    {
        p2Collider = portal2.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("DynamicObject"))
        {
            StartCoroutine(Teleport(other));
        }
    }

    IEnumerator Teleport(Collider2D obj)
    {
        p2Collider.enabled = false;
        obj.transform.position = portal2.position;

        yield return new WaitForSeconds(2);

        p2Collider.enabled = true;
    }
}
