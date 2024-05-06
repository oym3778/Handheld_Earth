using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoManager : MonoBehaviour
{
    private SerialCommunication serial;

    // based on the prefabs set in the inspector
    public GameObject prey;
    public GameObject predator;

    // used to keep track of all dinos, used for deleting purposes
    public List<GameObject> dinos;
    public GameObject[] preyList;
    public GameObject[] predList;

    // Will be used to set the target of the dinos
    public int currentIndxPred;
    public int currentIndxPrey;

    // current new instance
    GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        serial = GameObject.FindAnyObjectByType<SerialCommunication>();
        currentIndxPred = 0;
        currentIndxPrey = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a T-Rex Predator
        if (serial.GetRButton() == 1)
        {
            instance = Instantiate(predator, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            preyList = GameObject.FindGameObjectsWithTag("Prey");
            instance.GetComponent<Dinosaur>().target = preyList[currentIndxPrey];
            dinos.Add(instance);

            currentIndxPrey = preyList.Length - 1;
            Debug.Log("Right button clicked");
        }

        // Creates a Stego Prey
        else if (serial.GetLButton() == 1)
        {
            instance = Instantiate(prey, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            predList = GameObject.FindGameObjectsWithTag("Predator");
            instance.GetComponent<Dinosaur>().target = predList[currentIndxPred];
            dinos.Add(instance);

            currentIndxPred = predList.Length - 1;
            Debug.Log("Left button clicked");
        }
    }

    static GameObject FindClosestDino(GameObject currentDino, List<GameObject> givenList)
    {
        if (givenList.Count == 0)
        {
            return currentDino;
        }

        float distance = Vector3.Distance(currentDino.GetComponent<Dinosaur>().position, givenList[0].GetComponent<Dinosaur>().position);
        GameObject closestDino = currentDino;
        for (int i = 0; i < givenList.Count; i++)
        {
            float tempDist = Vector3.Distance(currentDino.GetComponent<Dinosaur>().position, givenList[i].GetComponent<Dinosaur>().position);
            if (tempDist < distance)
            {
                distance = tempDist;
                closestDino = givenList[i];
            }
        }

        return closestDino;
    }
}
