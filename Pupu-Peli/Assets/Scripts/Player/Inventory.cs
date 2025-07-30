using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{

    // Big item data and positions
    public Transform bigItemHolder;
    public Carriable bigItem;

    // Small item data and positions
    public Transform rightHand;
    public Transform leftHand;
    public List<Carriable> smallItems;

    public InteractionManager interactionManager;
    public GameObject activeInteraction = null;
    public float interactionCD; // Cooldown for interacting with objects
    public float interactionTImer;

    private Animator animator;

    private void Start()
    {
        interactionManager = this.gameObject.GetComponent<InteractionManager>();
        animator = GetComponent<Movement>().model;
    }
    
    private void Update()
    {
        if (bigItem != null)
            SetItemLocation();
    }
    
    public void SetBigItem(Carriable obj)
    {
        if (Time.time < interactionTImer) { return; }
        interactionTImer = Time.time + interactionCD;
        Debug.Log("Setting big item!");
        if (obj != interactionManager.currentInteraction) { return; }

        Debug.Log(smallItems.Count);
        if (smallItems.Count > 0) { obj.PickUpFailed(); return; }
        obj.PickUpSuccess();

        GetComponent<Movement>().model.SetBool("basket", true);

        // ckeck if player has room for item
        bigItem = obj;
        obj.transform.SetParent(bigItemHolder);
        obj.transform.position = bigItemHolder.position;
        obj.transform.rotation = bigItemHolder.rotation;
        activeInteraction = null;
        StartCoroutine(WaitForNextActive());
    }


    public void SetSmallItem(Carriable obj)
    {
        if (Time.time < interactionTImer) { return; }
        interactionTImer = Time.time + interactionCD;
        Debug.Log("Setting small item!");
        if (obj != interactionManager.currentInteraction) { return; }

        if (bigItem?.GetComponent<Basket>())
        {
            obj.PickUpSuccess();
            bigItem.GetComponent<Basket>().AddItem(obj);
            StartCoroutine(WaitForNextActive());
        }
        else
        {
            // ckeck if player has room for item
            if (bigItem != null && smallItems.Count <= 2) { obj.PickUpFailed(); return; }

            obj.PickUpSuccess();
            smallItems.Add(obj);

            if (rightHand.childCount == 0)
            {
                obj.transform.SetParent(rightHand);
                obj.transform.position = rightHand.position;
                obj.transform.rotation = rightHand.rotation;
            }
            else if (leftHand.childCount == 0)
            {
                obj.transform.SetParent(leftHand);
                obj.transform.position = leftHand.position;
                obj.transform.rotation = leftHand.rotation;
            }
            else
            {
                Debug.Log("Hands full!");
            }
            StartCoroutine(WaitForNextActive());
        }

    }

    public void SetItemLocation()
    {
        bigItemHolder.transform.position = animator.GetBoneTransform(HumanBodyBones.RightHand).position;
        bigItemHolder.transform.localPosition += bigItem.holdingPivot;
        bigItemHolder.localEulerAngles = bigItem.rotationPivot;
    }
    /*
    public void CheckInteractionDistance(GameObject newObj)
    {
        if (activeInteraction == null) { activeInteraction = newObj; return; }
        float distance1 = Vector3.Distance(this.transform.position, newObj.transform.position);
        float distance2 = Vector3.Distance(this.transform.position, activeInteraction.transform.position);
        if (distance1 < distance2) { activeInteraction = newObj; }
    }
    */
    public void DropItem()
    {
        if (bigItem != null)
        {
            bigItem?.DropItem();
            bigItem = null;
            GetComponent<Movement>().model.SetBool("basket", false);
        }
        for (int i = smallItems.Count - 1; i >= 0; i--)
        {
            smallItems[i]?.DropItem();
            smallItems.RemoveAt(i);
        }
    }

    IEnumerator WaitForNextActive()
    {
        yield return new WaitForSeconds(interactionCD);
        interactionManager.currentInteraction = null;
    }
}
