using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Girl2Skills : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject target;
    public GameObject thisGirl;
    public GameObject thisGirlMenu;
    public Animator cecEffect;
    public GameObject bEffect;

    public int skill1Damage = 2;

    public int spSkillDamage;
    public TextMeshProUGUI turnTxt;

    public List<GameObject> enemiesToAttack;
    public GameObject[] enemiesToRemove;
    public int i = 0;

    public TextMeshProUGUI noSkillPointsText;

    public Animator gAnim;

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
        gAnim.SetTrigger("brisa");
        StartCoroutine(BasicEffect());

        target.GetComponent<EnemyStats>().EnemySufferDamage(skill1Damage);

        if (target.GetComponent<EnemyStats>().health <= 0)
        {
            mainCamera.GetComponent<ListController>().RemoveFromList(target);
        }

        thisGirl.GetComponent<GirlStats>().IncreaseManaAtEndTurn();
        thisGirl.GetComponent<GirlStats>().thisgirlHud.enabled = false;
        thisGirlMenu.SetActive(false);
        StartCoroutine(RBasicAttack());
    }

    IEnumerator RBasicAttack()
    {
        turnTxt.gameObject.SetActive(true);
        turnTxt.text = "Cecilia causa " + skill1Damage + " de dano a " + target.GetComponent<EnemyStats>().nome;
        yield return new WaitForSeconds(1.5f);
        turnTxt.gameObject.SetActive(false);
        mainCamera.GetComponent<ListController>().NextTurn();
    }

    public void SpAttack()
    {
        if (thisGirl.GetComponent<GirlStats>().maxMana == thisGirl.GetComponent<GirlStats>().mana)
        {
            gAnim.SetTrigger("ventania");
            StartCoroutine(SPAnim());
            enemiesToAttack = mainCamera.GetComponent<ListController>().enemiesInListC;
            foreach(GameObject enemyTA in enemiesToAttack)
            {
                enemyTA.GetComponent<EnemyStats>().EnemySufferDamage(spSkillDamage);
                if (enemyTA.GetComponent<EnemyStats>().health <= 0)
                {
                    enemiesToRemove[i] = enemyTA;
                    i++;
                }
            }

            if (i != 0)
            {
                Debug.Log("remove start");
                for (int count=0; count != i; count++)
                {
                    Debug.Log(enemiesToRemove[count]);
                    mainCamera.GetComponent<ListController>().RemoveFromList(enemiesToRemove[count]);
                }
                i = 0;
            }


            thisGirl.GetComponent<GirlStats>().mana = 0;
            thisGirl.GetComponent<GirlStats>().SetGirlStatsText();
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
        yield return new WaitForSeconds(0.3f);
        turnTxt.gameObject.SetActive(true);
        turnTxt.text = "Cecilia causa " + spSkillDamage + " a todos os inimigos!";
        yield return new WaitForSeconds(1.5f);
        turnTxt.gameObject.SetActive(false);
        mainCamera.GetComponent<ListController>().NextTurn();
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
        cecEffect.transform.position = new Vector2(-3.03f, -0.09f);
        cecEffect.gameObject.SetActive(true);
        cecEffect.SetTrigger("csp");
        yield return new WaitForSeconds(2f);
        cecEffect.gameObject.SetActive(false);
    }
    IEnumerator BasicEffect()
    {
        bEffect.transform.position = target.transform.position;
        bEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        bEffect.SetActive(false);
    }
}

