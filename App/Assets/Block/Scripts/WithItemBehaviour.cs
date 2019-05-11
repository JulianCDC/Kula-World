using UnityEngine;

/// <summary>
/// The behaviour for block with item
/// </summary>
public class WithItemBehaviour : MonoBehaviour
{
    /// <summary>
    /// Specify the position of the item
    /// </summary>
    public enum Positions
    {
        up,
        right,
        left,
        down,
        front,
        back,
        none
    };

    public Positions itemPosition;

    /// <summary>
    /// The collectible assigned to the GameObject
    /// </summary>
    private GameObject childCollectible;

    /// <summary>
    /// The behaviour of <see cref="childCollectible"/>
    /// </summary>
    private Collectible childBehaviour;

    /// <summary>
    /// Initial yaw of the GameObject
    /// </summary>
    private float initialYaw;

    /// <summary>
    /// Initial pitch of the GameObject
    /// </summary>
    private float initialPitch;

    /// <summary>
    /// Initial roll of the GameObject
    /// </summary>
    private float initialRoll;

    /// <summary>
    /// Create the collectible and puts its initial rotations values into <see cref="initialYaw"/>, <see cref="initialRoll"/>, <see cref="initialPitch"/>
    /// </summary>
    public void Start()
    {
        this.childCollectible = this.transform.GetChild(0).gameObject;
        this.childBehaviour = (Collectible) this.childCollectible.GetComponent(typeof(Collectible));

        Quaternion rotation = childCollectible.transform.rotation;
        this.initialYaw = rotation.eulerAngles.x;
        this.initialPitch = rotation.eulerAngles.y;
        this.initialRoll = rotation.eulerAngles.z;

        UpdateItemPosition();
    }

    void Update()
    {
    }

    /// <summary>
    /// Set the item position depending on <see cref="itemPosition"/>
    /// </summary>
    public void UpdateItemPosition()
    {
        Quaternion newRotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        this.childCollectible.transform.localPosition = new Vector3(0, 0, 0);
        var oldRotation = this.childCollectible.transform.localRotation;
        this.childCollectible.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        this.childCollectible.transform.Translate(Vector3.up * 1);
        this.childCollectible.transform.localRotation = oldRotation;

        switch (this.itemPosition)
        {
            default:
            case Positions.up:
                break;
            case Positions.right:
                transform.Rotate(Vector3.right * 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.left:
                transform.Rotate(Vector3.left * 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.down:
                transform.Rotate(Vector3.down * 180);
                break;
            case Positions.front:
                transform.Rotate(Vector3.up * 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.back:
                transform.Rotate(Vector3.down * 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
        }
    }
}