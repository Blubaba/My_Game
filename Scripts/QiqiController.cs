using System.Collections;
using UnityEngine;

public class QiqiController : MonoBehaviour
{
    public float moveSpeed = 2f; // 移动速度
    private Vector3 targetPosition;
    private bool isWalking = false;
    private Animator animator;
    private bool hasDisappeared = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isWalking)
        {
            Debug.Log("Qiqi is walking.");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Mathf.Approximately(transform.position.x, targetPosition.x))
            {
                isWalking = false;
                animator.SetBool("isWalking", false);
                Debug.Log("Qiqi reached the target position.");
                StartCoroutine(Disappear());
            }
        }
    }

    public void StartWalking(Vector3 doorPosition)
    {
        targetPosition = new Vector3(doorPosition.x, transform.position.y, transform.position.z);
        isWalking = true;
        animator.SetBool("isWalking", true);
        Debug.Log("Qiqi started walking.");
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false); // 隐藏Qiqi
        hasDisappeared = true;
        Debug.Log("Qiqi disappeared.");
    }

    public bool HasDisappeared()
    {
        return hasDisappeared;
    }
}
