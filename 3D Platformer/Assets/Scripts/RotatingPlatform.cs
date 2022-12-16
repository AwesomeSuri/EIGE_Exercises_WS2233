using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 speed = Vector3.up * 90;

    private void Update()
    {
        var amtToRotate = speed * Time.deltaTime;
        transform.Rotate(amtToRotate);
    }

    private void OnDrawGizmos()
    {
        if (speed.sqrMagnitude == 0) return;

        var corners = 16;
        var angle = 360f / corners;

        var radius = 2f;
        var bounds = EncapsulateTree(transform, new Bounds());
        radius = Mathf.Max(radius, bounds.extents.x, bounds.extents.y, bounds.extents.z);

        Gizmos.color = Color.red;
        var rot = transform.rotation;
        rot *= Quaternion.FromToRotation(Vector3.forward, speed.normalized);
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, rot, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;

        for (int i = 0; i < corners; i++)
        {
            var fromAngle = i * angle;
            var toAngle = (i + 1) * angle;
            Vector3 from, to;
            from.x = Mathf.Sin(Mathf.Deg2Rad * fromAngle);
            from.y = Mathf.Cos(Mathf.Deg2Rad * fromAngle);
            from.z = 0;
            to.x = Mathf.Sin(Mathf.Deg2Rad * toAngle);
            to.y = Mathf.Cos(Mathf.Deg2Rad * toAngle);
            to.z = 0;

            Gizmos.DrawLine(from * radius, to * radius);
        }
    }

    private Bounds EncapsulateTree(Transform root, Bounds bounds)
    {
        var rend = root.GetComponent<Renderer>();
        if (rend != null)
        {
            if (bounds.size == Vector3.zero)
            {
                bounds = rend.bounds;
            }
            else
            {
                bounds.Encapsulate(rend.bounds);
            }
        }

        foreach (Transform child in root) bounds = EncapsulateTree(child, bounds);

        return bounds;
    }
}