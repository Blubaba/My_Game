using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject youWinPanel;

    private Rigidbody2D rb;
    private bool isGrounded;
    public Animator ani;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();
        originalScale = transform.localScale;
        youWinPanel.SetActive(false);
    }

    void Update()
    {
        ani.SetBool(name:"walk", value:false);

        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (move < 0)
        {
            ani.SetBool("walk", true);
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); //翻转角色朝向左边
        }
        else if (move > 0)
        {
            ani.SetBool("walk", true);
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); //翻转角色朝向右边
        }

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
    }

    void FireBullet()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("qiqi"))
        {
            ShowYouWinPanel();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void ShowYouWinPanel()
    {
        youWinPanel.SetActive(true);
        Animator gameOverAnimator = youWinPanel.GetComponent<Animator>();
        if (gameOverAnimator != null)
        {
            gameOverAnimator.SetTrigger("Show");
        }
    }
}
