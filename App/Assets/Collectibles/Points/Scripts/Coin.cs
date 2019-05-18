using System;

[Serializable] public enum CoinColors
{
    blue = 0,
    yellow = 1
}

public class Coin : Collectible, Collorable<CoinColors>
{
    public CoinColors color;

    private int pointValue
    {
        get
        {
            switch (color)
            {
                case CoinColors.blue:
                    return 550;
                case CoinColors.yellow:
                    return 250;
            }

            return 0;
        }
    }

    public override void Collected()
    {
        base.Collected();
        GameManager.Instance.PlayerScore += pointValue;
    }

}
