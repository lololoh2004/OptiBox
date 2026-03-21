using _Main.Scripts.ModFeature;
using UnityEngine;
using TMPro;

public class PropDebugInfo : MonoBehaviour
{
    [SerializeField] private RaycastManager Rcm;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject menu;
    private Vector3 _targetPos;
    private Vector3 _targetScale;
    void Update()
    {
        if (Rcm.hit.collider != null) {
            _targetPos = Vector3.Lerp(playerTransform.position, Rcm.hit.collider.transform.position, 0.5f);
            
            float distance = Vector3.Distance(playerTransform.position, _targetPos);
            _targetScale = Vector3.one * (distance * 0.1f);

            // Если это UI-текст:
            menu.GetComponentInChildren<TextMeshProUGUI>().text = 
                "<" + Rcm.hit.collider.gameObject.name + ">\n" +
                "Speed : " + "null\n" + 
                "Health: " + "null\n" + 
                "Custom: " + "null";

        } else {
            //_targetPos = new Vector3(0, -100, 0);
            //_targetScale = Vector3.zero;
        }
        menu.transform.position = Vector3.Lerp(menu.transform.position, _targetPos, Time.deltaTime * 5f);
        menu.transform.localScale = Vector3.Lerp(menu.transform.localScale, _targetScale, Time.deltaTime * 5f);
    }
}
