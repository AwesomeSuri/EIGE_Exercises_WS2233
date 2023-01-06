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

    private Rigidbody enemyRigidbody;
    private Behaviour behaviour;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (behaviour)
        {
            case Behaviour.LineOfSight:
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
}
