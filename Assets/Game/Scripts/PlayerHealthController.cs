using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    //µ•¿˝
    public static PlayerHealthController instance;
    public void Awake()
    {
        instance = this;
    }


    public float currentHealth, maxHealth;

    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Test
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    TakeDamage(10f);
        //}
    }

    //≥– ‹…À∫¶
    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        healthSlider.value = currentHealth;
    }
}
