using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 speed = Vector3.up * 90;

    private void Update()
    {
        var amtToRotate = speed * Time.deltaTime;
        transform.Rotate(amtToRotate);
    }
}
