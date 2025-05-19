using System.Collections;
using UnityEngine;

public class DialoguePopup : MonoBehaviour
{
    public bool activateWhenNear; // If not true considered to be activetad by interacting

    public GameObject speechBubble;
    public bool dialogueShown = false;
    public float dialogueDuration;

    public void ActivateDialogue()
    {
        speechBubble.SetActive(true);
        StartCoroutine(DialogueTimer(dialogueDuration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(dialogueShown) { return; }
        if (!activateWhenNear) { return; }
        if ((other.transform.tag == "Player"))
        {
            // Activate popup if is based on trigger
            ActivateDialogue();
        }
    }

    IEnumerator DialogueTimer(float time)
    {
        yield return new WaitForSeconds(time);
        dialogueShown = true;
        speechBubble?.SetActive(false);
    }

}
