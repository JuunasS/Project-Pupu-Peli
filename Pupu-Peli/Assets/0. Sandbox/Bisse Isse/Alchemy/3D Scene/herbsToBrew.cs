using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class herbsToBrew : Interactable
{
    public List<Herb> herbs;
    GameObject floatingText;
    public Teapot teapot;

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
        Inventory inv = player.GetComponent<Inventory>();
        foreach (Carriable car in inv.smallItems)
        {
            if (car.GetComponent<carriableHerb>())
            {
                herbs.Add(car.GetComponent<carriableHerb>().herb);
                car.DropItem();
                //inv.smallItems.Remove(car);
                car.transform.parent = this.transform;
                car.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                car.enabled = false;
            }
        }
        foreach (Herb herb in herbs)
        {
            teapot.addHerb(herb);
        }
    }
}
