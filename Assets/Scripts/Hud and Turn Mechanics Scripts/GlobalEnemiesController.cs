using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnemiesController : MonoBehaviour
{
    public int currentLevel;
    

    public GameObject e1;
    public GameObject e2;
    public GameObject e3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckEnemy(string enemyToCheck)
    {
        if (currentLevel == 1)
        {
            switch (enemyToCheck)
            {
                case "e1":
                    //Debug.Log("e1 turn");
                    e1.GetComponent<Enemy1Attack>().CheckEnemySkillPoints();
                    break;
                case "e2":
                    //Debug.Log("e2 turn");
                    e2.GetComponent<Enemy2Attack>().CheckEnemySkillPoints();
                    break;
                case "e3":
                    //Debug.Log("e3 turn");
                    e3.GetComponent<Enemy3Attack>().CheckEnemySkillPoints();
                    break;
            }
        }
        


    }

}
