using UnityEngine;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour
{
    void OnEnable()
    {
        float time = GameObject.Find("Main").GetComponent<GameLoader>().timer;

        gameObject.GetComponent<Text>().text = "Total time: " + EditorCursor.Round(time, 2) + " sec";
    }
}
