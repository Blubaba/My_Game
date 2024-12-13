using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveDistance = 5f; // 平移距离
    public float moveSpeed = 2f; // 平移速度

    public void MoveUp()
    {
        //Debug.Log($"{gameObject.name} is moving up.");
        StartCoroutine(MoveRoutine(Vector3.up));
    }

    public void MoveRight()
    {
        //Debug.Log($"{gameObject.name} is moving right.");
        StartCoroutine(MoveRoutine(Vector3.right));
    }

    IEnumerator MoveRoutine(Vector3 direction)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * moveDistance;

        float elapsedTime = 0f;
        while (elapsedTime < moveDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime * moveSpeed) / moveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        //Debug.Log($"{gameObject.name} finished moving.");
    }
}
