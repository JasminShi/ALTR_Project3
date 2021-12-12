using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortraitManager : MonoBehaviour
{

    public FrameHighlightController FrameHighlight;

    public AudioSource PortraitAudio;

    //public GameObject Spotlight1;

    public UnityEvent OnFinished = new UnityEvent();

    bool hadStarted = false;

    public void playPortrait()
    {
        if (!hadStarted) 
        {
            //Spotlight.SetActive(true);
            FrameHighlight.StartHighlight();
            PortraitAudio.Play();
            StartCoroutine(waitForAudioToEnd());
            hadStarted = true;
        }
    }

    IEnumerator waitForAudioToEnd()
    {
        yield return new WaitForSeconds(PortraitAudio.clip.length);
        FrameHighlight.StopHighlight();
        OnFinished.Invoke();
    }

 
}
