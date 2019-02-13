using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithItemBehaviour : MonoBehaviour
{
    public enum Positions { up, right, left, down, front, back };

    public Positions itemPosition;
    private GameObject childCollectible;
    private Collectible childBehaviour;
    private float initialYaw;
    private float initialPitch;
    private float initialRoll;

    public void Start()
    {
        this.childCollectible = this.transform.GetChild(0).gameObject;
        this.childBehaviour = (Collectible)this.childCollectible.GetComponent(typeof(Collectible));

        Quaternion rotation = childCollectible.transform.rotation;
        this.initialYaw = rotation.eulerAngles.x;
        this.initialPitch = rotation.eulerAngles.y;
        this.initialRoll = rotation.eulerAngles.z;

        setItemPosition();
    }

    void Update()
    {
        
    }

    public void setItemPosition()
    {
        Quaternion newRotation;

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

        this.childCollectible.transform.localRotation = newRotation;
    }
}
