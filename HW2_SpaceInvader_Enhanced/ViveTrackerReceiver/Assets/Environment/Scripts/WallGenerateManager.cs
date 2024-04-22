using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerateManager : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject wallParent; // New GameObject to hold the walls
    public bool leftWallGeneratingStatus;
    public bool leftHandTriggerStatus;
    public bool rightWallGeneratingStatus;
    public bool rightHandTriggerStatus;

    public AudioSource putAudio;

    void Start()
    {
        leftWallGeneratingStatus = false;
        leftHandTriggerStatus = false;
        rightWallGeneratingStatus = false;
        rightHandTriggerStatus = false;
    }

    void Update()
    {
        LeftStartGenerateWall();
        RightStartGenerateWall();
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
        putAudio.Play();
        newWall.transform.SetParent(wallParent.transform); // Set the wall as a child of WallParent
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
        putAudio.Play();
        newWall.transform.SetParent(wallParent.transform); // Set the wall as a child of WallParent
    }
}