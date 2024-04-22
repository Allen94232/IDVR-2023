using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallGenerateManager : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject wallParent; // New GameObject to hold the walls
    public bool leftWallGeneratingStatus;
    public bool leftHandTriggerStatus;

    void Start()
    {
        leftWallGeneratingStatus = false;
        leftHandTriggerStatus = false;
    }

    void Update()
    {
        LeftStartGenerateWall();
    }
    public void LeftStartGenerateWall()
    {
        if (leftWallGeneratingStatus)
        {
            return;
        }

        if (!leftHandTriggerStatus)
        {
            return;
        }

        leftWallGeneratingStatus = true;

        wallParent = new GameObject("LeftWallParent");
        Vector3 wallPosition = GameObject.Find("LeftHandAnchor").transform.position;
        GameObject newWall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);
        newWall.transform.SetParent(wallParent.transform); // Set the wall as a child of WallParent

        leftHandTriggerStatus = false;
    }
}
