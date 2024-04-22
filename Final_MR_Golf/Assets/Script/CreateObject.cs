using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject Target;
    public Vector3 TargetPosition;
    public Vector3 TargetRotation;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Target);
        Target.transform.position = TargetPosition;
        Target.transform.rotation = Quaternion.Euler(TargetRotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createObject()
    {
        Instantiate(Target);
        Target.transform.position = TargetPosition;
        Target.transform.rotation = Quaternion.Euler(TargetRotation);
    }
}
