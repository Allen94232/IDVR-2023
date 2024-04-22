using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPassthrough : MonoBehaviour
{
    private KeyCode OpenOrClosePassthroughKey = KeyCode.W;
    private OVRManager PassthroughControl;
    [SerializeField] private Material settingSkybox;
    // Start is called before the first frame update
    void Start()
    {
        PassthroughControl = GameObject.Find("OVRCameraRig").GetComponent<OVRManager>();
        ControlPassthroughEnable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(OpenOrClosePassthroughKey))
        {
            ControlPassthroughEnable();
        }
    }

    public void ControlPassthroughEnable()
    {
        PassthroughControl.isInsightPassthroughEnabled = !PassthroughControl.isInsightPassthroughEnabled;
        bool isClose = PassthroughControl.isInsightPassthroughEnabled;
        if (!isClose)
        {
            RenderSettings.skybox = settingSkybox;
        }
        else
        {
            RenderSettings.skybox = null;
        }
    }
}
