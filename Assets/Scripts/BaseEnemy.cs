using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [field: SerializeField]
    public Health health;
    public int maxAttackDamage = 5;
    public int attackDamage;
    public TextMesh attackDamageTextMesh;

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
