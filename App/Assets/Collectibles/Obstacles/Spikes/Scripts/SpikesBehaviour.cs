using System.Collections;
using UnityEngine;

public class SpikesBehaviour : Collectible
{
    public bool permanent = true;
    [SerializeField] private int idleTime = 3;
    
    protected override void Start()
    {
        base.Start();
        
        this.transform.Translate(Vector3.back * 0.42f);
        if (!this.permanent)
        {
           // animate
        }
    }

    public override void Collected()
    {
        // kill player
    }
}
