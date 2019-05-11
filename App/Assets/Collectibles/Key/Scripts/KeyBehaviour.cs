/// <summary>
/// The Main Behaviour for the Key GameObject
/// </summary>
public class KeyBehaviour : Collectible
{
    private int Id;

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
}
