using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Girl1Skills : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject target;
    public GameObject thisGirl;
    public GameObject thisGirlMenu;

    public int skill1Damage = 1;
    public GameObject parede;
    public Animator paredeAni;
    public GameObject bEffect;

    public TextMeshProUGUI noSkillPointsText;
    public TextMeshProUGUI turnTxt;

    public Animator raissaAnim;


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
        raissaAnim.SetTrigger("bencao");
        StartCoroutine(RBasicAttack());
    }

    IEnumerator RBasicAttack()
    {
        yield return new WaitForSeconds(0.4f);
        turnTxt.gameObject.SetActive(true);
        turnTxt.text = "Raissa causa " + skill1Damage + " de dano a " + target.GetComponent<EnemyStats>().nome; 
        StartCoroutine(BasicEffect());
        target.GetComponent<EnemyStats>().EnemySufferDamage(skill1Damage);
        if (target.GetComponent<EnemyStats>().health <= 0)
        {
            mainCamera.GetComponent<ListController>().RemoveFromList(target);
        }
        thisGirl.GetComponent<GirlStats>().IncreaseManaAtEndTurn();
        yield return new WaitForSeconds(1.5f);
        turnTxt.gameObject.SetActive(false);
        mainCamera.GetComponent<ListController>().NextTurn();
    }

    public void SpAttack()
    {
        if (thisGirl.GetComponent<GirlStats>().maxMana == thisGirl.GetComponent<GirlStats>().mana)
        {
            raissaAnim.SetTrigger("desvio");
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
        yield return new WaitForSeconds(0.7F);
        parede.SetActive(true);
        paredeAni.SetTrigger("pup");

        yield return new WaitForSeconds(0.7f);
        turnTxt.gameObject.SetActive(true);
        turnTxt.text = "Raissa ergueu uma parede de areia!";
        mainCamera.GetComponent<ListController>().girlsInvincibility = true;
        thisGirl.GetComponent<GirlStats>().mana = 0;
        thisGirl.GetComponent<GirlStats>().SetGirlStatsText();
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

    IEnumerator BasicEffect()
    {
        bEffect.transform.position = target.transform.position;
        bEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        bEffect.SetActive(false);
    }
}
