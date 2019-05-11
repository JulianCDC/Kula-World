using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : Collectible
{
    public override void Collected()
    {
        base.Collected();

        if (GameManager.Instance.requiredKeys == GameManager.Instance.retrievedKeys)
        {
            if (GameManager.Instance.officialLevel)
            {
                GameManager.Instance.NextLevel();
            }
            else
            {
                GameSceneBehaviour.Win();
            }
        }
    }
}
