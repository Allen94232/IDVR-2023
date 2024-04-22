using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // number of enemies
    public int numEnemies = 0;
    public EnemySpawner[] spawners;

    public void CreateEnemy(){

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GenerateEnemy();
            numEnemies += spawners[i].maxNum;
        }
    }

    public void KillAll()
    {
        //find all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        numEnemies = 0;

        //iterate through these enemies and destroy them
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
