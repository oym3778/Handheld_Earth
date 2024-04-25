using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] SerialCommunication sc;

    private Vector3 initialEuler; 
    private bool testedInitial = false;

    // holds min(x) and max(y) for individual clamps
    [SerializeField] Vector2 xClamp;
    [SerializeField] Vector2 yClamp;

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

        // when y goes above +_75 degrees it flips the camera because x follows it.
        // clamping to prevent that from happening
        float y = Mathf.Clamp(sc.GetRotEuler().y, yClamp.x, yClamp.y);
        float x = Mathf.Clamp(sc.GetRotEuler().x, xClamp.x, xClamp.y);
        this.transform.localEulerAngles = new Vector3(x, y, 0f);
        //this.transform.localEulerAngles = sc.GetRotEuler();
    }
}
