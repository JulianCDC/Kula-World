using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    private Texture2D[] textures;
    public Image previewTemplate;

    void Start()
    {
        textures = Resources.LoadAll<Texture2D>("EditorItemPreview");

        foreach (Texture2D prefabTexture in textures)
        {
            Image image = Instantiate(previewTemplate, this.transform);

            Sprite imageSprite = Sprite.Create(prefabTexture, new Rect(0, 0, 128, 128), new Vector2(0, 0));

            image.sprite = imageSprite;
        }
    }

    void Update()
    {
        
    }
}
