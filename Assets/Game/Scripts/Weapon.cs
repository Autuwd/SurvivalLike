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

    //升级武器
    public void LevelUp()
    {
        if(weaponLevel < stats.Count -1)
        {
            weaponLevel++;

            statsUpdated = true;

            //当前武器等级升满后，将组件移到满级武器列表里
            if(weaponLevel >=  stats.Count -1)
            {
                PlayerController.instance.fullyLevelledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
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
