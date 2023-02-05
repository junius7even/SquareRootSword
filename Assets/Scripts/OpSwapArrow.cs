using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpSwapArrow : MonoBehaviour
{
    public enum ArrowDirection { Left, Right };

    public BaseHero hero;
    public BattleSystem battleSystem;

    [field: SerializeField]
    private ArrowDirection currentDirection;

    void OnMouseUp()
    {
        Debug.Log("You've clicked");
        if (battleSystem.state != BattleState.PLAYERTURN) { return; }
        if (currentDirection == ArrowDirection.Left)
        {
            if (hero.currentOperator == Operator.Plus)
            hero.currentOperator = Operator.Division;
            else
                hero.currentOperator -= 1;
        }
            
        else
        {
            if (hero.currentOperator == Operator.Division)
            hero.currentOperator = Operator.Plus;
            else
                hero.currentOperator += 1;
        }
    }
}
