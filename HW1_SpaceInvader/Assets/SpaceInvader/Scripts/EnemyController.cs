using OVR;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // movement range
    public float rangeH;
    public float rangeV;

    // speed
    public float speed;

    // health
    public float MaxHealth;
    public float CurrentHealth;

    public Material redMaterial;
    private Material originalMaterial;

    private Transform childTransform;

    private Renderer enemyRenderer;

    //bullet damage
    private float damage = 0;

    // direction
    int direction = 1;

    // accumulated movement
    float accMovement = 0;

    // available states
    enum State { MovingHorizontally, MovingVertically, Dead };

    // keep track of the current state
    State currState;

    // Game Manager
    GameManager gm;

    // Enemy Manager
    EnemyManager em;

    // Player Controller
    PlayerController pc;


    // Start is called before the first frame update
    void Start()
    {
        // initial state
        currState = State.MovingHorizontally;

        // game manager
        gm = GameObject.FindObjectOfType<GameManager>();

        // log error if it wasn't found
        if (gm == null)
        {
            Debug.LogError("there needs to be an GameManager in the scene");
        }

        // enemy manager
        em = GameObject.FindObjectOfType<EnemyManager>();

        // log error if it wasn't found
        if (em == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }

        pc = GameObject.FindObjectOfType<PlayerController>();

        if (pc == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }

        speed = em.speed;
        MaxHealth = em.MaxHealth;
        rangeH = em.H;
        rangeV = em.V;

        // initial health
        CurrentHealth = MaxHealth;

        childTransform = transform.Find("Invader Model");

    }

    // Update is called once per frame
    void Update()
    {
        // nothing happens if the enemy is dead
        if (currState == State.Dead) return;

        // calculate movement  v = d / t --> d = v * t
        float movement = speed * Time.deltaTime;

        // set damage
        damage = pc.GetComponent<PlayerController>().damage;

        // update accumulate movement
        accMovement += movement;

        // are we moving horizontally?
        if (currState == State.MovingHorizontally)
        {
            // if yes, then transition to moving vertically
            if (accMovement >= rangeH)
            {
                // transition to moving vertically
                currState = State.MovingVertically;

                // reverse direction (for horizontal movement)
                direction *= -1;

                // reset acc movement
                accMovement = 0;
            }
            // if not, move the invader horizontally
            else
            {
                transform.position += transform.forward * movement * direction;
            }
        }
        // this is, if we are moving vertically
        else
        {
            // if yes, then transition to moving horizontally
            if (accMovement >= rangeV)
            {
                // transition to moving horiz
                currState = State.MovingHorizontally;

                // reset acc movement
                accMovement = 0;
            }
            // if not, move the invader vertically
            else
            {
                transform.position += Vector3.down * movement;
            }
        }
    }

    public void HitEnemy()
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        enemyRenderer = childTransform.GetComponent<Renderer>();

        // Store the original material at the start
        originalMaterial = enemyRenderer.material;

        // Change the material to red when the enemy gets hit
        enemyRenderer.material = redMaterial;

        // After a certain duration, reset the material to the original
        StartCoroutine(ResetMaterial());

        CurrentHealth -= damage;

    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.15f);
        enemyRenderer.material = originalMaterial;
    }


    public void CheckKillEnemy()
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        if (CurrentHealth <= 0)
        {
            // set the state to dead
            currState = State.Dead;

            //[implement your own effect here]

            //audio
            AudioSource HitAudio = GameObject.FindGameObjectWithTag("EnemyAudio").GetComponent<AudioSource>();

            if (HitAudio != null && HitAudio.clip != null)
            {
                // Play the enemy death audio clip
                HitAudio.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource or audio clip is not properly set.");
            }

            //destroy the enemy
            Destroy(gameObject);

            //die effect
            ParticleSystem deathParticles = GameObject.FindGameObjectWithTag("EnemyDieEffect").GetComponent<ParticleSystem>();
            deathParticles.transform.position = transform.position;
            deathParticles.Play();

            // decrease number of enemies
            em.numEnemies--;

            // check winning condition
            gm.HandleEnemyDead();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        //check if the enemy hit the player
        if (other.CompareTag("Player Body"))
        {
            gm.GameOver();
        }

        //check if the enemy reached the floor
        else if (other.CompareTag("Ground"))
        {
            gm.GameOver();
        }
    }
}
