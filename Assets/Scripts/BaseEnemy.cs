using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [field: SerializeField]
    public Health health;
    public int attackDamage = 5;
    public TextMesh attackDamageTextMesh;

    void Update()
    {
        this.attackDamageTextMesh.text = attackDamage.ToString();
    }
}
