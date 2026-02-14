using UnityEngine;

public class DynamicMenu : MonoBehaviour
{
    //[SerializeField] float k;
    [SerializeField] GameObject menuObj;
    RectTransform rt;
    void Start()
    {
        rt = menuObj.GetComponent<RectTransform>();
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        Vector2 targetPos = new Vector2(mouseX, mouseY) * -10f;                                        
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime * 10);

        
    }
}
