using UnityEngine;

public class WaterRingFade : MonoBehaviour
{
    [SerializeField] int Speed = 1;
    private Material mat;
    private Color objColor;
    void Start() 
    { 
        mat = GetComponent<Renderer>().material; 
    }

    void Update()
    {
        objColor = mat.color;
        objColor.a -= Time.deltaTime * 0.5f;
        mat.color = objColor;

        transform.localScale += transform.localScale * Speed * Time.deltaTime;
        if (objColor.a <= 0) Destroy(gameObject); // Kill self
        
    }
}