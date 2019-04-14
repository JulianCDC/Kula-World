using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockBehaviour : MonoBehaviour
{
    private bool isMoving;
    private Vector3 movingDirection = Vector3.zero;

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
        iTween.MoveBy(this.gameObject, iTween.Hash("amount", movingDirection, "time", .25f / GameManager.Instance.playerSpeed, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "delay", .0, "oncomplete", nameof(StopMoving)));
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
