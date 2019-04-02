using UnityEngine;

/// <summary>
/// Behaviour specific to the unity editor for the block with item GameObject
/// </summary>
[ExecuteInEditMode]
public class WithItemEditorDisplay : MonoBehaviour
{
    /// <summary>
    /// Main behaviour of the block with item
    /// </summary>
    public WithItemBehaviour script;

    /// <summary>
    /// call the start function of <see cref="WithItemBehaviour"/>
    /// </summary>
    void Start()
    {
#if UNITY_EDITOR
        script.Start();
#endif
    }
    
    /// <summary>
    /// update the position of <see cref="WithItemBehaviour"/>
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR
        script.setItemPosition();
#endif
    }
}
