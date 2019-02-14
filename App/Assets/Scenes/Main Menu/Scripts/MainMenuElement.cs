using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuElement : MonoBehaviour
{
    // list actions here
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ExempleScene");
    }
}
