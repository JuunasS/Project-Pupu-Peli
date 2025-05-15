using UnityEngine;

public class Carriable : MonoBehaviour
{
    // Parent class for big items
    public GameObject popupText;
    public bool pickedUp;

    public bool inRange;
    public Collider col;
    public Rigidbody rigidBody;

    public bool bigItem; // True if object is a big item. Otherwise object is considered a small item.

    public virtual void OnTriggerStay(Collider other)
    {
        //Debug.Log("inRange: " + inRange + " pickedUp: " + pickedUp);
        if (pickedUp) { return; }

        if (other.transform.tag == "Player")
        {
            other.GetComponent<Inventory>().CheckInteractionDistance(this.gameObject);

            if (other.GetComponent<Inventory>().activeInteraction != gameObject)
            {
                popupText.SetActive(false);
                inRange = false;
                return;
            }

            other.GetComponent<Inventory>().activeInteraction = this.gameObject;
            popupText.SetActive(true);
            inRange = true;

            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Set item for player!");
                if (bigItem)
                {
                    other.GetComponent<Inventory>().SetBigItem(this);
                }
                else
                {
                    other.GetComponent<Inventory>().SetSmallItem(this);
                }
            }
        }
    }


    public virtual void OnTriggerExit(Collider other)
    {
        if (pickedUp) { return; }
        if (other.transform.tag == "Player")
        {
            popupText.SetActive(false);
            inRange = false;
            if (other.GetComponent<Inventory>().activeInteraction == this.gameObject)
            {
                other.GetComponent<Inventory>().activeInteraction = null;
            }
        }
    }


    public virtual void PickUpSuccess()
    {
        popupText.SetActive(false);
        pickedUp = true;

        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        col.enabled = false;
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
