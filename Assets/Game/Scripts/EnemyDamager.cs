using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime;

    public float growSpeed;
    private Vector3 targetSize;

    public bool shouldKnockBack;

    public bool destroyParent;

    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    public bool destroyOnImpact;


    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, lifeTime);

        //初始化缩放大小
        targetSize = transform.localScale;
        this.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //实现平滑缩放效果
        this.transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        //寿命周期结束销毁对象
        if(lifeTime <= 0 )
        {
            targetSize = Vector3.zero;

            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if(destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if(damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if (damageCounter <= 0)
            {
                damageCounter = timeBetweenDamage;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if(enemiesInRange[i] != null )
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(damageOverTime == false)
        {
            //当碰撞到敌人标签时使其受伤2并击退
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);

                if(destroyOnImpact == true)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(damageOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
