using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private Transform tf;
    private pController contr;
    GameLoader gl;

    [SerializeField] private Text fps, sx, sy, posx, posy, maxSpeed, timer;
    GameLoader loader;

    void Start()
    {
        gameObject.SetActive(GlobalData.AdvancedStats);
        if (!GlobalData.AdvancedStats) return;

        rb = player.GetComponent<Rigidbody2D>();
        tf = player.GetComponent<Transform>();
        contr = player.GetComponent<pController>();

        loader = FindObjectOfType<GameLoader>();
        gl = FindObjectOfType<GameLoader>();
    }

    void LateUpdate()
    {
        if (rb && tf && contr && !(loader.pausePanel.activeSelf || loader.finishMenu.activeSelf))
        {
            fps.text = "fps: " + (int)(0.1f / Time.smoothDeltaTime);
            sx.text = "sx: " + GlobalControl.Round(rb.velocity.x, 3);
            sy.text = "sy: " + GlobalControl.Round(rb.velocity.y, 3);
            posx.text = "posx: " + GlobalControl.Round(tf.position.x, 3);
            posy.text = "posy: " + GlobalControl.Round(tf.position.y, 3);
            maxSpeed.text = "maxs: " + contr.speed;
            timer.text = "time: " + GlobalControl.Round(gl.timer, 3);
        }
    }
}
