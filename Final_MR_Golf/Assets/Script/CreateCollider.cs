using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCollider : MonoBehaviour
{
    public GameObject ActionTriggerPrefab;
    public GameObject ButtonPrefab;
    public GameObject MenuPrefab;
    public int TriggerCount;
    public int ButtonCount;
    public Vector3[] TriggerPosition;
    public Vector3[] TriggerRotation;
    public Vector3[] ButtonPosition;
    public Vector3[] ButtonRotation;
    public Vector3[] MenuPosition;
    public Vector3[] MenuRotation;
    void Start()
    {
        for (int i = 0; i < TriggerCount; i++)
        {
            GameObject Trigger = Instantiate(ActionTriggerPrefab);
            Trigger.transform.parent = transform;
            Trigger.transform.position = TriggerPosition[i];
            Trigger.transform.rotation = Quaternion.Euler(TriggerRotation[i]);
            //Debug.Log(Trigger.transform.position);
        }
        for (int i = 0; i < ButtonCount; i++)
        {
            GameObject Button = Instantiate(ButtonPrefab);
            Button.transform.parent = transform;
            Button.transform.position = ButtonPosition[i];
            Button.transform.rotation = Quaternion.Euler(ButtonRotation[i]);
            //Debug.Log(Trigger.transform.position);
        }
        for (int i = 0; i < ButtonCount; i++)
        {
            GameObject Menu = Instantiate(MenuPrefab);
            Menu.transform.parent = transform;
            Menu.transform.position = MenuPosition[i];
            Menu.transform.rotation = Quaternion.Euler(MenuRotation[i]);
            //Debug.Log(Trigger.transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
