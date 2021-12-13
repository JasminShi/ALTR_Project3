using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class BirdController : MonoBehaviour
{
    //private bool step1Start = false;
    //private bool step1Complete = false;
    //private bool step2Complete = false;
    //private bool step3Complete = false;


    // waterGrab first, portraitActive next

    //public bool waterGrab;
    //public bool waterExit;

    //public bool portraitActive;
    //public bool portraitDisactive;

    public Transform userLocation;

    public NavMeshAgent Agent;

    public Transform CTA_portriatLocation;
    public Transform CTA_leaveLocation;

    public AudioSource birdDialogue1;
    public AudioSource birdDialogue2;
    public AudioSource birdDialogue3;

    public float speed = 1.0f;

    public Material dissolveMaterial;
    float currentDissolveAmount = 0.0f;

    //reference to check whether each of the actions are complete
    //public checkWaterState waterState;
    //public checkPortraitState portraitState;

    AudioSource AudioClip;

    //IEnumerator Start()
    //{

    //    CTA_drink();

    //    yield return new WaitForSeconds(20);
    //    CTA_portrait();

    //    yield return WaitForBirdToReachDesitnation();

    //    yield return new WaitForSeconds(20);
    //    CTA_leave();
    //}

    public UnityEvent OnFinished = new UnityEvent();

    public HighlightController BirdDissolve;

    private void Start()
    {
        CTA_drink();
    }


    public void CTA_drink()
    {
        birdDialogue1.Play();   

    }


    public void CTA_portrait()
    {

        Agent.SetDestination(CTA_portriatLocation.position);
        StartCoroutine(PlayBirdDialogue2());
        StartCoroutine(waitForAudio2ToEnd());

    }

    public void CTA_leave()
    {

        Agent.SetDestination(CTA_leaveLocation.position);
        StartCoroutine(PlayBirdDialogue3());
        StartCoroutine(waitForAudio3ToEnd());
        StartCoroutine(Dissolve(0.9f));
    }

    IEnumerator PlayBirdDialogue2()
    {
        yield return WaitForBirdToReachDesitnation();
        yield return new WaitForSeconds(1);
        birdDialogue2.Play();
    }

    IEnumerator PlayBirdDialogue3()
    {
        yield return WaitForBirdToReachDesitnation();

        transform.LookAt(userLocation.position, Vector3.up);

        birdDialogue3.Play();

    }


    IEnumerator WaitForBirdToReachDesitnation()
    {
        yield return new WaitForSeconds(0);

        while (Agent.pathPending) yield return null;

        while (Agent.remainingDistance > Agent.stoppingDistance)
        {
            yield return null;
        }
    }


    //IEnumerator AlignWith(Transform rotation)
    //{
    //    while(Vector3.Dot(transform.forward, rotation.forward) < 1.0)
    //    {
    //        transform.LookAt(rotation, rotation.position);
    //        yield return null;
    //    }
    //}

    IEnumerator waitForAudio2ToEnd()
    {
        yield return new WaitForSeconds(birdDialogue2.clip.length);
        //FrameHighlight.StopHighlight();
        OnFinished.Invoke();
    }

    IEnumerator waitForAudio3ToEnd()
    {
        yield return new WaitForSeconds(birdDialogue3.clip.length);
    }



    IEnumerator Dissolve(float target)
    {
           
            currentDissolveAmount = Mathf.MoveTowards(currentDissolveAmount, target, speed * Time.deltaTime);
            dissolveMaterial.SetFloat("_DissolveAmount", currentDissolveAmount);
            yield return null;

    }
}
