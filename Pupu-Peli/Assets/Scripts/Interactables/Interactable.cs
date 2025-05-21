using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // This class should contain OnTriggerStay function for checking that player is near player and a input check for interacting with this object

    public KeyCode interactKey;

    public virtual void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            if (Input.GetKey(interactKey))
            {
                Interact();
            }
        }

    }


    public abstract void Interact();
}
