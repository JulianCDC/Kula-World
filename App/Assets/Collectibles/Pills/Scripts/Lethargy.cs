using System.Threading;
using System.Threading.Tasks;

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
        var cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancelTokenSource.Token;
        
        GameManager.Instance.runningTasksTokens.Add(cancelTokenSource);

        Task.Delay(15000).ContinueWith(delegate
        {
            cancellationToken.ThrowIfCancellationRequested();
            GameManager.Instance.playerSpeed *= 2;
            GameManager.Instance.secondsPerTick /= 2;
        }, cancellationToken);
    }
}