using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public GameObject mainCameraE;

    public int damage;
    public int spDamage = 30;
    public GameObject targetE;
    private string fome = "Fome";
    public TextMeshProUGUI turnText;
    public Animator fomeAnim;
    private TextMeshProUGUI damageText;

    public int skPoints;
    public int maxSkPoints = 2;
    public TextMeshProUGUI skPointsTxt;

    public GameObject bEffect;
    public Animator spEffect;
    private void Start()
    {
        skPoints = 0;
        skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
    }

    public void CheckEnemySkillPoints()
    {
        if (skPoints == maxSkPoints)
        {
            skPoints = 0;
            skPointsTxt.color = Color.white;
            skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
            E2SpecialAttack();
        }
        else
        {
            skPoints++;
            E2SingleRandomAttack();
            if (skPoints == maxSkPoints)
            {
                skPointsTxt.color = Color.red;
            }
            skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
        }
    }

        public void E2SingleRandomAttack()
    {
        targetE = mainCameraE.GetComponent<ListController>().RandomGirlPicker();
        damageText = targetE.GetComponent<GirlStats>().gDamageText;
        fomeAnim.SetTrigger("at1");
        StartCoroutine(BasicAttack());

    }

    IEnumerator BasicAttack()
    {
        yield return new WaitForSeconds(0.6f);
        if (mainCameraE.GetComponent<ListController>().girlsInvincibility == false)
        {        
            bEffect.transform.position = targetE.transform.position;
            StartCoroutine(BasicEffect());
            targetE.GetComponent<GirlStats>().SufferDamage(damage);
            StartCoroutine(ShowText(damage));
            StartCoroutine(ShowDamage(damage));
            if (targetE.GetComponent<GirlStats>().health <= 0)
            {
                mainCameraE.GetComponent<ListController>().RemoveFromList(targetE);
            }
        }
        else
        {
            bEffect.transform.position = new Vector2(-0.73f, -1.31f);
            StartCoroutine(BasicEffect());
            StartCoroutine(ShowText(0));
            StartCoroutine(ShowDamage(0));
        }
        mainCameraE.GetComponent<ListController>().NextTurn();
    }

    public void E2SpecialAttack()
    {
        targetE = mainCameraE.GetComponent<ListController>().RandomGirlPicker();
        damageText = targetE.GetComponent<GirlStats>().gDamageText;
        fomeAnim.SetTrigger("at2");
        StartCoroutine(SpAttack());

    }
    IEnumerator SpAttack()
    {        


        if (mainCameraE.GetComponent<ListController>().girlsInvincibility == false)
        {               
            spEffect.transform.position = targetE.transform.position;
            StartCoroutine(SpEffect());
            yield return new WaitForSeconds(0.4f);   
            targetE.GetComponent<GirlStats>().SufferDamage(spDamage);
            StartCoroutine(ShowText(spDamage));
            StartCoroutine(ShowDamage(spDamage));
            if (targetE.GetComponent<GirlStats>().health <= 0)
            {
                mainCameraE.GetComponent<ListController>().RemoveFromList(targetE);
            }
        }
        else
        {
            spEffect.transform.position = new Vector2(-0.73f, -1.31f);
            StartCoroutine(SpEffect());
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(ShowText(0));
            StartCoroutine(ShowDamage(0));
        }
        mainCameraE.GetComponent<ListController>().NextTurn();
    }

    IEnumerator SpEffect()
    {

        spEffect.gameObject.SetActive(true);
        spEffect.SetTrigger("fsp");
        yield return new WaitForSeconds(0.7f);
        spEffect.gameObject.SetActive(false);
    }

    IEnumerator ShowText(int dmg)
    {
        turnText.gameObject.SetActive(true);
        turnText.text = fome + " causa " + dmg + " de dano a " + targetE.name;
        yield return new WaitForSeconds(2f);
        turnText.gameObject.SetActive(false);
    }
    IEnumerator ShowDamage(int dmg)
    {
        damageText.text = "- " + dmg;
        damageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        damageText.gameObject.SetActive(false);
    }

    IEnumerator BasicEffect()
    {

        bEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        bEffect.SetActive(false);
    }
}
