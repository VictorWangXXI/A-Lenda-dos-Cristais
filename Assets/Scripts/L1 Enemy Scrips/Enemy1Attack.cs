using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class Enemy1Attack : MonoBehaviour
{
    public GameObject mainCameraE;

    public int damage;
    public int spDamage;
    public int skPoints;
    public int maxSkPoints=2;
    public TextMeshProUGUI skPointsTxt;
    public Animator ansEffect;
    public float waitEffect=0.5f;

    public GameObject targetE;
    private string ansiedade = "Ansiedade";
    public TextMeshProUGUI turnText;
    public Animator ansAnim;
    private TextMeshProUGUI damageText;

    public GameObject bEffect;
    private void Start()
    {
        skPoints = 0;
        skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
    }
    //public void E1Attack()
    //{
    //    targetE = mainCameraE.GetComponent<UnitSelector>().currentGirlSelected;

    //    targetE.GetComponent<GirlStats>().health = targetE.GetComponent<GirlStats>().health - damage;



    //    if (targetE.GetComponent<GirlStats>().health <= 0)
    //    {
    //        mainCameraE.GetComponent<ListController>().RemoveFromList(targetE);
    //    }

    //}

    public void CheckEnemySkillPoints()
    {
        if (skPoints == maxSkPoints)
        {
            skPoints = 0;
            skPointsTxt.color = Color.white;
            skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
            E1SpecialAttack();
        }
        else
        {
            skPoints++;
            E1SingleRandomAttack();

            if (skPoints == maxSkPoints)
            {
                skPointsTxt.color = Color.red;
            }

            skPointsTxt.text = "Pt. Hab.: " + skPoints + "/" + maxSkPoints;
        }
    }

    public void E1SingleRandomAttack()
    {
        targetE = mainCameraE.GetComponent<ListController>().RandomGirlPicker();
        damageText = targetE.GetComponent<GirlStats>().gDamageText;

        ansAnim.SetTrigger("golpe");
        StartCoroutine(BasicAttack());
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
        mainCameraE.GetComponent<ListController>().NextTurn();
    }


    public void E1SpecialAttack()
    {
        targetE = mainCameraE.GetComponent<ListController>().RandomGirlPicker();
        damageText = targetE.GetComponent<GirlStats>().gDamageText;
        ansAnim.SetTrigger("frio");
        StartCoroutine(SPAnim());
        StartCoroutine(SpAttack());

    }

    IEnumerator SPAnim()
    {
        yield return new WaitForSeconds(waitEffect);
        ansEffect.transform.position = new Vector2(3.57f, -0.39f);
        ansEffect.gameObject.SetActive(true);
        ansEffect.SetTrigger("asp");
        yield return new WaitForSeconds(2f);
        ansEffect.gameObject.SetActive(false);
    }

    IEnumerator SpAttack()
    {
        yield return new WaitForSeconds(waitEffect + 0.2f);
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
            Debug.Log("No dmg 1");
            StartCoroutine(ShowText(0));
            StartCoroutine(ShowDamage(0));
        }
        mainCameraE.GetComponent<ListController>().NextTurn();
    }

    IEnumerator ShowText(int dmg)
    {
        turnText.gameObject.SetActive(true);
        turnText.text = ansiedade + " causa " + dmg + " de dano a " + targetE.name;
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
