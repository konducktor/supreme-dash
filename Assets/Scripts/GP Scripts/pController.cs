using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pController : MonoBehaviour
{
    class CheckpointSave
    {
        public string mode;
        public float speed;
        public Vector3 position;

        public CheckpointSave(Vector3 pos)
        {
            mode = "cube";
            speed = 30f;
            position = pos;
        }
    }

    [SerializeField] private GameObject[] gameModes;
    [SerializeField] public float speed = 30f;
    public float playerSpeed = 30f;
    [SerializeField] private float smoothing = 0.05f;
    private Vector3 velocity = Vector3.zero;

    private Rigidbody2D rb;
    private TrailRenderer tr;

    private string gameMode;

    CheckpointSave checkpointSave;
    private static bool IsAbleToChange = true;

    public List<BoxAlive> aliveBoxes = new List<BoxAlive>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        rb.gravityScale = 1f;

        checkpointSave = new CheckpointSave(transform.position);
        SetGamemode("cube");
    }

    float horizontalMove = 0f;
    bool isJumpung = false, isButtonDown = false;

    private void Update()
    {
        horizontalMove = GameInput.HorizontalConstrolls();
        if (GameInput.IsVerticalControlls()) isJumpung = true;
        if (GameInput.ButtonDown()) isButtonDown = true;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneTransition.ChangeScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        Move(GameInput.HorizontalConstrolls() * speed * Time.fixedDeltaTime * 10f, isJumpung, isButtonDown);
        isJumpung = false;
        isButtonDown = false;
    }

    private void Move(float move, bool jumpung, bool buttonDown)
    {
        if (jumpung)
        {
            switch (gameMode)
            {
                case "cube":
                    rb.AddForce(Vector2.up * 30f);
                    break;

                case "ball":
                    if (buttonDown)
                    {
                        rb.gravityScale *= -1f;
                    }
                    break;
            }
        }

        float deltaX = move - rb.velocity.x;
        Vector3 targetVelocity = (Vector2.right * deltaX) + new Vector2(rb.velocity.x, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothing);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "EndPoint":
                Destroy(gameObject);
                break;

            case "Checkpoint":
                checkpointSave.mode = gameMode;
                checkpointSave.speed = speed;

                checkpointSave.position = collider.transform.position;

                foreach (var box in aliveBoxes)
                {
                    box.Checkpoint();
                }

                break;

            case "BallPortal":
                if (IsAbleToChange)
                {
                    SetGamemode((gameMode == "ball") ? "cube" : "ball");
                    rb.gravityScale = (gameMode == "ball") ? 2f : 1f;

                    StartCoroutine(Ball());

                    rb.AddForce(transform.forward * 10f, ForceMode2D.Impulse);
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

    public void ResetPosition()
    {
        transform.position = checkpointSave.position;
        transform.eulerAngles = Vector3.zero;
        speed = checkpointSave.speed;
        gameMode = checkpointSave.mode;

        SetGamemode(gameMode);

        rb.gravityScale = (gameMode == "cube") ? 1f : 2f;
        rb.MoveRotation(0f);
        rb.velocity = Vector3.zero;

        tr.Clear();

        foreach (var box in aliveBoxes)
        {
            box.ResetPosition();
        }
    }
}
