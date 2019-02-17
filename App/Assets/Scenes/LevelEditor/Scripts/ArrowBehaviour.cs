using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public enum Direction
    {
        up,
        down,
        right,
        left,
        front,
        back
    };

    private Direction _direction;
    public Direction direction
    {
        get { return _direction; }
        set
        {
            _direction = value;

            Vector3 position = new Vector3();
            Quaternion rotation = new Quaternion();

            const float distanceFromOrigin = 1.4f;

            switch (value)
            {
                case Direction.up:
                    break;
                case Direction.down:
                    break;
                case Direction.right:
                    position = new Vector3(distanceFromOrigin, 0, 0);
                    rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case Direction.left:
                    position = new Vector3(-distanceFromOrigin, 0, 0);
                    rotation = Quaternion.Euler(90, 0, 180);
                    break;
                case Direction.front:
                    position = new Vector3(0, 0, -distanceFromOrigin);
                    rotation = Quaternion.Euler(90, 90, 0);
                    break;
                case Direction.back:
                    position = new Vector3(0, 0, distanceFromOrigin);
                    rotation = Quaternion.Euler(90, -90, 0);
                    break;
            }

            this.gameObject.transform.position = position;
            this.gameObject.transform.rotation = rotation;
        }
    }
}
