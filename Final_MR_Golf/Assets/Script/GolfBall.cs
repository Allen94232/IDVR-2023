using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GolfBall : MonoBehaviour
{
    public float kineticEnergyMultiplier = 100.0f; // Adjust this multiplier as needed
    public UnityEvent onCollision; // UnityEvent for handling collisions
    public GameObject Hand;
    public GameObject Effect;
    public GameObject prefab_101;
    public TMP_Text hitCount;
    public bool isEnd = false;
    public float lastStartGameTime = 0.0f; // Time of start end game

    public float collisionCount = 0.0f; // Number of collisions
    public float lastResetTime = 0.0f; // Time of last reset

    public AudioSource GolfHit;
    public AudioSource Victory;

    private float lastCollisionTime = 0.0f; // Time of last collision

    private void Update()
    {
        Debug.Log("Collision count: " + collisionCount);
        hitCount.text = collisionCount.ToString();
        // if time < 1 second or press 'A', set the position of the ball to the position of the hand
        if (Time.time - lastStartGameTime < 2 || OVRInput.GetDown(OVRInput.Button.One))
        {
            if (Time.time - lastStartGameTime >= 2)
            {
                collisionCount += 1.0f;
            }
            gameObject.layer = 6;
            // set the velocity of the ball to zero
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = Hand.transform.position;

            lastResetTime = Time.time;
        }

        if (Time.time - lastResetTime > 1f)
        {
            // set layer to "default"
           gameObject.layer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the time since the last collision is less than 0.1 seconds, ignore the collision
        if (Time.time - lastCollisionTime < 1.0f)
        {
            return;
        }
        CalculateAndApplyForce(collision);
        // onCollision.Invoke(); // Invoke the UnityEvent for handling collisions

        // record current time
        lastCollisionTime = Time.time;
    }

    private void CalculateAndApplyForce(Collision collision)
    {

        if (collision.gameObject.tag == "Club")
        {
            Rigidbody ballRigidbody = GetComponent<Rigidbody>();
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // give haptic feedback
            collisionCount += 1.0f;
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            GolfHit.Play();
            Debug.Log("Hit by club");

            // Calculate relative velocity
            //Vector3 relativeVelocity = ballRigidbody.velocity - otherRigidbody.velocity;
            //Debug.Log("ball velocity: " + ballRigidbody.velocity);
            //Debug.Log("club velocity: " + otherRigidbody.velocity);
            //Debug.Log("Relative velocity: " + relativeVelocity);

            //Debug.Log("Relative velocity: " + collision.relativeVelocity.magnitude);
            // Calculate kinetic energy
            float kineticEnergy = 0.5f * ballRigidbody.mass * collision.relativeVelocity.magnitude;
            //Debug.Log("Kinetic energy: " + kineticEnergy);

            // Apply force based on kinetic energ
            Vector3 force = collision.gameObject.transform.forward * kineticEnergy * kineticEnergyMultiplier;

            ballRigidbody.AddForce(-force * 6, ForceMode.Impulse);
            Debug.Log("Force: " + force);
            Debug.Log("Ball velocity: " + ballRigidbody.velocity);
        }
        else if (collision.gameObject.tag == "Pond")
        {
            Debug.Log("Ball in lake");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "holeTrigger")
        {
            Debug.Log("Ball in hole");
            GameObject hole = GameObject.Find("GolfHole(Clone)");
            Vector3 holePos = hole.transform.position;
            holePos.y = hole.transform.position.y - 3f;
            // Instantiate the 101 prefab, rotation is (-90, 0, 0)
            Instantiate(prefab_101, holePos, Quaternion.Euler(-90, 0, 0));
            Effect.transform.position = transform.position + new Vector3(0, 1f, 0);
            Victory.Play();
            Effect.SetActive(true);

            // disable the ball
            isEnd = true;
            gameObject.SetActive(false);
        }
    }
}

