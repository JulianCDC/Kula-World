using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ExempleScene");
    }

    public void RetrunMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}
