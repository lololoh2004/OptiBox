using UnityEngine;
using UnityEngine.UI;

public class DynamicCursor : MonoBehaviour
{
    [Space(10)]
    [Header("= Obj cfg =")]
    [SerializeField] private Camera cam;
    [SerializeField] Sprite[] cursorTextures; // 0 - defauult (0), 1 - default (1) // 0 - off or non-selected, 1 - on or seleced version
    [SerializeField] Image centerCursor;
    [SerializeField] GameObject cursorObj;
    [Space(10)]
    [Header("= Other shi cfg =")]
    [SerializeField] LayerMask nonInteractLayer;
    [SerializeField] float rayDistance;
    [SerializeField] float k;
    public static RaycastHit hit;
    RectTransform rt; 
    private void Start() { LevelLoading(); }
    public void LevelLoading()
    {
        Cursor.lockState = CursorLockMode.Locked;
        centerCursor.sprite = cursorTextures[0];
        rt = cursorObj.GetComponent<RectTransform>();
    }
    private void Update()
    {
        //Var.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Lib.
        if (Physics.Raycast(ray, out hit, rayDistance, ~nonInteractLayer))
        {
            centerCursor.sprite = cursorTextures[2];
        }
        else
        {
            centerCursor.sprite = cursorTextures[0];
            hit = new RaycastHit();
        }

        // Immersive Part
        rt.localEulerAngles = new Vector3(0, 0, mouseX * -5f);

        Vector2 targetPos = new Vector2(mouseX, mouseY) * -10f * k;                                        // k is a Serialize value
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime * 10);       // 0.5 is a anchored pos in center of canvas 
    }

}
