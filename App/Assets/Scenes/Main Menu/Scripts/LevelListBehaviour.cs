using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelListBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject listItem;
    
    void Start()
    {
        foreach (var file in Directory.GetFiles(Const.MAP_DIRECTORY, "*.map"))
        {
            var mapEntry = Instantiate(listItem, this.transform);
            LevelListItemBehaviour mapEntryBehaviour = mapEntry.GetComponent<LevelListItemBehaviour>();

            mapEntryBehaviour.MapName = Path.GetFileNameWithoutExtension(Path.Combine(Const.MAP_DIRECTORY, file));
        }
    }
}
