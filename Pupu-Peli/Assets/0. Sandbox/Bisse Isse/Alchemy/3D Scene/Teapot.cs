using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teapot : Interactable
{
    SpriteRenderer[] renders;
    List<Herb> addedHerbs = new();
    public herbsToBrew herbsToBrew;

    void Awake()
    {
        renders = GetComponentsInChildren<SpriteRenderer>();
    }

    public List<Herb> brewAndDrink()
    {
        return addedHerbs;
    }

    public void addHerb(Herb herb)
    {
        addedHerbs.Add(herb);
        if (addedHerbs.Count < 3)
        {
            renders[addedHerbs.Count].enabled = true;
            renders[addedHerbs.Count].transform.GetComponentInChildren<TextMeshPro>().text = herb.name;

            Vector2 size = new Vector2(2.5f, 2.5f + 2.25f * (addedHerbs.Count - 1));
            renders[0].size = size;
        }
    }

    public override void Interact(GameObject player)
    {

    }
}