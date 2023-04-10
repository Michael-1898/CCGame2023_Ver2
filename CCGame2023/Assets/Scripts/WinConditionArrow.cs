using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionArrow : MonoBehaviour
{
    // Start is called before the first frame update
    string winCondition;
    List<GameObject> enemiesList = new List<GameObject>();
    void Start()
    {
        if (Camera.main.GetComponent<LoadLevel>())
        {
            winCondition = Camera.main.GetComponent<LoadLevel>().winCondition;
            if (winCondition == "Kill All")
            {
                enemiesList = Camera.main.GetComponent<LoadLevel>().enemiesList;
            }
            if (winCondition == "Collect All")
            {
                print("Collect All");
            }
            if (winCondition == "Get To Exit")
            {
                print("Get To Exit");
            }

        }
        else winCondition = "None";
    }

    // Update is called once per frame
    void Update()
    {
        if(winCondition == "Kill All")
        {
            for(int i = 0; i < enemiesList.Count; i++)
            {
                if(enemiesList[i]==null)
                {
                    enemiesList.RemoveAt(i);
                    i--;
                }
                print(enemiesList[i]);
            }
            if(enemiesList.Count == 0)
            {
                Victory();
            }
            
        }
        if (winCondition == "Collect All")
        {
            print("Collect All");
        }
        if (winCondition == "Get To Exit")
        {
            print("Get To Exit");
        }
    }

    void Victory()
    {
        print("YOU WIN");
    }
}
