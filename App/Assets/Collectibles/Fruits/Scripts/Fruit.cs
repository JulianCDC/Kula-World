public class Fruit : Collectible
{
    public enum fruits
    {
        apple, 
        banana, 
        watermelon, 
        strawberry, 
        pumpkin,
        none
    };

    public fruits type;

    public override void Collected()
    {
        base.Collected();
        GameManager.Instance.PlayerScore += 2500;
        Hud.CollectFruit(this.type);
    }
}
