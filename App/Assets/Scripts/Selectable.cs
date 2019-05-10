using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour, IPointerClickHandler
{
    private List<Action> onSelect = new List<Action>();
    private List<Action> onUnSelect = new List<Action>();
    private bool selected;

    public void AddOnSelectListener(Action callback)
    {
        onSelect.Add(callback);
    }

    public void AddOnUnSelectListener(Action callback)
    {
        onUnSelect.Add(callback);
    }

    public void RemoveOnSelectListener(Action callback)
    {
        onSelect.Remove(callback);
    }


    public void RemoveOnUnSelectListener(Action callback)
    {
        onUnSelect.Remove(callback);
    }

    public virtual void Select()
    {
        if (selected) return;
        selected = true;
        
        foreach (Action action in onSelect)
        {
            action();
        }
    }
    
    public virtual void Unselect()
    {
        if (!selected) return;
        selected = false;
        
        foreach (Action action in onUnSelect)
        {
            action();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}