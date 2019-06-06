using UnityEngine;

public class KeyBehaviour : Collectible
{
    private static int currentNumberOfKeys;
    private int Id;

    protected override void Start()
    {
        base.Start();

        currentNumberOfKeys += 1;
        this.Id = currentNumberOfKeys;
    }
    
    public override void Collected()
    {
        base.Collected();
        GameManager.Instance.retrievedKeys += 1;
        Hud.CollectKey(this.Id);
    }

    public static void ResetKeys()
    {
        currentNumberOfKeys = 0;
    }
}
