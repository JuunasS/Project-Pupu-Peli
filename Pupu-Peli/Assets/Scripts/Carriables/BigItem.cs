using TMPro;
using UnityEngine;

public class BigItem : MonoBehaviour
{
    // Parent class for big items
    public GameObject popupText;
    public bool pickedUp;

    public bool inRange;

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


            popupText.SetActive(true);
            inRange = true;
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Set item for player!");
                other.GetComponent<Inventory>().SetBigItem(this);
                popupText.SetActive(false);
                pickedUp = true;
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
            other.GetComponent<Inventory>().activeInteraction = null;
        }
    }

    public virtual void PickUpFailed()
    {
        pickedUp = false;
    }
}
