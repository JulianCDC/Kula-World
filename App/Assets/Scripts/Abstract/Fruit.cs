using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Fruit : Collectible
{
    public enum fruits { apple, banana, watermelon, strawberry, pumpkin };
    public fruits type;

    protected override void Collected()
    {
        // collect type
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
