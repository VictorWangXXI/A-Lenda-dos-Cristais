using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy3Attack : MonoBehaviour
{
    public GameObject mainCameraE;

    public int damage;
    public int spDamage;
    public int skPoints;
    public TextMeshProUGUI skPointsTxt;
    public int maxSkPoints = 2;

    public GameObject targetE;
    private string euforia = "Euforia";
    public TextMeshProUGUI turnText;
    public Animator eufAnim;
    private TextMeshProUGUI damageText;

    public GameObject bEffect;
    public Animator bangEffect;

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
            E3SpecialAttack();
        }
        else
        {
            skPoints++;
            E3SingleRandomAttack();
            if (skPoints == maxSkPoints)
            {
                skPointsTxt.color = Color.red;
            }
            skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
        }
    }
        public void E3SingleRandomAttack()
    {
        targetE = mainCameraE.GetComponent<ListController>().RandomGirlPicker();
        damageText = targetE.GetComponent<GirlStats>().gDamageText;
        eufAnim.SetTrigger("punch");
        StartCoroutine(BasicAttack());
    }

    public void E3SpecialAttack()
    {
        targetE = mainCameraE.GetComponent<ListController>().RandomGirlPicker();
        damageText = targetE.GetComponent<GirlStats>().gDamageText;
        eufAnim.SetTrigger("bang");
        StartCoroutine(SpAttack());
    }

    IEnumerator BasicAttack()
    {
        yield return new WaitForSeconds(0.8f);
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
        yield return new WaitForSeconds(1f);
        mainCameraE.GetComponent<ListController>().NextTurn();
    }

    IEnumerator SpAttack()
    {
        yield return new WaitForSeconds(1.01f);
        bangEffect.gameObject.SetActive(true);
        bangEffect.SetTrigger("bef");
        if (mainCameraE.GetComponent<ListController>().girlsInvincibility == false)
        {
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
            Debug.Log("No dmg 3");
            StartCoroutine(ShowText(0));
            StartCoroutine(ShowDamage(0));
        }
        yield return new WaitForSeconds(1f);
        bangEffect.gameObject.SetActive(false);
        mainCameraE.GetComponent<ListController>().NextTurn();
    }

        IEnumerator ShowText(int dmg)
    {
        turnText.gameObject.SetActive(true);
        turnText.text = euforia + " causa " + dmg + " de dano a " + targetE.name;
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
