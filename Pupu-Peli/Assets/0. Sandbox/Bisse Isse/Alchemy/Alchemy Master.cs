using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlchemyMaster : MonoBehaviour
{
    public GameObject alchemyUI;
    public List<Button> basketButtons;
    public List<Button> potButtons;

    public void enterPuzzle(GameObject player, List<Herb> herbs)
    {
        //player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Alchemy");

        alchemyUI.SetActive(true);

        int i = 0;
        foreach (Herb herb in herbs)
        {
            basketButtons[i].gameObject.SetActive(true);
            basketButtons[i].transform.GetComponent<Image>().sprite = herb.image;
        }
    }

    public void exitPuzzle()
    {

    }
}
