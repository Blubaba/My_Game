using System.Collections;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public Transform character;
    public Transform spike;
    public GameObject knifeObject;

    public float initialCharacterSpeed = 20f;
    public float initialSpikeSpeed = 20f;
    public float deceleration = 5f;

    public float characterStopY = -4f;
    public float spikeStopY = 4f;

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
        character.position = new Vector3(character.position.x, characterStopY, character.position.z);
        characterAnimator.SetTrigger("main");
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
        spike.position = new Vector3(spike.position.x, spikeStopY, spike.position.z);
        yield return new WaitForSeconds(0.5f);
        knifeObject.SetActive(true);
        knifeAnimator.SetTrigger("knife");
    }
}
