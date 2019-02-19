using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBarButtonBehaviour : MonoBehaviour
{
    public enum Type { action, scene };
    public Type actionType;
    public int actionSceneId;
    public UnityEngine.Events.UnityEvent actionFunction;

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
}
