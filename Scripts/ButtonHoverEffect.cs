using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float transitionSpeed = 5f;

    private Button button;
    private RectTransform rectTransform;
    private Image image;

    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        image.color = normalColor;
        rectTransform.localScale = normalScale;
    }

    void Update()
    {
        if (IsMouseOver())
        {
            image.color = Color.Lerp(image.color, hoverColor, transitionSpeed * Time.deltaTime);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, hoverScale, transitionSpeed * Time.deltaTime);
        }
        else
        {
            image.color = Color.Lerp(image.color, normalColor, transitionSpeed * Time.deltaTime);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, normalScale, transitionSpeed * Time.deltaTime);
        }
    }

    private bool IsMouseOver()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint);
        return rectTransform.rect.Contains(localPoint);
    }
}
