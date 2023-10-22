using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabDetect : MonoBehaviour
{
    // check whether hand is grabbing object
    private OVRGrabbable ovrGrab;
    private bool isGrabGun = false;

    private void Start()
    {
        ovrGrab = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        if (ovrGrab.isGrabbed) isGrabGun = true;
        else isGrabGun = false;
            
    }
    
    public bool isCurrentGrabGun()
    {
        return isGrabGun;
    }
}
