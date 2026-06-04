using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public float moveSpeed;
    
    private Transform target;
    
    public float damage;

    public float hitWaitTime = 1f;
    private float hitCounter;


    public float health = 5f;

    public float knockBackTime = 0.5f;
    private float knockBackCounter;

    public int expToGive = 1;


    // Start is called before the first frame update
    void Start()
    {
        //获取玩家位置信息
        //target = FindAnyObjectByType<PlayerController>().transform;

        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //进行击退
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2;
            }

            if(knockBackCounter <=  0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }


        //怪物移动
        rigidbody2d.velocity = (target.position - transform.position).normalized * moveSpeed;

        //伤害暂停倒计时
        if(hitWaitTime > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }


    //Unity碰撞检测函数
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hitCounter <= 0)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    //承受伤害
    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0f)
        {
            Destroy(gameObject);

            //生成经验球
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }


    //方法重载，带击退
    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if(shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
