using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    int currentTally;
    [SerializeField] int maxTally;
    [SerializeField] float healthChance;
    [SerializeField] GameObject healthPack;

    // Start is called before the first frame update
    void Start()
    {
        currentTally = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if presses 'c', restore health and lose a health pack
    }

    public void addTally(Vector3 pos) {
        currentTally++;

        if(currentTally >= maxTally) {
            //drop healthpack
            Instantiate(healthPack, pos, Quaternion.identity);

            currentTally = 0;
        } else {
            int random = Random.Range(0, 101);
            if(random <= healthChance) {
                //drop healthpack
                Instantiate(healthPack, pos, Quaternion.identity);

                currentTally = 0;
            }
        }
    }
}
