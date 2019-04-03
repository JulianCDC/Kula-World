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
    private float speed = 0.2f;
    /// <summary>
    /// The number of block the player will jump before touching the ground
    /// </summary>
    private short jumpLength = 1;

    private Vector3 positionBeforeMovement = Vector3.zero;
    private bool isMoving;
    private Vector3 movingDirection = Vector3.zero;
    private Rigidbody rb;

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!this.isMoving)
        {
            ListenForMovement();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        var position = this.gameObject.transform.position;
        position = Vector3.MoveTowards(position, position + movingDirection, speed);
        this.gameObject.transform.position = position;
        if (this.transform.position == this.positionBeforeMovement + movingDirection)
        {
            this.isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        Collectible behaviour = collided.GetComponent<Collectible>();

        if (behaviour != null)
        {
            behaviour.Collected();
        }
    }

    private void ListenForMovement()
    {
        if (Input.GetKeyDown("left"))
        {
            this.movingDirection = Vector3.right;
        }
        else if (Input.GetKeyDown("right"))
        {
            this.movingDirection = Vector3.left;
        }
        else if (Input.GetKeyDown("up"))
        {
            this.movingDirection = Vector3.back;
        }
        else if (Input.GetKeyDown("down"))
        {
            this.movingDirection = Vector3.forward;
        }
        else
        {
            return;
        }

        this.positionBeforeMovement = this.gameObject.transform.position;
        this.isMoving = true;
    }
    
    public void FinishedMoving()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}