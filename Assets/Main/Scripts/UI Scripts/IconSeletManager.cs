using UnityEngine;
using UnityEngine.UI;

public class IconSeletManager : MonoBehaviour
{
    public SpriteRenderer player;

    [SerializeField] private GameObject slot;

    private int iconAmount = 37;

    void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);

        GameObject currentIcon;
        for (int i = 0; i < iconAmount; i++)
        {
            currentIcon = Instantiate(slot, transform);

            currentIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cubes/icon" + (i + 1).ToString());
            currentIcon.GetComponent<SelectIcon>().ID = i;
        }

        Resources.UnloadUnusedAssets();
    }
}
