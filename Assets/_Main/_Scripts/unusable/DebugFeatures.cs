using UnityEngine;
using TMPro;

public class DebugFeatures : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsText;
    [SerializeField] TextMeshProUGUI fpsSubText;
    float fps;

    private void Start()
    {
        InvokeRepeating("FPSUpdate", 0.5f, 0.5f);
    }

    void FPSUpdate()
    {
        fpsText.text = "FPS";
        //fpsSubText.text = (1 / Time.deltaTime).ToString("000");
        fps = 1 / Time.deltaTime;

        if (fps < 60)      {fpsSubText.color = Color.red;}
        else if (fps < 100) {fpsSubText.color = Color.yellow;}
        else                {fpsSubText.color = Color.white;}

        fpsSubText.text = fps.ToString("000");
    }
}