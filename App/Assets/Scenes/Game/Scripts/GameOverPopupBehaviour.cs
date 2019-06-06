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
    [SerializeField] private Button reset;
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
                GameManager.Instance.TotalReset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        if (reset != null)
        {
            reset.onClick.AddListener(delegate
            {
                PlayerData.Erase();
                GameManager.Instance.TotalReset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        exit.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
    }
}