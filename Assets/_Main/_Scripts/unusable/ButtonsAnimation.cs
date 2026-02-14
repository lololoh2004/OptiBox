using UnityEngine;

public class ButtonsAnimation : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float range = 1f;
    private RectTransform rt;
    private Vector2 startPos;
    bool isHovered;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        startPos = rt.anchoredPosition;
    }

    void Update()
    {
        if (isHovered)
        {
            Vector2 targetPos = new Vector2(
                        startPos.x + Random.Range(-range, range),
                        startPos.y + Random.Range(-range, range)
                    );
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime * speed);

            rt.localScale = Vector2.Lerp(rt.localScale, Vector2.one * 3, Time.deltaTime * speed * 5);
        }
        else
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, startPos, Time.deltaTime * speed);
            rt.localScale = Vector2.Lerp(rt.localScale, Vector2.one * 2, Time.deltaTime * speed * 5);
        }

    }
    public void OnPointerEnter() { isHovered = true; }
    public void OnPointerExit() { isHovered = false; }
}
