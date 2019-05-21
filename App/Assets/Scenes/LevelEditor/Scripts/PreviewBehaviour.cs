using System;
using UnityEngine;

/// <summary>
/// The behaviour of the preview images displayed in the right slider in the editor
/// </summary>
public class PreviewBehaviour : MonoBehaviour
{
    private EditableBlockBehaviour editableBlockScript;
    [NonSerialized] public GameObject LinkedObject;

    public void AddToScene()
    {
        GenerateAt0();
        editableBlockScript.Select();
    }

    public void GenerateAt0()
    {
        GameObject createdBlock = Instantiate(LinkedObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0));
        EditorManager.Instance.SelectedBlock = createdBlock;
        editableBlockScript = createdBlock.GetComponent<EditableBlockBehaviour>();
        createdBlock.transform.parent = GameObject.Find("Map").transform;
    }

    public void GenerateInitial()
    {
        GenerateAt0();
        Destroy(editableBlockScript);
    }
}