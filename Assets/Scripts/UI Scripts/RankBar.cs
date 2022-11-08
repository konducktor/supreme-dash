using UnityEngine;
using UnityEngine.UI;

public class RankBar : MonoBehaviour
{
    private int fullRank;
    private int levelRank;
    private int rankLevel;

    [SerializeField] private Text textLevel;
    [SerializeField] private Text textLastLevel;
    [SerializeField] private Text textNextLevel;
    [SerializeField] private Scrollbar scrollLevel;

    public int Rank
    {
        get
        {
            return fullRank;
        }
        set
        {
            fullRank = value;
            levelRank = value / 10;
            rankLevel = value % 10;

            scrollLevel.size = (float)rankLevel / 10f;

            textLevel.text = "Rank Level: " + levelRank;
            textLastLevel.text = (fullRank - rankLevel).ToString();
            textNextLevel.text = (fullRank - rankLevel + 10).ToString();
        }
    }
}
