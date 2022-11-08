using UnityEngine;
using UnityEngine.UI;

public class SelectIcon : MonoBehaviour
{
    public int id;
    public string gameMode = "Cube";
    public bool isUnlocked = false;

    void Start()
    {
        GetComponent<Button>().interactable = isUnlocked;
    }

    public void ChangeIcon()
    {
        switch (gameMode)
        {
            case "Cube":
                GlobalData.CurrentCube = id;
                break;
            case "Ball":
                GlobalData.CurrentBall = id;
                break;
        }

        GetComponentInParent<IconSelectManager>().player.sprite = GetComponent<Image>().sprite;
    }
}
