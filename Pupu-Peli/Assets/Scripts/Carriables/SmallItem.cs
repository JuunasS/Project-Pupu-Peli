using UnityEngine;

public class SmallItem : MonoBehaviour
{
    // Parent class for small items
    public GameObject popupText;
    public bool pickedUp;

    public bool inRange;
    public Collider collider;
    public Rigidbody rigidBody;


    public virtual void OnTriggerStay(Collider other)
    {
        //Debug.Log("inRange: " + inRange + " pickedUp: " + pickedUp);
        if (pickedUp) { return; }

        if (other.transform.tag == "Player")
        {
            if (other.GetComponent<Inventory>().activeInteraction != null)
            {
                other.GetComponent<Inventory>().CheckInteractionDistance(this.gameObject);
                if (other.GetComponent<Inventory>().activeInteraction != gameObject)
                {
                    popupText.SetActive(false);
                    inRange = false;
                    return;
                }
            }

            other.GetComponent<Inventory>().activeInteraction = this.gameObject;
            popupText.SetActive(true);
            inRange = true;
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Set item for player!");
                other.GetComponent<Inventory>().SetSmallItem(this);
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
        collider.enabled = false;
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
        collider.enabled = true;
        pickedUp = false;
    }
}

