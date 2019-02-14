using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBarButtonBehaviour : MonoBehaviour
{
    public enum Type { action, scene };
    public Type actionType;
    public UnityEngine.SceneManagement.Scene actionScene;
    public UnityEngine.Events.UnityEvent actionFunction;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ExecuteAction()
    {
        switch (actionType)
        {
            case Type.scene:
                UnityEngine.SceneManagement.SceneManager.LoadScene(actionScene.buildIndex);
                break;
            case Type.action:
                actionFunction.Invoke();
                break;
        }
    }

    // actions' functions
}
