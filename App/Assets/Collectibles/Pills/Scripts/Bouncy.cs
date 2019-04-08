using System.Threading.Tasks;

/// <summary>
/// The Main Behaviour of a BouncyPill GameObject
/// </summary>
public class Bouncy : Collectible
{
    public override void Collected()
    {
        base.Collected();
        GameManager.Instance.playerSpeed *= 2;
        GameManager.Instance.playerJumpLength += 1;
        SetExpiracyTimer();
    }

    private void SetExpiracyTimer()
    {
        Task.Delay(15000).ContinueWith(delegate
        {
            GameManager.Instance.playerSpeed /= 2;
        });
    }
}
