using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    public enum Behaviour
    {
        LineOfSight,
        Intercept,
        PatternMovement,
        ChasePatternMovement,
        Hide
    }
    
    [SerializeField]
    private float chaseSpeed;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private Transform prey;
    [SerializeField]
    private Behaviour behaviour;

    private Rigidbody enemyRigidbody;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (behaviour)
        {
            case Behaviour.LineOfSight:
                ChaseLineOfSight(prey.position, chaseSpeed);
                break;
            case Behaviour.Intercept:
                break;
            case Behaviour.PatternMovement:
                break;
            case Behaviour.ChasePatternMovement:
                break;
            case Behaviour.Hide:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ChaseLineOfSight(Vector3 targetPosition, float speed)
    {
        var direction = (targetPosition - transform.position).normalized * speed;
        enemyRigidbody.velocity = new Vector3(
            direction.x,
            enemyRigidbody.velocity.y,
            direction.z);
    }
}
