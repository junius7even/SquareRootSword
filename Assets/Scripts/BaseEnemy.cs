using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseEnemy : MonoBehaviour
{
    public int[] attackDamagePerLevel;

    public int[] HealthPerLevel;

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
        HealthPerLevel = new [] { 7, 19, 91, 151, 331 };
        attackDamagePerLevel = new [] { 0, 4, 12, 23, 31 };
        animatorOverrideControllers = new[] { level1Animator, level2Animator, level3Animator, level4Animator, level5Animator };
        enemyAnimatorController.runtimeAnimatorController = animatorOverrideControllers[BattleSystem.levelNumber-1];
        Debug.Log("The current level of battleSystem: " + BattleSystem.levelNumber);
        Debug.Log(animatorOverrideControllers[BattleSystem.levelNumber-1].name.ToString());
        ResetAttackDamage();
    }

    void Update()
    {
        enemyAnimatorController.runtimeAnimatorController = animatorOverrideControllers[BattleSystem.levelNumber-1];
        this.attackDamageTextMesh.text = attackDamage.ToString();
        maxAttackDamage = attackDamagePerLevel[BattleSystem.levelNumber-1];
    }
    public void ResetAttackDamage()
    {
        attackDamage = maxAttackDamage;
    }

}
