using UnityEngine;
using UnityEngine.UI;

public class PageScroller : MonoBehaviour
{
    [SerializeField] private Button nextButton, previousButton;
    [SerializeField] private string folder;
    [SerializeField] private int amountOfImages;
    [SerializeField] Image image;

    private int imageIndex;

    void OnEnable()
    {
        imageIndex = 0;
        RefreshImage();

        nextButton.onClick.AddListener(NextButton);
        previousButton.onClick.AddListener(PreviousButton);
    }

    void NextButton()
    {
        if (imageIndex < amountOfImages - 1)
        {
            imageIndex++;
            RefreshImage();
        }
    }

    void PreviousButton()
    {
        if (imageIndex > 0)
        {
            imageIndex--;
            RefreshImage();
        }
    }

    void RefreshImage()
    {
        image.sprite = Resources.Load<Sprite>(folder + imageIndex);

        Resources.UnloadUnusedAssets();
    }
}
