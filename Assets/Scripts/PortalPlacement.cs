using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMove))]
public class PortalPlacement : MonoBehaviour
{
    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Crosshair crosshair;

    [SerializeField]
    private float forwardOffset=0.1f;

    [SerializeField]
    private float upOffset=7.5f;

    private CameraMove cameraMove;

    private void Awake()
    {
        cameraMove = GetComponent<CameraMove>();
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position += cameraMove.GetForward() * forwardOffset;
        position += transform.up * upOffset;

        if(Input.GetButtonDown("Fire1"))
        {
            FirePortal(0, position, cameraMove.GetForward(), 250.0f);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            FirePortal(1, position, cameraMove.GetForward(), 250.0f);
        }
    }

    private void FirePortal(int portalID, Vector3 pos, Vector3 dir, float distance, int recursionDepth = 0)
    {
        const int maxRecursionDepth = 10; // Limit recursion to prevent stack overflow
        if (recursionDepth > maxRecursionDepth)
        {
            Debug.LogWarning("Exceeded maximum portal traversal depth.");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, distance, layerMask))
        {
            // Handle portal-to-portal traversal
            if (hit.collider.CompareTag("Portal"))
            {
                var inPortal = hit.collider.GetComponent<Portal>();
                if (inPortal == null) return;

                var outPortal = inPortal.OtherPortal;
                if (outPortal == null) return;

                // Update position and direction
                Vector3 relativePos = inPortal.transform.InverseTransformPoint(hit.point);
                pos = outPortal.transform.TransformPoint(relativePos);

                Vector3 relativeDir = inPortal.transform.InverseTransformDirection(dir);
                dir = outPortal.transform.TransformDirection(relativeDir);

                distance -= Vector3.Distance(pos, hit.point);

                // Recursive call
                FirePortal(portalID, pos, dir, distance, recursionDepth + 1);
                return;
            }

            // Calculate portal orientation
            var portalRight = Vector3.Cross(Vector3.up, hit.normal).normalized;
            if (Mathf.Abs(portalRight.x) > Mathf.Abs(portalRight.z))
            {
                portalRight = (portalRight.x > 0) ? Vector3.right : -Vector3.right;
            }
            else
            {
                portalRight = (portalRight.z > 0) ? Vector3.forward : -Vector3.forward;
            }

            var portalUp = Vector3.Cross(portalRight, -hit.normal).normalized;
            var portalRotation = Quaternion.LookRotation(-hit.normal, portalUp);

            bool wasPlaced = portals.Portals[portalID].PlacePortal(hit.collider, hit.point, portalRotation);
            if (!wasPlaced)
            {
                Debug.LogError($"Portal {portalID} failed to place. Collider: {hit.collider.name}, Point: {hit.point}, Rotation: {portalRotation}");
                Debug.Log($"Remaining Distance: {distance}, Hit Normal: {hit.normal}");
                crosshair.SetPortalPlaced(portalID, false);
                return;
            }
            else
            {
                crosshair.SetPortalPlaced(portalID, true);
                Debug.Log($"Portal {portalID} successfully placed at {hit.point}.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any surface.");
        }
    }
}
