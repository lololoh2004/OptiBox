using UnityEngine;

public class ShittyWaterScript : MonoBehaviour
{
    [SerializeField] float bounceForce = 30f;
    //[SerializeField] float waterToughness = 5f;
    [SerializeField] float waterLineY = -0.5f;
    [SerializeField] float k = 2f;
    [SerializeField] GameObject splashEffect;
    [SerializeField] GameObject ringEffectPrefab;


    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        float depth = Mathf.Max(0, waterLineY - other.transform.position.y);

        if (depth > k)
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * depth * rb.mass * bounceForce, ForceMode.Force);   // will lift up objs
                //Debug.Log("Water physics detected: " + rb.name);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 objPos = other.bounds.center;

        Instantiate(splashEffect, objPos, Quaternion.Euler(-90, 0, 0));
        Debug.Log("Splash Effect created");

        Instantiate(ringEffectPrefab, objPos, Quaternion.Euler(-90, 0, 0));

    }
}