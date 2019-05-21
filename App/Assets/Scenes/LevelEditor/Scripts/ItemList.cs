using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Main Behaviour for the list of Preview GameObject displayed in the Editor scene
/// </summary>
public class ItemList : MonoBehaviour
{
    private GameObject[] previews;
    [SerializeField] private Light sceneLight;
    public Image previewTemplate;

    void Start()
    {
        previews = Resources.LoadAll<GameObject>("EditorItemPrefabs");

        int i = 0;
        foreach (GameObject prefab in previews)
        {
            Image image = Instantiate(previewTemplate, this.transform);
            PreviewBehaviour imageBehaviour = image.GetComponent<PreviewBehaviour>();

            Sprite imageSprite = GeneratePreviewSpriteForGameObject(prefab);

            imageBehaviour.linkedObject = prefab;

            image.sprite = imageSprite;
            
            if (i == 0)
            {
                imageBehaviour.GenerateInitial();
            }
            
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