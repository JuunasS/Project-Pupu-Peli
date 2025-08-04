using System.Collections.Generic;
using UnityEngine;

public class carriableHerb : Carriable 
{
    public List<Vector3> basketPivots;
    public List<Vector3> basketRotationPivots;
    public Herb herb;
    public Transform carryModel;

    public override void Start()
    {
        base.Start();
        herb.isReverse = false;
    }
}
