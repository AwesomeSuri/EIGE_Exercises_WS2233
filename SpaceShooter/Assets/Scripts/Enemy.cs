using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1;
    [SerializeField] private float maxSpeed = 5;

    private float _speed;

    private void Start()
    {
        SetSpeedAndPosition();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * _speed));
    }

    private void OnBecameInvisible()
    {
        SetSpeedAndPosition();
    }

    public void SetSpeedAndPosition()
    {
        // speed
        _speed = Random.Range(minSpeed, maxSpeed);

        // position
        Vector3 pos;
        pos.x = Random.Range(0f, 1f);
        pos.y = 1;
        pos.z = -Camera.main.transform.position.z;
        pos = Camera.main.ViewportToWorldPoint(pos);
        transform.position = pos;
    }
}
