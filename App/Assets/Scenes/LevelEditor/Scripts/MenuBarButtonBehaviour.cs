using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// The behaviour of the buttons in the top bar menu
/// </summary>
public class MenuBarButtonBehaviour : MonoBehaviour
{   
    /// <summary>
    /// Specify the possible type for the onclick action of the button
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// A Unity Event
        /// </summary>
        action,
        /// <summary>
        /// The id of a scene
        /// </summary>
        scene
    };

    /// <summary>
    /// The type of the action performed on click
    /// </summary>
    public Type actionType;
    /// <summary>
    /// The id of the destination scene (ignored if <see cref="actionType"/> = <see cref="Type.action"/>)
    /// </summary>
    public int actionSceneId;
    /// <summary>
    /// The action performed on click
    /// </summary>
    public UnityEvent actionFunction;
    /// <summary>
    /// The canvas the button belongs to
    /// </summary>
    public GameObject rootCanvas;
    
    /// <summary>
    /// Launch the action assigned to this button
    /// </summary>
    public void ExecuteAction()
    {
        switch (actionType)
        {
            case Type.scene:
                SceneManager.LoadScene(actionSceneId);
                break;
            case Type.action:
                actionFunction.Invoke();
                break;
        }
    }

    // actions' functions
    
    /// <summary>
    /// Open a prompt for choosing the name and saving the map
    /// </summary>
    public void PromptForSave()
    {
        if(Map.fruityBlock == 5)
        {
            ClickCapturerBehaviour clickCapturer = ClickCapturerBehaviour.GenerateIn(rootCanvas.transform);
            clickCapturer.Transparency = 0f;

            GameObject prompt = Resources.Load<GameObject>("TextPrompt/Prefabs/TextPrompt");
            prompt = Instantiate(prompt, rootCanvas.transform);

            TextPrompt promptBehaviour = prompt.GetComponent<TextPrompt>();

            promptBehaviour.Text = "What will be the name of the map ?";
            promptBehaviour.Placeholder = "name...";

            Action closePrompt = () =>
            {
                Destroy(clickCapturer.gameObject);
                promptBehaviour.Destroy();
            };

            promptBehaviour.OkAction = () =>
            {
                string chosenFileName = promptBehaviour.UserInputText;
                string formattedFileName = Helpers.FormatFileName(chosenFileName);

                Map.WriteToLocation(formattedFileName);
                closePrompt();
            };

            promptBehaviour.CancelAction = () => { closePrompt(); };
        }
        else
        {
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject errorSave = Instantiate(Resources.Load<GameObject>("Prefabs/ErrorSave"), canvas.transform);
            Destroy(errorSave, 5);
        } 
    }
}