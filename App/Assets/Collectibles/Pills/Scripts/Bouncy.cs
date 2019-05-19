using System.Threading;
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
        var cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancelTokenSource.Token;
        
        GameManager.Instance.runningTasksTokens.Add(cancelTokenSource);

        Task.Delay(15000).ContinueWith(delegate
        {
            cancellationToken.ThrowIfCancellationRequested();
            GameManager.Instance.playerSpeed /= 2;
        }, cancellationToken);
    }
}
