using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Bullet velocity

    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {

        // GunActionManager();
    }

    // void OnFire()
    // {
    //     if (Time.time - lastShotTime >= fireCoolDown)
    //     {
    //         //play ShootAudio
    //         ShootAudio.Play();

    //         //haptic feedback
    //         OVRInput.SetControllerVibration(0.5f, 1f, OVRInput.Controller.RTouch);

    //         // spawn a new bullet
    //         GameObject newBullet = Instantiate(bullet);

    //         // pass the game manager
    //         newBullet.GetComponent<BulletController>().gm = gm;

    //         // position will be that of the gun
    //         newBullet.transform.position = ShootOutput.transform.position;

    //         // get rigid body
    //         Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

    //         // let the bullet face to the forward when shoot

    //         if (gun_3.GetComponent<ObjectGrabDetect>().isCurrentGrabGun())
    //         {
    //             newBullet.transform.LookAt(ShootOutput.transform.forward *180f);
    //             newBullet.transform.rotation *= Quaternion.Euler(90f, 0f, 0f);
    //         }
    //         else
    //         {
    //             newBullet.transform.LookAt(ShootOutput.transform.forward * 90f);
    //         }

    //         // give the bullet velocity
    //         bulletRb.velocity = ShootOutput.transform.forward * bulletSpeed;

    //         // Update the last shot time to the current time.
    //         lastShotTime = Time.time;
    //     }
    // }

    // void GunActionManager()
    // {
    //     // shoot gun
    //     if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
    //     {
    //         if (gun != null)
    //         {
    //             OnFire();
    //         }
    //     }
    // }


}
