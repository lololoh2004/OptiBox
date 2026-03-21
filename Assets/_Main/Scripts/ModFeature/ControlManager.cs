using UnityEngine;
using UnityEngine.EventSystems;

public class ControlManager : MonoBehaviour, IDragHandler
{
    public float deltaX;
    public float deltaY;

    public void OnDrag(PointerEventData eventData)
    { deltaX = eventData.delta.x; deltaY = eventData.delta.y; }
    
    //public void OnPointerUp(PointerEventData eventData)
    //{ deltaX = 0; deltaY = 0; }
}
