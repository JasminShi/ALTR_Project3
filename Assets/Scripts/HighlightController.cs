using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HighlightController : MonoBehaviour
{
    public float speed = 1.0f;

    public Material dissolveMaterial;
    float currentDissolveAmount = 0.0f;
    public AudioSource birdFinalEnding;
    public AudioSource crownFinalEnding;
    //public XRSceneTransitionController xRSceneTransitionController;
    //public XRSceneTransitionManager xRSceneTransitionManager;

    public GameObject crown;
    public GameObject bird;

    public NPCController nPCController;
    //public Material highlightableMaterial;
    //public float highlightSpeed = 1f;

    float currentHighlightAmount = 0.1f;

    public UnityEvent OnFinished = new UnityEvent();

    public void StartHighlight()
    {
        StartCoroutine(Highlight(0.9f));
    }

    public void StartDissolve()
    {
        StartCoroutine(Dissolve(0.9f));
    }

    IEnumerator Highlight(float target)
    {
        nPCController.npcDialogue.Stop();
        while (!Mathf.Approximately(currentHighlightAmount, target))
        {
            crownFinalEnding.Play();
            currentHighlightAmount = Mathf.MoveTowards(currentHighlightAmount, target, speed * Time.deltaTime);
            dissolveMaterial.SetFloat("_GlowAmount", currentHighlightAmount);
            yield return null;
        }
        bird.SetActive(false);
        XRSceneTransitionManager.Instance.transitionSpeed = 1.0f;
        StartCoroutine(XRSceneTransitionManager.Instance.Fade(1.0f));
        //Fade to Lobby after audio played
        StartCoroutine(waitForcrownFinalEndingToEnd());

    }

    IEnumerator Dissolve(float target)
    {
        nPCController.npcDialogue.Stop();
        while (!Mathf.Approximately(currentDissolveAmount, target))
        {
            birdFinalEnding.Play();
            currentDissolveAmount = Mathf.MoveTowards(currentDissolveAmount, target, speed * Time.deltaTime);
            dissolveMaterial.SetFloat("_DissolveAmount", currentDissolveAmount);
            yield return null;
        }
        crown.SetActive(false);

        StartCoroutine(waitForAnimationToEnd());
        XRSceneTransitionManager.Instance.transitionSpeed = 1.0f;
        StartCoroutine(XRSceneTransitionManager.Instance.Fade(1.0f));

        StartCoroutine(waitForbirdFinalEndingToEnd());

    }

    IEnumerator waitForcrownFinalEndingToEnd()
    {
        yield return new WaitForSeconds(crownFinalEnding.clip.length);
        //FrameHighlight.StopHighlight();
        OnFinished.Invoke();
        //xRSceneTransitionController.Transition();
        //xRSceneTransitionManager.TransitionTo("Lobby");
    }

    IEnumerator waitForbirdFinalEndingToEnd()
    {
        yield return new WaitForSeconds(birdFinalEnding.clip.length);
        //FrameHighlight.StopHighlight();
        OnFinished.Invoke();
       
    }

    IEnumerator waitForAnimationToEnd()
    {
        yield return new WaitForSeconds(1);

    }
}