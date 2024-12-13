using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    public Color normalColor = Color.white; // 按钮的初始颜色
    public Color hoverColor = Color.gray; // 悬停时按钮的颜色
    public Vector3 normalScale = new Vector3(1f, 1f, 1f); // 按钮的初始大小
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // 悬停时按钮的大小
    public float transitionSpeed = 5f; // 颜色和大小变化的速度

    private Button button;
    private RectTransform rectTransform;
    private Image image;

    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        // 初始化按钮颜色和大小
        image.color = normalColor;
        rectTransform.localScale = normalScale;
    }

    void Update()
    {
        if (IsMouseOver())
        {
            // 改变按钮颜色和大小
            image.color = Color.Lerp(image.color, hoverColor, transitionSpeed * Time.deltaTime);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, hoverScale, transitionSpeed * Time.deltaTime);
        }
        else
        {
            // 恢复按钮颜色和大小
            image.color = Color.Lerp(image.color, normalColor, transitionSpeed * Time.deltaTime);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, normalScale, transitionSpeed * Time.deltaTime);
        }
    }

    private bool IsMouseOver()
    {
        // 检查鼠标是否悬停在按钮上
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint);
        return rectTransform.rect.Contains(localPoint);
    }
}
