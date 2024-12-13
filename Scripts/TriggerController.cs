using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public List<GameObject> obstacles;
    public List<GameObject> additionalObstacles;
    public TriggerController nextTriggerController;
    public TriggerController additionalTriggerController;
    public bool isTriggered = false;
    public bool moveRight = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} triggered by Player");
            if (!isTriggered && gameObject.name == "检测-1")
            {
                isTriggered = true;
                StartCoroutine(MoveObstacles());
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
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void MoveAdditionalObstacles()
    {
        foreach (GameObject additionalObstacle in additionalObstacles)
        {
            if (additionalObstacle != null)
            {
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
                
            }
        }
    }
}
