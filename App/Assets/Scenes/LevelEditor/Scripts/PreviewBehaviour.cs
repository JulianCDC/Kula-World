using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBehaviour : MonoBehaviour
{
    private EditableBlockBehaviour editableBlockScript;
    [System.NonSerialized]
    public GameObject LinkedObject;

    public void AddToScene()
    {
        GameObject gameObject = Instantiate(LinkedObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        gameObject.transform.parent = GameObject.Find("Map").transform;
    }
}
