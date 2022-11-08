using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    [SerializeField] private Text textUser, textGems, textDoublers, textTriplers;
    [SerializeField] private Image icon;

    public void SetProfile(string userName, int iconID, string color, int gems, int rank, int doublers, int triplers)
    {
        textUser.text = userName;
        textGems.text = gems.ToString();
        textDoublers.text = doublers.ToString();
        textTriplers.text = triplers.ToString();

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
