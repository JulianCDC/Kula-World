using System.Threading.Tasks;

/// <summary>
/// The Main Behaviour for the LethargyPill GameObject
/// </summary>
public class Lethargy : Collectible
{
    public override void Collected()
    {
        base.Collected();
        GameManager.Instance.playerSpeed /= 2;
        GameManager.Instance.secondsPerTick *= 2;
        setExpiracyTimer();
    }

    private void setExpiracyTimer()
    {
        Task.Delay(15000).ContinueWith(delegate
        {
            GameManager.Instance.playerSpeed *= 2;
            GameManager.Instance.secondsPerTick /= 2;
        });
    }
}