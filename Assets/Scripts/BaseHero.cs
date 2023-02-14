using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum Operator{Plus, Minus, Multiplication, Division,};

public class BaseHero : MonoBehaviour
{

    [field: SerializeField]
    public Health health;
    
    public bool isClicked = false;
    
    public static int levelHealth = 30;

    // UI elements
    public SpriteRenderer operatorSprite;

    public SpriteRenderer heroSprite;

    [field: SerializeField]
    public Operator currentOperator = Operator.Multiplication;
    
    // Start is called before the first frame update
    void Start()
    {
        health.currentHealth = levelHealth;
        Debug.Log("Current health = "+ levelHealth);
        this.operatorSprite.sprite = Resources.Load<Sprite>("MathSymbols/" + (string)Enum.GetName(typeof(Operator), currentOperator));
        this.heroSprite.sprite = Resources.Load<Sprite>("HeroSprites/" + (string)Enum.GetName(typeof(Operator), currentOperator));
    }

    // Update is called once per frame
    void Update()
    {
        this.operatorSprite.sprite = Resources.Load<Sprite>("MathSymbols/" + (string)Enum.GetName(typeof(Operator), currentOperator));
        // this.heroSprite.sprite = Resources.Load<Sprite>("HeroSprites/" + (string)Enum.GetName(typeof(Operator), currentOperator));
    }

    void OnMouseDown()
    {
        isClicked = true;
    }
}
