using System;
using UnityEngine;

[Serializable]
public class TransformStruct
{
    [Serializable]
    public class TransformType
    {
        public float x;
        public float y;
        public float z;
    }

    [SerializeField] public TransformType position;
    [SerializeField] public TransformType rotation;
    [SerializeField] public TransformType scale;
}