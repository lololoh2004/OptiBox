using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ToolGunScript : MonoBehaviour
{
    [Space(10)]
    [Header("= Obj cfg =")]
    [SerializeField] private TextMeshProUGUI modeText;
    [SerializeField] private Camera cam;
    [Space(10)]
    [Header("= Non-cfg-rble =")]
    Transform selectedObj;
    int mode = 1;
    private Rigidbody grabbedObj;
    private float holdDistance;

    void Update()
    {
        switch (mode)
        {
            case 0:
                return;
            case 1:
                if (Input.GetMouseButtonDown(0))               //if (Input.GetMouseButtonDown(0) && inventory.Inventory.Contains(gameObject))
                {
                    if (selectedObj == null)
                    {
                        selectedObj = DynamicCursor.hit.transform;
                    }
                    else
                    {
                        selectedObj.transform.SetParent(DynamicCursor.hit.transform, true);
                        selectedObj = null;
                    }
                    Debug.Log(selectedObj);
                }
                break;
            case 2:
                if (Input.GetMouseButtonDown(0))
                {
                    if (DynamicCursor.hit.rigidbody != null)
                    {
                        grabbedObj = DynamicCursor.hit.rigidbody;
                        if (grabbedObj.isKinematic)
                        {
                            grabbedObj.isKinematic = false;
                        }
                        holdDistance = DynamicCursor.hit.distance;
                        grabbedObj.useGravity = false;
                    }
                }
                if (grabbedObj != null)
                {
                    // Control
                    holdDistance += Input.GetAxis("Mouse ScrollWheel") * 5f;
                    Vector3 targetPosition = cam.transform.position + cam.transform.forward* holdDistance;

                    // Change Pos
                    grabbedObj.velocity = (targetPosition - grabbedObj.position) * 10f;

                    if (Input.GetMouseButtonDown(1))
                    {
                        grabbedObj.isKinematic = true;
                        ReleaseObject();
                    }
                }

                if (Input.GetMouseButtonUp(0) && grabbedObj != null) { ReleaseObject(); }
                break;
        }

        if (Input.GetKeyDown(KeyCode.F1)){ if (mode != 1) { mode--; } }
        if (Input.GetKeyDown(KeyCode.F2)){ mode++; }
        modeText.text = mode.ToString();
    }
    void ReleaseObject()
    {
        if (grabbedObj != null)
        {
            if (!grabbedObj.isKinematic) { grabbedObj.useGravity = true; }
            grabbedObj = null;
        }
    }
}
