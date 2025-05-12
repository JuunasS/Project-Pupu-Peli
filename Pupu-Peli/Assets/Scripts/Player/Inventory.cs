using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    // Big item data and positions
    public Transform bigItemHolder;
    public BigItem bigItem;

    // Small item data and positions
    public Transform rightHand;
    public Transform leftHand;
    public SmallItem[] smallItems = new SmallItem[2];


    public void SetBigItem(BigItem obj)
    {
        // ckeck if player has room for item
        bigItem = obj;
        obj.transform.SetParent(bigItemHolder);
        obj.transform.position = bigItemHolder.position;
    }


    public void SetSmallItem(SmallItem obj)
    {
        // ckeck if player has room for item
        if(bigItem != null) {return; }

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

    public void DropItem()
    {
        if (bigItem)
        {
            
        }
    }
}
