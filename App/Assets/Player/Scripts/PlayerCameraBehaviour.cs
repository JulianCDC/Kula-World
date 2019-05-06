using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraBehaviour : MonoBehaviour
{
    public enum Position
    {
        close,
        normal
    }
    
    [SerializeField] private TransformStruct closeToPlayer;
    [SerializeField] private TransformStruct defaultTransform;

    public void ChangePositionTo(Position newPosition)
    {
        switch (newPosition)
        {
            case Position.close:
                LoadTransform(closeToPlayer);
                break;
            default:
            case Position.normal:
                LoadTransform(defaultTransform);
                break;
        }
    }

    private void LoadTransform(TransformStruct transformStruct)
    {
        this.transform.position = new Vector3(transformStruct.position.x, transformStruct.position.y, transformStruct.position.z);
        this.transform.rotation = Quaternion.Euler(new Vector3(transformStruct.rotation.x, transformStruct.rotation.y, transformStruct.rotation.z));
        this.transform.localScale = new Vector3(transformStruct.scale.x, transformStruct.scale.y, transformStruct.scale.z);
    }
}
