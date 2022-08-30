using UnityEngine;
using UnityEngine.SceneManagement;

public class pController : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject ball;

    [SerializeField] public float speed = 7f;
    private Rigidbody2D rb;

    private Vector3 startPos;

    private string gameMode;
    private string savedGameMode;
    private static bool IsAbleToChange = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        savedGameMode = "cube";
        gameMode = "cube";
    }

    private void Update()
    {
        rb.velocity = new Vector2(GameInput.HorizontalConstrolls() * speed, rb.velocity.y);
        if (GameInput.IsVerticalControlls())
        {
            string text = gameMode;
            if (text != null)
            {
                if (!(text == "cube"))
                {
                    if (text == "ball")
                    {
                        if (GameInput.ButtonDown())
                        {
                            rb.gravityScale *= -1f;
                        }
                    }
                }
                else
                {
                    rb.AddForce(Vector2.up * 1400f * Time.deltaTime);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ResetPosition()
    {
        transform.position = startPos;
        speed = 7f;
        gameMode = savedGameMode;
        cube.SetActive(false);
        ball.SetActive(false);
        string text = gameMode;
        if (text != null)
        {
            if (!(text == "cube"))
            {
                if (text == "ball")
                {
                    ball.SetActive(true);
                }
            }
            else
            {
                cube.SetActive(true);
            }
        }
        rb.gravityScale = 1f;
        rb.MoveRotation(0f);
        rb.velocity = new Vector3(0f, 0f);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EndPoint"))
        {
            Object.Destroy(gameObject);
            return;
        }
        if (collider.CompareTag("Checkpoint"))
        {
            savedGameMode = gameMode;
            startPos = collider.transform.position;
            return;
        }
        if (collider.CompareTag("Spike"))
        {
            ResetPosition();
            return;
        }
        if (collider.CompareTag("BallPortal") && IsAbleToChange)
        {
            if (!ball.activeSelf)
            {
                cube.SetActive(false);
                ball.SetActive(true);
                gameMode = "ball";
                rb.gravityScale = 2f;
                IsAbleToChange = false;
                return;
            }
            cube.SetActive(true);
            ball.SetActive(false);
            gameMode = "cube";
            rb.gravityScale = 1f;
            IsAbleToChange = false;
        }
    }
}
