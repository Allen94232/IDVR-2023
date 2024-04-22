using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Bullet velocity
    public float bulletSpeed = 0;

    // Gun shoot out position
    public GameObject ShootOutput;

    // gun prefabs
    public GameObject gun_1;
    public GameObject gun_2;
    public GameObject gun_3;

    // bullet prefabs
    public GameObject bullet_1;
    public GameObject bullet_2;
    public GameObject bullet_3;

    // bullet damage
    public float damage = 0;

    //audio
    public AudioSource ShootAudio_1;
    public AudioSource ShootAudio_2;
    public AudioSource ShootAudio_3;

    [Range(0.01f, 1f)]
    public float speedH = 1.0f;
    [Range(0.01f, 1f)]
    public float speedV = 1.0f;

    public GameObject gun = null;
    private GameObject bullet;
    private AudioSource ShootAudio;
    private float lastShotTime = 0.0f;
    public float fireCoolDown = 0.0f;

    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        gun = null;
        bullet = null;
        ShootAudio = null;
    }

    void Update()
    {
        if (gun_1.GetComponent<ObjectGrabDetect>().isCurrentGrabGun())
        {
            gun = gun_1;
            bullet = bullet_1;
            ShootAudio = ShootAudio_1;
            bulletSpeed = 20.0f;
            fireCoolDown = 0.2f;
            damage = 4;
        }

        else if (gun_2.GetComponent<ObjectGrabDetect>().isCurrentGrabGun())
        {
            gun = gun_2;
            bullet = bullet_2;
            ShootAudio = ShootAudio_2;
            bulletSpeed = 8.0f;
            fireCoolDown = 0.5f;
            damage = 10;
        }

        else if (gun_3.GetComponent<ObjectGrabDetect>().isCurrentGrabGun())
        {
            gun = gun_3;
            bullet = bullet_3;
            ShootAudio = ShootAudio_3;
            bulletSpeed = 50.0f;
            fireCoolDown = 0.3f;
            damage = 3;
        }

        else
        {
            gun = null;
            bullet = null;
            ShootAudio = null;
        }
        GunActionManager();
    }

    void OnFire()
    {
        if (Time.time - lastShotTime >= fireCoolDown)
        {
            //play ShootAudio
            ShootAudio.Play();

            //haptic feedback
            OVRInput.SetControllerVibration(0.5f, 1f, OVRInput.Controller.RTouch);

            // spawn a new bullet
            GameObject newBullet = Instantiate(bullet);

            // pass the game manager
            newBullet.GetComponent<BulletController>().gm = gm;

            // position will be that of the gun
            newBullet.transform.position = ShootOutput.transform.position;

            // get rigid body
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

            // let the bullet face to the forward when shoot

            if (gun_3.GetComponent<ObjectGrabDetect>().isCurrentGrabGun())
            {
                newBullet.transform.LookAt(ShootOutput.transform.forward *180f);
                newBullet.transform.rotation *= Quaternion.Euler(90f, 0f, 0f);
            }
            else
            {
                newBullet.transform.LookAt(ShootOutput.transform.forward * 90f);
            }

            // give the bullet velocity
            bulletRb.velocity = ShootOutput.transform.forward * bulletSpeed;

            // Update the last shot time to the current time.
            lastShotTime = Time.time;
        }
    }

    void GunActionManager()
    {
        // shoot gun
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (gun != null)
            {
                OnFire();
            }
        }
    }

    
}
