using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabClub : MonoBehaviour
{
    public GameObject Hand;
    public GameObject CenterEye;

    public bool state;
    
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {

        state = true;
        CenterEye = GameObject.Find("CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        // if OVRinput 'A' is pressed, set the state to true
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            state = true;
        }

        if (state)
        {
            // set the position of the obj to the position of the hand
            transform.position = Hand.transform.position;

            // set the rotation of the obj to the rotation of the hand
            transform.rotation = Hand.transform.rotation;

        }

        // canvas.transform.GetChild(0).LookAt(Camera.transform);
        canvas.transform.position = Hand.transform.position;
        canvas.transform.rotation = Hand.transform.rotation;
    }
}
