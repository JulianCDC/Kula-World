using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : Collectible
{
    public bool Obtained { get; private set; }

    protected override void Collected()
    {
        this.Obtained = true;
    }
}
