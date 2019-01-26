using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollectible : Collectible
{
    public int value;

    protected override void Collected()
    {
        // increment player score by value
    }
}
