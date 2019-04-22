using System;
using UnityEngine;

public class PlayerBlockBehaviour : MonoBehaviour
{
    private bool isMoving;
    private Vector3 movingDirection = Vector3.zero;
    [SerializeField] private GameObject player;
    private PlayerBehaviour playerBehaviour;

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
            StopMoving();
            // use slerp for transition
        }
        else if (Map.isEmpty(transform.position + transform.TransformDirection(movingDirection)))
        {
            Rotate(movingDirection);
        }
        else
        {
            TranslatePlayer(movingDirection);
        }
    }

    private void Rotate(Vector3 amount)
    {
        var rotationAmount = new Vector3(amount.z, amount.x, amount.y);

        iTween.RotateBy(this.gameObject,
            iTween.Hash("amount", rotationAmount / 4, "time", .25f / GameManager.Instance.playerSpeed, "easetype",
                iTween.EaseType.linear, "oncomplete",
                nameof(StopMoving)));
    }

    private void TranslatePlayer(Vector3 amount)
    {
        // TODO : remplacer par méthode écrite manuellement un jour peut être
        iTween.MoveBy(this.gameObject,
            iTween.Hash("amount", amount, "time", .25f / GameManager.Instance.playerSpeed, "easetype",
                iTween.EaseType.linear, "oncomplete",
                nameof(StopMoving)));
    }

    private void StopMoving()
    {
        isMoving = false;
        movingDirection = Vector3.zero;
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

        if (Input.GetKeyDown("right"))
        {
            this.movingDirection = Vector3.right;
            movement = () => Rotate(movingDirection);
        }
        else if (Input.GetKeyDown("left"))
        {
            this.movingDirection = Vector3.left;
            movement = () => Rotate(movingDirection);
        }
        else if (Input.GetKeyDown("up"))
        {
            this.movingDirection = Vector3.forward;
            movement = Move;
        }
        else if (Input.GetKeyDown("down"))
        {
            this.movingDirection = Vector3.back;
            movement = Move;
        }
        else
        {
            return;
        }

        this.isMoving = true;

        movement();
    }
}