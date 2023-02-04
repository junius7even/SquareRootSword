using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : MonoBehaviour
{
    [field: SerializeField]
<<<<<<< HEAD
    public Health health;

    [field: SerializeField]
    private BaseEnemy currentEnemy;

    [field: SerializeField]
    private Operator currentOperator;

    public enum Operator{SquareRoot, Plus, Minus, Multiplication, Division,}
=======
    public Operator currentOperator = Operator.Multiplication;

    public enum Operator{SquareRoot, Plus, Minus, Multiplication, Division,};
>>>>>>> patch-2

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
