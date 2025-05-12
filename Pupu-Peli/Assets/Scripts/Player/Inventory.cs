using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{

    // Big item data and positions
    public Transform bigItemHolder;
    public BigItem bigItem;

    // Small item data and positions
    public Transform rightHand;
    public Transform leftHand;
    public SmallItem[] smallItems = new SmallItem[2];

    public GameObject activeInteraction = null;


    public void SetBigItem(BigItem obj)
    {
        if(smallItems.Length > 0) { obj.PickUpFailed(); return; }
        // ckeck if player has room for item
        bigItem = obj;
        obj.transform.SetParent(bigItemHolder);
        obj.transform.position = bigItemHolder.position;
    }


    public void SetSmallItem(SmallItem obj)
    {
        // ckeck if player has room for item
        if(bigItem != null) { obj.PickUpFailed(); return; }

        if (rightHand.childCount == 0)
        {
            smallItems[0] = obj;
            obj.transform.SetParent(rightHand);
            obj.transform.position = rightHand.position;
        }
        else if (leftHand.childCount == 0)
        {
            smallItems[1] = obj;
            obj.transform.SetParent(leftHand); 
            obj.transform.position = leftHand.position;
        }
        else {
            Debug.Log("Hands full!");
        }
    }

    public void CheckInteractionDistance(GameObject newObj)
    {
        float distance1 = Vector3.Distance(this.transform.position, newObj.transform.position);
        float distance2 = Vector3.Distance(this.transform.position, activeInteraction.transform.position);
        if (distance1 < distance2) { activeInteraction = newObj; }
    }
    
    public void DropItem()
    {
        if (bigItem)
        {
            
        }
    }
}
