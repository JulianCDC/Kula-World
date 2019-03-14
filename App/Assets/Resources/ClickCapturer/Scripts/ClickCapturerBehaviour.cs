using UnityEngine;
using UnityEngine.Serialization;
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
}
