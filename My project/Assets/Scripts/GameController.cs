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
    [SerializeField] TMP_Text AgentOneState;
    [SerializeField] TMP_Text AgentTwoState;
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
    public void updateState(int type,int Agent)
    {
        //NPC Score
        if (type == 0)
        {
            if(Agent==1)
            {
                AgentOneState.text = "AI 1 State: Patrol";
            }
            else if(Agent==2)
            {
                AgentTwoState.text = "AI 2 State: Patrol";
            }
        }
        else if (type == 1)
        {
            if (Agent == 1)
            {
                AgentOneState.text = "AI 1 State: Attack";
            }
            else if (Agent == 2)
            {
                AgentTwoState.text = "AI 2 State: Chase Player";
            }
        }
        else if (type == 2)
        {
            if (Agent == 1)
            {
                AgentOneState.text = "AI 1 State: Activated Flee";
            }
            else if (Agent == 2)
            {
                AgentTwoState.text = "AI 2 State: Flee";
            }
        }
        else if (type == 3)
        {
            if (Agent == 1)
            {
                AgentOneState.text = "AI 1 State: Chase";
            }
            else if (Agent == 2)
            {
                AgentTwoState.text = "AI 2 State: Chase Target";
            }
        }
    }


}
