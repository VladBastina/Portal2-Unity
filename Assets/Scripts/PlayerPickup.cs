using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Camera m_Camera; // Reference to the player's camera
    [SerializeField] private float pickupDistance = 2.5f; // Max distance to pick up the cube
    [SerializeField] private Transform holdPoint; // Point where the object will be held
    private Rigidbody heldObject; // Reference to the picked-up object's Rigidbody

    void Update()
    {
        // Check if Fire3 (default: Shift) is pressed.
        if (Input.GetButton("Fire3"))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                HoldObject();
            }
        }
        else if (heldObject != null)
        {
            ReleaseObject();
        }
    }

    private void TryPickupObject()
    {
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        RaycastHit hit;

        // Check for a cube within the pickup distance.
        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.collider.CompareTag("Cube")) // Ensure the object has the tag "Pickup".
            {
                // Get the Rigidbody of the object.
                heldObject = hit.collider.GetComponent<Rigidbody>();

                if (heldObject != null)
                {
                    // Disable gravity and collisions for the picked-up object.
                    heldObject.useGravity = false;
                    heldObject.isKinematic = true;

                    // Enable collision detection callback.
                    var pickupCollision = heldObject.gameObject.GetComponent<PickupCollision>();
                    if (pickupCollision == null)
                    {
                        pickupCollision = heldObject.gameObject.AddComponent<PickupCollision>();
                    }

                    pickupCollision.OnCollisionEvent += HandlePickupCollision;

                    // Optionally, disable its collider to prevent issues.
                    Collider objectCollider = heldObject.GetComponent<Collider>();
                    if (objectCollider != null)
                    {
                        objectCollider.enabled = true;
                        objectCollider.isTrigger = true;
                    }
                }
            }
        }
    }

    private void HoldObject()
    {
        if (heldObject != null)
        {
            // Keep the object at the hold point.
            heldObject.transform.position = holdPoint.position;
            heldObject.transform.rotation = holdPoint.rotation;
        }
    }

    private void ReleaseObject()
    {
        if (heldObject != null)
        {
            // Re-enable gravity and collisions for the object.
            heldObject.useGravity = true;
            heldObject.isKinematic = false;

            var pickupCollision = heldObject.gameObject.GetComponent<PickupCollision>();
            if (pickupCollision != null)
            {
                pickupCollision.OnCollisionEvent -= HandlePickupCollision;
                Destroy(pickupCollision); // Remove the component if not needed anymore.
            }

            Collider objectCollider = heldObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.enabled = true;
                objectCollider.isTrigger = false;
            }

            // Clear the reference to the held object.
            heldObject = null;
        }
    }

    private void HandlePickupCollision(Collider other)
    {
        if (other.CompareTag("Wall")) // Replace "Wall" with any relevant tag.
        {
            Debug.Log("Held object hit a wall. Dropping...");
            ReleaseObject();
        }
    }
}
