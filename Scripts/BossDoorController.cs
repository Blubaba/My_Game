using System.Collections;
using UnityEngine;

public class BossDoorController : MonoBehaviour
{
    public float moveDistance = 7f; // 平移距离
    public float moveSpeed = 4f; // 平移速度
    public GameObject bulletPrefab; // 子弹Prefab
    public float fireInterval = 0.5f; // 发射子弹的时间间隔
    public PlayerController playerController; // 玩家控制器的引用

    private bool isPlayerMovementEnabled = true; // 控制玩家移动是否影响Boss DOOR
    private bool movingRight = true;
    private float leftBound;
    private float rightBound;
    private BossHealthManager healthManager;
    private Animator animator;

    private bool hasStartedMoving = false; // 标志是否已经开始移动

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
            if (!hasStartedMoving) // 如果尚未开始移动，检查玩家是否移动
            {
                if (isPlayerMovementEnabled && playerController.IsPlayerMoving())
                {
                    hasStartedMoving = true;
                }
            }
            else // 已经开始移动，无需再检查玩家移动状态
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
            if (!hasStartedMoving) // 如果尚未开始移动，检查玩家是否移动
            {
                if (isPlayerMovementEnabled && playerController.IsPlayerMoving())
                {
                    hasStartedMoving = true;
                }
            }
            else // 已经开始移动，无需再检查玩家移动状态
            {
                yield return new WaitForSeconds(fireInterval);
                float randomHeight = Random.Range(-2f, 2f); // 随机高度
                Vector3 bulletSpawnPosition = transform.position + new Vector3(0, randomHeight, 0);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
                bullet.GetComponent<Bullet2>().SetDirection(Vector2.left);
            }
            yield return null;
        }
    }

    // 设置是否允许玩家移动状态影响Boss DOOR
    public void SetPlayerMovementEnabled(bool enabled)
    {
        isPlayerMovementEnabled = enabled;
    }
}
