﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public Transform cube;
    public NavMeshAgent agent;
    Vector3 initialPosition, vectorDirectionToWalk, vectorUnitary;
    int tryAnswer = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = GetComponent<Transform>().position;

        agent.speed = 20;
        agent.acceleration = 40;
    }

    public void activeEnemy()
    {
        vectorDirectionToWalk = cube.position - transform.position;
        vectorUnitary = vectorDirectionToWalk / vectorDirectionToWalk.magnitude;

        switch (tryAnswer)
        {
            case 3:
                agent.SetDestination(transform.position + 20 * vectorUnitary);
                break;

            case 2:
                agent.SetDestination(
                  transform.position + vectorDirectionToWalk.magnitude * vectorUnitary / 2
                );
                break;

            case 1:
                agent.SetDestination(cube.position);
                break;

            default:
                HideEnemy();
                break;
        }

        tryAnswer--;

    }

    public void HideEnemy()
    {
        tryAnswer = 3;
        agent.SetDestination(initialPosition);
    }

    public void SetVelocity(float time, float z)
    {
        float velocity;

        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        if (time != 0)
        {
            velocity = (z - transform.position.z) / time;
        }
        else
        {
            velocity = 0;
        }

        body.velocity = Vector3.forward * velocity;
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    public void RestorePosition()
    {
        gameObject.transform.position = initialPosition;
    }
}
