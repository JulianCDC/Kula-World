using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Collectible : MonoBehaviour
{
    abstract protected void Collected();

    virtual protected void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
