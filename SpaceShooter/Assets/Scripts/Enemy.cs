using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public Vector2 minMaxSpeed;
    [SerializeField]
    private float maxRotationSpeed;
    [SerializeField]
    private Vector2 minMaxScale;
    [SerializeField]
    private float speedGrowth;
    
    private float speed;
    private Vector3 rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeedAndPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // move
        float amtToMove = Time.deltaTime * speed;
        transform.Translate(Vector3.down * amtToMove, Space.World);
        
        // rotate
        Vector3 amtToRotate = Time.deltaTime * rotationSpeed;
        transform.Rotate(amtToRotate);

        // respawn when too low
        if (Camera.main.WorldToViewportPoint(transform.position).y < -0.1f)
        {
            SetSpeedAndPosition();
        }
    }


    public void SetSpeedAndPosition()
    {
        // randomize speed
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        
        // randomize rotation
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        
        // randomize scale
        float scale = Random.Range(minMaxScale.x, minMaxScale.y);
        transform.localScale = Vector3.one * scale;
            
        // randomize position
        Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.05f, 0.95f), 1, 0));
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}