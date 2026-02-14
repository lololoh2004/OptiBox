using UnityEngine;

public class RayManager : MonoBehaviour
{
    [SerializeField] float rayDistance = 1;
    [SerializeField] LayerMask nonInteractLayer;
    Ray downRay;

    public RaycastHit downHit;
    public bool isDownHit;

    private void Update()
    {
        downRay = new (transform.position, Vector3.down);
        isDownHit = Physics.Raycast(downRay, out downHit, rayDistance, ~nonInteractLayer);
        //Debug.Log(downHit);
        Debug.DrawRay(downRay.origin, downRay.direction * rayDistance, Color.green);
    }
    
}
