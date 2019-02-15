using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditableBlockBehaviour : MonoBehaviour
{
    private bool selected = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Select()
    {
        this.selected = true;
        print("selected");
    }

    public void UnSelect()
    {
        this.selected = false;
        print("unselected");
    }
}
