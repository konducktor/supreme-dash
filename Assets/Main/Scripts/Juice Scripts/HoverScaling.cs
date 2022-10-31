using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverScaling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0.5f, 2f)]
    public float scaleValue = 1.1f;

    [Range(1f, 10f)]
    public float smoothness = 10f;

    private Vector3 originalScale, currentScale;

    void Start()
    {
        originalScale = transform.localScale;
        currentScale = originalScale;
    }

    void Update()
    {
        if (currentScale == transform.localScale) return;

        transform.localScale += (currentScale - transform.localScale) * smoothness * Time.deltaTime * 10f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentScale *= scaleValue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentScale = originalScale;
    }
}
