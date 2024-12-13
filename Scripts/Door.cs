using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            OpenDoor();
        }
        else if (other.CompareTag("Player") && isOpen)
        {
            LoadNextLevel();
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.SetTrigger("OpenDoor");
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("Scenes2");
    }
}
