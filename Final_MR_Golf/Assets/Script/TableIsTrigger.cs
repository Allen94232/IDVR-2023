using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableIsTrigger : MonoBehaviour
{

    private void Start()
    {
        this.GetComponent<Renderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // This function is called when the GameObject with this script enters a trigger collider.
            // Check if the object that entered the trigger has a "Player" tag.
        Debug.Log("TriggerEnter: " + other.name);
        this.GetComponent<Renderer>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Renderer>().enabled = false;
    }
}
