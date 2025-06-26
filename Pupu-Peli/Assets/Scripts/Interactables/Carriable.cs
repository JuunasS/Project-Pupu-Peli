using System.Collections.Generic;
using UnityEngine;

public class Carriable : Interactable
{
    public GameObject interactText;
    public bool pickedUp;

    public DialoguePopup popup;
    public bool hasDialoguePopUp;

    public Collider col;
    public Rigidbody rigidBody;

    public bool bigItem; // True if object is a big item. Otherwise object is considered a small item.

    public Renderer _renderer;
    public Material[] mats;
    public Material outline; // The outline material to use in code
    public string outlineAssetName = "OutlineMaterial (Instance)";
    public float outlineThickness = 1.13f;


    public virtual void Start()
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
        Debug.Log("Set item for player!");
        if (bigItem)
        {
            player.GetComponent<Inventory>().SetBigItem(this);
        }
        else
        {
            player.GetComponent<Inventory>().SetSmallItem(this);
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        // If object has dialogue popup show it before letting player pick it up
        if (hasDialoguePopUp)
        {
            if(!popup.dialogueShown) { return; }
        }
        //Debug.Log("inRange: " + inRange + " pickedUp: " + pickedUp);
        if (pickedUp) { return; }

        if (other.transform.tag == "Player")
        {
            other.GetComponent<InteractionManager>().CheckInteractionDistance(this);

            if (other.GetComponent<InteractionManager>().currentInteraction != this)
            {
                interactText.SetActive(false);
                outline.SetFloat("_Outline_Thickness", 0);
                return;
            }


            outline.SetFloat("_Outline_Thickness", outlineThickness);
            interactText.SetActive(true);
        }
    }


    public virtual void OnTriggerExit(Collider other)
    {
        if (pickedUp) { return; }
        if (other.transform.tag == "Player")
        {
            interactText.SetActive(false);
            if (other.GetComponent<InteractionManager>().currentInteraction == this)
            {
                other.GetComponent<InteractionManager>().currentInteraction = null;
                outline.SetFloat("_Outline_Thickness", 0);
            }
        }
    }


    public virtual void PickUpSuccess()
    {
        interactText.SetActive(false);
        pickedUp = true;

        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        col.enabled = false;
        outline.SetFloat("_Outline_Thickness", 0);
    }

    public virtual void PickUpFailed()
    {
        pickedUp = false;
    }

    public virtual void DropItem()
    {
        this.transform.SetParent(null);
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        col.enabled = true;
        pickedUp = false;
    }

}
