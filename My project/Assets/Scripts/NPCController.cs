using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float speed = 2.0f;
    private Transform playerTransform;
    private Vector3 direction;
    [SerializeField] List<Transform> waypoints;
    private int waypointIndex=0;
    [SerializeField] GameController controller;
    [SerializeField] NavMeshAgent agent;

    private Transform currentTarget;


    public GameObject player;
    public NPCState currentState = NPCState.Patrol;
    private float distanceToPlayer;
    private float distanceToWaypoint;
    public float attackDistance = 10f;
    public float retreatDistance = 2.5f;
    public enum NPCState
    {
        Patrol,
        Attack,
        Retreat
    }
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = NPCState.Patrol;
        agent=this.GetComponent<NavMeshAgent>();
        if(waypoints.Count>0&&waypoints[0]!=null)
        {
            currentTarget=waypoints[waypointIndex];
        }
    }


    // Define AI behavior for the characters
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceToWaypoint = Vector3.Distance(currentTarget.transform.position,transform.position);
        stateMachine();
        if(distanceToWaypoint<=5f)
        {
            waypointIndex=Random.Range(0,waypoints.Count);
            currentTarget=waypoints[waypointIndex];
            agent.SetDestination(currentTarget.position);

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube")
        {
            other.gameObject.SetActive(false);
            controller.updateScore(0);
        }
    }
    void stateMachine()
    {
        switch (currentState)
        {
            case NPCState.Patrol:
                // Code to move the NPC in a patrol pattern here
                agent.isStopped=false;
                agent.SetDestination(currentTarget.position);
                if (distanceToPlayer <= attackDistance)
                {
                    
                    currentState = NPCState.Attack;
                    Debug.Log("Attack State");
                }
                break;

            case NPCState.Attack:
                // Code to make the NPC attack the player here
                agent.isStopped=true;
                direction = (playerTransform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                if (distanceToPlayer > attackDistance)
                {
                    currentState = NPCState.Patrol;
                    Debug.Log("Patrol State");
                }
                else if (distanceToPlayer <= retreatDistance)
                {
                    currentState = NPCState.Retreat;
                    Debug.Log("Retreat State");
                }
                break;

            case NPCState.Retreat:
                // Code to make the NPC move away from the player here
                agent.isStopped=true;
                direction = (playerTransform.position - transform.position).normalized;
                transform.position += -direction * speed * Time.deltaTime;
                if (distanceToPlayer > retreatDistance)
                {
                    currentState = NPCState.Patrol;
                    Debug.Log("Patrol State");
                }
                break;
        }
    }
}
