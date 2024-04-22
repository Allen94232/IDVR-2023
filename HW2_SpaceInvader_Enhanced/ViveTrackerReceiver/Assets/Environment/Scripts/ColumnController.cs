using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public GameObject tracker;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3 (0.0f, -0.4f, 0.0f);
        transform.position = tracker.transform.position + offset;
    }
}
