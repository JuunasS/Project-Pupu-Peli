using TMPro;
using UnityEngine;

public class BigItem : MonoBehaviour
{
    // Parent class for big items
    public GameObject popupText;

    // Pickup popup
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            popupText.SetActive(true);
            // Pickup function?
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            popupText.SetActive(false);
        }
    }
}
