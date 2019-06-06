using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : Collectible
{
    public AudioClip ExitClip;
    public AudioSource ExitSource;

    public override void Collected()
    {
        ExitSource.clip = ExitClip;

        if (GameManager.Instance.requiredKeys == GameManager.Instance.retrievedKeys)
        {
            if (GameManager.Instance.officialLevel)
            {
                GameManager.Instance.NextLevel();
                ExitSource.Play();
            }
            else
            {
                GameSceneBehaviour.Win();
                ExitSource.Play();
            }
        }
    }
}
