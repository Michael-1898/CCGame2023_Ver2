using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToucanCP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Toucan") == 1) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Health>().currentHealth <= 0) {
            PlayerPrefs.SetInt("Toucan", 1);
        }
    }
}
