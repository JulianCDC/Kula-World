using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Main Behaviour for the list of Preview GameObject displayed in the Editor scene
/// </summary>
public class ItemList : MonoBehaviour
{
    /// <summary>
    /// The textures of all the items to be displayd
    /// </summary>
    private Texture2D[] textures;
    /// <summary>
    /// The template of an item in the list
    /// </summary>
    public Image previewTemplate;

    /// <summary>
    /// Load all the <see cref="textures"/> in the list using <see cref="previewTemplate"/>
    /// </summary>
    void Start()
    {
        textures = Resources.LoadAll<Texture2D>("EditorItemPreview");

        int i = 0;
        foreach (Texture2D prefabTexture in textures)
        {
            Image image = Instantiate(previewTemplate, this.transform);
            PreviewBehaviour imageBehaviour = image.GetComponent<PreviewBehaviour>();

            Sprite imageSprite = Sprite.Create(prefabTexture, new Rect(0, 0, 128, 128), new Vector2(0, 0));

            string prefabName = prefabTexture.name.ToLower().Replace("_", " ");

            imageBehaviour.LinkedObject = Resources.Load<GameObject>("EditorItemPrefabs/editable_" + prefabName);

            image.sprite = imageSprite;
            i += 1;
        }
    }
}