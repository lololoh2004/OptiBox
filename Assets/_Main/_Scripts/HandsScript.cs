using UnityEngine;

public class HandsScript : MonoBehaviour
{
    [Header("= Obj cfg =")]
    [SerializeField] private Camera cam;             // I can use Camera.main but no

    [Header("= Phys cfg =")]
    [SerializeField] private float Speed = 12f;      // Speed of following
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] LayerMask ignoreLayer;

    private Rigidbody grabbedObj;
    private float holdDistance;
    private Collision lastCollision;

    

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Object capture
        if (Input.GetMouseButtonDown(0))
        {
            // This will find main parent with his RigidBody
            if (DynamicCursor.hit.collider != null && DynamicCursor.hit.collider.attachedRigidbody != null)
            {
                grabbedObj = DynamicCursor.hit.collider.attachedRigidbody;  // Get RigidBody in parent
                grabbedObj.isKinematic = false;
                grabbedObj.useGravity = false;
                holdDistance = DynamicCursor.hit.distance;                  // Save original position

                // Ñolliders from child objects will become part of the parent
                foreach (Rigidbody rb in grabbedObj.GetComponentsInChildren<Rigidbody>())
                {
                    if (rb != grabbedObj)
                    {
                        // DELETE child Rigidbody to weld with parent FOREVEEEER
                        Destroy(rb);
                    }
                }
            }
        }

        // Control the distance
        if (grabbedObj != null)
        {
            holdDistance += Input.GetAxis("Mouse ScrollWheel") * sensitivity; // Increases or decreases distance depending on sensitivity
            holdDistance = Mathf.Clamp(holdDistance, 1f, 20f);                // Create a distance limit

            // Letting go(0) or fixing in air(1)
            if (Input.GetMouseButtonDown(1))
            {
                grabbedObj.isKinematic = true;
                ReleaseObject();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ReleaseObject();
            }
            if (Input.GetKeyDown(KeyCode.C) && grabbedObj != null)
            {
                RaycastHit hit;
                // Ray from grabbedObj
                if (Physics.Raycast(grabbedObj.position, cam.transform.forward, out hit, 2f, ~ignoreLayer))
                {
                    if (hit.transform != grabbedObj.transform)
                    {
                        // Save
                        //Vector3 worldScale = grabbedObj.transform.lossyScale;
                        Quaternion worldRot = grabbedObj.transform.rotation;
                        Vector3 worldPos = grabbedObj.transform.position;

                        // Weld
                        grabbedObj.transform.SetParent(hit.transform);

                        //grabbedObj.transform.position = worldPos;
                        //grabbedObj.transform.rotation = worldRot;
                        //grabbedObj.transform.localScale = new Vector3(1, 1, 1);

                        grabbedObj.isKinematic = true;
                        Destroy(grabbedObj);
                        grabbedObj = null;

                        Debug.Log("Welded!");
                    }
                }
            }
        }
    }

    // Fixed update func to fix the physics
    void FixedUpdate()
    {
        if (grabbedObj != null && !grabbedObj.isKinematic)
        {
            Vector3 targetPos = cam.transform.position + cam.transform.forward * holdDistance;
            // Smooth calculation of the velocity vector without sudden moves (Fixed formula)
            grabbedObj.velocity = (targetPos - grabbedObj.position) * Speed;
            grabbedObj.angularVelocity *= 0.9f; // Limit of the rotation to fix chaotic rotation
        }
    }

    void ReleaseObject()
    {
        if (grabbedObj != null)
        {
            if (!grabbedObj.isKinematic) grabbedObj.useGravity = true; // If u freeze obj this wont work and all will be chikipuki 
            grabbedObj = null;                                         // Clear buffer for stability
        }
    }

    void OnCollisionStay(Collision collision) 
    {
        lastCollision = collision; 
        Debug.Log(collision.ToString());
    }
    void OnCollisionExit(Collision collision) 
    { 
        lastCollision = null; 
    }
}
