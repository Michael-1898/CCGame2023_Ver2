using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("robot") == 1) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Health>().currentHealth <= 0) {
            PlayerPrefs.SetInt("toucan", 1);
        }
    }
}
