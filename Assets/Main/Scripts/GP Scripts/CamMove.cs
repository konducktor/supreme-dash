using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] private Transform player;


    private void Update()
    {
        if (player != null)
        {
            transform.position = player.position + new Vector3(0, 0, -10f);
        }
    }
}
