using UnityEngine;
using UnityEngine.UI;

/// <summary>The ClickCapturer is used to prevent click action to trigger in the area where it has been created.</summary>
///     The main Behaviour of the ClickCapturer GameObject.<br/>
///     Example: For preventing click in UI element when a specific UI element has focus.<br/>
public class ClickCapturerBehaviour : MonoBehaviour
{
    /// <summary>
    ///     Set the transparency by changing the alpha value of the ClickCapturer color.
    /// </summary>
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

    /// <summary>
    ///    Generate an instance of the ClickCapturer GameObject and return the ClickCapturerBehaviour linked to this GameObject.
    /// </summary>
    /// <param name="canvas">The transform object of the canvas to generate the GameObject in.</param>
    /// <returns>The ClickCapturerBehaviour of the generated GameObject.</returns>
    public static ClickCapturerBehaviour GenerateIn(Transform canvas)
    {
        GameObject clickCapturerResource = Resources.Load<GameObject>("ClickCapturer/Prefabs/ClickCapturer");
        GameObject clickCapturer = Instantiate(clickCapturerResource, canvas);
        ClickCapturerBehaviour clickCapturerBehaviour = clickCapturer.GetComponent<ClickCapturerBehaviour>();

        return clickCapturerBehaviour;
    }
}
