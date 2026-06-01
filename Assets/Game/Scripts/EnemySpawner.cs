using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawnPoint;
    public Transform maxSpawnPoint;

    private Transform target;

    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int enemyToCheck;

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;

        //销毁位置信息
        despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 4f;
    }

    // Update is called once per frame
    void Update()
    {
        //定时生成敌人
        spawnCounter -= Time.deltaTime;
        if( spawnCounter <= 0 )
        {
            spawnCounter = timeToSpawn;

            //Instantiate(enemyToSpawn, transform.position, transform.rotation);

            //添加新敌人到列表中
            GameObject newEnemy =  Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }

        transform.position = target.position;


        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if(enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if(Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);

                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    //设置敌人产生的位置
    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);

            if(Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.x = minSpawnPoint.position.x;
            }
            else
            {
                spawnPoint.x = maxSpawnPoint.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.y = minSpawnPoint.position.y;
            }
            else
            {
                spawnPoint.y = maxSpawnPoint.position.y;
            }
        }

        return spawnPoint;
    }
}
