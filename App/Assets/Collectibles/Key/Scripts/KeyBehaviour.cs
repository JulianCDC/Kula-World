/// <summary>
/// The Main Behaviour for the Key GameObject
/// </summary>
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

    /// <inheritdoc cref="Collectible.Collected"/>
    /// <summary>
    /// Set <see cref="Obtained"/> to true
    /// </summary>
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
