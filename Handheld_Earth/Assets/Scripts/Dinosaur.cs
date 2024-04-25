using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dinosaur : MonoBehaviour
{

    [SerializeField] public Vector3 position = Vector3.zero;
    [SerializeField] public GameObject target;

    private SerialCommunication serial;
    public NavMeshAgent agent;

    // use later to track all dinosaurs and have each predator follow closest prey
    //List<int> numbers = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //serial.GetRButton();
        //serial.GetLButton();
        position = transform.position;

        serial = GameObject.FindAnyObjectByType<SerialCommunication>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.name == "Prey")
        {
            // Go the opposite direction of the predator
            agent.SetDestination(target.transform.position * -1);
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
        

    }
}
