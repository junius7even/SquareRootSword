using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class OpSwapArrow : MonoBehaviour
{
    public enum ArrowDirection { Left, Right };

    public BaseHero hero;
    public BattleSystem battleSystem;

    public Animator heroAnimator;
    public AnimatorOverrideController plusAnimator; 
    public AnimatorOverrideController minusAnimator;
    public AnimatorOverrideController multiplicationAnimator;
    public AnimatorOverrideController divisionAnimator;
    private AnimatorOverrideController[] animatorOverrideControllers;
    
    [field: SerializeField]
    private ArrowDirection currentDirection;

    void Start()
    {
        animatorOverrideControllers =  new []{plusAnimator, minusAnimator, multiplicationAnimator, divisionAnimator};
    }
    
    void OnMouseUp()
    {
        Debug.Log("Current animator: " + heroAnimator.runtimeAnimatorController.name);
        if (battleSystem.state != BattleState.PLAYERTURN) { return; }
        if (currentDirection == ArrowDirection.Left)
        {
            if (hero.currentOperator == Operator.Plus)
                hero.currentOperator = Operator.Division;
            else
                hero.currentOperator -= 1;
            heroAnimator.runtimeAnimatorController = animatorOverrideControllers[(int)hero.currentOperator];
            Debug.Log("current operator: " + (int)hero.currentOperator);
        }
            
        else
        {
            if (hero.currentOperator == Operator.Division)
                hero.currentOperator = Operator.Plus;
            else
                hero.currentOperator += 1;
            heroAnimator.runtimeAnimatorController = animatorOverrideControllers[(int)hero.currentOperator];
        }
    }
}
