using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public enum Operator{SquareRoot, Plus, Minus, Multiplication, Division,};

public class BaseHero : MonoBehaviour
{

    [field: SerializeField]
    public Health health;

    [field: SerializeField]
    private BaseEnemy currentEnemy;


    // UI elements
    public TextMeshProUGUI operatorText;

    [field: SerializeField]
    public Operator currentOperator = Operator.Multiplication;

    // Start is called before the first frame update
    void Start()
    {
        this.operatorText.text = Enum.GetName(typeof(Operator), currentOperator);
        
    }

    // Update is called once per frame
    void Update()
    {
        this.operatorText.text = Enum.GetName(typeof(Operator), currentOperator);
        
    }

    public void endTurn()
    {
        
    }
}
