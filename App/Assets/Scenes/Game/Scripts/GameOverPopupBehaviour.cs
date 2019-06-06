using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopupBehaviour : MonoBehaviour
{
    [SerializeField] private Button retry;
    [SerializeField] private Button exit;
    public AudioSource DeathSource;
    public AudioClip DeathClip;

    private void Start()
    {
        DeathSource.clip = DeathClip;
        DeathSource.Play();
        if (retry != null)
        {
            retry.onClick.AddListener(delegate
            {
                retry.onClick.AddListener(delegate
                {
                    GameManager.Instance.TotalReset();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                });
            });
        }

        exit.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
    }
}
