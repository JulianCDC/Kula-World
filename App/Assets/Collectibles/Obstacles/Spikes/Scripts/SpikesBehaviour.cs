using System.Collections;
using UnityEngine;

public class SpikesBehaviour : Collectible
{
    public bool permanent = true;
    private static readonly int Permanent = Animator.StringToHash("Permanent");

    protected override void Start()
    {
        base.Start();

        print(transform.position);
        this.transform.Translate(Vector3.back * 0.42f);
        print(transform.position);
        GetComponent<Animator>().SetBool(Permanent, this.permanent);
    }

    public override void Collected()
    {
        // kill player
    }
}