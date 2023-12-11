using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public GameObject currentEnemySelected;
    public GameObject startingSelectedEnemy;
    public GameObject SelectedEnemyIcon;


    public GameObject currentGirlSelected;
    public GameObject startingSelectedGirl;
    public GameObject SelectedGirlIcon;



    void Start()
    {
        currentEnemySelected = startingSelectedEnemy;
        currentGirlSelected = startingSelectedGirl;
    }

    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                if (targetObject.tag == "Enemy") //COLOCAR A TAG DOS OBJETOS QUE PODEM SER ARRASTADOS
                {
                    CheckWichEnemy(targetObject.gameObject);
                }

                if (targetObject.tag == "Girl")
                {
                    CheckWichGirl(targetObject.gameObject);
                }
            }
        }

    }

    public void CheckWichEnemy(GameObject enemyToCheck)
    {
        currentEnemySelected = enemyToCheck;
        if (enemyToCheck.name == "e1")
        {
            SelectEnemy1();
        }
        else if (enemyToCheck.name == "e2")
        {
            SelectEnemy2();
        }
        else
        {
            SelectEnemy3();
        }
    }

    public void CheckWichGirl(GameObject girlToCheck)
    {
        currentGirlSelected = girlToCheck;
        if (girlToCheck.name == "g1")
        {
            SelectGirl1();
        }
        else if (girlToCheck.name == "g2")
        {
            SelectGirl2();
        }
        else
        {
            SelectGirl3();
        }
    }



    public void SelectEnemy1()
    {
        SelectedEnemyIcon.transform.position = new Vector2(5.5f, 1.7f);
    }

    public void SelectEnemy2()
    {
        SelectedEnemyIcon.transform.position = new Vector2(3.488f, 1.25f);
    }

    public void SelectEnemy3()
    {
        SelectedEnemyIcon.transform.position = new Vector2(7.47f, 0.64f);
    }

    public void SelectGirl1()
    {
        SelectedGirlIcon.transform.position = new Vector2(-5.6f, -1.06f);
    }
    public void SelectGirl2()
    {
        SelectedGirlIcon.transform.position = new Vector2(-5.6f, -2.57f);
    }
    public void SelectGirl3()
    {
        SelectedGirlIcon.transform.position = new Vector2(-5.6f, -4.11f);
    }


    public void DisableEnemyIcon()
    {
        SelectedEnemyIcon.SetActive(false);
    }
    public void DisableGirlIcon()
    {
        SelectedGirlIcon.SetActive(false);
    }


}
