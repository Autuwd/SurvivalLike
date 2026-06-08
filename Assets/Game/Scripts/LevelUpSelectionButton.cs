using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeText;
    public TMP_Text nameLevelText;
    public Image weaponIcon;

    //更新按钮的显示信息
    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        upgradeText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText; 
        weaponIcon.sprite = theWeapon.icon;

        nameLevelText.text = theWeapon.name + "-等级" + theWeapon.weaponLevel;
    }
}
