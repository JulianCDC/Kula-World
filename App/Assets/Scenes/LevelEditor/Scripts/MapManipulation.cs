using System.Collections;
using System.Collections.Generic;

public class MapManipulation
{
    static void CreateMapFile()
    {

    }

    static void MoveItem(int id, int newXPos, int newYPos, int newZPos)
    {
        CheckIfMoveIsPossible(id, newXPos, newYPos, newZPos);
    }

    static void CreateItem(string gameObjectName, int id, int x, int y, int z)
    {

    }

    static void CreateItem(string gameObjectName, int id, int[] positions)
    {
        CreateItem(gameObjectName, id, positions[0], positions[1], positions[2]);
    }

    static void CheckIfMoveIsPossible(int id, int newXPos, int newYPos, int newZPos)
    {

    }
}
