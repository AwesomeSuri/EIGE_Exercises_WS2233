using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    private void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * speed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("We hit: " + other.name);
            }
        }
    }
}
