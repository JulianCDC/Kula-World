using System.Collections;
using UnityEngine;

public class ExitPadBehaviour : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CycleColor());
    }

    private IEnumerator CycleColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color currentColor = renderer.material.color;
        
        while (true)
        {
            var t = Mathf.PingPong(Time.time, 1);
            currentColor = Color.Lerp(Color.white, Color.blue, t);

            renderer.material.color = currentColor;
            
            yield return new WaitForEndOfFrame();
        }
    }
}
