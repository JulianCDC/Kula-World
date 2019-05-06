/// <summary>
/// The Main Behaviour for the Key GameObject
/// </summary>
public class KeyBehaviour : Collectible
{
    /// <summary>
    /// Return if the object has been collected by the player
    /// </summary>
    public bool Obtained { get; private set; }

    private int Id;

    /// <inheritdoc cref="Collectible.Collected"/>
    /// <summary>
    /// Set <see cref="Obtained"/> to true
    /// </summary>
    public override void Collected()
    {
        base.Collected();
        this.Obtained = true;
        Hud.CollectKey(this.Id);
    }
}
