using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class fadeToBlack : MonoBehaviour
{
    public float fadeTime;
    Image img;

    [Button("Fade to Black")]
    public void fade()
    {
        img = GetComponent<Image>();
        StartCoroutine(fadeAnim(true));
    }

    [Button("Fade Back")]
    public void fadeBack()
    {
        img = GetComponent<Image>();
        StartCoroutine(fadeAnim(false));
    }

    public IEnumerator fadeAnim(bool black)
    {
        Color temp = Color.black;
        for (float i = 0; i < fadeTime; i += Time.deltaTime)
        {
            temp.a = (black == true) ? i / fadeTime : 1 - (i / fadeTime);
            img.color = temp;
            yield return null;
        }
        temp.a = (black == true) ? 1 : 0;
        img.color = temp;
        StopCoroutine(fadeAnim(true));
    }
}
