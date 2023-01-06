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
    private float chaseSpeed = 6;
    [SerializeField]
    private float normalSpeed = 3;
    [SerializeField]
    private Rigidbody prey;
    [SerializeField]
    private Behaviour behaviour;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private float distanceThreshold = 2;
    [SerializeField]
    private float chaseEvadeDistance = 10;

    private Rigidbody enemyRigidbody;
    private int currentWayPoint;

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
                Intercept(prey.position, prey.velocity);
                break;
            case Behaviour.PatternMovement:
                PatternMovement();
                break;
            case Behaviour.ChasePatternMovement:
                ChasePatternMovement(prey.position);
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
        Debug.DrawLine(transform.position, transform.position + direction, Color.red);
        enemyRigidbody.velocity = new Vector3(
            direction.x,
            enemyRigidbody.velocity.y,
            direction.z);
    }

    private void Intercept(Vector3 targetPosition, Vector3 targetVelocity)
    {
        var velocityRelative = targetVelocity - enemyRigidbody.velocity;
        var distance = Vector3.Distance(targetPosition, transform.position);
        var timeToClose = distance / velocityRelative.magnitude;
        var predictedInterceptionPoint = targetPosition + timeToClose * targetVelocity;
        Debug.DrawLine(targetPosition, predictedInterceptionPoint, Color.blue);
        
        ChaseLineOfSight(predictedInterceptionPoint, chaseSpeed);
    }

    private void PatternMovement()
    {
        var targetPos = wayPoints[currentWayPoint].position;
        ChaseLineOfSight(targetPos, normalSpeed);
        if (Vector3.Distance(transform.position, targetPos) < distanceThreshold)
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }
    }

    private void ChasePatternMovement(Vector3 targetPosition)
    {
        if (Vector3.Distance(targetPosition, transform.position) < chaseEvadeDistance)
        {
            ChaseLineOfSight(targetPosition, chaseSpeed);
        }
        else
        {
            PatternMovement();
        }
    }
}
