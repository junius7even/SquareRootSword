using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, PLAYERATTACK, ENEMYATTACK, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BaseHero hero;
    public BaseEnemy enemy;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        SetupBattle();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ends the battle and goes to the high score board if the player loses
        if (Input.GetKeyDown(KeyCode.H) && state == BattleState.LOST)
        {
            Debug.Log("You'ev hit the H button");
            state = BattleState.START;
            
            StartTurn();
        }  

        // Player attacks happen here
        if (Input.GetKeyDown(KeyCode.A) && state == BattleState.PLAYERTURN)
        {
            enemy.health.turnHealth = enemy.health.turnHealth - 1;
        }

        // Go to player turn
        if (Input.GetKeyDown(KeyCode.L) && state == BattleState.START)
        {
            state = BattleState.PLAYERTURN;
            
        }
        
        // Player ends turn
        if (Input.GetKeyDown(KeyCode.Return) && state == BattleState.PLAYERTURN)
        {
            state = BattleState.PLAYERATTACK;
            EndTurn();
        }

        // Player attacks with square root sword
        if (state == BattleState.PLAYERATTACK)
        {
            double rootResult = Mathf.Sqrt(enemy.health.currentHealth);
            bool isSquare = (rootResult % 1 == 0);
            if (isSquare)
            {
                enemy.health.currentHealth = 0;
            }
            state = BattleState.ENEMYATTACK;
        }

        // Enemy attacks player if it's not dead from the previous player attack
        if (state == BattleState.ENEMYATTACK)
        {
            if (enemy.health.currentHealth == 0)
            { 
                state = BattleState.WON;
            }
            else 
            {
                // Enemy attacks
            }
            state = BattleState.START;
        }
    }

    private void SetupBattle()
    {
        state = BattleState.LOST;
    }

    private void StartTurn()
    {
        hero.health.turnStart();
        enemy.health.turnStart();
    }

    private void EndTurn()
    {
        hero.health.endTurn();
        enemy.health.endTurn();
    }
}
