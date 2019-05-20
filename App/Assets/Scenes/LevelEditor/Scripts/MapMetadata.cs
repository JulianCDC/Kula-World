using System;
using System.Xml.Serialization;

[Serializable]
public struct MapMetadata
{
    [XmlAttribute("keys"), NonSerialized] public int numberOfKeys;
    [XmlAttribute("time"), NonSerialized] public int timeToFinish;
}