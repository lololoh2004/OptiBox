using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetFloat("PosX", transform.position.x);
        PlayerPrefs.SetFloat("PosY", transform.position.y);
        PlayerPrefs.SetFloat("PosZ", transform.position.z);

        transform.position = new Vector3(
            PlayerPrefs.GetFloat("PosX"),
            PlayerPrefs.GetFloat("PosY"),
            PlayerPrefs.GetFloat("PosZ"));
    }
    void Update()
    {
        
    }
}
