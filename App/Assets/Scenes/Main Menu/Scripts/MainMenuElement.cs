using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Main Behaviour for the Main Menu Scene
/// </summary>
public class MainMenuElement : MonoBehaviour
{
    // list actions here
    
    /// <summary>
    /// Load the Editor Scene
    /// </summary>
    public void LoadEditor()
    {
        SceneManager.LoadScene(1);
    }
}
