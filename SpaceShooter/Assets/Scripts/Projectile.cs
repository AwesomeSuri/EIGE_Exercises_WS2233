using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionPrefab;
    [SerializeField]
    private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        // ReceiveGI screen = Camera.main.pixelRect;
        // check if projectile is inside camera view, view space is always 0 -> 1
        Vector3 posInViewSpace = Camera.main.WorldToViewportPoint(transform.position);
        if(posInViewSpace.y > 1.2f)
        {
            Destroy(gameObject);
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        Enemy collideWith = other.GetComponent<Enemy>();
        if(collideWith != null)
        {
            // Debug.Log("We hit: " + other.name);
            // const bool initiateInWorldSapce = true;
            Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);

            collideWith.minMaxSpeed.x += .5f;
            collideWith.minMaxSpeed.y += .5f;
            
            collideWith.SetSpeedAndPosition();

            Player.score += 10;

            // Debug.Log("Your Points: " + Player.score);

            Destroy(gameObject);
        }
    }
}
