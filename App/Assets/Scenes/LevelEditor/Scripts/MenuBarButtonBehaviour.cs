using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuBarButtonBehaviour : MonoBehaviour
{
    public enum Type
    {
        action,
        scene
    };
    
    public Type actionType;
    public int actionSceneId;
    public UnityEvent actionFunction;
    public GameObject rootCanvas;

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
    
    public void PromptForSave()
    {
        if (Map.HasAllFruits())
        {
            ClickCapturerBehaviour clickCapturer = ClickCapturerBehaviour.GenerateIn(rootCanvas.transform);
            clickCapturer.Transparency = 0f;

            GameObject prompt = Resources.Load<GameObject>("TextPrompt/Prefabs/TextPrompt");
            prompt = Instantiate(prompt, rootCanvas.transform);

            TextPrompt promptBehaviour = prompt.GetComponent<TextPrompt>();

            promptBehaviour.Text = "What will be the name of the map ?";
            promptBehaviour.Placeholder = "name...";

            promptBehaviour.TimeHint = "What will be the time to finish the map";
            promptBehaviour.TimePlaceholder = "Time";



            Action closePrompt = () =>
            {
                Destroy(clickCapturer.gameObject);
                promptBehaviour.Destroy();
            };

            promptBehaviour.OkAction = () =>
            {
                int nbSecond = promptBehaviour.TimeInSecond;
                Map.mapInstance.metadata.timeToFinish = nbSecond;

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