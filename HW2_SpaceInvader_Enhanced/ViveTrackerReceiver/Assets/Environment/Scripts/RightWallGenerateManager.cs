using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallGenerateManager : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject wallParent; // New GameObject to hold the walls
    public bool rightWallGeneratingStatus;
    public bool rightHandTriggerStatus;

    void Start()
    {
        rightWallGeneratingStatus = false;
        rightHandTriggerStatus = false;
    }

    void Update()
    {
        RightStartGenerateWall();
    }

    public void RightStartGenerateWall()
    {
        if(rightWallGeneratingStatus)
        {
            return;
        }

        if(!rightHandTriggerStatus)
        {
            return;
        }

        rightWallGeneratingStatus = true;

        wallParent = new GameObject("RightWallParent");
        Vector3 wallPosition = GameObject.Find("RightHandAnchor").transform.position;
        GameObject newWall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);
        newWall.transform.SetParent(wallParent.transform); // Set the wall as a child of WallParent

        rightHandTriggerStatus = false;
    }
}
