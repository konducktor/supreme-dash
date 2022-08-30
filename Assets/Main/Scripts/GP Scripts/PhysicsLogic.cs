using UnityEngine;

public class PhysicsLogic : MonoBehaviour
{
    private Transform tf;
    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        tf = transform;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;

        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(tf.position, player.position) < 10f)
        {
            rb.isKinematic = false;
        }
    }
}
