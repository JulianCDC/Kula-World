using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverAndClick : MonoBehaviour
{
    public AudioClip MusicClip;

    public AudioSource MusicSource;

    void Start()
    {
        MusicSource.clip = MusicClip;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MusicSource.Play();
        }
    }
}