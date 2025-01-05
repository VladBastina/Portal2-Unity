using UnityEngine;

public class CubeController : PortalableObject
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Awake();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -50.0f)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    public override void Warp()
    {
        base.Warp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision with {collision.collider.name}");
    }

}
