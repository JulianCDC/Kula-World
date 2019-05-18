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

    private void Start()
    {
        retry.onClick.AddListener(delegate
        {
            GameManager.Instance.TotalReset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        exit.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
    }
}
