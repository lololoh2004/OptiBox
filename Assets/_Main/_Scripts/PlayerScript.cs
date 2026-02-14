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
    [SerializeField] RayManager RayManager;
    [SerializeField] GameObject skin;
    [Space(10)]
    [Header("= Player cfg =")]
    [SerializeField][Range(100, 500)] float sensitivity = 100f;
    [SerializeField] int camType = 1;

    Rigidbody rb;
    MeshRenderer skinMR;
    CapsuleCollider coll;
    Vector3 direction;
    float camCurrentHeight;
    float camStartHeight;
    float collStartHeight;

    void Start()
    {
        // components
        rb = GetComponent<Rigidbody>();
        skinMR = skin.GetComponent<MeshRenderer>();
        coll = GetComponent<CapsuleCollider>();

        camStartHeight = camTransform.position.y;
        collStartHeight = coll.height;
    }

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            camCurrentHeight = camStartHeight / 2;
            coll.height = collStartHeight / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y * 2), transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            camCurrentHeight = camStartHeight;
            coll.height = collStartHeight;

            transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y * 2), transform.position.z);
        }
        if (RayManager.isDownHit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
    }

    private void FixedUpdate() { rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime); }
}
