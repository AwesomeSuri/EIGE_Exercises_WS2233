using UnityEngine;

public class CreateAndDestroyPlatform : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.E;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Vector3 offset = Vector3.forward * 3f;
    [SerializeField] private LayerMask destroyable;
    [SerializeField] private Material toDestroyHighlight;

    private Renderer toDestroy;
    private Material defaultMaterial;

    private void Start()
    {
        defaultMaterial = platformPrefab.GetComponent<Renderer>().sharedMaterial;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(offset), out var hit, destroyable))
        {
            if (toDestroy != null)
            {
                toDestroy.sharedMaterial = defaultMaterial;
            }

            toDestroy = hit.collider.GetComponent<Renderer>();
            toDestroy.sharedMaterial = toDestroyHighlight;
        }
        else
        {
            if (toDestroy != null)
            {
                toDestroy.sharedMaterial = defaultMaterial;
            }

            toDestroy = null;
        }

        if(Input.GetKeyDown(key))
        {
            if (toDestroy == null)
            {
                Instantiate(platformPrefab,
                    transform.position + transform.TransformDirection(offset),
                    transform.rotation);
            }
            else
            {
                Destroy(toDestroy.gameObject);
                toDestroy = null;
            }
        }
    }
}
