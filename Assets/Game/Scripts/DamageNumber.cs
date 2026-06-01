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
                Destroy(gameObject);
            }
        }

        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    Setup(27);
        //}
        
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }


    public void Setup(int damageDisplay)
    {
        lifeCounter = lifeTime;

        damageText.text = damageDisplay.ToString();
    }
}
