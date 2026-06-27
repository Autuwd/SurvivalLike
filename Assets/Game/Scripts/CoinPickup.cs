using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmount = 1;

    private bool movingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = 0.2f;
    private float checkCounter;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //若标识为真，则向玩家移动，否则定时检查玩家是否在范围内
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;
                //如果玩家在范围内 修改标识并增加移动速度
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞器标签是Player时进行经验值获取和销毁当前经验球
        if (collision.tag == "Player")
        {
            CoinController.instance.AddCoins(coinAmount);

            Destroy(gameObject);
        }
    }
}
