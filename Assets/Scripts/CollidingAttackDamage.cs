using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollidingAttackDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private BattleSystem battleSystemRef;
    void Start()
    {
        battleSystemRef = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
    }

    void OnMouseEnter()
    {
        // If the player drags from the hero to the text
        if (battleSystemRef.state == BattleState.PLAYERWAITING && battleSystemRef.heroIsDragging)
        {
            // Set the battleSystem reference to current object
            battleSystemRef.damageTextCurrentlyHovering = this;
        }
    }

    void OnMouseExit()
    {
        battleSystemRef.damageTextCurrentlyHovering = null;
    }
}