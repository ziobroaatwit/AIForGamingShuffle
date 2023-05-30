using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
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
        playerTransform = playerLoc.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        delta = this.transform.position-playerTransform.position;
        dist = delta.magnitude;
        Debug.Log(dist);
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
        else if(enemy_distance=="Close" && health=="High")
        {
            Debug.Log("Retreat");
        }
    }
}
