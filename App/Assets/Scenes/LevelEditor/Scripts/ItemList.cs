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

    [SerializeField] private Light sceneLight;

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


            string prefabName = prefabTexture.name.ToLower().Replace("_", " ");
            GameObject objectResource = Resources.Load<GameObject>("EditorItemPrefabs/editable_" + prefabName);

            Sprite imageSprite = GeneratePreviewSpriteForGameObject(objectResource);

            imageBehaviour.LinkedObject = objectResource;

            image.sprite = imageSprite;
            i += 1;
        }
    }

    private Sprite GeneratePreviewSpriteForGameObject(GameObject gameObject)
    {
        LightShadows oldShadows = sceneLight.shadows;
        sceneLight.shadows = LightShadows.None;

        GameObject previewObject = Instantiate(gameObject);
        
        RuntimePreviewGenerator.TransparentBackground = true;
        Texture2D itemPreview = RuntimePreviewGenerator.GenerateModelPreview(previewObject.transform, 256, 256);

        Sprite imageSprite = Sprite.Create(itemPreview, new Rect(0, 0, 256, 256), new Vector2(0, 0));

        Destroy(previewObject);
        sceneLight.shadows = oldShadows;

        return imageSprite;
    }
}