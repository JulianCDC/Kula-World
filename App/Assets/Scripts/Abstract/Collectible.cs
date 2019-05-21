using UnityEngine;

/// <summary>
/// An abstract class for collectibles items
/// </summary>
public abstract class Collectible : MonoBehaviour
{
    /// <summary>
    /// The yaw of the GameObject
    /// </summary>
    private float yaw;
    /// <summary>
    /// The pitch of the GameObject
    /// </summary>
    private float pitch;
    /// <summary>
    /// The roll of the GameObject
    /// </summary>
    private float roll;

    /// <summary>
    /// The possible rotation direction of the GameObject
    /// </summary>
    public enum RotationDirections
    {
        /// <summary>
        /// GameObject will rotate on the x axis
        /// </summary>
        x,
        /// <summary>
        /// GameObject will rotate on the y axis
        /// </summary>
        y,
        /// <summary>
        /// GameObject will rotate on the z axis
        /// </summary>
        z,
        none
    };
    /// <summary>
    /// The direction of the rotation of the GameObject
    /// </summary>
    public RotationDirections rotationDirection;

    /// <summary>
    /// Called when the GameObject is Collected by the player
    /// </summary>
    public virtual void Collected()
    {
        ParticleSystem collectedParticle = Resources.Load<ParticleSystem>("Particles/Collected");
        collectedParticle = Instantiate(collectedParticle, gameObject.transform.position, gameObject.transform.rotation);
        var shape = collectedParticle.shape;

        shape.mesh = GetMesh();
        shape.scale = transform.localScale;
        
        Destroy(this.gameObject);
    }

    private Mesh GetMesh()
    {
        var meshCollider = GetComponent<MeshCollider>();

        if (meshCollider == null)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var mesh = cube.GetComponent<MeshFilter>().mesh;
            Destroy(cube);

            return mesh;
        }

        return meshCollider.sharedMesh;
    }

    /// <summary>
    /// Put the base rotation of the GameObject into <see cref="yaw"/>, <see cref="roll"/>, <see cref="pitch"/>
    /// </summary>
    protected virtual void Start()
    {
        Quaternion angle = this.transform.rotation;
        this.yaw = angle.eulerAngles.x;
        this.roll = angle.eulerAngles.z;
        this.pitch = angle.eulerAngles.y;
    }

    /// <summary>
    /// Rotate the GameObject
    /// </summary>
    protected virtual void Update()
    {
        switch(this.rotationDirection)
        {
            case RotationDirections.x:
                this.yaw += 50 * Time.deltaTime;
                break;
            case RotationDirections.y:
                this.pitch += 50 * Time.deltaTime;
                break;
            case RotationDirections.z:
                this.roll += 50 * Time.deltaTime;
                break;
        }

        transform.localRotation = Quaternion.Euler(yaw, pitch, roll);
    }
}
