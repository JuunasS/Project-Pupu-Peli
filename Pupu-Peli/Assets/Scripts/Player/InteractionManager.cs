using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public Interactable currentInteraction;
    public float interactionCD;

    public void CheckInteractionDistance(Interactable newObj)
    {
        if (currentInteraction == null) { currentInteraction = newObj; return; }
        float distance1 = Vector3.Distance(this.transform.position, newObj.transform.position);
        float distance2 = Vector3.Distance(this.transform.position, currentInteraction.transform.position);
        if (distance1 < distance2) { currentInteraction = newObj; }
    }

    public void InteractEvent(InputAction.CallbackContext con)
    {
        Debug.Log("Interaction Event Called!");
        if (currentInteraction != null)
        {
            currentInteraction.Interact(this.gameObject);
            StartCoroutine(InteractionCooldown());
        }
    }

    public IEnumerator InteractionCooldown()
    {
        yield return new WaitForSeconds(interactionCD);
        currentInteraction = null;
    }
}
