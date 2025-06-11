using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlchemyMaster : MonoBehaviour
{
    public GameObject alchemyUI;
    public List<Image> basketButtons;
    public List<Image> potButtons;

    List<Herb> herbs = new();
    GameObject lastSelected;

    GameObject player;

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null && alchemyUI.activeSelf)
            lastSelected = EventSystem.current.currentSelectedGameObject;
        else if (alchemyUI.activeSelf)
            EventSystem.current.SetSelectedGameObject(lastSelected);
    }

    public void brew()
    {
        player.GetComponentInChildren<thoughtBubble>().drawShape(herbs);
    }

    public void herbReverse(int button)
    {
        herbs[button].isReverse ^= true;

        if (herbs[button].isReverse)
            basketButtons[button].sprite = herbs[button].reverse;
        else
            basketButtons[button].sprite = herbs[button].image;
    }

    public void enterPuzzle(GameObject _player, List<Herb> _herbs)
    {
        herbs = _herbs;
        player = _player;

        alchemyUI.SetActive(true);

        int i = 0;
        foreach (Herb herb in herbs)
        {
            basketButtons[i].transform.parent.gameObject.SetActive(true);
            basketButtons[i].sprite = herb.image;
        }

        EventSystem.current.SetSelectedGameObject(basketButtons[0].transform.parent.gameObject);
    }

    public void exitPuzzle()
    {
        foreach (Image img in basketButtons)
        {
            img.sprite = null;
        }

        alchemyUI.SetActive(false);
    }
}
