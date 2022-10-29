using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Start()
    {
        transform.position = player.position;
    }

    private void Update()
    {

        if (player != null)
        {
            Vector3 currentPos = player.position;


            // if (Vector3.Distance(player.position, transform.position) < 30f)
            // {
            //     currentPos += (player.position - transform.position) * (8f / Time.deltaTime);
            // }
            // else
            // {
            //     currentPos = player.position;
            // }

            currentPos.z = -10f;
            transform.position = currentPos;
        }
    }
}
