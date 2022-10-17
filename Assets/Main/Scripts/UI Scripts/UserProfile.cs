using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    [SerializeField] private Text textUser;
    [SerializeField] private Text textGems;
    [SerializeField] private Image icon;

    public void SetProfile(string userName, int iconID, string color, int gems, int rank)
    {
        textUser.text = userName;
        textGems.text = gems.ToString();

        GetComponentInChildren<RankBar>().Rank = rank;
        icon.sprite = Resources.Load<Sprite>("Cube/icon" + iconID.ToString());
        icon.color = ColorSelector.ColorFromString(color);
    }

    public void UserLevels()
    {
        GameObject.Find("Main Camera").GetComponent<ServerLogic>().Search("author", textUser.text);
        Destroy(gameObject);
    }

    void OnDisable()
    {
        Destroy(gameObject);
    }
}
