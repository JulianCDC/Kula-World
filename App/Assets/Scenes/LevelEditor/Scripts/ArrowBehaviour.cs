using UnityEngine;

/// <summary>The Main Behaviour of an Arrow GameObject</summary>
public class ArrowBehaviour : MonoBehaviour
{
    /// <summary>
    ///     Specify the possible direction of the Arrow.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Pointing up
        /// </summary>
        up,
        /// <summary>
        /// Pointing down
        /// </summary>
        down,
        /// <summary>
        /// Pointing right
        /// </summary>
        right,
        /// <summary>
        /// Pointing left
        /// </summary>
        left,
        /// <summary>
        /// Pointing to the front
        /// </summary>
        front,
        /// <summary>
        /// Pointing to the back
        /// </summary>
        back
    };

    private Direction _direction;
    /// <summary>The direction the Arrow is pointing.</summary>
    public Direction direction
    {
        get { return _direction; }
        set
        {
            _direction = value;

            Vector3 position = this.gameObject.transform.position;
            Quaternion rotation = new Quaternion();

            const float distanceFromOrigin = 1.4f;

            switch (value)
            {
                case Direction.up:
                    position += Vector3.up * distanceFromOrigin;
                    rotation = Quaternion.Euler(0, 90, 90);
                    break;
                case Direction.down:
                    position += Vector3.down * distanceFromOrigin;
                    rotation = Quaternion.Euler(0, 90, -90);
                    break;
                case Direction.right:
                    position += Vector3.right * distanceFromOrigin;
                    rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case Direction.left:
                    position += Vector3.left * distanceFromOrigin;
                    rotation = Quaternion.Euler(90, 0, 180);
                    break;
                case Direction.back:
                    position += Vector3.back * distanceFromOrigin;
                    rotation = Quaternion.Euler(90, 90, 0);
                    break;
                case Direction.front:
                    position += Vector3.forward * distanceFromOrigin;
                    rotation = Quaternion.Euler(90, -90, 0);
                    break;
            }

            this.gameObject.transform.position = position;
            this.gameObject.transform.rotation = rotation;
        }
    }
}
