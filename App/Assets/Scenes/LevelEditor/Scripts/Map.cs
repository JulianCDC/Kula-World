using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Map")]
public class Map
{
    [Serializable]
    public struct XmlProperties
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
    }

    [XmlArray("Blocks"), XmlArrayItem("Block")]
    public List<XmlProperties> Blocks = new List<XmlProperties>();

    [XmlIgnore] public bool[] hasFruit = new bool[Enum.GetNames(typeof(Fruit.fruits)).Length];

    public static Map mapInstance = new Map();
    public static int currentBlockId = 1;
    
    public Map()
    {
        hasFruit[hasFruit.Length - 1] = false; // Fruit.fruits.none always false
    }

    public static bool AddBlock(XmlProperties blockXml)
    {
        if (!mapInstance.CheckIfAddIsPossible(blockXml))
        {
            return false;
        }

        if (blockXml.fruit != Fruit.fruits.none)
        {
            mapInstance.hasFruit[(int) blockXml.fruit] = true;
        }

        mapInstance.Blocks.Add(blockXml);
        currentBlockId += 1;
        return true;
    }

    public static bool DeleteBlock(XmlProperties blockXml)
    {
        mapInstance.Blocks.Remove(blockXml);
        return true;
    }

    public static bool MoveItem(int id, float newXPos, float newYPos, float newZPos)
    {
        if (!mapInstance.CheckIfMoveIsPossible(id, newXPos, newYPos, newZPos))
        {
            return false;
        }

        XmlProperties blockToModify = mapInstance.Blocks.Find(x => x.id == id);

        mapInstance.Blocks.Remove(blockToModify);

        blockToModify.xPos = newXPos;
        blockToModify.yPos = newYPos;
        blockToModify.zPos = newZPos;

        mapInstance.Blocks.Add(blockToModify);

        return true;
    }

    public bool CheckIfMoveIsPossible(int id, float newXPos, float newYPos, float newZPos)
    {
        XmlProperties blockToCheck = mapInstance.Blocks.Find(x => x.id == id);
        
        foreach (XmlProperties blockProperty in this.Blocks)
        {
            if (blockProperty.id != id)
            {
                if (newXPos == blockProperty.xPos && newYPos == blockProperty.yPos && newZPos == blockProperty.zPos)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool CheckIfAddIsPossible(XmlProperties blockXml)
    {
        foreach (XmlProperties blockProperty in this.Blocks)
        {
            if (blockProperty.xPos == blockXml.xPos && blockProperty.yPos == blockXml.yPos && blockProperty.zPos == blockXml.zPos)
            {
                return false;
            }

            if (blockXml.hasItem)
            {
                // TODO : check if item doesn't collide with block (preferably in another method)
            }
        }

        if (this.hasFruit[(int) blockXml.fruit])
        {
            return false;
        }

        return true;
    }

    public static void WriteToTempLocation()
    {
        var serializer = new XmlSerializer(typeof(Map));
        var stream = new FileStream(Application.temporaryCachePath + "/temp.map", FileMode.Create);
        serializer.Serialize(stream, mapInstance);
        stream.Close();
    }

    public static void WriteToLocation(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Map));
        FileStream stream = new FileStream(Application.persistentDataPath + "/maps/customs/" + fileName, FileMode.Create);
        serializer.Serialize(stream, mapInstance);
        stream.Close();
    }
}