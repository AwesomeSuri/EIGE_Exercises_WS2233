using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private string triggerTag = "Player";
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(triggerTag))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnDrawGizmos()
    {
        var bounds = GetComponent<Renderer>().bounds;
        
        Gizmos.color = Color.red;

        var distance = Mathf.Abs(Physics.gravity.y);
        if (Physics.Raycast(transform.position, Vector3.down * distance, out var hit))
        {
            distance = Mathf.Min(distance, hit.distance);
        }

        var center = transform.position + Vector3.down * distance / 2f + Vector3.up * bounds.extents.y / 2f;
        var size = Vector3.one;
        size.y = distance + bounds.extents.y;
        Gizmos.DrawWireCube(center, size);
    }
}
