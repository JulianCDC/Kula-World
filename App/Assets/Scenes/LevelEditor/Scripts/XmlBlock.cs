using System;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Serializable structure for saving block into xml file
/// </summary>
[Serializable]
public struct XmlBlock
{
    [XmlAttribute("id"), NonSerialized] public int id;
    [XmlAttribute("has_item")] public bool hasItem;
    /// <summary>
    /// The name of the original game object assigned to this block
    /// </summary>
    /// This property is used when loading back the block into a map
    [XmlAttribute("type"), NonSerialized] public string objectType;
    [XmlAttribute("x"), NonSerialized] public float xPos;
    [XmlAttribute("y"), NonSerialized] public float yPos;
    [XmlAttribute("z"), NonSerialized] public float zPos;

    /// <summary>
    /// The position of the item assigned to the block, <see cref="WithItemBehaviour.Positions.none"/> if the item doesn't have an object
    /// </summary>
    [XmlAttribute("item_position"), NonSerialized]
    public WithItemBehaviour.Positions itemPosition;

    /// <summary>
    /// The fruit of the object, <see cref="Fruit.fruits.none"/> if collectible is not a fruit
    /// </summary>
    [XmlIgnore, NonSerialized] public Fruit.fruits fruit;

    /// <summary>
    /// Get position of the block as Vector3
    /// </summary>
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