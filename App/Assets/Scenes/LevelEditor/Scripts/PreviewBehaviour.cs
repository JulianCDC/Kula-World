using System;
using UnityEngine;

/// <summary>
/// The behaviour of the preview images displayed in the right slider in the editor
/// </summary>
public class PreviewBehaviour : MonoBehaviour
{
    private EditableBlockBehaviour editableBlockScript;
    /// <summary>
    /// The object that will be instantiated when the preview is clicked
    /// </summary>
    [NonSerialized]
    public GameObject LinkedObject;

    /// <summary>
    /// Add <see cref="LinkedObject"/> to the <see cref="Map"/> GameObject in the scene
    /// </summary>
    public void AddToScene()
    {
        GameObject gameObject = Instantiate(LinkedObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        gameObject.transform.parent = GameObject.Find("Map").transform;
    }
}
