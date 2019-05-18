using System;

[Serializable] public enum GemColors
{
    blue = 0,
    red = 1,
    green = 2
}

public class Gem : Collectible, Collorable<GemColors>
{
    public GemColors color;

    public override void Collected()
    {
        base.Collected();
        GameManager.Instance.PlayerScore += 2975;
    }

}
