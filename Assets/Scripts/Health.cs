using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public int maxHealth;

    public int currentHealth;

    public int turnHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void turnStart()
    {
        this.turnHealth = currentHealth;
    }

    public void endTurn()
    {
        this.currentHealth = turnHealth;
    }
}
