using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDestroy : MonoBehaviour
{
    public float existTime;
    public float destroyTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        existTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        existTime += Time.deltaTime;
        if(existTime >= destroyTime)
        {
            Destroy(transform.gameObject);
        }
    }
}
