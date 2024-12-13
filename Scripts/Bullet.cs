using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime); // 在指定时间后销毁子弹
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // 子弹平移
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // 设置子弹的方向
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // 碰到任何物体时销毁子弹

        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Door"))
    {
        Destroy(gameObject); // 碰到障碍物或门时销毁子弹
    }
    }

    
}
