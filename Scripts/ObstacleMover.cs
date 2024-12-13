using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    public void MoveUp()
    {
        StartCoroutine(MoveRoutine(Vector3.up));
    }

    public void MoveRight()
    {
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
    }
}
