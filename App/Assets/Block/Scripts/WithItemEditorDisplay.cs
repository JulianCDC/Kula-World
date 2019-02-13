using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WithItemEditorDisplay : MonoBehaviour
{
    public WithItemBehaviour script;

    void Start()
    {
#if UNITY_EDITOR
        script.Start();
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        script.setItemPosition();
#endif
    }
}
