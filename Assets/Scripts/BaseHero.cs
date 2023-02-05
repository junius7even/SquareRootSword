using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operator{SquareRoot, Plus, Minus, Multiplication, Division,};

public class BaseHero : MonoBehaviour
{

    [field: SerializeField]
    public Health health;

    [field: SerializeField]
    private BaseEnemy currentEnemy;

    [field: SerializeField]
    public Operator currentOperator = Operator.Multiplication;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endTurn()
    {
   
    }
}
