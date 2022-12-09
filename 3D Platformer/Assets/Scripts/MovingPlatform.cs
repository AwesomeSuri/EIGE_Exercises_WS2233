using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float speed = 1;

    private Vector3 startPosition;
    private bool toEnd;

    private void Start()
    {
        startPosition = transform.position;
        toEnd = true;
    }

    private void Update()
    {
        var nextPosition = toEnd ? startPosition + endPosition : startPosition;
        var amtToMove = (nextPosition - transform.position).normalized;
        amtToMove *= Time.deltaTime * speed;
        transform.Translate(amtToMove, Space.World);
        
        if (Vector3.Distance(transform.position, nextPosition) < amtToMove.magnitude)
            toEnd = !toEnd;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(startPosition, startPosition + endPosition);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + endPosition);
        }
    }
}
