using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int[] attackDamagePerLevel = { 0, 9, 10, 19, 23 };
    public int[] healthPerLevel = { 23, 1, 1, 1, 1 };
    // public int[] healthPerLevel = { 9, 19, 91, 151, 331 };

    private int maxAttackDamage;
    [field: SerializeField]
    public Health health;
    public int attackDamage;
    public TextMesh attackDamageTextMesh;

    public BattleSystem battleSystem;

    private AnimatorOverrideController[] animatorOverrideControllers;

    public AnimatorOverrideController level1Animator;
    public AnimatorOverrideController level2Animator;
    public AnimatorOverrideController level3Animator;
    public AnimatorOverrideController level4Animator;
    public AnimatorOverrideController level5Animator;

    public SpriteRenderer enemySprite;
    public Animator enemyAnimatorController;
    void Start()
    {
        animatorOverrideControllers = new[] { level1Animator, level2Animator, level3Animator, level4Animator, level5Animator };
        Debug.Log("The current level of battleSystem: " + BattleSystem.levelNumber);
        enemyAnimatorController.runtimeAnimatorController = animatorOverrideControllers[BattleSystem.levelNumber-1];
        Debug.Log(animatorOverrideControllers[BattleSystem.levelNumber-1].name.ToString());
        ResetAttackDamage();
    }

    void Update()
    {
        this.attackDamageTextMesh.text = attackDamage.ToString();
        maxAttackDamage = attackDamagePerLevel[BattleSystem.levelNumber-1];
    }
    public void ResetAttackDamage()
    {
        attackDamage = maxAttackDamage;
    }
}
