using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;

    public float lifeTime;
    private float lifeCounter;

    public float floatSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {

        
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0)
            {
                //Destroy(gameObject);
                //将当前对象放回对象池中
                DamageNumberController.instance.PlaceInPool(this);
            }
        }

        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    Setup(27);
        //}
        
        //伤害数字动效
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }


    //更新值
    public void Setup(int damageDisplay)
    {
        lifeCounter = lifeTime;

        damageText.text = damageDisplay.ToString();
    }
}
