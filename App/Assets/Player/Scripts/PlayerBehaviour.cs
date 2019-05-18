using System;
using System.Timers;
using UnityEngine;

/// <summary>
/// The behaviour of the player
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject invisibleBlock;
    public float rotationVelocity;

    public void RotateAnimation(Vector3 rotationDirection)
    {
        this.transform.Rotate(new Vector3(rotationDirection.z * rotationVelocity, 0, 0) * GameManager.Instance.playerSpeed);
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
}