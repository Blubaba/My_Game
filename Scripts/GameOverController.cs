using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}
