using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;
    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;

    public EnemyDamager damager;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();

        //更新升级按钮显示
        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(this);
    }

    // Update is called once per frame
    void Update()
    {
        //武器旋转
        //holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);

            //根据生成武器的数量，在指定位置生成火球
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360 / stats[weaponLevel].amount) * i;

                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);

                SFXManager.instance.PlaySFX(8);
            }
        }

        //升级进行属性改变
        if(statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    //根据武器等级设置属性
    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;

        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweeenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}
