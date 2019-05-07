using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListItemBehaviour : MonoBehaviour
{
    private string _mapName;
    [SerializeField] private Text label;

    public string MapName
    {
        get { return _mapName; }
        set
        {
            label.text = value;
            _mapName = value;
        }
    }
}
