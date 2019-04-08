using System;

/// <summary>
/// The Main Behaviour for the Hourglass GameObject
/// </summary>
public class HourglassBehaviour : Collectible
{
    public override void Collected()
    {
        base.Collected();
        int bonusScore = 12 * GameManager.Instance.elapsedTime / 10;
        bonusScore = bonusScore < 1 ? 10 : bonusScore * 10;
        GameManager.Instance.playerScore += 1200 - bonusScore;
        GameManager.Instance.elapsedTime = GameManager.Instance.maxTime - GameManager.Instance.elapsedTime;
    }
}
