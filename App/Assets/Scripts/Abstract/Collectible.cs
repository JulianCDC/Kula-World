using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    private float yaw;
    private float pitch;
    private float roll;
    
    public enum RotationDirections
    {
        x,
        y,
        z,
        none
    };
    public RotationDirections rotationDirection;
    
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
    
    protected virtual void Start()
    {
        Quaternion angle = this.transform.rotation;
        this.yaw = angle.eulerAngles.x;
        this.roll = angle.eulerAngles.z;
        this.pitch = angle.eulerAngles.y;
    }
    
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
