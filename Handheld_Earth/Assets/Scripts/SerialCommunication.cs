using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialCommunication : MonoBehaviour
{

    SerialPort stream = new SerialPort("COM3", 9600);
    int rButton = 0;
    int lButton = 0;

    // Start is called before the first frame update
    void Start()
    {
       stream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        string value = stream.ReadLine();
        Debug.Log(value);
    }
}