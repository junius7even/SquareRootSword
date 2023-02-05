using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public int maxHealth;

    public TextMeshProUGUI healthText;

    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        this.healthText.text = currentHealth.ToString();
        currentHealth = maxHealth;
    }
    void Update()
    {
        this.healthText.text = currentHealth.ToString();
    }
}
