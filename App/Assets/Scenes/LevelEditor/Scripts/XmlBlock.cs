using System;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public struct XmlBlock
{
    [XmlAttribute("id"), NonSerialized] public int id;
    [XmlAttribute("has_item")] public bool hasItem;
    [XmlAttribute("type"), NonSerialized] public string objectType;
    [XmlAttribute("x"), NonSerialized] public float xPos;
    [XmlAttribute("y"), NonSerialized] public float yPos;
    [XmlAttribute("z"), NonSerialized] public float zPos;

    [XmlAttribute("item_position"), NonSerialized]
    public WithItemBehaviour.Positions itemPosition;

    [XmlIgnore, NonSerialized] public Fruit.fruits fruit;

    public Vector3 position
    {
        get
        {
            return new Vector3(xPos, yPos, zPos);
        }
    }

    public static bool operator ==(XmlBlock lhs, XmlBlock rhs)
    {
        return lhs.id == rhs.id;
    }

    public static bool operator !=(XmlBlock lhs, XmlBlock rhs)
    {
        return lhs.id != rhs.id;
    }
}