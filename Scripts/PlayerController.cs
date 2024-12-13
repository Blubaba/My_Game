using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    public GameObject bulletPrefab; // 子弹Prefab
    public GameObject gameOverPanel;
    public QiqiController qiqiController; // 引用 QiqiController
    public float positionTolerance = 0.1f; // 允许的误差范围

    private Rigidbody2D rb;
    private int jumpCount;
    private bool isGrounded;
    public Animator ani;
    private Vector3 originalScale;
    private bool isPlayerMoving = false;

    private bool gameIsOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();
        originalScale = transform.localScale; // 存储角色的初始比例
        gameOverPanel.SetActive(false); // 初始隐藏Game Over 面板

        if (qiqiController == null)
        {
            Debug.LogError("QiqiController reference is not set.");
        }
    }

    void Update()
    {
        ani.SetBool("walk", false);

        // 移动
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (move < 0)
        {
            ani.SetBool("walk", true);
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // 翻转角色朝向左边
        }
        else if (move > 0)
        {
            ani.SetBool("walk", true);
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // 翻转角色朝向右边
        }

        // 检查跳跃输入
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            ani.SetTrigger("jump");
        }

        // 根据垂直速度设置fall动画
        if (rb.velocity.y < 0)
        {
            ani.SetBool("fall", true);
        }
        else
        {
            ani.SetBool("fall", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FireBullet();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }

        // 检查是否达到目标位置
        if (qiqiController != null && qiqiController.HasDisappeared())
        {
            Debug.Log("Qiqi has disappeared.");
            if (Mathf.Abs(transform.position.x - qiqiController.transform.position.x) < positionTolerance)
            {
                Debug.Log("Player reached the target position. Loading End scene...");
                SceneManager.LoadScene("End");
            }
            else
            {
                Debug.Log("Player has not yet reached the target position.");
            }
        }
    }

    void FireBullet()
    {
        Vector3 bulletPosition = transform.position + new Vector3(transform.localScale.x > 0 ? 1 : -1, 0, 0); // 设置子弹发射位置
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        Vector2 bulletDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left; // 根据角色朝向设置子弹方向
        bullet.GetComponent<Bullet>().SetDirection(bulletDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; // 落地时重置跳跃次数
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TriggerGameOverAnimation()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            gameOverPanel.SetActive(true);
            Animator gameOverAnimator = gameOverPanel.GetComponent<Animator>();
            if (gameOverAnimator != null)
            {
                gameOverAnimator.SetTrigger("Show");
            }
        }
    }

    public bool IsPlayerMoving()
    {
        return isPlayerMoving;
    }
}
