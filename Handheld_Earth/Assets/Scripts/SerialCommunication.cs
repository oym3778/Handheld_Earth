using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialCommunication : MonoBehaviour
{
    // Set the port amd the baud rate as indicated by the arduino
    SerialPort stream = new SerialPort("COM3", 9600);

    // Variables to be used once received data is split into an array
    int rButton = 0;
    int lButton = 0;

    Vector3 rotEuler;

    // Start is called before the first frame update
    void Start()
    {
        // Open the serial stream
       stream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        // Roll, Pitch, Yaw, Lbutton, Rbutton
        string value = stream.ReadLine();
        string[] splitArray = value.Split(",");

        Debug.Log(splitArray[3] + " " + splitArray[4]);

        rotEuler = new Vector3(
            float.Parse(splitArray[0]),
            float.Parse(splitArray[1]),
            float.Parse(splitArray[2]));

        lButton = Int32.Parse(splitArray[3]);
        rButton = Int32.Parse(splitArray[4]);
    }

    public int GetRButton()
    {
        return rButton;
    }

    public int GetLButton()
    {
        return lButton;
    }

    public Vector3 GetRotEuler()
    {
        return rotEuler;
    }
}