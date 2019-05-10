using UnityEngine;

/// <summary>
/// The behaviour for block with item
/// </summary>
public class WithItemBehaviour : MonoBehaviour
{
    /// <summary>
    /// Specify the position of the item
    /// </summary>
    public enum Positions { up, right, left, down, front, back, none };

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
        this.childBehaviour = (Collectible)this.childCollectible.GetComponent(typeof(Collectible));

        Quaternion rotation = childCollectible.transform.rotation;
        this.initialYaw = rotation.eulerAngles.x;
        this.initialPitch = rotation.eulerAngles.y;
        this.initialRoll = rotation.eulerAngles.z;

        SetItemPosition();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Set the item position depending on <see cref="itemPosition"/>
    /// </summary>
    public void SetItemPosition()
    {
        Quaternion newRotation;

        Collectible.RotationDirections nextRotationDirection = Collectible.RotationDirections.x;
        if (this.childBehaviour.rotationDirection == Collectible.RotationDirections.none)
        {
            nextRotationDirection = Collectible.RotationDirections.none;
        }
        
        switch (this.itemPosition)
        {
            default:
            case Positions.up:
                this.childCollectible.transform.localPosition = new Vector3(0, 1, 0);
                newRotation = Quaternion.Euler(initialYaw, initialPitch, initialRoll);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.y;
                break;
            case Positions.right:
                this.childCollectible.transform.localPosition = new Vector3(0, 0, 1);
                newRotation = Quaternion.Euler(initialYaw + 60, initialPitch, initialRoll - 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.left:
                this.childCollectible.transform.localPosition = new Vector3(0, 0, -1);
                newRotation = Quaternion.Euler(initialYaw - 120, initialPitch, initialRoll - 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.down:
                this.childCollectible.transform.localPosition = new Vector3(0, -1, 0);
                newRotation = Quaternion.Euler(initialYaw + 150, initialPitch, initialRoll - 90);
                break;
            case Positions.front:
                this.childCollectible.transform.localPosition = new Vector3(-1, 0, 0);
                newRotation = Quaternion.Euler(initialYaw + 60, initialPitch - 60, initialRoll - 270);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.back:
                this.childCollectible.transform.localPosition = new Vector3(1, 0, 0);
                newRotation = Quaternion.Euler(initialYaw + 60, initialPitch + 60, initialRoll - 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
        }

        if (nextRotationDirection != Collectible.RotationDirections.x)
        {
            this.childBehaviour.rotationDirection = Collectible.RotationDirections.none;
        }
        
        this.childCollectible.transform.localRotation = newRotation;
    }
}
