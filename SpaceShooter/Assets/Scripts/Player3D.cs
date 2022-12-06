using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player3D : MonoBehaviour
{
    public enum State
    {
        Playing,
        Explosion,
        Invincible
    }

    private State playerState;


    public static int score = 0;
    public static int lives = 3;

    private Quaternion initalRotation;
    [SerializeField] private float speed = 14.0f;
    [SerializeField] private Vector2 tilt;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform weaponLocation;
    [SerializeField] private TMP_Text scoreUI;
    [SerializeField] private float respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        initalRotation = transform.rotation;

        lives = 3;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != State.Explosion)
        {
            float amtToMoveX = Time.deltaTime * speed * Input.GetAxis("Horizontal");
            float amtToMoveY = Time.deltaTime * speed * Input.GetAxis("Vertical");
            transform.position += Vector3.right * amtToMoveX + Vector3.back * amtToMoveY;
            // check if projectile is inside camera view, view space is always 0 -> 1
            Vector3 posInViewSpace = Camera.main.WorldToViewportPoint(transform.position);
            if (posInViewSpace.x < 0.0f)
            {
                transform.position =
                    Camera.main.ViewportToWorldPoint(new Vector3(1, posInViewSpace.y, posInViewSpace.z));
            }

            if (posInViewSpace.x > 1.0f)
            {
                transform.position =
                    Camera.main.ViewportToWorldPoint(new Vector3(0, posInViewSpace.y, posInViewSpace.z));
            }

            transform.rotation = Quaternion.Slerp(initalRotation,
                Quaternion.Euler(tilt.y * Input.GetAxis("Vertical"), -tilt.x * Input.GetAxis("Horizontal"), 0), 1);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var mousePos = Input.mousePosition;
                mousePos.z = 10;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                var direction = (mousePos - weaponLocation.position).normalized;
                var rotation = Quaternion.LookRotation(direction, Vector3.back);
                rotation *= Quaternion.Euler(Vector3.right * 90);
                Instantiate(projectile, weaponLocation.position, rotation);
            }

            scoreUI.text = "Score: " + score + "<br>Lives: " + lives;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy collideWith = other.GetComponent<Enemy>();
        if (collideWith != null && playerState == State.Playing)
        {
            // asteroid explosion
            Instantiate(explosionPrefab, transform.position, other.transform.rotation);
            // reset asteroid
            collideWith.SetSpeedAndPosition();

            // destroy ship
            StartCoroutine(DestroyShip());
        }
    }

    private IEnumerator DestroyShip()
    {
        // ship explosion
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        GetComponent<Renderer>().enabled = false;
        playerState = State.Explosion;

        // lose life and wait for respawn
        lives--;
        yield return new WaitForSeconds(respawnTime);

        // check lives
        if (lives <= 0)
        {
            // game over
            SceneManager.LoadScene("Lose");
        }
        else
        {
            // respawn
            // reset position
            transform.position = Vector3.down * 10;
            StartCoroutine(Blinking());

            // move up
            while (transform.position.y < 0)
            {
                float amtToMoveY = Time.deltaTime * speed;
                transform.position += Vector3.up * amtToMoveY;
                yield return null;
            }

            // invisibility
            playerState = State.Invincible;
            yield return new WaitForSeconds(respawnTime);
            playerState = State.Playing;
        }
    }

    private IEnumerator Blinking()
    {
        while (playerState != State.Playing)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(.2f);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(.2f);
        }
    }
}