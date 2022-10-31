using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pController : MonoBehaviour
{
    class CheckpointSave
    {
        public string mode = "cube";
        public float speed = 7f;
    }

    [SerializeField] private GameObject[] gameModes;
    [SerializeField] public float speed = 7f;

    public Vector2 forces;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private string gameMode;

    CheckpointSave checkpointSave;
    private static bool IsAbleToChange = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        checkpointSave = new CheckpointSave();
        SetGamemode("cube");
    }

    private void Update()
    {
        rb.velocity = new Vector2(GameInput.HorizontalConstrolls() * speed + forces.x * speed, rb.velocity.y + forces.y);

        if (GameInput.IsVerticalControlls())
        {
            switch (gameMode)
            {
                case "cube":
                    rb.AddForce(Vector2.up * 1400f * Time.deltaTime);
                    break;

                case "ball":
                    if (GameInput.ButtonDown())
                    {
                        rb.gravityScale *= -1f;
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        forces.y *= 0.9f;
        forces.x *= 0.995f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "EndPoint":
                Destroy(gameObject);
                break;

            case "Spike":
                ResetPosition();
                break;

            case "Checkpoint":
                checkpointSave.mode = gameMode;
                checkpointSave.speed = speed;

                startPos = collider.transform.position;
                break;

            case "BallPortal":
                if (IsAbleToChange)
                {
                    SetGamemode((gameMode == "ball") ? "cube" : "ball");
                    rb.gravityScale = (gameMode == "ball") ? 1f : 2f;

                    StartCoroutine(Ball());
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Spike")
        {
            ResetPosition();
        }
    }

    IEnumerator Ball()
    {
        IsAbleToChange = false;

        yield return new WaitForSeconds(2f);

        IsAbleToChange = true;
    }

    private void SetGamemode(string mode = "cube")
    {
        gameMode = mode;

        foreach (GameObject currentMode in gameModes)
        {
            currentMode.SetActive(false);
            if (currentMode.name.ToLower() == mode)
            {
                currentMode.SetActive(true);
            }

        }
    }

    private void ResetPosition()
    {
        transform.position = startPos;

        speed = checkpointSave.speed;
        gameMode = checkpointSave.mode;

        SetGamemode(gameMode);

        rb.gravityScale = (gameMode == "cube") ? 1f : 2f;
        rb.MoveRotation(0f);
        rb.velocity = Vector3.zero;
    }
}
