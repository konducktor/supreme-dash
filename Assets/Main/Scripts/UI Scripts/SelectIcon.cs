using UnityEngine;
using UnityEngine.UI;

public class SelectIcon : MonoBehaviour
{

    [SerializeField] private SpriteRenderer player;
    [SerializeField] private int ID;
    [SerializeField] private string gameModeName = "Icon";

    public void ChangeIcon()
    {
        PlayerPrefs.SetInt(gameModeName, ID);
        player.sprite = GetComponent<Image>().sprite;
    }
}
