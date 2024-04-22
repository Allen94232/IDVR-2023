using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallGenerateController : MonoBehaviour
{
    public GameObject wallPrefab;
    public float timeToEndGenerating = 1.0f;

    public float timeToDestroy = 5.0f;
    public int maxWallNumber = 15;

    public ParticleSystem destroyEffect;
    public AudioSource putAudio;

    public AudioSource destroyAudio;

    private float wallExistTimeCount = 0.0f;
    private int wallNumber;
    private Vector3 wallPosition;
    private bool leftGenerating;
    private bool IsLastCube;

    void Start()
    {
        wallNumber = transform.parent.childCount;
        wallPosition = transform.position;
        leftGenerating = GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().leftWallGeneratingStatus;
        IsLastCube = false;
    }

    void Update()
    {
        wallExistTimeCount += Time.deltaTime;
        GenerateWall();
        if (IsLastCube && wallExistTimeCount > timeToDestroy)
        {
            foreach (Transform child in transform.parent)
            {
                destroyEffect.transform.position = child.position;
                destroyEffect.Play();
            }
            Destroy(transform.parent.gameObject);
            destroyAudio.Play();Destroy(transform.parent.gameObject);
        }
    }

    public void GenerateWall()
    {
        if (!leftGenerating)
        {
            return;
        }
        
        if (wallNumber >= maxWallNumber || wallExistTimeCount >= timeToEndGenerating)
        {
            leftGenerating = false;
            GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().leftWallGeneratingStatus = false;
            IsLastCube = true;
            return;
        }

        //get the position of right hand
        Vector3 leftHandPosition = GameObject.Find("LeftHandAnchor").transform.position;

        //if the position of right hand is 20cm away from the wall, then generate a new wall
        if (Vector3.Distance(leftHandPosition, wallPosition) < 0.2f)
        {
            return;
        }

        //generate a new wall from at wallPosition + Offset where Offset is the right hand position - wall position
        Vector3 Offset = leftHandPosition - wallPosition;
        GameObject newWall = Instantiate(wallPrefab, wallPosition + Offset, Quaternion.identity);
        newWall.transform.SetParent(transform.parent.transform); // Set the wall as a child of WallParent
        putAudio.Play();

        leftGenerating = false;
    }
}
