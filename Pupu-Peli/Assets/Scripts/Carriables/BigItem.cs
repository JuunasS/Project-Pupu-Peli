using TMPro;
using UnityEngine;

public class BigItem : MonoBehaviour
{
    // Parent class for big items
    public GameObject popupText;
    public bool pickedUp;

    public bool inRange;

    // Pickup popup
    public virtual void OnTriggerEnter(Collider other)
    {
        if(pickedUp) { return; }
        if (other.transform.tag == "Player")
        {
            popupText.SetActive(true);
            inRange = true;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        Debug.Log("inRange: " + inRange + " pickedUp: " + pickedUp);
        if (other.transform.tag == "Player" && inRange && !pickedUp)
        {
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
        }
    }
}
