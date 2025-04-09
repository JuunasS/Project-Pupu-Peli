using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alchemy : MonoBehaviour
{
    public List<Herb> herbList = new();
    public List<string> picks = new();
    public List<string> solution = new();
    public List<Image> playerImage = new();

    public Dictionary<string, Sprite> herbs = new();

    public herbSolution sol;

    private void Start()
    {
        foreach (var herb in herbList)
        {
            herbs.Add(herb.name, herb.image);
            herbs.Add(herb.name + 'R', herb.reverse);
        }
    }

    public void Check()
    {
        bool correct = true;
        Display();

        if (picks.Count == solution.Count)
        {
            foreach (string herb in solution)
            {
                if (!picks.Contains(herb))
                    correct = false;
            }
        }
        else
            correct = false;

        if (correct)
            Debug.Log("Correct!");
        else
            Debug.Log("Incorrect :(");
    }

    public void Display()
    {
        List<string> sorted = new();

        foreach (var herb in picks)
        {
            if (!herb.EndsWith("R"))
                sorted.Add(herb);
        }
        foreach (var herb in picks)
        {
            if (herb.EndsWith("R"))
                sorted.Add(herb);
        }
        for (int i = 0; i < sorted.Count; i++)
        {
            playerImage[i].sprite = herbs[sorted[i]];
        }
    }

    public void addHerb(string str)
    {
        if (!picks.Contains(str))
            picks.Add(str);
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
