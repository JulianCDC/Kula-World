using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIBehaviour : MonoBehaviour
{
    public static GUIBehaviour Instance { get; private set; }

    [SerializeField] public KeyIndicatorBehaviour front;
    [SerializeField] public KeyIndicatorBehaviour back;
    [SerializeField] public KeyIndicatorBehaviour right;
    [SerializeField] public KeyIndicatorBehaviour left;
    [SerializeField] public KeyIndicatorBehaviour up;
    [SerializeField] public KeyIndicatorBehaviour down;
    public KeyIndicatorBehaviour currentSelected;

    void Start()
    {
        Instance = this;
    }

    public void Toggle(ref KeyIndicatorBehaviour keyIndicatorBehaviour)
    {
        if (currentSelected)
        {
            currentSelected.Toggled = false;
        }

        currentSelected = keyIndicatorBehaviour;
        keyIndicatorBehaviour.Toggled = true;
    }
}
