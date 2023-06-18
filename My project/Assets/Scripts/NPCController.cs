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
    [SerializeField] int AgentID;
    [SerializeField] Transform AgentOneTransform;
    [SerializeField] Transform AgentTwoTransform;

    private Transform currentTarget;


    public GameObject player;
    public NPCState currentState = NPCState.Patrol;
    private float distanceToPlayer;
    private float distanceToWaypoint;
    private float distanceToAgent1;
    private float distanceToAgent2;
    public float attackDistance = 10f;
    public float retreatDistance = 2.5f;
    public enum NPCState
    {
        Patrol,
        Attack,
        Retreat,
        ChasePlayer,
        ChaseTarget,
        Chase
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
        distanceToAgent1 = Vector3.Distance(AgentOneTransform.position, transform.position);
        distanceToAgent2 = Vector3.Distance(AgentTwoTransform.position, transform.position);

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
        //if (other.gameObject.tag == "Cube")
        //{
           // other.gameObject.SetActive(false);
          //  controller.updateScore(0);
       // }
    }
    void stateMachine()
    {
        if(this.AgentID==1)
        {
            switch (currentState)
            {
                case NPCState.Patrol:
                    // Code to move the NPC in a patrol pattern here
                    controller.updateState(0, AgentID);
                    agent.isStopped = false;
                    agent.SetDestination(currentTarget.position);
                    //if (distanceToPlayer <= attackDistance)
                    //{

                       // currentState = NPCState.Attack;
                       // Debug.Log("Attack State");
                    //}
                    if(distanceToWaypoint<30f && distanceToPlayer<20f)
                    {
                        currentState = NPCState.Retreat;
                    }
                    if (distanceToWaypoint >= 30f || distanceToPlayer>=20f)
                    {
                        currentState = NPCState.Chase;
                    }
                    break;

                case NPCState.Attack:
                    // Code to make the NPC attack the player here
                    controller.updateState(1, AgentID);
                    agent.isStopped = true;
                    direction = (playerTransform.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;

                    if (distanceToPlayer > attackDistance && distanceToWaypoint<=10f)
                    {
                        currentState = NPCState.Patrol;
                        Debug.Log("Patrol State");
                    }
                    else if (distanceToPlayer <= retreatDistance)
                    {
                        currentState = NPCState.Retreat;
                        Debug.Log("Retreat State");
                    }
                    else if(distanceToPlayer > attackDistance && distanceToWaypoint > 10f)
                    {
                        currentState = NPCState.Chase;
                        Debug.Log("Chase State");
                    }
                    break;
                case NPCState.Retreat:
                    controller.updateState(2, AgentID);
                    // Code to make the NPC move away from the player here
                    agent.isStopped = false;
                    if(distanceToPlayer<=retreatDistance)
                    {
                        agent.isStopped = true;
                        direction = (playerTransform.position - transform.position).normalized;
                        transform.position += -direction * speed * Time.deltaTime;
                    }
                    if (distanceToWaypoint >= 30f || distanceToPlayer >= 20f)
                    {
                        currentState = NPCState.Chase;
                    }
                    break;
                 case NPCState.Chase:
                    controller.updateState(3, AgentID);
                    agent.isStopped = true;
                    direction = (playerTransform.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                    if (distanceToWaypoint < 30f && distanceToPlayer < 20f)
                    {
                        currentState = NPCState.Patrol;
                    }
                    break;

            }
        }
        else if(this.AgentID==2)
        {
            switch (currentState)
            {
                case NPCState.Patrol:
                    // Code to move the NPC in a patrol pattern here
                    controller.updateState(0, AgentID);
                    agent.isStopped = true;
                    agent.SetDestination(currentTarget.position);
                    if (distanceToPlayer < distanceToWaypoint && distanceToAgent1>=15f)
                    {

                        currentState = NPCState.ChasePlayer;
                        Debug.Log("ChasePlayer State");
                    }
                    if(distanceToPlayer>=distanceToWaypoint && distanceToAgent1>=15f)
                    {
                        currentState = NPCState.ChaseTarget;
                        Debug.Log("ChaseTarget State");
                    }
                    if(distanceToAgent1<15f)
                    {
                        currentState = NPCState.Retreat;
                    }
                    break;
                case NPCState.ChasePlayer:
                    controller.updateState(1, AgentID);
                    agent.isStopped = true;
                    agent.SetDestination(currentTarget.position);
                    direction = (playerTransform.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                    if (distanceToPlayer >= distanceToWaypoint)
                    {

                        currentState = NPCState.Patrol;
                        Debug.Log("Patrol State");
                    }
                    if (distanceToAgent1<15f)
                    {

                        currentState = NPCState.Retreat;
                        Debug.Log("Retreat State");
                    }
                    break;
                case NPCState.ChaseTarget:
                    controller.updateState(3, AgentID);
                    agent.isStopped = false;
                    agent.SetDestination(currentTarget.position);
                    if (distanceToWaypoint<=2f)
                    {
                        currentState = NPCState.Patrol;
                        Debug.Log("Patrol State");
                    }
                    if (distanceToPlayer <= distanceToWaypoint && distanceToAgent1 >= 15f)
                    {

                        currentState = NPCState.ChasePlayer;
                        Debug.Log("ChasePlayer State");
                    }
                    if (distanceToAgent1 < 15f)
                    {

                        currentState = NPCState.Retreat;
                        Debug.Log("Retreat State");
                    }
                    break;
                case NPCState.Retreat:
                    controller.updateState(2, AgentID);
                    // Code to make the NPC move away from the player here
                    agent.isStopped = true;
                    direction = (AgentOneTransform.position - transform.position).normalized;
                    transform.position += -direction * speed * Time.deltaTime;
                    if (distanceToAgent1 >= 15f)
                    {
                        currentState = NPCState.Patrol;
                        Debug.Log("Patrol State");
                    }
                    break;

            }
        }

    }
}
