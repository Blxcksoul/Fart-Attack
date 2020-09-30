using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    public static SpeechBubbleManager instance;
    public GameObject speechBubblePrefab;
    GameObject speechBubbleRef;
    public string speechBubbleWords;

    Text speechBubbleText;
    Animator speechBubbleAnim;
    Coroutine speechBubbleDisabler;



    private void Awake()
    {
        //Laziness, replaces old instances because it is not meant to be singleton, it is 1 static per level, not 1 static per entire game
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;

        Transform playerCanvas = GameObject.Find("Player Canvas").transform;
        speechBubbleRef = Instantiate(speechBubblePrefab, playerCanvas);

        //Get in children, so that you can just keep reference of the parent, background image of speech bubble, dont need to assign 2 things.
        speechBubbleText = speechBubbleRef.GetComponentInChildren<Text>();
        speechBubbleAnim = speechBubbleRef.GetComponentInChildren<Animator>();

        speechBubbleRef.SetActive(false);
        speechBubbleText.text = speechBubbleWords;
    }

    public void PlaySpeechBubble()
    {
        speechBubbleRef.SetActive(true);
        speechBubbleAnim.Play("SpeechBubble");

        if (speechBubbleDisabler != null)
        {
            StopCoroutine(speechBubbleDisabler);
        }

        speechBubbleDisabler = StartCoroutine(DisableAfterAnim());
    }

    IEnumerator DisableAfterAnim()
    {
        yield return new WaitForSeconds(speechBubbleAnim.runtimeAnimatorController.animationClips[0].length);
        speechBubbleRef.SetActive(false);
    }
}
