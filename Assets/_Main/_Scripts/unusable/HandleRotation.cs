using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    //bool isHold;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        
        
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(speed * -Time.deltaTime * 1000, 0, 0);
        }
        else
        {
            if (mouseX != 0)
            {
                transform.Rotate(speed * -Time.deltaTime * mouseY * 200, 0, 0);
            }
            else
            {
                transform.Rotate(speed * -Time.deltaTime * 5, 0, 0);
            }
        }
    }

    public void StartHold()
    {
        //isHold = true;
    }
    public void EndHold()
    {
        //isHold = false;
    }
}