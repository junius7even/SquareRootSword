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
        if (Input.GetKeyDown(KeyCode.H) && state == BattleState.LOST)
        {
            Debug.Log("You'ev hit the H button");
            state = BattleState.START;
            
            StartTurn();
        }  
        if (Input.GetKeyDown(KeyCode.A) && state == BattleState.PLAYERTURN)
        {
            enemy.health.turnHealth = enemy.health.turnHealth - 1;
        }

        if (Input.GetKeyDown(KeyCode.L) && state == BattleState.START)
        {
            state = BattleState.PLAYERTURN;
            
        }
        if (Input.GetKeyDown(KeyCode.Return) && state == BattleState.PLAYERTURN)
        {
            state = BattleState.PLAYERATTACK;
            EndTurn();
        }
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
