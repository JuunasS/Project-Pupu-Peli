using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Solution", menuName = "Scriptable Objects/Herb Solution")]

public class herbSolution : ScriptableObject
{
    public List<Herb> herbs = new();
}
