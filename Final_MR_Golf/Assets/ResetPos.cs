using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    // original position of the object
    public Vector3 originalPos;

    // original rotation of the object
    public Quaternion originalRot;
    
    // Start is called before the first frame update
    void Start()
    {
        // set the original position to the current position
        originalPos = transform.position;

        // set the original rotation to the current rotation
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}
