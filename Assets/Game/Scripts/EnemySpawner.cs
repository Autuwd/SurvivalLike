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

    public List<WaveInfo> waves;

    private int currentWave;

    private float waveCounter;


    // Start is called before the first frame update
    void Start()
    {
        //spawnCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;

        //销毁位置信息
        despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 4f;

        currentWave = -1;
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        ////定时生成敌人
        //spawnCounter -= Time.deltaTime;
        //if( spawnCounter <= 0 )
        //{
        //    spawnCounter = timeToSpawn;

        //    //Instantiate(enemyToSpawn, transform.position, transform.rotation);

        //    //添加新敌人到列表中
        //    GameObject newEnemy =  Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
        //    spawnedEnemies.Add(newEnemy);
        //}


        //当玩家存活时，不断生成敌人
        if(PlayerHealthController.instance.gameObject.activeSelf)
        {
            if(currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;

                //当前波次结束，进入下一波敌人
                if(waveCounter <= 0)
                {
                    GoToNextWave();
                }

                spawnCounter -= Time.deltaTime;

                if(spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    // 实例化当前波次指定的敌人预制体，并将其加入已生成敌人列表以便追踪
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        //跟随玩家
        transform.position = target.position;

        int checkTarget = enemyToCheck + checkPerFrame;

        //分批检测并清理超出 despawnDistance的敌人
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

    //设置敌人产生的位置区域
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


    //进入下一波敌人
    public void GoToNextWave()
    {
        currentWave++;

        //超出总波次，后续的每波敌人都是最后一波次的敌人
        if(currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        // 根据当前波次的配置，重新初始化波次剩余时间和敌人生成间隔时间
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }

}


[System.Serializable]
public class WaveInfo
{
    // 当前波次需要实例化的敌人预制体
    public GameObject enemyToSpawn;

    // 当前波次的总持续时间
    public float waveLength = 10f;

    // 连续生成两个敌人之间的时间间隔
    public float timeBetweenSpawns = 1f;
}