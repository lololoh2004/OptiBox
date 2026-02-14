using UnityEngine;

public class SimpleFollowManager : MonoBehaviour
{
    [Space(10)]
    [Header("= Link obj cfg =")]
    [SerializeField] private GameObject objToFollow;
    [Space(10)]
    [Header("= Mode cfg =")]
    public FollowMode mode; 
    [SerializeField] bool useX = false;
    [SerializeField] bool useY = false;
    [SerializeField] bool useZ = false;
    public enum FollowMode { DefaultTp, Alternative }

    void Update()
    {
        Vector3 objPos = objToFollow.transform.position;

        transform.position = new Vector3 (
        useX ? objPos.x : transform.position.x,
        useY ? objPos.y : transform.position.y,
        useZ ? objPos.z : transform.position.z
        );
    }
}
