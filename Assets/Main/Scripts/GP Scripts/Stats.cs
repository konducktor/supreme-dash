using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private Transform tf;
    private pController contr;

    [SerializeField] private Text fps, sx, sy, posx, posy, maxSpeed;

    void Start()
    {
        gameObject.SetActive(GlobalData.AdvancedStats);
        if (!GlobalData.AdvancedStats) return;

        rb = player.GetComponent<Rigidbody2D>();
        tf = player.GetComponent<Transform>();
        contr = player.GetComponent<pController>();
    }

    void LateUpdate()
    {
        if (rb && tf && contr)
        {
            fps.text = "fps: " + EditorCursor.Round(0.1f / Time.smoothDeltaTime, 0);
            sx.text = "sx: " + EditorCursor.Round(rb.velocity.x, 3);
            sy.text = "sy: " + EditorCursor.Round(rb.velocity.y, 3);
            posx.text = "posx: " + EditorCursor.Round(tf.position.x, 3);
            posy.text = "posy: " + EditorCursor.Round(tf.position.y, 3);
            maxSpeed.text = "maxs: " + contr.speed;
        }
    }
}
