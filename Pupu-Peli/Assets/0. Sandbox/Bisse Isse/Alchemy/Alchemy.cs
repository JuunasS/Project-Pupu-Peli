using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alchemy : MonoBehaviour
{
    public List<Herb> herbs = new();
    public List<Herb> picks = new();
    public List<GameObject> target = new();
    public List<Image> playerImage = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Restart()
    {
        Extensions.Shuffle(herbs);
    }

    public void Check()
    {
        List<Herb> sorted = new();

        foreach (var herb in picks)
        {
            if (!herb.isReverse)
                sorted.Add(herb);
        }
        foreach (var herb in picks)
        {
            if (herb.isReverse)
                sorted.Add(herb);
        }
        for (int i = 0; i < sorted.Count; i++)
        {
            Debug.Log(sorted[i].image);
            if (sorted[i].isReverse)
                playerImage[i].sprite = sorted[i].reverse;
            else
                playerImage[i].sprite = sorted[i].image;
        }
    }

    public void addHerb(Herb herb)
    {
        if (!picks.Contains(herb))
            picks.Add(herb);
    }

    public void addHerbReverse(Herb herb)
    {
        if (!picks.Contains(herb))
            herb.isReverse = true;
            picks.Add(herb);
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
