using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int score = 0;
    public static int lives = 3;

    private Quaternion initalRotation;
    [SerializeField]
    private float speed = 14.0f;
    [SerializeField]
    private Vector2 tilt;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform weaponLocation;

    // Start is called before the first frame update
    void Start()
    {
        initalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMoveX = Time.deltaTime * speed * Input.GetAxis("Horizontal");
        float amtToMoveY = Time.deltaTime * speed * Input.GetAxis("Vertical");
        transform.position += Vector3.right * amtToMoveX + Vector3.up * amtToMoveY;
        // check if projectile is inside camera view, view space is always 0 -> 1
        Vector3 posInViewSpace = Camera.main.WorldToViewportPoint(transform.position);
        if (posInViewSpace.x < 0.0f)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, posInViewSpace.y, posInViewSpace.z));
        }

        if (posInViewSpace.x > 1.0f)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, posInViewSpace.y, posInViewSpace.z));
        }

        transform.rotation = Quaternion.Slerp(initalRotation,
            Quaternion.Euler(tilt.y * Input.GetAxis("Vertical"), -tilt.x * Input.GetAxis("Horizontal"), 0), 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, weaponLocation.position, transform.rotation);
        }
    }


}