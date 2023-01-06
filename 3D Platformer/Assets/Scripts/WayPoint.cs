using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1);
    }
}
