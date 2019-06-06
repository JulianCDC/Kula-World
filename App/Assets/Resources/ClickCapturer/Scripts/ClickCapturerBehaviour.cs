using UnityEngine;
using UnityEngine.UI;

public class ClickCapturerBehaviour : MonoBehaviour
{
    public float Transparency
    {
        set
        {
            Image image = GetComponent<Image>();
            Color imageColor = image.color;
            imageColor.a = value;
            image.color = imageColor;
        }
    }
    
    public static ClickCapturerBehaviour GenerateIn(Transform canvas)
    {
        GameObject clickCapturerResource = Resources.Load<GameObject>("ClickCapturer/Prefabs/ClickCapturer");
        GameObject clickCapturer = Instantiate(clickCapturerResource, canvas);
        ClickCapturerBehaviour clickCapturerBehaviour = clickCapturer.GetComponent<ClickCapturerBehaviour>();

        return clickCapturerBehaviour;
    }
}
