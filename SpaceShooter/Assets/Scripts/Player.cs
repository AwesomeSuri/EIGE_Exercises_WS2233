using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 0.2f;
    public GameObject projectile;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        var amtMove = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        _transform.Translate(Vector3.right * amtMove);

        var pos = _transform.position;
        var x = pos.x;
        if (x > 10) x -= 20;
        if (x < -10) x += 20;
        pos.x = x;
        _transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, _transform.position, projectile.transform.rotation);
        }
    }
}
