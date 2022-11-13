using UnityEngine;
using UnityEngine.UI;

public class IconSelectManager : MonoBehaviour
{
    public SpriteRenderer player;

    public int[] iconCategories;
    public Color[] colors;


    [SerializeField] private GameObject slot;
    [SerializeField] private string gameMode;

    void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);

        SelectIcon icon;
        Image image;

        GameObject currentIcon;
        for (int i = 0; i < iconCategories.Length; i++)
        {
            currentIcon = Instantiate(slot, transform);

            icon = currentIcon.GetComponent<SelectIcon>();
            image = currentIcon.GetComponent<Image>();

            icon.id = i;
            icon.gameMode = gameMode;
            if (iconCategories[i] == 0 || GlobalData.UnlockedCubes[i]) icon.isUnlocked = true;

            image.sprite = Resources.Load<Sprite>(gameMode + "/icon" + i.ToString());
            image.color = colors[iconCategories[i]];
        }

        Resources.UnloadUnusedAssets();
    }
}
