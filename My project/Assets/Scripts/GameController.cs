using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController: MonoBehaviour

{
    private int npcScore = 0;
    private int playerScore = 0;
    [SerializeField] TMP_Text playerScoreText;
    [SerializeField] TMP_Text npcScoreText;
    public void Awake()
    {
        npcScoreText.text = "NPC Score: " + npcScore;
        playerScoreText.text = "Player Score: " + playerScore;
    }
    public void updateScore(int type)
    {
        //NPC Score
        if(type == 0)
        {
            npcScore--;
        }
        else if(type == 1)
        {
            playerScore++;
        }
        //Above is player score
        npcScoreText.text = "NPC Score: " + npcScore;
        playerScoreText.text = "Player Score: " + playerScore;
    }


}
