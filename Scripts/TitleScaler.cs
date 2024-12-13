using System.Collections;
using UnityEngine;

public class TitleScaler : MonoBehaviour
{
    public Transform title; // 标题图片
    public float scaleDuration = 1f; // 完成一个缩放周期所需的时间
    public float minScaleFactor = 0.8f; // 最小缩放比例
    public float maxScaleFactor = 1.2f; // 最大缩放比例
    private Vector3 initialScale;

    void Start()
    {
        initialScale = title.localScale;
        StartCoroutine(ScaleTitle());
    }

    IEnumerator ScaleTitle()
    {
        while (true)
        {
            yield return StartCoroutine(ScaleOverTime(minScaleFactor, maxScaleFactor, scaleDuration / 2));
            yield return StartCoroutine(ScaleOverTime(maxScaleFactor, minScaleFactor, scaleDuration / 2));
        }
    }

    IEnumerator ScaleOverTime(float startScaleFactor, float endScaleFactor, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float scale = Mathf.Lerp(startScaleFactor, endScaleFactor, elapsed / duration);
            title.localScale = initialScale * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }
        title.localScale = initialScale * endScaleFactor;
    }
}
