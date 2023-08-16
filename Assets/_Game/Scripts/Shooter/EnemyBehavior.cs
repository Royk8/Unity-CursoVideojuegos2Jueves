using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Patrolling;
    public List<Transform> PatrolPlaces = new List<Transform>();
    public Transform eyes;
    public float visionDistance = 10f;
    public Transform lastSeenGhost;


    private NavMeshAgent agent;
    private Transform targetPlayer;
    private Vector3 lastSeen;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        StateMachine();
    }

    public void StateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.CheckingPlace:
                CheckPlace();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    public void Patrol()
    {
        //Patrol
    }

    public void Chase()
    {
        //Comportamiento
        agent.SetDestination(targetPlayer.position);
    }

    public void CheckPlace()
    {
        agent.SetDestination(lastSeen);
        lastSeenGhost.gameObject.SetActive(true);
        lastSeenGhost.position = lastSeen;
    }
    public void Attack()
    {
        //Attack
    }

    public void ChangeToChase(Transform player)
    {
        currentState = EnemyState.Chasing;
        targetPlayer = player;
        lastSeen = player.position;
    }

    public void LeaveCheckPlace()
    {
        lastSeenGhost.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Physics.Raycast(eyes.position, other.transform.position - eyes.position + Vector3.up, out RaycastHit hit, visionDistance))
            {
                //Si ve algo
                if (hit.collider.CompareTag("Player"))
                {
                    //Si vio al jugador
                    if(currentState == EnemyState.CheckingPlace)
                    {
                        LeaveCheckPlace();
                    }
                    ChangeToChase(other.transform);
                }
                else
                {
                    //Vio algo que no era el jugador
                    if(currentState == EnemyState.Chasing)
                    {
                        currentState = EnemyState.CheckingPlace;
                    }
                }
            }
            else
            {
                //No ve nada
                if (currentState == EnemyState.Chasing)
                {
                    currentState = EnemyState.CheckingPlace;
                }
            }

        }
    }
}

