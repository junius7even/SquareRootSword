using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI shopInstructions;

    public GameObject option1;

    public GameObject option2;

    public void addMaxHealth()
    {
        BaseHero.levelHealth += 25;
        shopInstructions.text = "Now go on! Defeat the monsters destroying this beautiful math land!";
        option1.SetActive(false);
        option2.SetActive(false);
    }

    public void addOneCard()
    {
        BattleSystem.numCards++;
        shopInstructions.text = "Now go on! Defeat the monsters destroying this beautiful math land!";
        option1.SetActive(false);
        option2.SetActive(false);
    }
}
