using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.GetComponent<InteractionManager>().CheckInteractionDistance(this);
        }
    }

    public abstract void Interact(GameObject player);
}
