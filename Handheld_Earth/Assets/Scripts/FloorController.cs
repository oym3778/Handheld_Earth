using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] SerialCommunication sc;
    [SerializeField] ControlState controlState;

    [Header("Arduino")]

    // holds min(x) and max(y) for individual clamps
    [SerializeField] Vector2 xClamp;
    [SerializeField] Vector2 yClamp;
    [SerializeField] Vector2 zClamp;

    [Header("Mouse")]
    [SerializeField] float roationSpeed;

    private Vector3 mousePos;
    private bool isRotating;


    private enum ControlState
    {
        ARDUINO,
        MOUSE
    }


    private void Update()
    {
        switch(controlState)
        {
            case ControlState.ARDUINO:
                SerialRotation();
                break;
            case ControlState.MOUSE:
                MouseRotation();
                break;
        }
    }

    private void SerialRotation()
    {
        // Don't continue untill sc has read at least once 
        if (!sc.GetHasFirstStreamRead())
            return;

        // when y goes above +_75 degrees it flips the camera because x follows it.
        // clamping to prevent that from happening
        float y = Mathf.Clamp(sc.GetRotEuler().y, yClamp.x, yClamp.y);
        float x = Mathf.Clamp(sc.GetRotEuler().x, xClamp.x, xClamp.y);
        //float z = Mathf.Clamp(sc.GetRotEuler().z, zClamp.x, zClamp.y);

        //z = Mathf.Abs(z);
        
        this.transform.localEulerAngles = new Vector3(y, x, 0f);
        //this.transform.localEulerAngles = sc.GetRotEuler();
    }

    private void MouseRotation()
    {
        if(Input.GetMouseButton(0))
        {
            RotWithMouse(true); // Left hold horizontal 
        }
        else if(Input.GetMouseButton(1))
        {
            RotWithMouse(false);// Right hold vertical 
        }

    }

    private void RotWithMouse(bool isVert)
    {
        // Whether to roate or not now 
        if (Input.GetMouseButtonDown(isVert ? 0 : 1))
        {
            isRotating = true;
            mousePos = Input.mousePosition; // Initial pos 
        }
        else if (Input.GetMouseButtonUp(isVert ? 0 : 1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Difference from hold to current 
            Vector3 offset = (Input.mousePosition - mousePos);
            Vector3 rot = Vector3.zero;

            if(isVert)
            {
                rot.y = -(offset.x) * roationSpeed;
            }
            else
            {
                rot.x = -(offset.y) * roationSpeed;
            }

            transform.Rotate(rot);
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0.0f);
        }


        // Update position 
        mousePos = Input.mousePosition;
    }
}
