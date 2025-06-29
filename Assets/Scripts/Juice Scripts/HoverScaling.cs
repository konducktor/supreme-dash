using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverScaling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0.5f, 2f)]
    public float scaleValue = 1.1f;

    [Range(0f, 1f)]
    public float smoothTime = 0.1f;

    private Vector3 originalScale, currentScale, velocity;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        originalScale = transform.localScale;
        currentScale = originalScale;

        velocity = Vector3.zero;

        GetComponent<Button>().onClick.AddListener(Click);
    }

    void Update()
    {
        if (currentScale == transform.localScale) return;

        transform.localScale = Vector3.SmoothDamp(transform.localScale, currentScale, ref velocity, smoothTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentScale *= scaleValue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentScale = originalScale;
    }

    void OnDisable()
    {
        transform.localScale = originalScale;
        currentScale = originalScale;
    }

    public void Click()
    {
        audioManager.Play("Click");
    }
}
