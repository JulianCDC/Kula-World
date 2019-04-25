using System;
using System.Collections;
using UnityEngine;

public class PlayerBlockBehaviour : MonoBehaviour
{
    private bool isMoving;
    private Vector3 movingDirection = Vector3.zero;
    [SerializeField] private GameObject player;
    private PlayerBehaviour playerBehaviour;
    private float playerSpeedBeforeMovement;

    private float MovementInterationsCount => 1 / MovementLength;
    private float MovementLength => 1 * playerSpeedBeforeMovement / 10;
    private float MovementDuration => 0.1f / MovementInterationsCount;

    private void Start()
    {
        this.playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

    private void Update()
    {
        if (!this.isMoving)
        {
            ListenForMovement();
        }
        else
        {
            this.playerBehaviour.RotateAnimation(movingDirection);
        }
    }

    private void Move()
    {
        if (!Map.isEmpty(transform.position + transform.TransformDirection(movingDirection + Vector3.up)))
        {
            StartCoroutine(Climb());
        }
        else if (Map.isEmpty(transform.position + transform.TransformDirection(movingDirection)))
        {
            StartCoroutine(Rotate(movingDirection));
        }
        else
        {
            StartCoroutine(TranslatePlayer());
        }
    }

    private IEnumerator Rotate(Vector3 amount)
    {
        var rotationAmount = new Vector3(amount.z, amount.x, amount.y) * 90;
        var numberOfIterations = MovementInterationsCount;

        for (int i = 0; i < numberOfIterations; i++)
        {
            this.transform.Rotate(rotationAmount * MovementLength);
            yield return new WaitForSeconds(MovementDuration);
        }

        StopMoving();
    }

    private IEnumerator Climb()
    {
        var numberOfIterations = MovementInterationsCount;

        var translationDestination = transform.TransformDirection(movingDirection + Vector3.up);

        for (int i = 0; i < numberOfIterations; i++)
        {
            this.transform.transform.position += translationDestination * MovementLength;
            this.transform.Rotate((Vector3.left * 90) * MovementLength);
            yield return new WaitForSeconds(MovementDuration);
        }

        StopMoving();
    }

    private void Jump()
    {
        // TODO : Disable collision while jumping to prevent collecting item
    }

    private IEnumerator TranslatePlayer()
    {
        var numberOfIterations = MovementInterationsCount;

        for (int i = 0; i < numberOfIterations; i++)
        {
            this.transform.Translate(movingDirection * MovementLength);
            yield return new WaitForSeconds(MovementDuration);
        }

        StopMoving();
    }

    private void StopMoving()
    {
        FixPosition();
        FixRotation();
        isMoving = false;
    }

    private void FixPosition()
    {
        var fixedX = Math.Round(transform.position.x);
        var fixedY = Math.Round(transform.position.y);
        var fixedZ = Math.Round(transform.position.z);
        
        transform.position = new Vector3((int) fixedX, (int) fixedY, (int) fixedZ);
    }

    private void FixRotation()
    {
        var fixedX = Math.Round(transform.eulerAngles.x);
        var fixedY = Math.Round(transform.eulerAngles.y);
        var fixedZ = Math.Round(transform.eulerAngles.z);

        transform.eulerAngles = new Vector3((int) fixedX, (int) fixedY, (int) fixedZ);
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
        Action movement;

        if (Input.GetAxis("Horizontal") > 0)
        {
            this.movingDirection = Vector3.right;
            movement = () => StartCoroutine(Rotate(movingDirection));
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            this.movingDirection = Vector3.left;
            movement = () => StartCoroutine(Rotate(movingDirection));
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            this.movingDirection = Vector3.forward;
            movement = Move;
        }
        else
        {
            return;
        }

        this.isMoving = true;

        playerSpeedBeforeMovement = GameManager.Instance.playerSpeed;
        movement();
    }
}