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
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject && !this.selected)
                {
                    Select();
                }

                if (this.selected && hit.collider.gameObject != this.gameObject)
                {
                    UnSelect();
                }
            }
            else
            {
                UnSelect();
            }
        }
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
