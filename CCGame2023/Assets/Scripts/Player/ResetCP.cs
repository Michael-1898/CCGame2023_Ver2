﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("toucan", 0);
        PlayerPrefs.SetInt("robot", 0);
        PlayerPrefs.SetInt("left", 0);
        PlayerPrefs.SetInt("right", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
