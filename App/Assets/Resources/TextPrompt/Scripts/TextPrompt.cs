using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// The behaviour for text prompt 
/// </summary>
/// Contains two buttons, a label and a text input
public class TextPrompt : MonoBehaviour
{
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private InputField userInput;
    [SerializeField] private Text hint;
    [SerializeField] private Text inputPlaceholder;

    /// <summary>
    /// The action performed on click on <see cref="okButton"/>
    /// </summary>
    public UnityAction OkAction
    {
        set { this.okButton.GetComponent<Button>().onClick.AddListener(value); }
    }

    /// <summary>
    /// The action performed on click on <see cref="cancelButton"/>
    /// </summary>
    public UnityAction CancelAction
    {
        set { this.cancelButton.GetComponent<Button>().onClick.AddListener(value); }
    }

    /// <summary>
    /// The text written by the user in the <see cref="userInput"/>
    /// </summary>
    public String UserInputText
    {
        get { return userInput.text; }
    }

    /// <summary>
    /// The placeholder text of the <see cref="userInput"/>
    /// </summary>
    public String Placeholder
    {
        set { this.inputPlaceholder.text = value; }
    }

    /// <summary>
    /// The text of the label describing the prompt
    /// </summary>
    public String Text
    {
        set { this.hint.text = value; }
    }

    private void Start()
    {
        this.OkAction = delegate { Destroy(this.gameObject); };
        this.CancelAction = delegate { Destroy(this.gameObject); };
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}