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
            if (Vector3.Distance(player.position, transform.position) > 30f)
            {

                Vector3 currentPos = transform.position;
                currentPos += (player.position - transform.position) * 8f * Time.deltaTime;
                currentPos.z = -10f;

                transform.position = currentPos;
                return;
            }

            transform.position = player.position;
        }
    }
}
