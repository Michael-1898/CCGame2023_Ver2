using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionArrow : MonoBehaviour
{
    // Start is called before the first frame update
    string winCondition;
    List<GameObject> enemiesList = new List<GameObject>();
    List<GameObject> gemList = new List<GameObject>();
    SpriteRenderer sr;
    bool turnedOn = false;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (Camera.main.GetComponent<LoadLevel>())
        {
            winCondition = Camera.main.GetComponent<LoadLevel>().winCondition;
            if (winCondition == "Kill All")
            {
                enemiesList = Camera.main.GetComponent<LoadLevel>().enemiesList;
            }
            if (winCondition == "Collect All")
            {
                gemList = Camera.main.GetComponent<LoadLevel>().gemList;
            }

        }
        else winCondition = "None";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || turnedOn)
        {
            sr.enabled = true;
        }
        else sr.enabled = false;
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            turnedOn = !turnedOn;
        }
        
        if(winCondition == "Kill All")
        {
            GameObject closestEnemy = null;
            float closestEnemyDistance = Mathf.Infinity;
            for(int i = 0; i < enemiesList.Count; i++)
            {
                if(enemiesList[i]==null)
                {
                    enemiesList.RemoveAt(i);
                    i--;
                }
                else
                {
                    if(Vector3.Distance(transform.position, enemiesList[i].transform.position) < closestEnemyDistance)
                    {
                        closestEnemyDistance = Vector3.Distance(transform.position, enemiesList[i].transform.position);
                        closestEnemy = enemiesList[i];
                    }
                    if(enemiesList[i].transform.position.y < -20f)
                    {
                        Destroy(enemiesList[i]);
                    }    
                }
                
            }
            if(enemiesList.Count == 0)
            {
                Victory();
            }
            Vector3 pointTowardsEnemy = closestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(pointTowardsEnemy.y, pointTowardsEnemy.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10000);
            print(closestEnemy);
            
            
        }
        if (winCondition == "Collect All")
        {
            GameObject closestGem = null;
            float closestGemDistance = Mathf.Infinity;
            for (int i = 0; i < gemList.Count; i++)
            {
                if (gemList[i] == null)
                {
                    gemList.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (Vector3.Distance(transform.position, gemList[i].transform.position) < closestGemDistance)
                    {
                        closestGemDistance = Vector3.Distance(transform.position, gemList[i].transform.position);
                        closestGem = gemList[i];
                    }
                    if (gemList[i].transform.position.y < -20f)
                    {
                        Destroy(gemList[i]);
                    }
                }

            }
            if (gemList.Count == 0)
            {
                Victory();
            }
            Vector3 pointTowardsEnemy = closestGem.transform.position - transform.position;
            float angle = Mathf.Atan2(pointTowardsEnemy.y, pointTowardsEnemy.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10000);
            print(closestGem);
        }
        if (winCondition == "Get To Exit")
        {
            print("Get To Exit");
        }
    }

    void Victory()
    {
        GameObject.Find("VictoryText").transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 4.5f, 0f);
        Destroy(gameObject);
        print("YOU WIN");
    }
}
