using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePopup : Interactable
{
    public bool activateWhenNear; // If not true considered to be activetad by interacting

    public GameObject speechBubble;
    public bool dialogueActive = false;
    public bool dialogueShown = false;
    public float dialogueDuration;


    public Renderer _renderer;
    public Material[] mats;
    public Material outline; // The outline material to use in code
    public string outlineAssetName = "OutlineMaterial (Instance)";
    public float outlineThickness = 1.13f;

    public void Start()
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

    public override void Interact(GameObject player)
    {
        if (dialogueActive) { return; }

        if (dialogueShown) { return; };

        if (activateWhenNear) { return; }

        Debug.Log("Dialogue interaction successfull");
        ActivateDialogue();

    }


    public void ActivateDialogue()
    {
        dialogueActive = true;
        speechBubble.SetActive(true);
        StartCoroutine(DialogueTimer(dialogueDuration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dialogueActive) { return; }
        if (dialogueShown) { return; }
        if (!activateWhenNear) { return; }

        if ((other.transform.tag == "Player"))
        {
            outline.SetFloat("_Outline_Thickness", outlineThickness);
            // Activate popup if is based on trigger
            ActivateDialogue();
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        if (dialogueActive) { return; }
        if (dialogueShown) { return; };
        if (activateWhenNear) { return; }

        if ((other.transform.tag == "Player"))
        {
            outline.SetFloat("_Outline_Thickness", outlineThickness);
            other.GetComponent<InteractionManager>().CheckInteractionDistance(this);
            /*
             if (Input.GetKey(KeyCode.E))
                {
                    ActivateDialogue();
                }
            */
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
