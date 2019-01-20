using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Collectible : MonoBehaviour
{
    public enum rotationDirections { x, y, z };
    public rotationDirections rotationDirection;
    abstract protected void Collected();

    virtual protected void Update()
    {
        float x = 0, y = 0, z = 0;
        switch(this.rotationDirection)
        {
            case rotationDirections.x:
                x = 50 * Time.deltaTime;
                break;
            case rotationDirections.y:
                y = 50 * Time.deltaTime;
                break;
            case rotationDirections.z:
                z = 50 * Time.deltaTime;
                break;
        }
        transform.Rotate(x, y, z);
    }
}
