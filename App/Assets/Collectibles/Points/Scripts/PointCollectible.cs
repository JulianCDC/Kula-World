using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The behaviour for the collectibles changing the score of the player
/// </summary>
public class PointCollectible : Collectible
{
    /// <summary>
    /// The number of point to add to the player when this item is collected
    /// </summary>
    public int value;

    public override void Collected()
    {
        // increment player score by value
    }
}