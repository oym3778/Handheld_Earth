using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoManager : MonoBehaviour
{

    //GameObject[] preys = GameObject.FindGameObjectsWithTag("Prey");
    //GameObject[] predators = GameObject.FindGameObjectsWithTag("Predator");

    private SerialCommunication serial;
    public GameObject prey;
    public GameObject predator;

    public List<GameObject> dinos;
    public List<GameObject> preyList;
    public List<GameObject> predList;
    GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        serial = GameObject.FindAnyObjectByType<SerialCommunication>();
    }

    // Update is called once per frame
    void Update()
    {
        if (serial.GetRButton() == 1)
        {
            instance = Instantiate(prey, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            
            //instance.tag = "Prey";
            instance.GetComponent<Dinosaur>().target = FindClosestDino(instance, predList);
            dinos.Add(instance);
            preyList.Add(instance);

            Debug.Log("Right button clicked");
        }
        else if (serial.GetLButton() == 1)
        {
            instance = Instantiate(predator, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            //instance.tag = "Predator";
            
            instance.GetComponent<Dinosaur>().target = FindClosestDino(instance, preyList);
            dinos.Add(instance);
            predList.Add(instance);
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
