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
            Climb();
            StopMoving();
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

    private void Climb()
    {
        this.transform.Translate(movingDirection + Vector3.up);
        this.transform.Rotate(Vector3.back * 90);
        this.transform.Rotate(-new Vector3(movingDirection.z, movingDirection.x, movingDirection.y) * 90);
        this.transform.Rotate(Vector3.down * 90);
    }

    private void Jump()
    {
        // TODO : Disable collision while jumping to prevent collecting item
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

        if (Input.GetKey("right"))
        {
            this.movingDirection = Vector3.right;
            movement = () => Rotate(movingDirection);
        }
        else if (Input.GetKey("left"))
        {
            this.movingDirection = Vector3.left;
            movement = () => Rotate(movingDirection);
        }
        else if (Input.GetKey("up"))
        {
            this.movingDirection = Vector3.forward;
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