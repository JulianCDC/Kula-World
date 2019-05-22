using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyIndicatorBehaviour : MonoBehaviour
{
    private bool toggled;
    [SerializeField] private RawImage image;
    private Color oldTint;

    public bool Toggled
    {
        get { return toggled; }
        set
        {
            var currentColor = image.color;
            if (value)
            {
                oldTint = currentColor;
                currentColor = Color.gray;
            }
            else
            {
                currentColor = oldTint;
            }

            image.color = currentColor;

            toggled = value;
        }
    }
}
