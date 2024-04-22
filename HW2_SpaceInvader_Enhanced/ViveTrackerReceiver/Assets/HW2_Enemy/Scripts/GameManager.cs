using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // graffiti
    // public Text uiText;

    public TMP_Text uiText;

    public AudioSource BGM;

    //states
    enum State { NotStarted, Playing, GameOver, WonGame };

    // current state
    State currState;

    // Enemy Manager
    EnemyManager enemyManager;

    PlayerBodyController player;

    // Start is called before the first frame update
    void Start()
    {
        // start as not playing
        currState = State.NotStarted;

        // uiText = GetComponent<TMP_Text>();

        // refresh UI
        RefreshUI();

        // find the enemy manager
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();

        player = GameObject.FindObjectOfType<PlayerBodyController>();

        // log error if it wasn't found
        if(enemyManager == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }
    }

    void RefreshUI()
    {
        // act according to the state

        switch (currState)
        {
            case State.NotStarted:
                uiText.text = "Show V with your hand to begin!";
                break;

            case State.Playing:
                uiText.text = "Enemies: " + enemyManager.numEnemies;
                break;

            case State.GameOver:
                uiText.text = "Game Over! Show V to restart!";
                break;

            case State.WonGame:
                uiText.text = "YOU WON! Show V to restart!";
                break;
        }
    }

    public void InitGame()
    {
        //don't initiate the game if the game is already running!
        if (currState == State.Playing) return;

        // set the state
        currState = State.Playing;

        enemyManager.KillAll();

        // create enemy wave
        enemyManager.CreateEnemy();

        BGM.Play();

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

        BGM.Stop();

        player.Restart();
    }

    // checks whether we've won, and if we did win, refresh UI
    public void HandleEnemyDead()
    {
        if (currState != State.Playing) return;

        RefreshUI();

        // have we won the game?
        if(enemyManager.numEnemies <= 0)
        {
            // Make it to the highest level, Win!!
            // set the state of the game
            currState = State.WonGame;

            // show text on the graffiti
            RefreshUI();

            // remove all enemies
            enemyManager.KillAll();

            BGM.Stop();

            player.Restart();
        }
    }

    public bool isPlaying(){
        if (currState == State.Playing) return true;
        else return false;
    }
}
