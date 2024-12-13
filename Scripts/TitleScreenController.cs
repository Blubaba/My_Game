using System.Collections;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public Transform character; // 人物
    public Transform spike; // 尖刺
    public GameObject knifeObject; // 刀光对象

    public float initialCharacterSpeed = 20f;
    public float initialSpikeSpeed = 20f;
    public float deceleration = 5f;

    public float characterStopY = -4f; // 人物停止移动的Y坐标
    public float spikeStopY = 4f; // 尖刺停止移动的Y坐标

    private Animator characterAnimator;
    private Animator knifeAnimator;

    void Start()
    {
        characterAnimator = character.GetComponent<Animator>();
        knifeAnimator = knifeObject.GetComponent<Animator>();
        knifeObject.SetActive(false);

        StartCoroutine(MoveCharacter());
        StartCoroutine(MoveSpike());
    }

    IEnumerator MoveCharacter()
    {
        float speed = initialCharacterSpeed;
        while (character.position.y > characterStopY)
        {
            character.position += Vector3.down * speed * Time.deltaTime;
            speed -= deceleration * Time.deltaTime;
            yield return null;
        }
        character.position = new Vector3(character.position.x, characterStopY, character.position.z); // 强制设置人物位置
        characterAnimator.SetTrigger("main"); // 触发人物动画
    }

    IEnumerator MoveSpike()
    {
        float speed = initialSpikeSpeed;
        while (spike.position.y < spikeStopY)
        {
            spike.position += Vector3.up * speed * Time.deltaTime;
            speed -= deceleration * Time.deltaTime;
            yield return null;
        }
        spike.position = new Vector3(spike.position.x, spikeStopY, spike.position.z); // 强制设置尖刺位置
        yield return new WaitForSeconds(0.5f); // 等待0.5秒
        knifeObject.SetActive(true);
        knifeAnimator.SetTrigger("knife"); // 触发刀光动画
    }
}
