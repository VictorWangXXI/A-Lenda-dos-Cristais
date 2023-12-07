using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GirlStats : MonoBehaviour
{
    public int maxHealth;
    public int maxMana;

    public int health;
    public int mana;

    public GameObject thisGirlMenu;
    public Animator thisgirlHud;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI spSkillButtonTxt;
    public Animator gAni;
    public Collider2D gcollider;
    public SpriteRenderer rend;

    public TextMeshProUGUI gDamageText;

    // Start is called before the first frame update
    void Start()
    {
        SetGirlStatsText();
    }


    public void IncreaseManaAtEndTurn()
    {
        if (mana < maxMana)
        {
            mana++;
            SetGirlStatsText();
        }

    }

    public void myTurn()
    {
        thisGirlMenu.SetActive(true);
        thisgirlHud.enabled = true;
    }

    public void SetGirlStatsText()
    {
        if (mana == maxMana)
        {
            manaText.color = Color.green;
            spSkillButtonTxt.color = Color.green;
        }
        else
        {
            manaText.color = Color.white;
            spSkillButtonTxt.color = Color.white;
        }

        if (health <= 30)
        {
            healthText.color = Color.red;
        }
        else
        {
            healthText.color = Color.white;
        }
        healthText.text = "Vida: " + health + "/" + maxHealth;
        manaText.text = "Pt. Hab.: " + mana + "/" + maxMana;
    }

    public void SufferDamage(int damage)
    {

        health = health - damage;
        SetGirlStatsText();
        if (health > 0)
        {
            gAni.SetTrigger("rdmg");
        }
    }

    public void GetHealed(int hPower)
    {
        //StartCoroutine(TurnGreen());
        if ((health + hPower) >= maxHealth)
        {
            StartCoroutine(ShowHealing(maxHealth - health));
            health = maxHealth;
            SetGirlStatsText();
        }
        else
        {
            health = health + hPower;
            StartCoroutine(ShowHealing(hPower));
            SetGirlStatsText();
        }
    }

    IEnumerator ShowHealing(int hpplus)
    {
        gDamageText.color = Color.green;
        gDamageText.text = "+ " + hpplus;
        gDamageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        gDamageText.gameObject.SetActive(false);
        gDamageText.color = Color.red;
    }

    public void Die()
    {
        gAni.SetTrigger("die");
        gcollider.enabled = false;
    }

}
