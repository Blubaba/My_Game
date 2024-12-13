using System.Collections;
using UnityEngine;

public class BossDoorController : MonoBehaviour
{
    public float moveDistance = 7f; //平移距离
    public float moveSpeed = 4f; //平移速度
    public GameObject bulletPrefab; //创建子弹
    public float fireInterval = 0.5f; //子弹发射间隔
    public PlayerController playerController;

    private bool isPlayerMovementEnabled = true;
    private bool movingRight = true;
    private float leftBound;
    private float rightBound;
    private BossHealthManager healthManager;
    private Animator animator;

    private bool hasStartedMoving = false;

    void Start()
    {
        Debug.Log("Boss door initialized.");
        leftBound = transform.position.x - moveDistance;
        rightBound = transform.position.x + moveDistance;
        healthManager = GetComponent<BossHealthManager>();
        animator = GetComponent<Animator>();

        StartCoroutine(MoveHorizontally());
        StartCoroutine(FireBullet());
    }

    IEnumerator MoveHorizontally()
    {
        Debug.Log("MoveHorizontally coroutine started.");

        while (healthManager.currentHealth > 0)
        {
            if (!hasStartedMoving)
            {
                if (isPlayerMovementEnabled && playerController.IsPlayerMoving())
                {
                    hasStartedMoving = true;
                }
            }
            else
            {
                if (movingRight)
                {
                    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                    Debug.Log("Moving right...");
                    if (transform.position.x >= rightBound)
                    {
                        movingRight = false;
                        Debug.Log("Reached right bound, moving left now.");
                    }
                }
                else
                {
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                    Debug.Log("Moving left...");
                    if (transform.position.x <= leftBound)
                    {
                        movingRight = true;
                        Debug.Log("Reached left bound, moving right now.");
                    }
                }
            }
            yield return null;
        }

        Debug.Log("MoveHorizontally coroutine finished.");
    }

    IEnumerator FireBullet()
    {
        while (healthManager.currentHealth > 0)
        {
            if (!hasStartedMoving)
            {
                if (isPlayerMovementEnabled && playerController.IsPlayerMoving())
                {
                    hasStartedMoving = true;
                }
            }
            else
            {
                yield return new WaitForSeconds(fireInterval);
                float randomHeight = Random.Range(-2f, 2f);
                Vector3 bulletSpawnPosition = transform.position + new Vector3(0, randomHeight, 0);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
                bullet.GetComponent<Bullet2>().SetDirection(Vector2.left);
            }
            yield return null;
        }
    }

    public void SetPlayerMovementEnabled(bool enabled)
    {
        isPlayerMovementEnabled = enabled;
    }
}
