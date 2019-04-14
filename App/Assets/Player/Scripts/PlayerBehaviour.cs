using System.Timers;
using UnityEngine;

/// <summary>
/// The behaviour of the player
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject invisibleBlock;

    void Start()
    {
        GameManager.Instance.maxTime = 60;
        InvokeRepeating(nameof(Tick), 0f, 1.0f);
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