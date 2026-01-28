using UnityEngine;
using UnityEngine.Pool;
// I made this script from YouTube tutorials and AI solutions (for problems)
// I forgot all, so it wont be in GameReady build
// I'll recreate this shiii

// P.s. I hate this script

public class MapCreatorPool : MonoBehaviour
{
    [Space(10)]
    [Header("= Pool cfg =")]
    [SerializeField] private GameObject prefab;
    private IObjectPool<GameObject> _pool;
    [Space(10)]
    [Header("= Editor cfg =")]
    [SerializeField] private Camera cam;
    [SerializeField] private float rayDistance = 100f;

    void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            // Set parent to created obj (this.transform) for clean hierarchy
            createFunc: () => {
                GameObject obj = Instantiate(prefab);
                obj.transform.SetParent(this.transform);
                return obj;
            },
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 200
        );
    }

    void Update()
    {
        // PC
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }

        // MOBILE
        /*
        if 
        */
    }

    private void HandleInput(Vector2 screenPosition)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("MapObject"))
            {
                ReleaseObject(hit.collider.gameObject);
            }
            else
            {
                Vector3 spawnPos = hit.point + new Vector3(0, 0.5f, 0); // Lift it up
                GetObject(spawnPos, Quaternion.identity);
            }
        }
    }

    public GameObject GetObject(Vector3 pos, Quaternion rot)
    {
        var obj = _pool.Get();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.SetParent(this.transform);
        return obj;
    }

    public void ReleaseObject(GameObject obj)
    {
        _pool.Release(obj);
    }
}
