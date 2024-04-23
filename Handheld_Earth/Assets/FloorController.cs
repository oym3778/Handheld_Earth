using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] SerialCommunication sc;

    private Vector3 initialEuler; 
    private bool testedInitial = false; 

    private void Update()
    {
        // Don't continue untill sc has read at least once 
        if (!sc.GetHasFirstStreamRead())
            return;

        // NOTE: The initial input might be an odd value so 
        //       we may need to callibrate our values. This
        //       still needs to be tested though 

        if(!testedInitial)
        {
            initialEuler = sc.GetRotEuler();
            testedInitial = true;
            return;
        }


        this.transform.localEulerAngles = sc.GetRotEuler() - initialEuler;
    }
}
