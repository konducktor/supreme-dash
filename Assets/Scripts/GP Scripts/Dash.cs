using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private bool auto;

    private Vector3 originalSize;

    void Start()
    {
        originalSize = transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("DynamicObject"))
        {
            transform.localScale *= 1.1f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("DynamicObject"))
        {
            transform.localScale = originalSize;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (auto || GameInput.IsVerticalControlls() && GameInput.ButtonDown()))
        {
            Vector2 force = AngleToVector3(transform.eulerAngles.z) * 5f;

            other.attachedRigidbody.AddForce(force);
            return;
        }

        if (other.CompareTag("DynamicObject") && auto)
        {
            Vector2 force = AngleToVector3(transform.eulerAngles.z) * 5f;

            other.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }


    public static Vector3 AngleToVector3(float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
    }
}
