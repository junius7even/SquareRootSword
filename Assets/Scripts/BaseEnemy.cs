using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int[] attackDamagePerLevel = { 0, 4, 6, 15, 10 };
    public int[] healthPerLevel = { 7, 19, 91, 151, 331 };

    [field: SerializeField]
    public Health health;
    public int maxAttackDamage = 5;
    public int attackDamage;
    public TextMesh attackDamageTextMesh;

    public SpriteRenderer enemySprite;

    void Start()
    {
        ResetAttackDamage();
    }

    void Update()
    {
        this.attackDamageTextMesh.text = attackDamage.ToString();
    }
    public void ResetAttackDamage()
    {
        attackDamage = maxAttackDamage;
    }
}
