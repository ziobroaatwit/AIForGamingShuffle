using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private GameController scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("GameController").GetComponent<GameController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Add to score when the player hits a cube
            scoreManager.updateScore(1);
            // Destroy the cube
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
