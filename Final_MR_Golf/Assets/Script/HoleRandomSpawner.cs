using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Oculus.Interaction;
using UnityEngine;

public class HoleRandomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject holePrefab;
    public GameObject[] floor;
    public GameObject[] tables;

    Vector3 spawnPosition;

    public bool isCreated;

    public float floor_offset;

    void Start()
    {
        // floor = GameObject.FindGameObjectsWithTag("Floor");
        // tables = GameObject.FindGameObjectsWithTag("Table");

        // Debug.Log(floor.Length);

        // randomCreatePos();
        // Instantiate(holePrefab, spawnPosition, Quaternion.identity);
        isCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        floor = GameObject.FindGameObjectsWithTag("Floor");
        tables = GameObject.FindGameObjectsWithTag("Table");
        
        // Debug.Log(floor.Length);
        // Debug.Log(tables.Length);

        if (!isCreated && Time.time > 1.0f)
        {
            randomCreatePos();
            Instantiate(holePrefab, spawnPosition, Quaternion.identity);
            isCreated = true;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            randomCreatePos();
            Instantiate(holePrefab, spawnPosition, Quaternion.identity);
        }
    }

    void randomCreatePos()
    {
        Collider floorCollider = floor[0].GetComponent<Collider>();
        
        Vector3 floor_center = floorCollider.bounds.center;
        Vector3 floor_size = floorCollider.bounds.extents;

        spawnPosition = new Vector3(Random.Range(floor_center.x-floor_size.x + 0.1f, floor_center.x+floor_size.x - 0.1f), 
                                    floor_center.y+floor_size.y, 
                                    Random.Range(floor_center.z - floor_size.z + 0.1f, floor_center.z + floor_size.z - 0.1f));

        Debug.Log("spawnPosition: " + spawnPosition);
        Debug.Log("floor_center - floor_size: " + (floor_center - floor_size));
        Debug.Log("floor_center + floor_size: " + (floor_center + floor_size));

        for (int i=0; i < tables.Length; i++)
        {
            Collider TableCollider = tables[i].GetComponent<Collider>();
            Vector3 table_center = TableCollider.bounds.center;
            Vector3 table_size = TableCollider.bounds.extents;

            float x_min = table_center.x - table_size.x;
            float x_max = table_center.x + table_size.x;
            float z_min = table_center.z - table_size.z;
            float z_max = table_center.z + table_size.z;

            while (spawnPosition.x < x_max && spawnPosition.x > x_min && spawnPosition.z < z_max && spawnPosition.z > z_min)
            {
                // is in the range of table
                // spawnPosition.y = table_center.y+table_size.y;
                spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x + 0.1f, floor_center.x + floor_size.x - 0.1f),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z + 0.1f, floor_center.z + floor_size.z - 0.1f));
                Debug.Log("in table range");
            }
        }

        Debug.Log(spawnPosition);
    }
}
