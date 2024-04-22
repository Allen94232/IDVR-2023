using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // objects to spawn
    public GameObject[] StonePrefab;
    public GameObject[] GrassPrefab;
    public GameObject[] TreePrefab;
    public GameObject[] PondPrefab;

    // objects to avoid
    public GameObject[] floor;
    public GameObject[] tables;
    public GameObject[] ponds;

    public int GrassNum;
    public int TreeNum;
    public int PondNum;

    public bool objectsCreated_1;
    public bool objectsCreated_2;
    public bool objectsCreated_3;
    public bool objectsCreated_4;

    // Start is called before the first frame update
    void Start()
    {
        objectsCreated_1 = false;
        objectsCreated_2 = false;
        objectsCreated_3 = false;
        objectsCreated_4 = false;
    }

    // Update is called once per frame
    void Update()
    {
        floor = GameObject.FindGameObjectsWithTag("Floor");
        tables = GameObject.FindGameObjectsWithTag("Table");

        //Debug.Log(objectsCreated);
        if (!objectsCreated_1 && Time.time > 1.5f)
        {
            CreateStone();
            objectsCreated_1 = true;
        }
        if (!objectsCreated_2 && Time.time > 2f)
        {
            CreatePond();
            objectsCreated_2 = true;
        }
        if (!objectsCreated_3 && Time.time > 2.5f)
        {
            CreateTree();
            objectsCreated_3 = true;
        }
        if (!objectsCreated_4 && Time.time > 3f)
        {
            CreateGrass();
            objectsCreated_4 = true;
        }
    }

    void CreateStone()
    {
        // change table to stone
        for (int i = 0; i < tables.Length; i++)
        {
            Collider TableCollider = tables[i].GetComponent<Collider>();
            Vector3 table_center = TableCollider.bounds.center;
            Vector3 table_size = TableCollider.bounds.size;

            // random pick a stone prefab
            int stoneIdx = Random.Range(0, StonePrefab.Length);

            GameObject newStone = Instantiate(StonePrefab[stoneIdx], table_center, Quaternion.identity);
            newStone.transform.localScale = new Vector3(table_size.x, table_size.y*2, table_size.z);
        }
    }

    void CreatePond()
    {
        Collider floorCollider = floor[0].GetComponent<Collider>();

        Vector3 floor_center = floorCollider.bounds.center;
        Vector3 floor_size = floorCollider.bounds.extents;

        for (int i=0;i< PondNum; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y + 0.01f,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));
            // Debug.Log("spawnPosition: " + spawnPosition);
            for (int j = 0; j < tables.Length; j++)
            {
                Collider TableCollider = tables[j].GetComponent<Collider>();
                Vector3 table_center = TableCollider.bounds.center;
                Vector3 table_size = TableCollider.bounds.extents;

                float x_min = table_center.x - table_size.x;
                float x_max = table_center.x + table_size.x;
                float z_min = table_center.z - table_size.z;
                float z_max = table_center.z + table_size.z;

                while (spawnPosition.x < x_max && spawnPosition.x > x_min && spawnPosition.z < z_max && spawnPosition.z > z_min)
                {
                    // is in the range of table, reproduce the position
                    spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y+0.01f,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));
                }
                // Debug.Log("j = " + j);
            }

            // random pick a pond prefab
            int pondIdx = Random.Range(0, PondPrefab.Length);

            // randomly create a random scale pond 
            GameObject newPond = Instantiate(PondPrefab[pondIdx], spawnPosition, Quaternion.identity);
            newPond.transform.localScale = new Vector3(Random.Range(0.02f, 0.03f), Random.Range(0.02f, 0.03f), Random.Range(0.02f, 0.03f));
            // Debug.Log("newPond.transform.localScale: " + newPond.transform.localScale);
        }

        // find all the pond
        ponds = GameObject.FindGameObjectsWithTag("Pond");
    }

    void CreateTree()
    {
        Collider floorCollider = floor[0].GetComponent<Collider>();

        Vector3 floor_center = floorCollider.bounds.center;
        Vector3 floor_size = floorCollider.bounds.extents;

        // create tree
        for (int i = 0; i < TreeNum; i++)
        {
            Debug.Log("i = " + i);
            Vector3 spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));

            // avoid table
            for (int j = 0; j < tables.Length; j++)
            {
                Collider TableCollider = tables[j].GetComponent<Collider>();
                Vector3 table_center = TableCollider.bounds.center;
                Vector3 table_size = TableCollider.bounds.extents;

                float x_min = table_center.x - table_size.x;
                float x_max = table_center.x + table_size.x;
                float z_min = table_center.z - table_size.z;
                float z_max = table_center.z + table_size.z;

                while (spawnPosition.x < x_max && spawnPosition.x > x_min && spawnPosition.z < z_max && spawnPosition.z > z_min)
                {
                    // is in the range of table, reproduce the position
                    spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));
                }
            }

            // avoid pond
            for (int j = 0; j < ponds.Length; j++)
            {
                Collider PondCollider = ponds[j].GetComponent<Collider>();
                Vector3 pond_center = PondCollider.bounds.center;
                Vector3 pond_size = PondCollider.bounds.extents;

                float x_min = pond_center.x - pond_size.x;
                float x_max = pond_center.x + pond_size.x;
                float z_min = pond_center.z - pond_size.z;
                float z_max = pond_center.z + pond_size.z;

                while (spawnPosition.x < x_max && spawnPosition.x > x_min && spawnPosition.z < z_max && spawnPosition.z > z_min)
                {
                    // is in the range of table, reproduce the position
                    spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));
                }
            }

            // random pick a pond prefab
            int objectIdx = Random.Range(0, TreePrefab.Length);

            // randomly create a random scale tree
            GameObject newTree = Instantiate(TreePrefab[objectIdx], spawnPosition, Quaternion.identity);
            newTree.transform.localScale = new Vector3(Random.Range(0.8f, 1.0f), 0.8f, Random.Range(0.8f, 1.0f));
        }

    }

    void CreateGrass()
    {
        Collider floorCollider = floor[0].GetComponent<Collider>();

        Vector3 floor_center = floorCollider.bounds.center;
        Vector3 floor_size = floorCollider.bounds.extents;

        // create tree
        int i = 0;
        while (i < GrassNum)
        {
            Debug.Log("i = " + i);
            Vector3 spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));

            // avoid table
            for (int j = 0; j < tables.Length; j++)
            {
                Collider TableCollider = tables[j].GetComponent<Collider>();
                Vector3 table_center = TableCollider.bounds.center;
                Vector3 table_size = TableCollider.bounds.extents;

                float x_min = table_center.x - table_size.x;
                float x_max = table_center.x + table_size.x;
                float z_min = table_center.z - table_size.z;
                float z_max = table_center.z + table_size.z;

                while (spawnPosition.x < x_max && spawnPosition.x > x_min && spawnPosition.z < z_max && spawnPosition.z > z_min)
                {
                    // is in the range of table, reproduce the position
                    spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));
                }
            }

            // avoid pond
            for (int j = 0; j < ponds.Length; j++)
            {
                Collider PondCollider = ponds[j].GetComponent<Collider>();
                Vector3 pond_center = PondCollider.bounds.center;
                Vector3 pond_size = PondCollider.bounds.extents;

                float x_min = pond_center.x - pond_size.x;
                float x_max = pond_center.x + pond_size.x;
                float z_min = pond_center.z - pond_size.z;
                float z_max = pond_center.z + pond_size.z;

                while (spawnPosition.x < x_max && spawnPosition.x > x_min && spawnPosition.z < z_max && spawnPosition.z > z_min)
                {
                    // is in the range of table, reproduce the position
                    spawnPosition = new Vector3(Random.Range(floor_center.x - floor_size.x, floor_center.x + floor_size.x),
                                    floor_center.y + floor_size.y,
                                    Random.Range(floor_center.z - floor_size.z, floor_center.z + floor_size.z));
                }
            }

            // random pick a pond prefab
            int objectIdx = Random.Range(0, GrassPrefab.Length);

            // randomly create a random scale tree
            GameObject newTree = Instantiate(GrassPrefab[objectIdx], spawnPosition, Quaternion.identity);
            newTree.transform.localScale = new Vector3(Random.Range(0.8f, 1.0f), 0.8f, Random.Range(0.8f, 1.0f));

            i++;
        }

    }
}
