using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //check all player prefs
        if(PlayerPrefs.GetInt("toucan") == 1 && PlayerPrefs.GetInt("robot") == 1 && PlayerPrefs.GetInt("left") == 1 && PlayerPrefs.GetInt("right") == 1) {
            GameObject.Find("WinContainer").transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("toucan") != 1 || PlayerPrefs.GetInt("robot") != 1 || PlayerPrefs.GetInt("left") != 1 || PlayerPrefs.GetInt("right") != 1 && GameObject.Find("WinOverlay").active) {
            GameObject.Find("WinContainer").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
