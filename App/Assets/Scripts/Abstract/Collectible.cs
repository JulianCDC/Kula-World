using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Collectible : MonoBehaviour
{
    private float yaw;
    private float pitch;
    private float roll;

    public enum RotationDirections { x, y, z };
    public RotationDirections rotationDirection;

    abstract protected void Collected();

    virtual protected void Start()
    {
        Quaternion angle = this.transform.rotation;
        this.yaw = angle.eulerAngles.x;
        this.roll = angle.eulerAngles.z;
        this.pitch = angle.eulerAngles.y;
    }

    virtual protected void Update()
    {
        switch(this.rotationDirection)
        {
            case RotationDirections.x:
                this.yaw += 50 * Time.deltaTime;
                break;
            case RotationDirections.y:
                this.pitch += 50 * Time.deltaTime;
                break;
            case RotationDirections.z:
                this.roll += 50 * Time.deltaTime;
                break;
        }

        transform.rotation = Quaternion.Euler(yaw, pitch, roll);
    }
}
