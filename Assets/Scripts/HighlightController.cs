using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    public Material highlightableMaterial;
    public float highlightSpeed = 1f;

    float currentHighlightAmount = 0.1f;

    public void StartHighlight()
    {
        StartCoroutine(Highlight(0.9f));
    }

    IEnumerator Highlight(float target)
    {
        while (!Mathf.Approximately(currentHighlightAmount, target))
        {
            currentHighlightAmount = Mathf.MoveTowards(currentHighlightAmount, target, highlightSpeed * Time.deltaTime);

            highlightableMaterial.SetFloat("_GlowAmount", currentHighlightAmount);

            yield return null;
        }
    }

}
