using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class MenuBarButtonBehaviour : MonoBehaviour
{   
    public enum Type
    {
        action,
        scene
    };

    public Type actionType;
    public int actionSceneId;
    public UnityEngine.Events.UnityEvent actionFunction;
    public GameObject rootCanvas;

    public void ExecuteAction()
    {
        switch (actionType)
        {
            case Type.scene:
                UnityEngine.SceneManagement.SceneManager.LoadScene(actionSceneId);
                break;
            case Type.action:
                actionFunction.Invoke();
                break;
        }
    }

    // actions' functions
    public void PromptForSave()
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
}