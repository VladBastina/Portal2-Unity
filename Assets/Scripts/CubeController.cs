using UnityEngine;

public class CubeController : PortalableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -5.0f)
        {
            transform.position =  new Vector3(transform.position.x,3.0f,transform.position.z);
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
