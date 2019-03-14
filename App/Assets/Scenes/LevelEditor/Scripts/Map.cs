using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Map")]
public class Map
{
    [XmlArray("Blocks"), XmlArrayItem("Block")]
    public List<XmlBlock> blocks = new List<XmlBlock>();

    [XmlIgnore] public bool[] hasFruit = new bool[Enum.GetNames(typeof(Fruit.fruits)).Length];

    public static Map mapInstance = new Map();
    public static int currentBlockId = 1;

    public Map()
    {
        hasFruit[hasFruit.Length - 1] = false; // Fruit.fruits.none always false
    }

    public static bool AddBlock(XmlBlock blockXml) // TODO : clean
    {
        if (!mapInstance.IsAddPossible(blockXml))
        {
            return false;
        }

        if (blockXml.fruit != Fruit.fruits.none)
        {
            mapInstance.hasFruit[(int) blockXml.fruit] = true;
        }

        mapInstance.blocks.Add(blockXml);
        currentBlockId += 1;
        return true;
    }

    public static bool DeleteBlock(XmlBlock blockXml)
    {
        mapInstance.blocks.Remove(blockXml);
        return true;
    }

    public static void MoveBlockTo(EditableBlockBehaviour blockToMoveBehaviour, Vector3 newPosition)
    {
        if (mapInstance.CanBlockMoveTo(newPosition))
        {
            blockToMoveBehaviour.transform.position = newPosition;

            XmlBlock blockToModify =
                mapInstance.blocks.Find(properties => properties == blockToMoveBehaviour.xmlBlock);

            mapInstance.blocks.Remove(blockToModify);

            blockToModify.xPos = newPosition.x;
            blockToModify.yPos = newPosition.y;
            blockToModify.zPos = newPosition.z;

            mapInstance.blocks.Add(blockToModify);
        }
    }

    public bool CanBlockMoveTo(Vector3 newPosition)
    {
        foreach (XmlBlock blockProperty in this.blocks)
        {
            if (newPosition == blockProperty.position)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsAddPossible(XmlBlock blockXml)
    {
        foreach (XmlBlock blockProperty in this.blocks)
        {
            if (blockProperty.position == blockXml.position)
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

        string mapDirectoryPath = Application.persistentDataPath + "/maps/customs/";
        System.IO.Directory.CreateDirectory(mapDirectoryPath);

        string filePath =
            Helpers.GetAvailableFilePath(mapDirectoryPath + fileName + ".map");

        FileStream stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, mapInstance);
        stream.Close();
    }
}