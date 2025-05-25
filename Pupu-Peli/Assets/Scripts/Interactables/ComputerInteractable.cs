using System.Threading.Tasks;
using UnityEngine;

public class ComputerInteractable : Interactable
{
    public GameObject canvasObject;

    public GameObject floatingText;
    public bool isActive = false;

    public override void OnTriggerStay(Collider other)
    {
        if(isActive) { return; }

        if (other.transform.tag == "Player")
        {
            other.GetComponent<InteractionManager>().CheckInteractionDistance(this);
            floatingText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        floatingText.SetActive(false);
    }

    public override void Interact(GameObject player)
    {
        ActivateComputer();
        //player.GetComponent<PlayerInput>().
    }


    public void ActivateComputer()
    {
        floatingText.SetActive(false);
        canvasObject.SetActive(true);
        isActive = true;
        // Set player input exit event to exitcomputer function

    }


    public virtual void ExitComputer()
    {
        canvasObject.SetActive(false);
        isActive = false;
    }
}
