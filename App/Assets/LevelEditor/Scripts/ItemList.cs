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

        int i = 0;
        foreach (Texture2D prefabTexture in textures)
        {
            Image image = Instantiate(previewTemplate, this.transform);
            //image.rectTransform.Translate(Vector2.down * ((i * 100) + (i * 2)));

            Sprite imageSprite = Sprite.Create(prefabTexture, new Rect(0, 0, 128, 128), new Vector2(0, 0));

            image.sprite = imageSprite;
            i += 1;
        }
    }

    void Update()
    {
        
    }
}
