using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject oculusCamera;

    // Start is called before the first frame update
    void Start()
    {
        // change player position to oculus camera position
        transform.position = oculusCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // change player position to oculus camera position
        transform.position = oculusCamera.transform.position;
    }
}
