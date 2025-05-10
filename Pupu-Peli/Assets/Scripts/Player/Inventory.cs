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
    public SmallItem[] smallItems;


    public void SetBigItem(BigItem obj)
    {
        // ckeck if player has room for item
        obj.transform.position = bigItemHolder.position;
    }


    public void SetSmallItem(SmallItem obj)
    {
        // ckeck if player has room for item
        if (rightHand.childCount == 0)
        {
            obj.transform.position = rightHand.position;
        }
        else if (leftHand.childCount == 0)
        {
            obj.transform.position = leftHand.position;
        }
    }
}
