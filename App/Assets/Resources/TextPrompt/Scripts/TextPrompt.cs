using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextPrompt : MonoBehaviour
{
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private InputField userInput;
    [SerializeField] private Text hint;
    [SerializeField] private Text inputPlaceholder;

    [SerializeField] private InputField timeInput;
    [SerializeField] private Text timeHint;
    [SerializeField] private Text timePlaceholder;
    
    public UnityAction OkAction
    {
        set { this.okButton.GetComponent<Button>().onClick.AddListener(value); }
    }

    public UnityAction CancelAction
    {
        set { this.cancelButton.GetComponent<Button>().onClick.AddListener(value); }
    }

    public String UserInputText
    {
        get { return userInput.text; }
    }

    public int TimeInSecond
    {
        get
        {
            int time;
            bool isInt = int.TryParse(timeInput.text, out time);
            if (isInt)
            {
                return time;
            }
            else
            {
                return 600;
            }
        }
    }

    public String TimeHint
    {
        set { this.timeHint.text = value; }
    }

    public String TimePlaceholder
    {
        set { this.timePlaceholder.text = value; }
    }

    public String Placeholder
    {
        set { this.inputPlaceholder.text = value; }
    }

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