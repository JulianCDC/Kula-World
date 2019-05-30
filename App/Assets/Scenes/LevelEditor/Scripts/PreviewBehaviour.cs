﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class PreviewBehaviour : MonoBehaviour
{
    private Color oldColor;
    [NonSerialized] public GameObject linkedObject;

    public void GenerateInitial()
    {
        GameObject createdBlock = Instantiate(linkedObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0));
        EditorManager.Instance.SelectedBlock = createdBlock;
        var editableBlockScript = createdBlock.GetComponent<EditableBlockBehaviour>();
        createdBlock.transform.parent = GameObject.Find("Map").transform;
        Destroy(editableBlockScript, 1);
    }

    public void OnClick()
    {
        EditorManager.Instance.newBlock = linkedObject;
        EditorManager.Instance.SelectedPreviewTile = this.gameObject;
        GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.up);
    }

    public void Select()
    {
        var image = GetComponent<Image>();
        oldColor = image.color;
        image.color = Color.gray;
    }

    public void Unselect()
    {
        var image = GetComponent<Image>();
        image.color = oldColor;
    }
}