using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    [HideInInspector]
    public bool statsUpdated;

    public Sprite icon;

    //Éý¼¶ÎäÆ÷
    public void LevelUp()
    {
        if(weaponLevel < stats.Count -1)
        {
            weaponLevel++;

            statsUpdated = true;
        }
    }
}


[System.Serializable]
public class WeaponStats
{
    public float speed;
    public float damage;
    public float range;
    public float timeBetweeenAttacks;
    public float amount;
    public float duration;

    public string upgradeText;
}
