using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBehaviour : MonoBehaviour
{
    private EditableBlockBehaviour editableBlockScript;
    private GameObject _LinkedObject;

    public GameObject LinkedObject
    {
        get { return this._LinkedObject; }
        set
        {
            this._LinkedObject = value;
            this.editableBlockScript = (EditableBlockBehaviour) value.GetComponent(typeof(EditableBlockBehaviour));
        }
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void AddToScene()
    {
        GameObject gameObject = Instantiate(LinkedObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        gameObject.transform.parent = GameObject.Find("Map").transform;
        this.editableBlockScript.Select();
    }
}
