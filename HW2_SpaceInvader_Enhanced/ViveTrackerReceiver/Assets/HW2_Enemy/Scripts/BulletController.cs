using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float maxDistance = 30;

    public GameManager gm;

    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // difference in all coordinate
        float diffX = Mathf.Abs(initPos.x - transform.position.x);
        float diffY = Mathf.Abs(initPos.y - transform.position.y);
        float diffZ = Mathf.Abs(initPos.z - transform.position.z);

        // destroy if it's too far away
        if (diffX >= maxDistance || diffY >= maxDistance || diffZ >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "cube" || other.gameObject.tag == "Ground" || other.gameObject.tag == "tombStone" || other.gameObject.tag == "tombMove")
        {

            Rigidbody bulletRb = this.GetComponent<Rigidbody>();

            Debug.Log("pong");
            Vector3 inDirection = (transform.position - initPos).normalized;
            Vector3 inNormal = other.contacts[0].normal;
            transform.forward = Vector3.Reflect(inDirection, inNormal);

            float bulletSpeed = GameObject.Find("tower_defence").GetComponent<shootBullet>().bulletSpeed;
            bulletRb.velocity = transform.forward * 10;
            initPos = transform.position;
        }

        else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<SkeletonController>().EnemyAttacked();
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "Player Body")
        {
            other.gameObject.GetComponent<PlayerBodyController>().PlayerAttacked();
            Destroy(gameObject);
        }
    }
}
