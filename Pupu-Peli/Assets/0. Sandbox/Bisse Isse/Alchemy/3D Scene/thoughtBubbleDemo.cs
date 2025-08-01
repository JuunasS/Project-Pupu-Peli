using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class thoughtBubbleDemo : MonoBehaviour
{
    Image[] renders;
    public Animator stateDrivenCamera;
    public AlchemyMaster AlchemyMaster;

    void Awake()
    {
        GetComponent<Image>().enabled = false;
        renders = GetComponentsInChildren<Image>();
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
        GetComponent<Image>().enabled = true;
        GetComponent<Animator>().Play("thoughtBubbleAnimationDemo");
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < sorted.Count; i++)
        {
            Debug.Log("Putting herb image in: " + renders[i]);
            renders[i + 1].enabled = true;
            renders[i + 1].sprite = sorted[i];
        }

        yield return new WaitForSeconds(3);

        foreach (var ren in renders)
            ren.enabled = false;

        StopCoroutine(showShape(sorted));
    }

    public void startCutscene()
    {
        if (stateDrivenCamera !=  null)
            stateDrivenCamera.Play("Cutscene");
    }

    public void endCutscene()
    {
        if (stateDrivenCamera != null)
            stateDrivenCamera.Play("Gazebo");
        if (AlchemyMaster != null)
            AlchemyMaster.exitPuzzle();
    }
}