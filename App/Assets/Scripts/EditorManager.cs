using UnityEngine;

public class EditorManager : Singleton<EditorManager>
{
    public GameObject selectedBlock;
    public EditableBlockBehaviour selectedBlockBehaviour;

    public GameObject SelectedBlock
    {
        get { return selectedBlock; }
        set
        {
            selectedBlock = value;
            selectedBlockBehaviour = selectedBlock.GetComponent<EditableBlockBehaviour>();
        }
    }
}