using UnityEngine;

[ExecuteInEditMode]
public class WithItemEditorDisplay : MonoBehaviour
{
    public WithItemBehaviour script;
    
    void Start()
    {
        if (Application.isPlaying) return;
        script.Start();
    }
    
    void Update()
    {
        if (Application.isPlaying) return;
        script.UpdateItemPosition();
    }
}
