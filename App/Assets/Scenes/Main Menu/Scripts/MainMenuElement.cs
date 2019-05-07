using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The Main Behaviour for the Main Menu Scene
/// </summary>
public class MainMenuElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text childText;
    [SerializeField] private Color textHoverColor;
    private Color oldTextColor;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        oldTextColor = childText.color;
        childText.color = textHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        childText.color = oldTextColor;
    }
    
    // list actions here
    
    /// <summary>
    /// Load the Editor Scene
    /// </summary>
    public void LoadEditor()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameplay()
    {
        SceneManager.LoadScene(2);
    }
}
