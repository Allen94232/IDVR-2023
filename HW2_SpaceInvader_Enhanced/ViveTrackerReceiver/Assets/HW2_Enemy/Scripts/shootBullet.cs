using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
    // Start is called before the first frame update

    // Game Manager
    GameManager gm;

    GameObject player;

    // bullet prefab
    public GameObject bulletPrefab;
    
    // for cannon
    public GameObject cannon;   
    public GameObject rotater;  // y-axis

    public AudioClip shootAudio;

    // Bullet velocity
    public float bulletSpeed = 10;

    // for shooting
    // accumulated time
    float accTime = 0;
    public float shootPeriod = 2f;

    void Start()
    {
        // game manager
        gm = GameObject.FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player Body");
    }

    // Update is called once per frame
    void Update()
    {
        // the rotater need to track the position of the player
        rotater.transform.LookAt(player.transform.position);

        if (gm.isPlaying()){
            accTime += Time.deltaTime;

            if (accTime > shootPeriod )
            {
                // reset accumulated time
                accTime = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab);

        // pass the game manager
        newBullet.GetComponent<BulletController>().gm = gm;

        // position will be that of enemy
        newBullet.transform.position = cannon.transform.position;

        // get rigid body
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

        // get the direction to the player
        Vector3 direction = (player.transform.position - cannon.transform.position).normalized;

        // let the bullet face to the player position
        newBullet.transform.LookAt(player.transform.position);
        bulletRb.velocity = direction * bulletSpeed;

        // shoot sound
        AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.4f);
    }
}
