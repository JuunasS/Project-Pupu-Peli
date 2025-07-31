using System.Collections.Generic;
using UnityEngine;

public class carriableHerb : Carriable 
{
    public List<Vector3> positionPivots;
    public List<Vector3> rotationPivots;
    public Herb herb;
    public Transform carryModel;

    public override void Start()
    {
        base.Start();
        herb.isReverse = false;
    }
}
