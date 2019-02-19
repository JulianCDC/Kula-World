using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuElement : MonoBehaviour
{
    // list actions here
    public void LoadEditor()
    {
        SceneManager.LoadScene(1);
    }
}
