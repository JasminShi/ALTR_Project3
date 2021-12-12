using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEndController : MonoBehaviour
{
    public float dissolvespeed = 5.0f;

    public Material dissolveMaterial;
    float currentDissolveAmount = 0.0f;
    bool dissolveEnabled = true;
    public AudioSource birdFinalEnding;
    public AudioSource crownFinalEnding;
    public XRSceneTransitionController xRSceneTransitionController;
    // bool portraitAudioEnabled = true;
    //public AudioSource PortraitAudio;


    Coroutine dissolveCoroutine;


    public void StartDissolve()
    {
        //if (!dissolveEnabled) return;
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }
        //if (enabled)
        dissolveCoroutine = StartCoroutine(Dissolve("_DissolveAmount", 1.0f));
    }

    //public void StopDissolve()
    //{
    //    if (dissolveCoroutine != null)
    //    {
    //        StopCoroutine(dissolveCoroutine);
    //    }
    //    if (enabled)
    //        dissolveCoroutine = StartCoroutine(Dissolve("_DissolveAmount", 0.5f));


    //}

    public void StartGlow()
    {
        //if (!dissolveEnabled) return;
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }
        //if (enabled)
        crownFinalEnding.Play();
        dissolveCoroutine = StartCoroutine(Dissolve("_GlowAmount", 1.0f));

        while (!crownFinalEnding.isPlaying)
        {
            xRSceneTransitionController.Transition();
        }
    }


    IEnumerator Dissolve(string prop, float target)
    {
        birdFinalEnding.Play();
        while (!Mathf.Approximately(currentDissolveAmount, target))
        {
            currentDissolveAmount = Mathf.MoveTowards(currentDissolveAmount, target, dissolvespeed * Time.deltaTime);
            dissolveMaterial.SetFloat(prop, currentDissolveAmount);
            yield return null;
        }
        //fade screen to black
        XRSceneTransitionManager.Instance.transitionSpeed = 1.0f;
        StartCoroutine(XRSceneTransitionManager.Instance.Fade(1.0f));

        while (!crownFinalEnding.isPlaying)
        {
            xRSceneTransitionController.Transition();
        }
    }
}


    //public void EnableDissolve()
    //{
    //    dissolveEnabled = true;
    //    //portraitAudioEnabled = true;
    //}

    //public void DisableDissolve()
    //{
    //    dissolveEnabled = false;
    //    //portraitAudioEnabled = false;
    //}
