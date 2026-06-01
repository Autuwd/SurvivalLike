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
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);
        }
    }
}
