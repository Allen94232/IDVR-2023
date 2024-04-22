using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject EnemyPrefab;

    float interval = 3.5f;

    public int maxNum;

    GameManager gm;
    int currentNum = 0;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    public void GenerateEnemy(){
        StartCoroutine(spawnEnemy(interval, EnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f)), Quaternion.identity);

        currentNum++;
        if (currentNum >= maxNum)
        {
            currentNum = 0;
        }
        else
        {
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }
}
