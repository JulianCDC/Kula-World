using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectible
{
    public enum fruits { apple, banana, watermelon, strawberry, pumpkin };
    public fruits type;

    public override void Collected()
    {
        // collect this.type in score
    }
}
