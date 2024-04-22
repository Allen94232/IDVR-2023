using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // graffiti
    public Text uiText;

    //Audios
    public AudioSource FightingBGM;
    public AudioSource Win;
    public AudioSource Lose;
    public AudioSource StartSound;

    public Material background;

    OVRCameraRig ovrCameraRig;

    //states
    public enum State { NotStarted_1, Playing_1, NotStarted_2, Playing_2, NotStarted_3, Playing_3, GameOver, WonGame, Ready }

    // current state
    State currState;
    State oriState;

    public float level;

    // Enemy Manager
    EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        // start as not playing
        currState = State.NotStarted_1;

        // refresh UI
        RefreshUI();

        // find the enemy manager
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();

        ovrCameraRig = FindObjectOfType<OVRCameraRig>();

        // log error if it wasn't found
        if (enemyManager == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }
    }

    void RefreshUI()
    {
        // act according to the state
        switch (currState)
        {
            case State.NotStarted_1:
                uiText.text = "Level 1, shoot here";
                break;

            case State.NotStarted_2:
                uiText.text = "Level 2, shoot here";
                break;

            case State.NotStarted_3:
                uiText.text = "Level 3, shoot here";
                break;

            case State.Playing_1:
            case State.Playing_2:
            case State.Playing_3:
                uiText.text = "Enemies left: " + enemyManager.numEnemies;
                break;

            case State.GameOver:
                uiText.text = "YOU LOSE! Shoot here";
                break;

            case State.WonGame:
                uiText.text = "YOU WON! Shoot here";
                break;
        }  
    }

    public void InitGame()
    {

        // set the state
        if (currState == State.NotStarted_1 || currState == State.WonGame || currState == State.GameOver)
        {
            currState = State.Playing_1;
            level = 1;
        }

        if (currState == State.NotStarted_2)
        {
            currState = State.Playing_2;
            level = 2;
        }

        if (currState == State.NotStarted_3)
        {
            currState = State.Playing_3;
            level = 3;
        }


        // create enemy wave
        enemyManager.CreateEnemyWave();

        // play BGM
        Win.Stop();
        Lose.Stop();
        FightingBGM.Play();

        ovrCameraRig.centerEyeAnchor.GetComponent<Skybox>().material = background;

    // show text on the graffiti
    RefreshUI();
    }

    // game over
    public void GameOver()
    {
        // do nothing if we were already on game over
        if (currState == State.GameOver) return;

        // set the state to game over
        currState = State.GameOver;

        // show text on the graffiti
        RefreshUI();

        // remove all enemies
        enemyManager.KillAll();

        // stop BGM and play lose audio
        FightingBGM.Stop();
        Lose.Play();
        Material emptySkyboxMaterial = Resources.Load<Material>("EmptySkyboxMaterial");

        // Set the skybox material to the empty material
        ovrCameraRig.centerEyeAnchor.GetComponent<Skybox>().material = emptySkyboxMaterial;
    }

    // checks whether we've won, and if we did win, refresh UI
    public void HandleEnemyDead()
    {
        if (currState != State.Playing_1 && currState != State.Playing_2 && currState != State.Playing_3)
        {
            return;
        }

        RefreshUI();

        // have we won the game?
        if(enemyManager.numEnemies <= 0)
        {
            // set the state of the game
            if (currState == State.Playing_1) currState = State.NotStarted_2;

            if (currState == State.Playing_2) currState = State.NotStarted_3;

            if (currState == State.Playing_3) currState = State.WonGame;

            // show text on the graffiti
            RefreshUI();

            // remove all enemies
            enemyManager.KillAll();

            // stop BGM and play win audio
            FightingBGM.Stop();
            Win.Play();

            Material emptySkyboxMaterial = Resources.Load<Material>("EmptySkyboxMaterial");

            // Set the skybox material to the empty material
            ovrCameraRig.centerEyeAnchor.GetComponent<Skybox>().material = emptySkyboxMaterial;
        }
    }

    public void GameReady()
    {
        //don't initiate the game if the game is already running!
        if (currState == State.Playing_1 || currState == State.Playing_2 || currState == State.Playing_3 || currState == State.Ready)
        {
            return;
        }
        Win.Stop();
        Lose.Stop();
        StartCoroutine(Reciprocal());
    }

    IEnumerator Reciprocal()
    {
        oriState = currState;
        currState = State.Ready;
        uiText.text = "3";
        StartSound.Play();
        yield return new WaitForSeconds(0.5f);
        StartSound.Stop();
        yield return new WaitForSeconds(0.5f);
        StartSound.Play();
        uiText.text = "2";
        yield return new WaitForSeconds(0.5f);
        StartSound.Stop();
        yield return new WaitForSeconds(0.5f);
        StartSound.Play();
        uiText.text = "1";
        yield return new WaitForSeconds(0.5f);
        StartSound.Stop();
        yield return new WaitForSeconds(0.5f);
        currState = oriState;
        InitGame();
    }
}
