/// <summary>
/// The Main Behaviour for the LethargyPill GameObject
/// </summary>
public class Lethargy : Collectible
{
    public override void Collected()
    {
        base.Collected();
        // slow down ball and speed up timer
    }
}
