﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    private int _choice;
    public Transform option1;
    public Transform option2;
    
    // Start is called before the first frame update
    void Start()
    {
        _choice = 1;
        transform.position = option1.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _choice = 1;
            transform.position = option1.position;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            _choice = 2;
            transform.position = option2.position;
        }

        if (_choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}