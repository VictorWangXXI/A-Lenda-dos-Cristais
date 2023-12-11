using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStats : MonoBehaviour
{
    public string nome;

    public int maxHealth;
    public int health;
    public Animator anim;
    public Collider2D collid;
    public SpriteRenderer rend;
    public TextMeshProUGUI eDamageText;

    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        SetEnemyStatsText();
    }

    public void SetEnemyStatsText()
    {
        healthText.text = health.ToString();
    }

    public void EnemySufferDamage(int damage)
    {
        //StartCoroutine(TurnRed());
        health = health - damage;
        SetEnemyStatsText();
        if (health > 0)
        {
            StartCoroutine(ShowDamage(damage));
            anim.SetTrigger("rdmg");

        }
    }

    //IEnumerator TurnRed()
    //{
    //    rend.color = Color.red;
    //    yield return new WaitForSeconds(0.5f);
    //    rend.color = Color.white;
    //}

    IEnumerator ShowDamage(int dmg)
    {
        //damageText.gameObject.SetActive(false);
        eDamageText.text = "- " + dmg;
        eDamageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        eDamageText.gameObject.SetActive(false);
    }

    public void Die()
    {
        anim.SetTrigger("die");
        collid.enabled = false;
    }
}
