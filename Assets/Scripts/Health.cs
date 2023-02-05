using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public int maxHealth = 100;

    public int currentHealth;

    public TextMesh textMeshRef; // set in editor

    // Start is called before the first frame update
    void Start()
    {

        // this.healthText.text = currentHealth.ToString();
        ResetHealth();
    }

    private void Update()
    {
        textMeshRef.text = currentHealth.ToString();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ClampHealthCheck()
    {
        if (currentHealth > maxHealth) ResetHealth();
    }
}
