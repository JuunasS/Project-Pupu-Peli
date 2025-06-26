using UnityEngine;

public class Bed : Interactable
{
    public GameObject interactText;

    // This is the time which is set when the bed is used
    public float morningTime;

    private void Awake()
    {
        DayNightCycle.OnDayTimeChanged += TimeChangeEvent;
        MissionManager.OnMissionProgressionChanged += ProgressionChangeEvent;
    }

    private void OnDestroy()
    {
        DayNightCycle.OnDayTimeChanged -= TimeChangeEvent;
        MissionManager.OnMissionProgressionChanged -= ProgressionChangeEvent;
    }

    private void TimeChangeEvent(TimeOfDay state)
    {
        Debug.Log("Time change event! " + state);
        if (state == TimeOfDay.Morning)
        {
            Debug.Log("Morning state event triggered!!");
        }
    }

    private void ProgressionChangeEvent(int progression)
    {
        Debug.Log("Progression change event! " + progression);
        if (progression == 1)
        {
            Debug.Log("Progression state event triggered!!");
        }
    }

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
        DayNightCycle.manager.SetTime(morningTime);
    }

   
}
