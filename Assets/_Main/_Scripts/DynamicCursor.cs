using UnityEngine;
using UnityEngine.UI;

public class DynamicCursor : MonoBehaviour
{
    [Space(10)]
    [Header("= Obj cfg =")]
    [SerializeField] private Camera cam;
    [SerializeField] Sprite[] cursorTextures; // 0 - defauult (0), 1 - default (1) // 0 - off or non-selected, 1 - on or seleced version
    [SerializeField] Image centerCursor;
    [Space(10)]
    [Header("= Other shi cfg =")]
    [SerializeField] LayerMask nonInteractLayer;
    [SerializeField] float rayDistance;
    public static RaycastHit hit;
    private void Start() { LevelLoading(); }
    public void LevelLoading()
    {
        Cursor.lockState = CursorLockMode.Locked;
        centerCursor.sprite = cursorTextures[0];
    }
    private void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, rayDistance, ~nonInteractLayer))
        {
            //Debug.Log("Попал в: " + hit.collider.name);
            // hit.collider.
            centerCursor.sprite = cursorTextures[2];
        }
        else { centerCursor.sprite = cursorTextures[0]; hit = new RaycastHit(); }
    }
}
