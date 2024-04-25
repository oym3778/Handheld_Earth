using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialCommunication : MonoBehaviour
{
    [Tooltip("How many inputs should this hold to average between for positions")]
    [SerializeField] int inputCount;
    [SerializeField] string portName = "COM3";

    // Set the port amd the baud rate as indicated by the arduino
    SerialPort stream;

    // Variables to be used once received data is split into an array
    int rButton = 0;
    int lButton = 0;

    Vector3 rotEuler;

    // Turns true when we read for the first time 
    private bool firstStreamRead = false;
    private Queue<Vector3> inputQueue;

    // Start is called before the first frame update
    void Start()
    {
        stream = new SerialPort(portName, 9600);

        // Open the serial stream
        stream.Open();

       inputQueue = new Queue<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        // Roll, Pitch, Yaw, Lbutton, Rbutton
        string value = stream.ReadLine();
        string[] splitArray = value.Split(",");

        print(splitArray[0] + ", " + splitArray[1] + ", " + splitArray[2]);
        //Debug.Log(splitArray[3] + " " + splitArray[4]);

        Vector3 inputEuler = new Vector3(
            float.Parse(splitArray[0]),
            float.Parse(splitArray[1]),
            float.Parse(splitArray[2]));

        if(inputQueue.Count >= inputCount)
        {
            inputQueue.Dequeue();
        }

        inputQueue.Enqueue(inputEuler);

        Vector3 sum = new Vector3();
        foreach(Vector3 v in inputQueue)
        {
            sum += v;
        }
        rotEuler = sum / (float)inputQueue.Count;

        lButton = Int32.Parse(splitArray[3]);
        rButton = Int32.Parse(splitArray[4]);

        firstStreamRead = true;
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

    public bool GetHasFirstStreamRead()
    {
        return firstStreamRead;
    }

}