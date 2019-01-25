using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : Collectible
{
    void Start()
    {
        
    }

    override protected void Update()
    {
        base.Update();
    }

    protected override void Collected()
    {
        // TODO : update score
    }
}
