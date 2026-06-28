using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager damager;
    public Projectile projectile;

    private float shotCounter;

    public float weaponRange;
    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].timeBetweeenAttacks;

            //检测附近的敌人并加入列表中
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);

            //如果附近存在敌人则发射匕首
            if (enemies.Length > 0)
            {
                //根据等级确定发射数量
                for(int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    //随机选择一个敌人
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    //将匕首顶部对准目标
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;

                    //确定旋转方向
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }

                //播放音效
                SFXManager.instance.PlaySFXPitched(6);
            }
        }
    }

    private void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        shotCounter = 0f;

        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
