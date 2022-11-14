using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    private void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * speed));
    }
}
