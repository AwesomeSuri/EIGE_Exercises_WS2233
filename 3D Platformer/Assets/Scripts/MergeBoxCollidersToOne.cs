using UnityEngine;

public class MergeBoxCollidersToOne : MonoBehaviour
{
    public PhysicMaterial physicMaterial;
    public bool isTrigger;
    
    private void Start()
    {
        var colliders = GetComponentsInChildren<BoxCollider>();
        var bounds = new Bounds();
        for (var i = 0; i < colliders.Length; i++)
        {
            var boxCollider = colliders[i];
            if (bounds.size == Vector3.zero)
            {
                bounds = boxCollider.bounds;
            }
            else
            {
                bounds.Encapsulate(boxCollider.bounds);
            }

            Destroy(boxCollider);
        }

        var col = gameObject.AddComponent<BoxCollider>();
        col.center = col.size = Vector3.zero;
        col.sharedMaterial = physicMaterial;
        col.isTrigger = isTrigger;
        col.center = bounds.center - transform.position;
        col.size = bounds.size;
    }
}
