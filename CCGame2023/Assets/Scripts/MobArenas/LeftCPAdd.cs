﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCPAdd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("left") == 1) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
