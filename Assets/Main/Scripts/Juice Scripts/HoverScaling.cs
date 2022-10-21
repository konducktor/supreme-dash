using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverScaling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(1.1f, 2f)]
    public float scaleValue = 1.1f;

    [Range(0.1f, 0.9f)]
    public float smoothness = 0.5f;

    private Vector3 originalScale;
    private Vector3 currentScale;

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
