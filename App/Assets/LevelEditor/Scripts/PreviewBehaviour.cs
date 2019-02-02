using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBehaviour : MonoBehaviour
{
    public GameObject linkedObject;
    private EditableBlockBehaviour editableBlockScript;

    void Start()
    {
        this.editableBlockScript = (EditableBlockBehaviour) this.linkedObject.GetComponent(typeof(EditableBlockBehaviour));
    }

    void Update()
    {
        
    }

    void AddToScene()
    {
        Instantiate(linkedObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        this.editableBlockScript.Select();
    }
}
