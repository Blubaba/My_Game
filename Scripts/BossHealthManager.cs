using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public int maxKeys = 5;
    public int hitsPerKey = 4;
    public int currentHealth;
    private int currentKeyHits;
    private Animator animator;

    public Image key1;
    public Image key2;
    public Image key3;
    public Image key4;
    public Image key5;

    public BossDoorController bossDoorController;
    public QiqiController qiqi; // 引用 QiqiController

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxKeys * hitsPerKey;
        currentKeyHits = 0;
        bossDoorController = GetComponent<BossDoorController>();
        UpdateHealthUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentKeyHits += damage;

        if (currentKeyHits >= hitsPerKey)
        {
            currentKeyHits = 0;
            RemoveKey();
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            qiqi.StartWalking(transform.position); // 传递DOOR的当前位置
            bossDoorController.StopAllCoroutines(); // 停止所有协程
            // 执行其他死亡逻辑，例如播放动画、销毁对象等
            animator.SetTrigger("Open2"); // 触发Open2动画
            //qiqi.StartWalking(transform.position); // 传递DOOR的当前位置
        }
    }

    void RemoveKey()
    {
        if (key5.enabled) key5.enabled = false;
        else if (key4.enabled) key4.enabled = false;
        else if (key3.enabled) key3.enabled = false;
        else if (key2.enabled) key2.enabled = false;
        else if (key1.enabled) key1.enabled = false;
    }

    void UpdateHealthUI()
    {
        key1.enabled = currentHealth >= 1 * hitsPerKey;
        key2.enabled = currentHealth >= 2 * hitsPerKey;
        key3.enabled = currentHealth >= 3 * hitsPerKey;
        key4.enabled = currentHealth >= 4 * hitsPerKey;
        key5.enabled = currentHealth >= 5 * hitsPerKey;
    }
}
