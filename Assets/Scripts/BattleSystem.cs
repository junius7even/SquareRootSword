using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// go to shop scene after transitioning to won/lost

public enum BattleState {START, PLAYERTURN, PLAYERTURNEND, ENEMYTURN, ENEMYTURNEND, WON, LOST, NONE}
public enum BSInputType { PLAYERATTACK, ENDTURN, NONE }

class BSInput
{
    public BSInputType type = BSInputType.NONE;
    public int cardValue = 0;
    public Operator op = Operator.Plus;
    public BSInput(BSInputType a_type, int a_cardValue, Operator a_op)
    {
        type = a_type;
        cardValue = a_cardValue;
        op = a_op;
    }
    public BSInput() {}
};

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject cardPrefab;

    public BaseHero hero;
    public BaseEnemy enemy;

    public Button endTurnButton;

    private List<GameObject> spawned_cards = new List<GameObject>();
    public BattleState state = BattleState.NONE;
    private List<BSInput> input_queue = new List<BSInput>();

    public GameObject buttonRef; // set in editor
    // bound to button
    public void EndTurn()
    {
        ReceiveInput(BSInputType.ENDTURN, 0, Operator.Plus);
    }

    private float elapsedTime = 0;
    private void TransitionState(BattleState new_state)
    {
        elapsedTime = 0;
        Debug.Log("Current state: " + new_state.ToString());
        state = new_state;
    }

    // example usage: ReceiveInput(ATTACK, 3)
    public void ReceiveInput(BSInputType type, int cardValue, Operator op)
    {
        input_queue.Add(new BSInput(type, cardValue, op));
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {

        // update timer
        elapsedTime += Time.deltaTime;

        // get input from queue
        BSInput cur_input = new BSInput();
        if (input_queue.Count != 0)
        {
            cur_input = input_queue[0];
            input_queue.RemoveAt(0);
        }

        // START state is in case we need a delay to run FX before starting player turn
        if (state == BattleState.START)
        {
            if (elapsedTime > 1)
            {
                SpawnCards();
                TransitionState(BattleState.PLAYERTURN);
            }
            return;
        }

        // Player attacks happen here
        if (state == BattleState.PLAYERTURN)
        {
            
            if (cur_input.type == BSInputType.PLAYERATTACK)
            {
                DealDamageToEnemy(cur_input.op, cur_input.cardValue);
                //if () TransitionState(BattleState.PLAYERTURNEND);
            } else if (cur_input.type == BSInputType.ENDTURN)
            {
                DeleteCards();
                TransitionState(BattleState.PLAYERTURNEND);
            }
            return;
        }

        if (state == BattleState.PLAYERTURNEND)
        {

            if (elapsedTime > 1) TransitionState(BattleState.ENEMYTURN);
            return;
        }

        if (state == BattleState.ENEMYTURN)
        {
            if (enemySquareRootable())
            {
                TransitionState(BattleState.WON);
                // To Do: play square root fx (square root sword drops down)
            }
            else if (enemy.health.currentHealth <= 0)
            {
                TransitionState(BattleState.WON);
                // To Do: play normal enemy death fx
                // show winning menu
                //   restart by calling ResetGame()
                //   load next level by going to new scene
                //   quit
            }
            else
            {
                // Enemy attacks player if it's not dead from the previous player attack
                DealDamageToHero();
                // TODO: play animation
                
                TransitionState(BattleState.ENEMYTURNEND);
            }
            return;
        }

        if (state == BattleState.ENEMYTURNEND)
        {
            if (elapsedTime > 2)
            {
                if (hero.health.currentHealth <= 0)
                {
                    TransitionState(BattleState.LOST);
                }
                else
                {
                    TransitionState(BattleState.START);
                }
            }
            return;
        }

        if (state == BattleState.WON)
        {
            if (elapsedTime > 3)
            {
                // To Do: show winning UI and highscores UI
                Debug.Log("squar rootable");
                SceneManager.LoadScene(Loader.Scene.Victory.ToString());
                // Loader.Load(Loader.Scene.Victory);   
                //   alternatively go straight to shop scene
                TransitionState(BattleState.NONE);

            }
            return;
        }

        if (state == BattleState.LOST)
        {
            if (elapsedTime > 3)
            {
                // To Do: show losing UI and highscores UI
                //   alternatively go straight to shop scene
                TransitionState(BattleState.NONE);
            }
            return;
        }
    }

    // reset the cards and enemy and player (i.e. restart the game)
    //   can be bound to UI button
    public void ResetGame()
    {
        // reset enemy health
        enemy.health.ResetHealth();

        // reset player health
        hero.health.ResetHealth();

        DeleteCards();
        TransitionState(BattleState.START);

        buttonRef.SetActive(true);
    }

    private void SpawnCards()
    {
        // setup new cards
        int numCards = Random.Range(3, 6); // spawn 3 to 5 cards
        Vector3 cardLoc = new Vector3(-6.87f, -3.24f, 0); // To Do
        for (int i = 0; i < numCards; ++i)
        {
            spawned_cards.Add(Instantiate(cardPrefab, cardLoc, Quaternion.identity)); // spawn card
            cardLoc.x += 2.6f;
        }

        buttonRef.SetActive(true);
    }

    private void DeleteCards()
    {
        while (spawned_cards.Count > 0)
        {
            Destroy(spawned_cards[0]);
            spawned_cards.RemoveAt(0);
        }

        buttonRef.SetActive(false);
    }

    private bool enemySquareRootable()
    {
        double rootResult = Mathf.Sqrt(enemy.health.currentHealth);
        return (rootResult % 1 == 0);
    }
    private void DealDamageToHero()
    {
        // To Do: play attack and damage fx
        hero.health.currentHealth -= 5; // To Do: make damage random?

    }
    private void DealDamageToEnemy(Operator mathOperation, int operand2 = 0)
    {
        // To Do: play attack and damage fx
        switch (mathOperation)
        {

            case Operator.Plus: 
                {
                    enemy.health.currentHealth += operand2;
                    break;
                }
            case Operator.Minus:
                {   
                    enemy.health.currentHealth -= operand2;
                    break;
                }
            case Operator.Multiplication:
                {
                    enemy.health.currentHealth *= operand2;
                    break;
                }
            case Operator.Division:
                {
                    double divisionResult = (double)enemy.health.currentHealth/operand2;
                    bool isDivisible = (divisionResult % 1 == 0);
                    if (isDivisible)
                        enemy.health.currentHealth /= operand2;
                    break;
                }
        }
    }
}
