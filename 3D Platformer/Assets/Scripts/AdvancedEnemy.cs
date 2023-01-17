using System;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedEnemy : MonoBehaviour
{
    public enum Behaviour
    {
        Chase,
        Intercept,
        Patrol,
        ChasePatrol,
        Hide,
        PatrolNavMesh
    }

    [SerializeField] private float chaseSpeed = 6;
    [SerializeField] private float normalSpeed = 3;
    [SerializeField] private Rigidbody prey;
    [SerializeField] private Behaviour behaviour;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float distanceThreshold = 2;
    [SerializeField] private float chaseEvadeDistance = 10;

    private Rigidbody enemyRigidbody;
    private NavMeshAgent agent;
    private int currentWayPoint;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.destination = wayPoints[currentWayPoint].position;
    }

    private void FixedUpdate()
    {
        switch (behaviour)
        {
            case Behaviour.Chase:
                Chase(prey.position, chaseSpeed);
                break;
            case Behaviour.Intercept:
                Intercept(prey.position, prey.velocity);
                break;
            case Behaviour.Patrol:
                Patrol();
                break;
            case Behaviour.ChasePatrol:
                ChasePatrol(prey.position);
                break;
            case Behaviour.Hide:
                Hide(prey.position);
                break;
            case Behaviour.PatrolNavMesh:
                if (!agent.pathPending && agent.remainingDistance < .5f)
                    NavigateToNextPoint();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Chase(Vector3 targetPosition, float speed)
    {
        var direction = (targetPosition - transform.position).normalized * speed;
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

        Chase(predictedInterceptionPoint, chaseSpeed);
    }

    private void Patrol()
    {
        var targetPos = wayPoints[currentWayPoint].position;
        Chase(targetPos, normalSpeed);
        if (Vector3.Distance(transform.position, targetPos) < distanceThreshold)
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }
    }

    private void ChasePatrol(Vector3 targetPosition)
    {
        if (Vector3.Distance(targetPosition, transform.position) < chaseEvadeDistance)
        {
            Chase(targetPosition, chaseSpeed);
        }
        else
        {
            Patrol();
        }
    }

    private void Hide(Vector3 targetPosition)
    {
        if (PlayerVisible(targetPosition))
        {
            Chase(targetPosition, chaseSpeed);
        }
        else
        {
            Patrol();
        }
    }

    private bool PlayerVisible(Vector3 targetPosition)
    {
        var directionToTarget = (targetPosition - transform.position).normalized;
        Physics.Raycast(transform.position, directionToTarget, out var hit);
        return hit.collider.CompareTag("Player");
    }

    private void NavigateToNextPoint()
    {
        currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        agent.destination = wayPoints[currentWayPoint].position;
    }

    private void OnDrawGizmos()
    {
        switch (behaviour)
        {
            case Behaviour.Patrol or Behaviour.ChasePatrol:
            {
                for (int i = 0; i < wayPoints.Length; i++)
                {
                    var to = (i + 1) % wayPoints.Length;
                    Gizmos.DrawLine(wayPoints[i].position, wayPoints[to].position);
                }

                break;
            }
            case Behaviour.PatrolNavMesh:
            {
                if(agent == null) return;
                
                for (int i = 0; i < agent.path.corners.Length - 1; i++)
                {
                    Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
                }

                break;
            }
        }
    }
}