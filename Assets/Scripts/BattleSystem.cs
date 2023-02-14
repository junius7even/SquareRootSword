using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

// go to shop scene after transitioning to won/lost

public enum BattleState {STARTLEVEL, CARDPREP, PLAYERTURN, PLAYERWAITING, PLAYERTURNEND, ENEMYTURN, ENEMYTURNEND, WON, LOST, NONE}

public class BattleSystem : MonoBehaviour
{
    public static int levelNumber = 1;

    public Animator heroAnimator;
    public Animator enemyAnimator;
    
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject cardPrefab;

    public BaseHero hero;
    public BaseEnemy enemy;

    public Button endTurnButton;

    private List<GameObject> spawned_cards = new List<GameObject>();
    public BattleState state = BattleState.NONE;
    private List<BSInput> input_queue = new List<BSInput>();

    private int cardValueCached;

    public CollidingHealthText healthTextCurrentlyHovering = null;
    public CollidingAttackDamage damageTextCurrentlyHovering = null;

    public bool heroIsDragging = false;

    public GameObject buttonRef; // set in editor

    public SpriteRenderer battleBackground; // set in editor

    private bool hasAttacked;

    private bool hasSquareRooted;
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
        if (new_state == BattleState.PLAYERWAITING)
            GetComponent<DragIndicator>().enabled = true;
        else
            GetComponent<DragIndicator>().enabled = false;
    }

    // example usage: ReceiveInput(ATTACK, 3)
    public void ReceiveInput(BSInputType type, int cardValue, Operator op)
    {
        if (type == BSInputType.PLAYERATTACK)
            cardValueCached = cardValue;
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

        if (state == BattleState.STARTLEVEL)
        {
            
            enemy.health.maxHealth = enemy.healthPerLevel[levelNumber - 1];
            enemy.health.currentHealth = enemy.health.maxHealth;
            enemy.attackDamage = enemy.attackDamagePerLevel[levelNumber - 1];
            enemy.enemySprite.sprite = Resources.Load<Sprite>("Enemies/level" + levelNumber.ToString());
            battleBackground.sprite = Resources.Load<Sprite>("Backgrounds/background" + levelNumber.ToString());
            TransitionState(BattleState.CARDPREP);
        }

        // START state is in case we need a delay to run FX before starting player turn
        if (state == BattleState.CARDPREP)
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
            Rigidbody2D rigidbody;
            if (!(rigidbody = hero.GetComponent<Rigidbody2D>()))
            {
                hero.AddComponent<Rigidbody2D>();
                hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            if (cur_input.type == BSInputType.PLAYERATTACK)
            {
                TransitionState(BattleState.PLAYERWAITING);
            } else if (cur_input.type == BSInputType.ENDTURN)
            {
                DeleteCards();
                TransitionState(BattleState.PLAYERTURNEND);
            }
            return;
        }
        
        // Handle player dragging to attack/health
        if (state == BattleState.PLAYERWAITING)
        {
            if (heroAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && heroAnimator.GetBool("IsAttacking"))
            {
                heroAnimator.SetBool("IsAttacking", false);
                heroAnimator.SetBool("IsReady", false);
                TransitionState(BattleState.PLAYERTURN);
            }
            heroAnimator.SetBool("IsReady", true);
            Rigidbody2D rigidBody = hero.GetComponent<Rigidbody2D>();
            Destroy(rigidBody);
            // Drag starts on 
            if (Input.GetMouseButtonDown(0) && hero.isClicked)
            {
                // Starts dragging from the hero
                heroIsDragging = true;
            }
            
            // If mouse is released
            if (Input.GetMouseButtonUp(0))
            {
                heroIsDragging = false;
                if (healthTextCurrentlyHovering != null)
                {
                    heroAnimator.SetBool("IsAttacking", true);
                    heroAnimator.SetBool("IsReady", false);
                    GetComponent<DragIndicator>().lr.enabled = false;
                    if (healthTextCurrentlyHovering.CompareTag("Enemy"))
                    {
                        DealDamageToEnemy(hero.currentOperator, cardValueCached);
                        healthTextCurrentlyHovering = null;
                    }
                    else if (healthTextCurrentlyHovering.CompareTag("Hero"))
                    {
                        DealDamageToHero(hero.currentOperator, cardValueCached);
                        healthTextCurrentlyHovering = null;
                    }
                }
                else if (damageTextCurrentlyHovering != null)
                {
                    heroAnimator.SetBool("IsAttacking", true);
                    heroAnimator.SetBool("IsReady", false);
                    GetComponent<DragIndicator>().lr.enabled = false;
                    ModifyAttackDamage(hero.currentOperator, cardValueCached);
                    damageTextCurrentlyHovering = null;
                }
            }
        }

        if (state == BattleState.PLAYERTURNEND)
        {
            if (elapsedTime > 1)
            {
                hero.health.ClampHealthCheck();
                TransitionState(BattleState.ENEMYTURN);
            }
            return;
        }

        if (state == BattleState.ENEMYTURN)
        {
            if (enemyHealthSquareRootable())
            {
                enemy.health.currentHealth = (int)Math.Sqrt(enemy.health.currentHealth);
            }
            else if (enemy.health.currentHealth <= 0)
            {
                
                if (enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
                    enemyAnimator.GetBool("IsDead"))
                {
                    enemyAnimator.SetBool("IsDead", false);
                    enemy.health.currentHealth = 0;
                    TransitionState(BattleState.WON);
                }
                else
                {
                    enemyAnimator.SetBool("IsDead", true);
                }
                
                // Play death animation
                // To Do: play square root fx (square root sword drops down)
                // To Do: play normal enemy death fx
                // show winning menu
                //   restart by calling ResetGame()
                //   load next level by going to new scene
                //   quit
            }
            else
            {
                if (enemyAttackDamageSquareRootable() && !hasSquareRooted)
                {
                    enemy.attackDamage = (int)Math.Sqrt(enemy.attackDamage);
                    hasSquareRooted = true;
                }
                
                if (!hasAttacked)
                {
                    if (heroHealthSquareRootable())
                    {
                        hero.health.currentHealth = 0;
                        enemyAnimator.SetBool("IsAttacking", true);
                        hasAttacked = true;
                    }
                    else
                    {
                        // Enemy attacks player if it's not dead from the previous player attack
                        DealDamageToHero(Operator.Minus, enemy.attackDamage);
                        enemyAnimator.SetBool("IsAttacking", true);
                        hasAttacked = true;
                    }
                }
                else if (enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
                         enemyAnimator.GetBool("IsAttacking"))
                {
                    enemyAnimator.SetBool("IsAttacking", false);
                    hasAttacked = false;
                    hasSquareRooted = false;
                    TransitionState(BattleState.ENEMYTURNEND);
                }
            }
        }
        
        if (state == BattleState.ENEMYTURNEND)
        {
            if (hero.health.currentHealth <= 0)
            {
                if (heroAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
                    heroAnimator.GetBool("IsDead"))
                {
                    TransitionState(BattleState.LOST);
                    return;
                }
                heroAnimator.SetBool("IsDead", true);
            }
            else
            {
                TransitionState(BattleState.CARDPREP);
                enemy.ResetAttackDamage();
                return;
            }
        }

        if (state == BattleState.WON)
        {
            if (elapsedTime > 3)
            {
                
                // To Do: show winning UI and highscores UI
                Debug.Log("square rootable");
                // Loader.Load(Loader.Scene.Victory);   
                Loader.AdditiveLoad(Loader.Scene.Victory);
                //   alternatively go straight to shop scene
            }

            if (elapsedTime > 6) {
                Loader.UnloadAdditive(Loader.Scene.Victory);
                TransitionState(BattleState.NONE);
                Loader.Load(Loader.Scene.ShopScene);
                levelNumber++;
            }

            return;
        } 
        
        if (state == BattleState.LOST)
        {
            if (elapsedTime > 3)
            {
                // To Do: show losing UI and highscores UI
                //   alternatively go straight to shop scene
                Loader.Load(Loader.Scene.GameOverScene);
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
        TransitionState(BattleState.STARTLEVEL);

        buttonRef.SetActive(true);
    }

    private void SpawnCards()
    {
        // setup new cards
        int numCards = Random.Range(3, 3); // spawn 3 to 5 cards
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

    private bool enemyHealthSquareRootable()
    {
        double rootResult = Mathf.Sqrt(enemy.health.currentHealth);
        return (rootResult % 1 == 0);
    }
    private bool heroHealthSquareRootable()
    {
        double rootResult = Mathf.Sqrt(hero.health.currentHealth);
        return (rootResult % 1 == 0);
    }
    private bool enemyAttackDamageSquareRootable()
    {
        double rootResult = Mathf.Sqrt(enemy.attackDamage);
        return (rootResult % 1 == 0);
    }
    
    public void ModifyAttackDamage(Operator mathOperation, int operand2)
    {
        // To Do: play attack and damage fx
        switch (mathOperation)
        {
            case Operator.Plus: 
            {
                enemy.attackDamage += operand2;
                break;
            }
            case Operator.Minus:
            {   
                enemy.attackDamage -= operand2;
                break;
            }
            case Operator.Multiplication:
            {
                enemy.attackDamage *= operand2;
                break;
            }
            case Operator.Division:
            {
                enemy.attackDamage /= operand2;
                break;
            }
        }
    }
    public void DealDamageToHero(Operator mathOperation, int operand2)
    {
        // To Do: play attack and damage fx
        switch (mathOperation)
        {
            case Operator.Plus: 
            {
                hero.health.currentHealth += operand2;
                break;
            }
            case Operator.Minus:
            {   
                hero.health.currentHealth -= operand2;
                break;
            }
            case Operator.Multiplication:
            {
                hero.health.currentHealth *= operand2;
                break;
            }
            case Operator.Division:
            {
                hero.health.currentHealth /= operand2;
                break;
            }
        }
    }
    public void DealDamageToEnemy(Operator mathOperation, int operand2 = 0)
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
                    enemy.health.currentHealth /= operand2;
                    break;
                }
        }
    }
}
