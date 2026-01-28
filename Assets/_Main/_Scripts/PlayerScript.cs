using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerScript : MonoBehaviour
{

    [Space(10)]
    [Header("= Character obj cfg =")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float speed = 5f;
    [Space(5)]
    [SerializeField] Transform camTransform;
    [SerializeField] GameObject skin;
    [Space(10)]
    [Header("= Player cfg =")]
    [SerializeField][Range(100, 500)] float sensitivity = 100f;
    [SerializeField] int camType = 1;

    Rigidbody rb;
    MeshRenderer skinMR;
    Vector3 direction;
    float camCurrentHeight;
    float camStartHeight;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        // components
        rb = GetComponent<Rigidbody>();
        skinMR = skin.GetComponent<MeshRenderer>();

        // other shi
        camStartHeight = camTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float keyX = Input.GetAxis("Horizontal");
        float keyY = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        direction = transform.TransformDirection(new Vector3(keyX, 0, keyY));

        switch (camType)
        {
            case 0:
                camTransform.localPosition = new Vector3(0, 1.5f, -5);
                transform.Rotate(Vector3.up, mouseX * sensitivity * Time.deltaTime);

                skin.GetComponent<MeshRenderer>().enabled = true;
                break;
            case 1:
                camTransform.localPosition = new Vector3(0, camCurrentHeight, 0.2f);
                camTransform.transform.Rotate(-mouseY, 0, 0);
                transform.Rotate(0, mouseX, 0);

                skinMR.enabled = false;
                break;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            camType = (camType + 1) % 2; // 2 is an amount of types
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            camCurrentHeight = camStartHeight / 2;
        }
        else
        {
            camCurrentHeight = camStartHeight;
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

    }
    private void OnCollisionStay(Collision other)
    {
        if (other != null)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}
