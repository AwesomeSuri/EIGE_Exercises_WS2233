using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(gameObject.transform.position, "deathzone");
    }
}
