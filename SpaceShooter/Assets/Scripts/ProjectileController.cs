using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10;

    private void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * speed));
    }
}
