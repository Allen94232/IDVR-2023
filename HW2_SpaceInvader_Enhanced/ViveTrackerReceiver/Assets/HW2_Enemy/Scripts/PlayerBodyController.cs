using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    OVRCameraRig oVRCameraRig;

    GameManager gm;

    public float MaxHP;

    public AudioSource getHittedAudio;
    float curHP;

    public PlayerBarController healthbar;
    // Start is called before the first frame update
    void Start()
    {
        oVRCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        curHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraPos = oVRCameraRig.centerEyeAnchor.position; 
        transform.position = new Vector3(CameraPos.x, CameraPos.y - 0.5f, CameraPos.z);

        healthbar.UpdateHealth(curHP / MaxHP);
    }

    public void PlayerAttacked(){
        
        if (gm.isPlaying()) curHP--;

        getHittedAudio.Play();

        if (curHP <= 0){
            gm.GameOver();
        }
    }

    public void Restart(){
        curHP = MaxHP;
    }
}
