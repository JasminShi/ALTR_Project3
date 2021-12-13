using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DreamTransfer : MonoBehaviour
{

    public UnityEvent OnFinished = new UnityEvent();

    public AudioSource dreamAudio;

    public void Update()
    {
       if(!dreamAudio.isPlaying)
        {
            OnFinished.Invoke();
        }
    }
}



//IEnumerator waitForDreamAudioToEnd()
//    {
//        yield return new WaitForSeconds(DreamAudio.clip.length);
        
//        OnFinished.Invoke();
       
//    }

