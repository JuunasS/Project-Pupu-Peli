using UnityEngine;

public class Bed : Interactable
{
    public DayNightCycle dayNightCycle;
    public GameObject interactText;

    // This is the time which is set when the bed is used
    public float morningTime;

    public override void OnTriggerStay(Collider other)
    {
        
        if (other.transform.tag == "Player")
        {
            other.GetComponent<InteractionManager>().CheckInteractionDistance(this);

            if (other.GetComponent<InteractionManager>().currentInteraction != this)
            {
                interactText.SetActive(false);
                return;
            }
            interactText.SetActive(true);

        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            interactText.SetActive(false);
            if (other.GetComponent<InteractionManager>().currentInteraction == this)
            {
                other.GetComponent<InteractionManager>().currentInteraction = null;
            }
        }
    }

    public override void Interact(GameObject player)
    {
        dayNightCycle.SetTime(morningTime);
    }
}
