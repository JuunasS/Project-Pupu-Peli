using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlchemyMaster : MonoBehaviour
{
    public GameObject alchemyUI;
    public herbsToBrew htb;
    public List<Image> basketButtons;
    public List<Image> potButtons;

    public herbSolution solution;

    List<Herb> herbs = new();
    GameObject lastSelected;

    GameObject player;

    public static event System.Action AlchemyGameSuccess;

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
        Debug.Log("Solution check: " + Check(herbs));

        foreach (var herb in htb.basket.basketContent)
            Object.Destroy(herb.gameObject);

        htb.basket.basketContent.Clear();

        exitPuzzle();
    }

    public bool Check(List<Herb> herbs)
    {
        if (herbs.Count == solution.herbs.Count)
        {
            foreach (Herb herb in solution.herbs)
            {
                bool correct = false;
                foreach (Herb pick in herbs)
                {
                    if (pick.name == herb.name && pick.isReverse == herb.isReverse)
                        correct = true;
                }
                if (!correct)
                    return false;
            }
        }
        else
            return false;

        AlchemyGameSuccess?.Invoke();
        return true;
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
        player.GetComponent<PlayerInput>().DeactivateInput();

        int i = 0;
        foreach (Herb herb in herbs)
        {
            basketButtons[i].transform.parent.gameObject.SetActive(true);
            basketButtons[i].sprite = herb.image;
            i++;
        }

        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void exitPuzzle()
    {
        foreach (Image img in basketButtons)
        {
            img.sprite = null;
            img.transform.parent.gameObject.SetActive(false);
        }

        alchemyUI.SetActive(false);

        //player.GetComponent<PlayerInput>().ActivateInput();
        // Player Input is resumed in the thought bubble script, which is called by the thought bubble animation

        herbs.Clear();

        htb.basketExit();
    }
}