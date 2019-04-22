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

    void Start()
    {
        GameManager.Instance.maxTime = 60;
        InvokeRepeating(nameof(Tick), 0f, 1.0f);
    }

    public void RotateAnimation(Vector3 rotationDirection)
    {
        this.transform.Rotate(new Vector3(rotationDirection.z * rotationVelocity, 0, 0));
    }

    void Tick()
    {
        GameManager.Instance.elapsedTime += GameManager.Instance.secondsPerTick;
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