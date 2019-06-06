using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContinu : MonoBehaviour
{

    private static SoundContinu instance = null;
    public static SoundContinu Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}