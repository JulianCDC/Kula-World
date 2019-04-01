/// <summary>
/// The Main Behaviour for Apple, Banana, Watermelon, Strawberry, Pumpkin GameObjects
/// </summary>
public class Fruit : Collectible
{
    /// <summary>
    /// Specify the fruit type
    /// </summary>
    public enum fruits
    {
        apple, 
        banana, 
        watermelon, 
        strawberry, 
        pumpkin,
        /// <summary>
        /// used for serialization when a block's collectible does not inherit Fruit
        /// </summary>
        none
    };
    /// <summary>
    /// The fruit type of the GameObject
    /// </summary>
    public fruits type;

    public override void Collected()
    {
        // collect this.type in score
    }
}
