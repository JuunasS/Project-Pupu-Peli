using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class Basket : Carriable
{
    // Add basket functionality

    public List<Carriable> basketContent;
    public int basketMax;

    public override void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
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

    public void AddItem(Carriable item)
    {
        if (basketMax <= basketContent.Count) { item.PickUpFailed(); return; }
        if (item.GetComponent<carriableHerb>().carryModel != null)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
            item.GetComponent<carriableHerb>().carryModel.gameObject.SetActive(true);
        }
        

        basketContent.Add(item);

        var hItem = item as carriableHerb;
        hItem.transform.SetParent(this.transform);

        if (hItem.basketPivots.Count != 3)
            hItem.transform.position = this.transform.position + new Vector3(0, (.2f * (basketContent.Count-1)), 0);
        else
        {
            hItem.transform.localPosition = hItem.basketPivots[basketContent.Count - 1];
            hItem.transform.rotation = Quaternion.Euler(hItem.basketRotationPivots[basketContent.Count - 1]);
        }
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
