using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithItemBehaviour : MonoBehaviour
{
    public enum Positions { up, right, left, down, front, back };

    public Positions itemPosition;
    private GameObject childCollectible;
    private Collectible childBehaviour;

    void Awake()
    {
        this.childCollectible = this.transform.GetChild(0).gameObject;
        this.childBehaviour = (Collectible)this.childCollectible.GetComponent(typeof(Collectible));

        Quaternion rotation = childCollectible.transform.rotation;
        Quaternion newRotation;
        float yaw = rotation.eulerAngles.x;
        float pitch = rotation.eulerAngles.y;
        float roll = rotation.eulerAngles.z;

        switch (this.itemPosition)
        {
            default:
            case Positions.up:
                this.childCollectible.transform.localPosition = new Vector3(0, 1, 0);
                newRotation = Quaternion.Euler(yaw, pitch, roll);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.y;
                break;
            case Positions.right:
                this.childCollectible.transform.localPosition = new Vector3(0, 0, 1);
                newRotation = Quaternion.Euler(yaw + 60, pitch, roll - 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.left:
                this.childCollectible.transform.localPosition = new Vector3(0, 0, -1);
                newRotation = Quaternion.Euler(yaw - 120, pitch, roll - 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.down:
                this.childCollectible.transform.localPosition = new Vector3(0, -1, 0);
                newRotation = Quaternion.Euler(yaw + 150, pitch, roll - 90);
                break;
            case Positions.front:
                this.childCollectible.transform.localPosition = new Vector3(-1, 0, 0);
                newRotation = Quaternion.Euler(yaw + 60, pitch - 60, roll - 270);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
            case Positions.back:
                this.childCollectible.transform.localPosition = new Vector3(1, 0, 0);
                newRotation = Quaternion.Euler(yaw + 60, pitch + 60, roll - 90);
                this.childBehaviour.rotationDirection = Collectible.RotationDirections.z;
                break;
        }

        this.childCollectible.transform.localRotation = newRotation;
    }

    void Update()
    {
        
    }
}
