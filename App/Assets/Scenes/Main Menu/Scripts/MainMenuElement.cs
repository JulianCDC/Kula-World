using System;
using System.Threading.Tasks;
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
    [SerializeField] private int destinationSceneId;
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

    public void LoadOfficial()
    {
        GameManager.Instance.TotalReset();
        GameManager.Instance.currentLevel = PlayerData.GetProgress().ToString();
        GameManager.Instance.officialLevel = true;
    }

    public void LoadCustom()
    {
        GameManager.Instance.TotalReset();
        GameManager.Instance.officialLevel = false;
    }

    public void OnClick()
    {
        SceneManager.LoadScene(destinationSceneId);
    }
}