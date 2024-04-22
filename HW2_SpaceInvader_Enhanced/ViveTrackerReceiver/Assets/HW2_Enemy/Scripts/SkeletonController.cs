using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    // speed
    public float speed = 100f;

    // accumulated time
    float accTime = 0;
    float waitAccTime = 0;

    //HP value
    public float MaxHP;
    float curHP;

    // available states
    // enum State { MovingHorizontally, MovingVertically, Dead};
    enum State { alive, Dead };

    // keep track of the current state
    State currState;

    // Game Manager
    GameManager gm;

    // Enemy Manager
    EnemyManager em;

    GameObject player;

    // for movement
    Animator anim;
    public float walkingPeriod = 10f;
    bool isWalking;

    public float attackInterval = 3;

    // health bar
    public BarController healthbar;
    
    // Enemy death effect

    // public ParticleSystem hitParticles;
    // public ParticleSystem deathParticles;
    //public AudioClip hitAudio;
    //public AudioClip deathAudio;

    // Start is called before the first frame update
    void Start()
    {
        // initial state
        currState = State.alive;

        // game manager
        /*
        gm = GameObject.FindObjectOfType<GameManager>();

        // log error if it wasn't found
        if (gm == null)
        {
            Debug.LogError("there needs to be an GameManager in the scene");
        }*/

        // enemy manager
        em = GameObject.FindObjectOfType<EnemyManager>();

        // log error if it wasn't found
        if (em == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }

        player = GameObject.FindGameObjectWithTag("Player Body");

        anim = GetComponent<Animator>();
        isWalking = false;

        curHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // nothing happens if the enemy is dead
        if (currState == State.Dead) return;

        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);

        // update accumulate movement
        accTime += Time.deltaTime;

        if (accTime > walkingPeriod)
        {
            accTime = 0;
            waitAccTime = 0;
            isWalking = !isWalking;
        }

        anim.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            // calculate movement  v = d / t --> d = v * t
            float movement = speed * Time.deltaTime;
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * movement;
        }
    }

    public void KillEnemy()
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        // set the state to dead
        currState = State.Dead;

        anim.SetBool("isKilled", true);

        // destroy enemy with sound
        //Instantiate(deathParticles, transform.position, transform.rotation);
        //AudioSource.PlayClipAtPoint(deathAudio, transform.position);
        Destroy(gameObject);

        // decrease number of enemies
        em.numEnemies--;

        // check winning condition
        gm.HandleEnemyDead();
    }

    public void EnemyAttacked()
    {
        curHP--;
        healthbar.UpdateHealth(curHP / MaxHP);
        // update health bar
        
        // attack sound
        //Instantiate(hitParticles, transform.position, transform.rotation);
        //AudioSource.PlayClipAtPoint(hitAudio, transform.position, 0.8f);

        // if hp = 0, enemy killed
        if (curHP <= 0)
        {
            KillEnemy();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        waitAccTime = attackInterval;

        //check if the enemy hit the player
        if (other.CompareTag("Player Body"))
        {
            isWalking = false;

            waitAccTime += Time.deltaTime;

            if(waitAccTime > attackInterval)
            {
                waitAccTime = 0;
                anim.SetBool("isAttacking", true);

                // cause damage to player
                other.gameObject.GetComponent<PlayerBodyController>().PlayerAttacked();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //check if the enemy hit the player
        if (other.CompareTag("Player Body"))
        {
            anim.SetBool("isAttacking", false);
        }
    }
}
