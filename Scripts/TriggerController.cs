using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public List<GameObject> obstacles; // 障碍物列表
    public List<GameObject> additionalObstacles; // 额外的障碍物列表
    public TriggerController nextTriggerController; // 检测-2的TriggerController
    public TriggerController additionalTriggerController; // 检测-3的TriggerController
    public bool isTriggered = false; // 标志检测是否已触发
    public bool moveRight = false; // 标志障碍物是否向右平移

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} triggered by Player");
            if (!isTriggered && gameObject.name == "检测-1")
            {
                isTriggered = true;
                StartCoroutine(MoveObstacles());
                // 设置检测-2和检测-3的isTriggered为true
                if (nextTriggerController != null)
                {
                    nextTriggerController.isTriggered = true;
                }
                if (additionalTriggerController != null)
                {
                    additionalTriggerController.isTriggered = true;
                }
            }
            else if (isTriggered && (gameObject.name == "检测-2" || gameObject.name == "检测-3"))
            {
                MoveAdditionalObstacles();
            }
        }
    }

    IEnumerator MoveObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();
            if (mover != null)
            {
                mover.MoveUp();
                yield return new WaitForSeconds(0.1f); // 设置障碍物移动的时间间隔
            }
        }
    }

    void MoveAdditionalObstacles()
    {
        foreach (GameObject additionalObstacle in additionalObstacles)
        {
            if (additionalObstacle != null)
            {
                //Debug.Log("Moving additional obstacle.");
                ObstacleMover mover = additionalObstacle.GetComponent<ObstacleMover>();
                if (mover != null)
                {
                    if (moveRight)
                    {
                        mover.MoveRight();
                    }
                    else
                    {
                        mover.MoveUp();
                    }
                }
                else
                {
                    Debug.LogWarning("No ObstacleMover component found on additional obstacle.");
                }
            }
            else
            {
                //Debug.LogWarning("Additional obstacle is not assigned.");
            }
        }
    }
}
