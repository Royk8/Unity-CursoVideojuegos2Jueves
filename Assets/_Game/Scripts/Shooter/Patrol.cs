using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent agent;
    public EnemyState currentState;
    public Transform eyes;
    Vector3 lastPosition;
    float persuinTime = 10;
    float lastSeen;
    Transform player;
    public Transform Target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        StateMachine();
    }

    public void StateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrolling();
                break;
            case EnemyState.Following:
                Following();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
            case EnemyState.CheckingPosition:
                CheckPosition();
                break;
            default:
                break;
        }
    }

    public void Patrolling()
    {
        //Points Logic

    }

    public void Attacking()
    {
        //Attack Logic
    }

    public void Following()
    {
        Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position) + Vector3.up, Color.red);
        if(Physics.Raycast(eyes.position, player.transform.position - eyes.position, out RaycastHit hit, 10f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                agent.SetDestination(player.transform.position);
                Target.position = player.transform.position;
            }
            else
            {
                currentState = EnemyState.CheckingPosition;
                lastSeen = Time.time;
                lastPosition = player.transform.position;
            }
        }
    }

    public void CheckPosition()
    {
        agent.SetDestination(lastPosition);
        Target.position = lastPosition;
        if(Time.time > (lastSeen + persuinTime))
        {
            currentState = EnemyState.Patrol;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            if(Physics.Raycast(eyes.position, (other.transform.position - eyes.position) + Vector3.up, out RaycastHit hit, 10f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("PLAYER!");
                    currentState = EnemyState.Following;
                    player = other.transform;
                    return;
                }
                else
                {
                    if(currentState == EnemyState.Following)
                    {
                        player = null;
                        lastSeen = Time.time;
                        currentState = EnemyState.CheckingPosition;
                        lastPosition = player.transform.position;

                        return;
                    }
                }

            }else
            {
                if (currentState == EnemyState.Following)
                {
                    lastSeen = Time.time;
                    lastPosition = player.transform.position;
                    player = null;
                    currentState = EnemyState.CheckingPosition;

                    return;
                }
            }
        }
    }

}

public enum EnemyState
{
    Patrol,
    Following,
    CheckingPosition,
    Attacking
}
