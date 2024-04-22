using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject holeSpawner;
    public GameObject ball;
    public GameObject Effect;
    public GameObject Hand;
    public Canvas canvas;
    public GameObject center_eye;
    // Update is called once per frame
    void Update()
    {
        if (ball.GetComponent<GolfBall>().isEnd)
        {
            GameObject tinker = GameObject.Find("tinker(Clone)");
            GameObject hole = GameObject.Find("GolfHole(Clone)");
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("tinker position: " + tinker.transform.position);
            if (tinker.transform.position.y < hole.transform.position.y)
            {
                tinker.transform.position = new Vector3(tinker.transform.position.x, tinker.transform.position.y + 0.0075f, tinker.transform.position.z);
                canvas.transform.position = tinker.transform.position + new Vector3(-0.3f, 2.5f, -0.5f);
            }
            center_eye = GameObject.Find("CenterEyeAnchor");
            canvas.transform.rotation = center_eye.transform.rotation;
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject hole = GameObject.Find("GolfHole(Clone)");
            GameObject tinker = GameObject.Find("tinker(Clone)");
            Destroy(tinker);
            Destroy(hole);
            holeSpawner.GetComponent<HoleRandomSpawner>().isCreated = false;
            ball.SetActive(true);
            ball.GetComponent<GolfBall>().isEnd = false;
            ball.GetComponent<GolfBall>().collisionCount = 0.0f;
            ball.GetComponent<GolfBall>().lastStartGameTime = Time.time;

            // stop the effect
            Effect.SetActive(false);
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                ball.SetActive(true);

                if (Time.time - ball.GetComponent<GolfBall>().lastStartGameTime > 2.0f)
                {
                    ball.GetComponent<GolfBall>().lastStartGameTime = Time.time;
                    ball.GetComponent<GolfBall>().collisionCount += 1.0f;
                }
                ball.layer = 6;
                // set the velocity of the ball to zero
                ball.GetComponent<GolfBall>().GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.position = Hand.transform.position;

                ball.GetComponent<GolfBall>().lastResetTime = Time.time;
            }
        }
    }
}
