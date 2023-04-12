using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCPAdd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("right") == 1) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
