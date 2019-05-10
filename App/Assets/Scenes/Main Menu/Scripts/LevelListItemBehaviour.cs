using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListItemBehaviour : Selectable
{
    private string _mapName;
    [SerializeField] private Text label;
    private string oldLabelText;

    private void Start()
    {
        this.AddOnSelectListener(AddSelectionIndicator);
        this.AddOnUnSelectListener(RemoveSelectionIndicator);
    }

    public string MapName
    {
        get { return _mapName; }
        set
        {
            label.text = value;
            _mapName = value;
        }
    }

    private void AddSelectionIndicator()
    {
        this.oldLabelText = this.label.text;
        this.label.text = $"> {this.label.text}";
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
    }

    private void RemoveSelectionIndicator()
    {
        this.label.text = this.oldLabelText;
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
    }
}
