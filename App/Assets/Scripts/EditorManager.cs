using System;
using UnityEngine;

public class EditorManager : Singleton<EditorManager>
{
    private GameObject selectedBlock;
    public EditableBlockBehaviour selectedBlockBehaviour;
    public GameObject newBlock;
    private GameObject selectedPreviewTile;
    private PreviewBehaviour selectedPreviewTileBehaviour;

    public GameObject SelectedBlock
    {
        get { return selectedBlock; }
        set
        {
            selectedBlock = value;
            try
            {
                selectedBlockBehaviour = selectedBlock.GetComponent<EditableBlockBehaviour>();
            }
            catch (NullReferenceException)
            {
                
            }
        }
    }

    public GameObject SelectedPreviewTile
    {
        get { return selectedPreviewTile; }
        set
        {
            if (selectedPreviewTile != null && selectedPreviewTile != value)
            {
                selectedPreviewTileBehaviour.Unselect();
            }

            try
            {
                selectedPreviewTileBehaviour = value.GetComponent<PreviewBehaviour>();
                selectedPreviewTileBehaviour.Select();
            }
            catch (NullReferenceException)
            {
                selectedPreviewTileBehaviour = null;
            }

            selectedPreviewTile = value;
        }
    }

    public void ClearPreSelection()
    {
        if (selectedBlockBehaviour == null) return;

        selectedBlockBehaviour.Cancel();
        SelectedBlock = null;
        selectedBlockBehaviour = null;

        SelectedPreviewTile = null;
        newBlock = null;
    }
}