using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class Basket : Carriable
{
    // Add basket functionality

    public List<Carriable> basketContent;
    public int basketMax;

    public void AddItem(Carriable item)
    {
        if (basketMax <= basketContent.Count) { item.PickUpFailed(); return; }
        basketContent.Add(item);

        item.transform.position = this.transform.position + new Vector3(0, (.2f * (basketContent.Count-1)), 0);
        item.transform.SetParent(this.transform);
    }

    public override void DropItem()
    {
        base.DropItem();
        for (int i = 0; i < basketContent.Count; i++)
        {
            basketContent[i].DropItem();
        }
        basketContent.Clear();
    }
}
