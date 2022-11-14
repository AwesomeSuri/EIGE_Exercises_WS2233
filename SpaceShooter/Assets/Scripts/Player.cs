using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int score = 0;
    public static int lives = 3;
    
    [SerializeField] private float playerSpeed = 0.2f;
    [SerializeField] private GameObject projectile;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        // move
        Vector3 move;
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        move.z = 0;
        move *= playerSpeed * Time.deltaTime;
        _transform.Translate(move);

        // adjust position
        var pos = _transform.position;
        // wrap horizontally
        var x = pos.x;
        if (x > 10) x -= 20;
        if (x < -10) x += 20;
        pos.x = x;
        // clamp vertically
        pos.y = Mathf.Clamp(pos.y, -4, 1);
        // apply changes
        _transform.position = pos;
        
        // shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, _transform.position, projectile.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lives--;
            Debug.Log("Current Life: " + lives);
        }
    }
}
