using System.Timers;
using UnityEngine;

/// <summary>
/// The behaviour of the player
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    private bool isMoving;
    private Vector3 movingDirection = Vector3.zero;

    void Start()
    {
        GameManager.Instance.maxTime = 60;
        InvokeRepeating(nameof(Tick), 0f, 1.0f);
    }

    void Tick()
    {
        GameManager.Instance.elapsedTime += GameManager.Instance.secondsPerTick;
    }

    void Update()
    {
        if (!this.isMoving)
        {
            ListenForMovement();
        }
    }

    private void Move()
    {
        // TODO : remplacer par méthode écrite manuellement un jour peut être
        iTween.MoveBy(this.gameObject, iTween.Hash("amount", movingDirection, "time", .25f / GameManager.Instance.playerSpeed, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "delay", .0, "oncomplete", "StopMoving"));
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

        this.isMoving = true;

        Move();
    }
}