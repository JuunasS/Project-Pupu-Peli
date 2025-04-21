using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alchemy : MonoBehaviour
{
    public List<Herb> picks = new();
    public List<Herb> solution = new();
    public List<Image> playerImage = new();
    public TextMeshProUGUI text;

    public GameObject win;
    public GameObject lose;
    

    public bool Check()
    {
        if (picks.Count == solution.Count)
        {
            foreach (Herb herb in solution)
            {
                bool correct = false;
                foreach (Herb pick in picks)
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
        
        return true;
    }

    public void Display()
    {
        List<Sprite> sorted = new();

        foreach (var herb in picks)
        {
            if (!herb.isReverse)
                sorted.Add(herb.image);
        }
        foreach (var herb in picks)
        {
            if (herb.isReverse)
                sorted.Add(herb.reverse);
        }
        for (int i = 0; i < sorted.Count; i++)
        {
            playerImage[i].enabled = true;
            playerImage[i].sprite = sorted[i];
        }

        if (Check())
        {
            win.SetActive(true);
        }
        else 
        {
            lose.SetActive(true);
        }
    }

    public void addHerb(Herb herb)
    {
        if (!picks.Contains(herb) && picks.Count < 3)
        {
            herb.isReverse = false;
            picks.Add(herb);
            text.text += herb.name + "\n";
        }
    }

    public void addHerbReverse(Herb herb)
    {
        if (!picks.Contains(herb) && picks.Count < 3)
        {
            herb.isReverse = true;
            picks.Add(herb);

            text.text += herb.name + " Reversed\n";
        }
    }

    public void Clear()
    {
        picks.Clear();
        text.text = "";
    }

    public void Reset()
    {
        Clear();
        foreach(Image image in playerImage)
        {
            image.sprite = null;
            image.enabled = false;
        }
    }
}

public static class Extensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}