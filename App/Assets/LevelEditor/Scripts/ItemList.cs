using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    private GameObject[] prefabs;
    public Image previewTemplate;

    void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("EditorItemPrefab");
        string position = "up";

        foreach (GameObject prefab in prefabs)
        {
            GameObject gameObject = Instantiate(prefab, this.gameObject.transform);

            Image image = Instantiate(previewTemplate, this.transform);

            RuntimePreviewGenerator.TransparentBackground = true;
            Sprite imageSprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(gameObject.transform, 100, 100), new Rect(0, 0, 100, 100), new Vector2(0, 0));

            image.sprite = imageSprite;

            Texture2D texture = new Texture2D(100, 100, TextureFormat.RGB24, false);

            texture.ReadPixels(image.sprite.rect, 0, 0);
            texture.Apply();

            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/" + prefab.name + "_" + position + ".png", bytes);

            Destroy(gameObject);
            Destroy(image);
        }
    }

    void Update()
    {
        
    }
}
