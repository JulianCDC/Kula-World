using System.IO;
using UnityEngine;

public class LevelListBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject listItem;
    private LevelListItemBehaviour selectedEntryBehaviour;
    
    void Start()
    {
        foreach (var file in Directory.GetFiles(Const.MAP_DIRECTORY, "*.map"))
        {
            var mapEntry = Instantiate(listItem, this.transform);
            LevelListItemBehaviour mapEntryBehaviour = mapEntry.GetComponent<LevelListItemBehaviour>();
            
            mapEntryBehaviour.AddOnSelectListener(delegate
            {
                if (selectedEntryBehaviour == mapEntryBehaviour) return;

                if (selectedEntryBehaviour != null)
                {
                    selectedEntryBehaviour.Unselect();
                }

                selectedEntryBehaviour = mapEntryBehaviour;
            });

            mapEntryBehaviour.MapName = Path.GetFileNameWithoutExtension(Path.Combine(Const.MAP_DIRECTORY, file));
        }
    }
}
