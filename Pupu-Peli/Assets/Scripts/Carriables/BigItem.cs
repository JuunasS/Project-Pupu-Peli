using UnityEngine;

public class BigItem : MonoBehaviour
{
    // Parent class for big items

    // Pickup popup
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

        }
    }
}
