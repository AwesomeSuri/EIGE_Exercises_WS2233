using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class InputSettings
    {
        public string FORWARD_AXIS = "Vertical";
        public string SIDEWAYS_AXIS = "Horizontal";
        public string TURN_AXIS = "Mouse X";
        public string JUMP_AXIS = "Jump";
    }
    
    [System.Serializable]
    public class MoveSettings
    {
        public float runVelocity = 12;
        public float rotateVelocity = 100;
        public float jumpVelocity = 8;
        public float distanceToGround = 1.3f;
        public LayerMask ground;
    }

    [SerializeField] private InputSettings inputSettings;
    [SerializeField] private MoveSettings moveSettings;

    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput, sidewaysInput, turnInput, jumpInput;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        targetRotation = transform.rotation;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
    }

    private void Update()
    {
        GetInput();
        Turn();
    }

    private void FixedUpdate()
    {
        Run();
        Jump();
    }

    private void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
        jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
    }

    private void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            var amtToRotate = moveSettings.rotateVelocity * turnInput * Time.deltaTime;
            targetRotation *= Quaternion.AngleAxis(amtToRotate, Vector3.up);
        }

        transform.rotation = targetRotation;
    }

    private void Run()
    {
        velocity.x = sidewaysInput * moveSettings.runVelocity;
        velocity.y = playerRigidbody.velocity.y;
        velocity.z = forwardInput * moveSettings.runVelocity;
        
        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    private void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            velocity.x = playerRigidbody.velocity.x;
            velocity.y = moveSettings.jumpVelocity;
            velocity.z = playerRigidbody.velocity.z;

            playerRigidbody.velocity = velocity;
        }
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 
            moveSettings.distanceToGround, moveSettings.ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Physics.Raycast(transform.position, Vector3.down,
            moveSettings.distanceToGround, moveSettings.ground) ?
            Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * moveSettings.distanceToGround);
    }
}
