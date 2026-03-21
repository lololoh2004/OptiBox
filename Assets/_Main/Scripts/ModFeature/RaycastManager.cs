using UnityEngine;

namespace _Main.Scripts.ModFeature
{
    public class RaycastManager : MonoBehaviour
    {
        [SerializeField] public LayerMask ignoreLayers;
        public RaycastHit hit;
        private Ray _ray;
        public void Update()
        {
            _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(_ray, out hit, 10f, ~ignoreLayers)) 
            { Debug.Log("Detected: " + hit.collider.name); }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_ray);
        }
    }
}
