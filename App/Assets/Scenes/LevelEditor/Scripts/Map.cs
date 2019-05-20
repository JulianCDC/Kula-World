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
    /// <summary>
    /// A list of all serialized blocks in the map
    /// </summary>
    [XmlArray("Blocks"), XmlArrayItem("Block")]
    public List<XmlBlock> blocks = new List<XmlBlock>();

    // replace with HashSet
    /// <summary>
    /// An array of boolean set to true if a fruit has already been placed in the map. In the same order as <see cref="Fruit.fruits"/>
    /// </summary>
    [XmlIgnore] public bool[] hasFruit = new bool[Enum.GetNames(typeof(Fruit.fruits)).Length];

    /// <summary>
    /// An instance of the map hold into the Map class
    /// </summary>
    public static Map mapInstance = new Map();
    /// <summary>
    /// The ID of the next new block, defined by the id of the last block placed
    /// </summary>
    /// Increment by 1 each block
    public static int currentBlockId = 1;
    
    /// <summary>
    /// Instantiate the map with the last value of <see cref="hasFruit"/> to false
    /// </summary>
    /// It is the value of none and we can always place block with no object
    public Map()
    {
        hasFruit[hasFruit.Length - 1] = false; // Fruit.fruits.none always false
    }

    /// <summary>
    /// Add a block to the <see cref="mapInstance"/> if possible
    /// </summary>
    /// <param name="blockXml">The block to add</param>
    /// <returns>true if the block was added, false otherwise</returns>
    public static bool AddBlock(XmlBlock blockXml) // TODO : clean
    {
        if (!mapInstance.IsAddPossible(blockXml))
        {
            return false;
        }

        if (checkIfBlockHasFruit(blockXml))
        {
            mapInstance.hasFruit[(int) blockXml.fruit] = true;
        }

        mapInstance.blocks.Add(blockXml);
        currentBlockId += 1;
        return true;
    }

    public static bool checkIfBlockHasFruit(XmlBlock blockXml)
    {
        return blockXml.fruit != Fruit.fruits.none;
    }

    /// <summary>
    /// Remove the block from the <see cref="mapInstance"/>
    /// </summary>
    /// <param name="blockXml">The block to remove</param>
    /// <returns>true if the block was successfully removed</returns>
    public static bool DeleteBlock(XmlBlock blockXml)
    {
        mapInstance.blocks.RemoveAll(properties => properties.id == blockXml.id);

        if (checkIfBlockHasFruit(blockXml))
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

    public static bool isEmpty(Vector3 position)
    {
        return !mapInstance.blocks.Exists(properties => properties.position == position);
    }

    /// <summary>
    /// Move a block from the map to a new position if possible
    /// </summary>
    /// Check if the block can move and, if possible, find it in the <see cref="mapInstance"/> and change its position
    /// <param name="blockToMoveBehaviour">The block to move</param>
    /// <param name="newPosition">The new position of the block</param>
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



    /// <summary>
    /// Check if a block can move to the new position
    /// </summary>
    /// <param name="newPosition">The position to check</param>
    /// <returns>true if the position is free, false otherwise</returns>
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

    /// <summary>
    /// Check if a block can be added
    /// </summary>
    /// <param name="blockXml">The block to add</param>
    /// <returns>true if the block can be added, otherwise false</returns>
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

    /// <summary>
    /// Save map to Application.temporaryCachePath
    /// </summary>
    public static void WriteToTempLocation()
    {
        var serializer = new XmlSerializer(typeof(Map));
        var stream = new FileStream(Application.temporaryCachePath + "/temp.map", FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
        serializer.Serialize(streamWriter, mapInstance);
        stream.Close();
    }

    /// <summary>
    /// Save map with specified file name
    /// </summary>
    /// <param name="fileName">Name of the file</param>
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