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
        gameOverPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Obstacle"))
    {
        TakeDamage();
    }

    if (other.CompareTag("Bullet2"))
    {
        TakeDamage();
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
        StartCoroutine(DelayBeforePause(0.7f));
    }

    IEnumerator DelayBeforePause(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
    }
}
