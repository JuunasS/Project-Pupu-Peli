using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class thoughtBubble : MonoBehaviour
{
    SpriteRenderer[] renders;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        renders = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(renders.Length);
    }

    public void drawShape(List<Herb> toSort)
    {
        Debug.Log("Drawing herbs of amount: " + toSort.Count);
        List<Sprite> sorted = new();

        foreach (var herb in toSort)
        {
            if (!herb.isReverse)
                sorted.Add(herb.image);
        }
        foreach (var herb in toSort)
        {
            if (herb.isReverse)
                sorted.Add(herb.reverse);
        }
        StartCoroutine(showShape(sorted));
    }

    IEnumerator showShape(List<Sprite> sorted)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        for (int i = 0; i < sorted.Count; i++)
        {
            Debug.Log("Putting herb image in: " + renders[i]);
            renders[i + 1].enabled = true;
            renders[i + 1].sprite = sorted[i];
        }

        yield return new WaitForSeconds(5);

        foreach (var ren in renders)
            ren.enabled = false;

        StopCoroutine(showShape(sorted));
    }
}