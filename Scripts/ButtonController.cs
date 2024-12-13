using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // 调用此方法以加载目标场景
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
