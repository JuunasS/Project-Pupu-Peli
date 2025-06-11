using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class herbsToBrew : Interactable
{
    public List<Herb> herbs;

    GameObject floatingText;
    Inventory inv;
    Transform invTrans;

    [HideInInspector]
    public Basket basket;

    void Start()
    {
        floatingText = transform.Find("FloatingText").gameObject;
    }

    public override void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<InteractionManager>().CheckInteractionDistance(this);
            floatingText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        floatingText.SetActive(false);
    }

    public override void Interact(GameObject player)
    {
        inv = player.GetComponent<Inventory>();
        
        if (inv.bigItem != null)
        {
            basket = inv.bigItem.GetComponent<Basket>();
            invTrans = basket.transform.parent;
            basket.transform.parent = this.transform;
            basket.transform.position = this.transform.position;
            inv.bigItem = null;

            foreach (Carriable item in basket.basketContent)
            {
                herbs.Add(item.GetComponent<carriableHerb>().herb);
            }

            transform.root.GetComponent<AlchemyMaster>().enterPuzzle(player, herbs);
        }
    }

    public void basketExit()
    {
        basket.transform.parent = invTrans;
        basket.transform.position = inv.bigItemHolder.position;
        inv.bigItem = basket;
    }
}
