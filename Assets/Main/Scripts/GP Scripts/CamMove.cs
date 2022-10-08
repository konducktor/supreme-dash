using UnityEngine;

public class CamMove : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D camBox;
    private float sizeX, sizeY, ratio;

    void Start()
    {
        cam = GetComponent<Camera>();
        camBox = GetComponent<BoxCollider2D>();

        transform.position = player.position;
    }

    [SerializeField] private Transform player;

    private void Update()
    {
        ratio = (float)Screen.width / (float)Screen.height;
        sizeY = cam.orthographicSize * 2;
        sizeX = sizeY * ratio;
        camBox.size = new Vector2(sizeX, sizeY);

        if (player != null)
        {
            Vector3 currentPos = transform.position;
            currentPos += (player.position - transform.position) * 8f * Time.deltaTime;
            currentPos.z = -10f;

            transform.position = currentPos;
        }
    }
}
