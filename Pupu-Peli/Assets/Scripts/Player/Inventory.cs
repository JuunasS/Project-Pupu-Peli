using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
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

    public GameObject activeInteraction = null;
    public float interactionCD; // Cooldown for interacting with objects

    private void Update()
    {
        // Move keyboard press stuff into the input system?
        if (Input.GetKeyUp(KeyCode.F))
        {
            DropItem();
        }
    }


    public void SetBigItem(Carriable obj)
    {
        Debug.Log(smallItems.Count);
        if (smallItems.Count > 0) { obj.PickUpFailed(); return; }
        obj.PickUpSuccess();
        // ckeck if player has room for item
        Debug.Log("Setting big item!");
        bigItem = obj;
        obj.transform.SetParent(bigItemHolder);
        obj.transform.position = bigItemHolder.position;
        obj.transform.rotation = bigItemHolder.rotation;
        activeInteraction = null;
        StartCoroutine(WaitForNextActive());
    }


    public void SetSmallItem(Carriable obj)
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

    public void CheckInteractionDistance(GameObject newObj)
    {
        float distance1 = Vector3.Distance(this.transform.position, newObj.transform.position);
        float distance2 = Vector3.Distance(this.transform.position, activeInteraction.transform.position);
        if (distance1 < distance2) { activeInteraction = newObj; }
    }

    public void DropItem()
    {
        if (bigItem != null)
        {
            bigItem?.DropItem();
            bigItem = null;
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
        activeInteraction = null;
    }
}
