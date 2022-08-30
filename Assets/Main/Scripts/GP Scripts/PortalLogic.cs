using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{
    private Collider2D mainCollider;

    private void Awake()
    {
        mainCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && mainCollider.enabled)
        {
            StartCoroutine(this.Reset());
        }
    }

    private IEnumerator Reset()
    {
        mainCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        mainCollider.enabled = true;
        yield break;
    }
}
