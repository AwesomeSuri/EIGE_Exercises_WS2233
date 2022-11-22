using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Vector2 minMaxSpeed;
    [SerializeField]
    Vector3 rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeedAndPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float amtToMove = Time.fixedDeltaTime * speed;
        transform.Translate(Vector3.down * amtToMove, Space.World);
        transform.Rotate(Time.deltaTime * rotationSpeed, Space.Self);

        if (Camera.main.WorldToViewportPoint(transform.position).y < -0.1f)
        {
            SetSpeedAndPosition();
        }
    }

    private float speed;

    public void SetSpeedAndPosition()
    {
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);

        Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.05f, 0.95f), 1, 0));
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}