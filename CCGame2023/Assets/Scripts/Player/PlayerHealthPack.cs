using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthPack : MonoBehaviour
{
    //variables for health
    [SerializeField] int maxPacks;
    public int currentPacks;

    //variables for health display
    [SerializeField] Image[] packs;
    [SerializeField] Sprite pack;

    // Start is called before the first frame update
    void Start()
    {
        currentPacks = 0;
        packs[0] = GameObject.Find("Canvas").transform.GetChild(1).GetChild(0).GetComponent<Image>();
        packs[1] = GameObject.Find("Canvas").transform.GetChild(1).GetChild(1).GetComponent<Image>();
        packs[2] = GameObject.Find("Canvas").transform.GetChild(1).GetChild(2).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < packs.Length; i++) {
            if(i < currentPacks) {
                packs[i].enabled = true;
            } else {
                packs[i].enabled = false;
            }

            // if(i < maxPacks) {
            //     packs[i].enabled = true;
            // } else {
            //     packs[i].enabled = false;
            // }
        }
    }

    public void addPack(int numPacks) {
        currentPacks += numPacks;
    }
}
