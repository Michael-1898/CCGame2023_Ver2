﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    //variables for kill count
    int currentTally;
    [SerializeField] int maxTally;
    [SerializeField] float healthChance;
    [SerializeField] GameObject healthPack;

    //variables for healing
    bool healing;
    float healTimer;
    [SerializeField] float healCooldown;
    [SerializeField] int maxHealth;
    [SerializeField] GameObject healthFX;

    // Start is called before the first frame update
    void Start()
    {
        currentTally = 0;
        healing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if presses 'c', restore health and lose a health pack
        if(Input.GetKeyDown("c") && !healing && GetComponent<PlayerHealthPack>().currentPacks > 0 && GetComponent<PlayerHealth>().currentHealth < maxHealth) {
            Instantiate(healthFX, new Vector2(transform.position.x, transform.position.y + 0.92f), Quaternion.identity);

            GetComponent<PlayerHealthPack>().subPack(1);
            if(GetComponent<PlayerHealth>().enabled == true) {
                GetComponent<PlayerHealth>().currentHealth = maxHealth;
            } else {
                GetComponent<PlyrHpMod>().currentHealth = maxHealth;
                PlayerPrefs.SetInt("currentHealth", maxHealth);
            }
            
            healing = true;
        }

        if(healing) {
            healTimer += Time.deltaTime;
        }
        if(healTimer >= healCooldown) {
            healing = false;
            healTimer = 0;
        }
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
