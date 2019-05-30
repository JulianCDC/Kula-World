using UnityEngine;

public class WithItemBehaviour : MonoBehaviour
{
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
    private GameObject childCollectible;
    private Collectible childBehaviour;
    private Transform initialCollectibleTransform;
    
    private float initialYaw;
    private float initialPitch;
    private float initialRoll;
    
    public void Start()
    {
        this.childCollectible = this.transform.GetChild(0).gameObject;
        this.initialCollectibleTransform = childCollectible.transform;
        this.childBehaviour = (Collectible) this.childCollectible.GetComponent(typeof(Collectible));

        Quaternion rotation = childCollectible.transform.rotation;
        this.initialYaw = rotation.eulerAngles.x;
        this.initialPitch = rotation.eulerAngles.y;
        this.initialRoll = rotation.eulerAngles.z;

        UpdateItemPosition();
    }
    
    public void UpdateItemPosition()
    {
        Quaternion newRotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        Collectible.RotationDirections nextRotationDirection = Collectible.RotationDirections.x;
        if (this.childBehaviour.rotationDirection == Collectible.RotationDirections.none)
        {
            nextRotationDirection = Collectible.RotationDirections.none;
        }

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
                break;
            case Positions.left:
                transform.Rotate(Vector3.left * 90);
                break;
            case Positions.down:
                transform.Rotate(Vector3.forward * 180);
                break;
            case Positions.front:
                transform.Rotate(Vector3.forward * 90);
                break;
            case Positions.back:
                transform.Rotate(Vector3.back * 90);
                break;
        }

        if (nextRotationDirection != Collectible.RotationDirections.x)
        {
            this.childBehaviour.rotationDirection = Collectible.RotationDirections.none;
        }
    }
}