using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;
    public GameObject gameOverPanel;

    void Start()
    {
        Debug.Log(Time.timeScale);
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverPanel.SetActive(false); // 隐藏GameOver界面
    }

    void OnTriggerEnter2D(Collider2D other)
{
    //if (other.CompareTag("Obstacle") || other.CompareTag("Bullet2"))
    if (other.CompareTag("Obstacle"))
    {
        TakeDamage();
    }

    if (other.CompareTag("Bullet2"))
    {
        //Debug.Log("Player collided with Bullet2!");
        TakeDamage();
        // 在这里可以添加额外的逻辑，例如扣血等操作
    }
}

    void TakeDamage()
    {
        currentHealth--;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthUI()
    {
        heart1.enabled = currentHealth >= 1;
        heart2.enabled = currentHealth >= 2;
        heart3.enabled = currentHealth >= 3;
        heart4.enabled = currentHealth >= 4;
        heart5.enabled = currentHealth >= 5;
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        // 使用协程延迟暂停游戏
        StartCoroutine(DelayBeforePause(0.7f));
    }

    IEnumerator DelayBeforePause(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 暂停游戏
        Time.timeScale = 0f;
    }
}
