using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollectible : Collectible
{
    public int value;

    public override void Collected()
    {
        // increment player score by value
    }
}
