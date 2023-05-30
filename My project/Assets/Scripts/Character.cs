using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject playerLoc;
    private Transform playerTransform;
    public string health;  // Set this value to "High" or "Low"
    public string enemy_distance; // This can be "Close", or "Far"
    // Start is called before the first frame update
    private Vector3 delta;
    private float dist;
    void Start()
    {
        health="High";
        playerTransform = playerLoc.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(health=="High" && Input.GetKey(KeyCode.H))
        {
            health="Low";
        }
        else if(health=="Low" && Input.GetKey(KeyCode.H))
        {
            health="High";
        }

        delta = this.transform.position-playerTransform.position;
        dist = delta.magnitude;
        if(dist>5.0f)
        {
            enemy_distance="Far";
        }
        else if(dist<=5.0f)
        {
            enemy_distance="Close";
        }

    }
    void FixedUpdate()
    {
        makeDecision();
        Debug.Log(enemy_distance);
        Debug.Log(health);
    }
    void makeDecision()
    {
        if(enemy_distance=="Far")
        {
            Debug.Log("Attack");
        }
        else if(enemy_distance=="Close" && health=="High")
        {
            Debug.Log("Attack");
        }
        else if(enemy_distance=="Close" && health=="Low")
        {
            Debug.Log("Retreat");
        }
    }
}
