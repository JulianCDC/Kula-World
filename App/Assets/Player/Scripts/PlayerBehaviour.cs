using System;
using UnityEngine;

/// <summary>
/// The behaviour of the player
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// The speed of the player
    /// </summary>
    private int speed;
    /// <summary>
    /// The number of block the player will jump before touching the ground
    /// </summary>
    private short jumpLength = 1;

    [NonSerialized] private bool isMoving = false;

    void Start()
    {
    }

    void Update()
    {
    }

    public void FinishedMoving()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}