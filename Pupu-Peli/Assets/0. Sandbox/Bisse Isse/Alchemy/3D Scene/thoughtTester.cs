using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class thoughtTester : MonoBehaviour
{
    public List<Herb> herbs = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var herb in herbs)
            GetComponent<Teapot>().addHerb(herb);
    }
}
