using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText;
    public TMP_Text nameLevelText;
    public Image weaponIcon;

    public Weapon assignedWeapon;

    //更新按钮的显示信息
    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        if(theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText; 
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name + "-等级" + theWeapon.weaponLevel;
        }
        else
        {
            upgradeDescText.text = "解锁" + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        
        assignedWeapon = theWeapon;
    }

    //处理升级选择
    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if(assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
