using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : MonoBehaviour
{
    [field: SerializeField]
    public Operator currentOperator = Operator.Multiplication;

    public enum Operator{SquareRoot, Plus, Minus, Multiplication, Division,};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
