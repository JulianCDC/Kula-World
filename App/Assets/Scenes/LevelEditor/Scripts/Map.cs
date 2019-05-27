using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// An object used for saving and loading serialized maps
/// </summary>
[XmlRoot("Map")]
public class Map
{
    [XmlArray("Blocks"), XmlArrayItem("Block")]
    public List<XmlBlock> blocks = new List<XmlBlock>();

    [XmlElement("Meta")] public MapMetadata metadata;
    
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

        if (CheckIfBlockHasFruit(blockXml))
        {
            mapInstance.hasFruit[(int) blockXml.fruit] = true;
        }

        mapInstance.blocks.Add(blockXml);
        currentBlockId += 1;
        return true;
    }

    public static bool HasAllFruits()
    {
        bool[] fruits = mapInstance.hasFruit;

        return fruits[0] && fruits[1] && fruits[2] && fruits[3] && fruits[4];
    }

    public static bool CheckIfBlockHasFruit(XmlBlock blockXml)
    {
        return blockXml.fruit != Fruit.fruits.none;
    }
    
    public static bool DeleteBlock(XmlBlock blockXml)
    {
        mapInstance.blocks.RemoveAll(properties => properties.id == blockXml.id);

        if (CheckIfBlockHasFruit(blockXml))
        {
            mapInstance.hasFruit[(int) blockXml.fruit] = false;
        }
        
        return true;
    }

    public static void ChangeItemPosition(XmlBlock blockXml, WithItemBehaviour.Positions newPosition)
    {
        XmlBlock block = mapInstance.blocks.Find(properties => properties.id == blockXml.id);

        mapInstance.blocks.Remove(block);

        block.itemPosition = newPosition;

        mapInstance.blocks.Add(block);
    }

    public static bool IsEmpty(Vector3 position)
    {
        return !mapInstance.blocks.Exists(properties => properties.position == position);
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
        bool isExit = blockXml.objectType == "Block with exit";
        
        foreach (XmlBlock blockProperty in this.blocks)
        {
            if (blockXml.hasItem)
            {
                // TODO : check if item doesn't collide with blockd
            }

            if (isExit && blockProperty.objectType == "Block with exit")
            {
                return false;
            }
        }

        if (this.hasFruit[(int) blockXml.fruit])
        {
            return false;
        }

        return true;
    }

    public static void WriteToLocation(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Map));

        string mapDirectoryPath = Const.MAP_DIRECTORY;
        System.IO.Directory.CreateDirectory(mapDirectoryPath);

        string filePath =
            Helpers.GetAvailableFilePath(mapDirectoryPath + fileName + ".map");

        FileStream stream = new FileStream(filePath, FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
        serializer.Serialize(streamWriter, mapInstance);
        stream.Close();
    }

    public static Map Load(string fileName)
    {
        XmlSerializer mapSerialized = new XmlSerializer(typeof(Map));
        
        string mapPath = Const.MAP_DIRECTORY + fileName + ".map";
        FileStream  readMapFile = new FileStream(mapPath, FileMode.Open);

        Map deserializedMap = (Map) mapSerialized.Deserialize(readMapFile);
        mapInstance = deserializedMap;
        return deserializedMap;
    }

    public static Map Load(TextAsset file)
    {
        var mapSerializer = new XmlSerializer(typeof(Map));
        XmlReader xmlReader = XmlReader.Create(new MemoryStream(file.bytes));

        Map deserializedMap = (Map) mapSerializer.Deserialize(xmlReader);
        mapInstance = deserializedMap;
        return deserializedMap;
    }
}