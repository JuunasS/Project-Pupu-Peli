using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePopup : MonoBehaviour
{
    public bool activateWhenNear; // If not true considered to be activetad by interacting

    public GameObject speechBubble;
    public bool dialogueShown = false;
    public float dialogueDuration;

    
    public Renderer _renderer;
    public Material[] mats;
    public Material outline; // The outline material to use in code
    public string outlineAssetName = "OutlineMaterial (Instance)";
    public float outlineThickness = 1.13f;

    public  void Start()
    {

        _renderer = GetComponent<Renderer>();
        mats = _renderer.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            Debug.Log(mats[i].name);
            if (mats[i].name == outlineAssetName)
            {
                outline = mats[i];
                break;
            }
        }
    }


    public void ActivateDialogue()
    {
        speechBubble.SetActive(true);
        StartCoroutine(DialogueTimer(dialogueDuration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dialogueShown) { return; }
        if (!activateWhenNear) { return; }

        if ((other.transform.tag == "Player"))
        {
            // Activate popup if is based on trigger
            ActivateDialogue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (dialogueShown) { return; };

        if (activateWhenNear) { return; }

        if ((other.transform.tag == "Player"))
        {
            outline.SetFloat("_Outline_Thickness", outlineThickness);
            if (Input.GetKey(KeyCode.E))
            {
                ActivateDialogue();
            }
        }
    }


    IEnumerator DialogueTimer(float time)
    {
        yield return new WaitForSeconds(time);
        dialogueShown = true;
        outline.SetFloat("_Outline_Thickness", 0);
        speechBubble?.SetActive(false);
    }

}
