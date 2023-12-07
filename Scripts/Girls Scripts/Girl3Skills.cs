using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Girl3Skills : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject target;
    public GameObject thisGirl;
    public GameObject thisGirlMenu;

    public int skill1Damage = 3;
    public Animator iaraEffect;
    public GameObject bEffect;

    public List<GameObject> alliesToHeal;
    public int healingPower;

    public TextMeshProUGUI noSkillPointsText;
    public TextMeshProUGUI turnTxt;

    public Animator iaraAnim;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BasicAttack()
    {
        target = mainCamera.GetComponent<UnitSelector>().currentEnemySelected;
        thisGirl.GetComponent<GirlStats>().thisgirlHud.enabled = false;
        thisGirlMenu.SetActive(false);
        iaraAnim.SetTrigger("aguas");
        StartCoroutine(RBasicAttack());
    }

    IEnumerator RBasicAttack()
    {
        yield return new WaitForSeconds(0.5f);
        turnTxt.gameObject.SetActive(true);
        turnTxt.text = "Iara causa " + (skill1Damage) + " de dano a " + target.GetComponent<EnemyStats>().nome;
        StartCoroutine(BasicEffect());
        target.GetComponent<EnemyStats>().EnemySufferDamage(skill1Damage);
        if (target.GetComponent<EnemyStats>().health <= 0)
        {
            mainCamera.GetComponent<ListController>().RemoveFromList(target);
        }

        thisGirl.GetComponent<GirlStats>().IncreaseManaAtEndTurn();
        mainCamera.GetComponent<ListController>().NextTurn();
        yield return new WaitForSeconds(1.7f);
        turnTxt.gameObject.SetActive(false);
    }

    public void SpAttack()
    {
        if (thisGirl.GetComponent<GirlStats>().maxMana == thisGirl.GetComponent<GirlStats>().mana)
        {
            alliesToHeal = mainCamera.GetComponent<ListController>().girlsInListC;
            iaraAnim.SetTrigger("calmaria");
            thisGirl.GetComponent<GirlStats>().thisgirlHud.enabled = false;
            thisGirlMenu.SetActive(false);
            StartCoroutine(RSpAttack());
        }
        else
        {
            if (noSkillPointsText.gameObject.activeSelf == false)
            {
                StartCoroutine(MessageNoSkillPoints());
            }
        }
    }

    IEnumerator RSpAttack()
    {
        yield return new WaitForSeconds(0.5f);
        turnTxt.gameObject.SetActive(true);
        turnTxt.text = "Iara cura todas as aliadas ativas em 50 pontos!";
        StartCoroutine(SPAnim());
        foreach (GameObject allyToHeal in alliesToHeal)
        {
            allyToHeal.GetComponent<GirlStats>().GetHealed(healingPower);
        }

        thisGirl.GetComponent<GirlStats>().mana = 0;
        thisGirl.GetComponent<GirlStats>().SetGirlStatsText();
        mainCamera.GetComponent<ListController>().NextTurn();
        yield return new WaitForSeconds(1.7f);
        turnTxt.gameObject.SetActive(false);

    }

    IEnumerator MessageNoSkillPoints()
    {
        noSkillPointsText.text = "Sem Pontos de Habilidae Suficientes!";
        noSkillPointsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        noSkillPointsText.gameObject.SetActive(false);
    }

    IEnumerator SPAnim()
    {
        iaraEffect.gameObject.SetActive(true);
        iaraEffect.SetTrigger("isp");
        yield return new WaitForSeconds(2f);
        iaraEffect.gameObject.SetActive(false);
    }

    IEnumerator BasicEffect()
    {
        bEffect.transform.position = target.transform.position;
        bEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        bEffect.SetActive(false);
    }

}

