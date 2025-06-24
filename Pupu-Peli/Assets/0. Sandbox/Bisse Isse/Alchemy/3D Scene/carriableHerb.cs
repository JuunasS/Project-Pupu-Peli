using UnityEngine;

public class carriableHerb : Carriable 
{
    public Herb herb;

    public override void Start()
    {
        base.Start();
        herb.isReverse = false;
    }
}
