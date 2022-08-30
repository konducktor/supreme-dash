using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    private InputField input;

    public string currentHex;

    void Awake()
    {
        input = GetComponent<InputField>();
    }
    void Start()
    {
        currentHex = PlayerPrefs.GetString("Color", "FFFFFF");
        input.text = currentHex;
    }



    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer jetpack;
    public void ChangeField()
    {
        currentHex = input.text;

        icon.color = ColorFromString(currentHex);
        jetpack.color = icon.color;

        PlayerPrefs.SetString("Color", currentHex);
    }


    public static float HexToFloat(string hex)
    {
        return System.Convert.ToInt32(hex, 16) / 255f;
    }
    public static Color ColorFromString(string hex)
    {
        float red = HexToFloat(hex.Substring(0, 2));
        float green = HexToFloat(hex.Substring(2, 2));
        float blue = HexToFloat(hex.Substring(4, 2));

        return new Color(red, green, blue);
    }
    public static string ColorToString(Color col)
    {
        string red = Mathf.RoundToInt(col.r * 255f).ToString("X2");
        string green = Mathf.RoundToInt(col.g * 255f).ToString("X2");
        string blue = Mathf.RoundToInt(col.b * 255f).ToString("X2");

        string output = red + green + blue;

        return output;
    }
}
