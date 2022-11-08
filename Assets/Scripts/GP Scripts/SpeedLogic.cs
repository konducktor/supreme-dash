using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLogic : MonoBehaviour
{
    [SerializeField] private float changeSpeed;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            pController player = collider.GetComponentInParent<pController>();
            player.speed = player.playerSpeed * changeSpeed;
        }
    }
}
