using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    private Image[] previews;

    void Start()
    {
        previews = Resources.LoadAll<Image>("EditorItemPreview");

        foreach (Image preview in previews)
        {
            Instantiate(preview, this.gameObject.transform);
        }
    }

    void Update()
    {
        
    }
}
