using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dinosaur : MonoBehaviour
{

    [SerializeField] public Vector3 position = Vector3.zero;
    [SerializeField] public GameObject target;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // TO_DO: Find the nearest prey as predator and target
        // TO_DO: Find the nearest pred as prey and !target (run from target)
        //      Do this using tags already applied
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
