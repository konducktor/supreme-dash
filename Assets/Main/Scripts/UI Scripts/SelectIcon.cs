using UnityEngine;
using UnityEngine.UI;

public class SelectIcon : MonoBehaviour
{
    public int ID;
    public string gameModeName = "Icon";

    public void ChangeIcon()
    {
        PlayerPrefs.SetInt(gameModeName, ID);
        GetComponentInParent<IconSeletManager>().player.sprite = GetComponent<Image>().sprite;
    }
}
