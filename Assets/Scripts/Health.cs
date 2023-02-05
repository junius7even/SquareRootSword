using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public int maxHealth = 100;

    public TextMeshProUGUI healthText;

    public int currentHealth;

    public TextMesh textMeshRef; // set in editor

    // Start is called before the first frame update
    void Start()
    {

        this.healthText.text = currentHealth.ToString();
        currentHealth = maxHealth;

        ResetHealth();
    }

    private void Update()
    {
        textMeshRef.text = currentHealth.ToString();
    }

    public void ResetHealth()
    {
        this.healthText.text = currentHealth.ToString();
        currentHealth = maxHealth;

    }
    void Update()
    {
        this.healthText.text = currentHealth.ToString();
    }
    void Update()
    {
        this.healthText.text = currentHealth.ToString();
    }
}
