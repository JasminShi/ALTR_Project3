using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassController : MonoBehaviour
{
    public GameObject glassfilling;

    public AudioSource WaterDrink;

    public void playDrinkWater()
    {
        WaterDrink.Play();
    }

    public void HideFilling()
    {
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        glassfilling.SetActive(false);
        yield return null;
    }
}
