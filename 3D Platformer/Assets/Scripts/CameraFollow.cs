using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followedObject;
    [SerializeField] private float distanceAway;
    [SerializeField] private float distanceUp;
    [SerializeField] private float smooth;

    private void LateUpdate()
    {
        var toPosition = followedObject.position
            - followedObject.forward * distanceAway
            + followedObject.up * distanceUp;
        
        transform.position = Vector3.Lerp(transform.position, toPosition, smooth * Time.deltaTime);

        var direction = transform.position - followedObject.position;
        if (Physics.Raycast(followedObject.position, direction, out var hit, direction.magnitude))
        {
            transform.position = hit.point;
        }
        
        transform.LookAt(followedObject);
    }
}
