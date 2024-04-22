using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootDetector : MonoBehaviour
{
    public float cooldown;

    public AudioSource putAudio;
    private bool check_ankle_height;
    public GameObject DefenceWall;
    // Start is called before the first frame update
    void Start()
    {
        check_ankle_height = false;
        cooldown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0.0f)
        {
            Debug.Log(transform.position.y);
            if (transform.position.y > 0.3f) check_ankle_height = true;
            if (check_ankle_height && transform.position.y < 0.15f)
            {
                DefenceWall.transform.rotation = transform.rotation;
                DefenceWall.transform.position = transform.position;
                Instantiate(DefenceWall);
                putAudio.Play();
                check_ankle_height = false;
                cooldown = 2.0f;
            }
        }
        if(cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
