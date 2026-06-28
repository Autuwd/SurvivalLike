using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUpgradeDisplay : MonoBehaviour
{
    public TMP_Text valueText;
    public TMP_Text costText;

    public GameObject upgradeButton;

    //更新属性升级按钮信息
    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        valueText.text = "数值" + oldValue.ToString("F1") + "->" + newValue.ToString("F1");
        costText.text = "黄金" + cost;

        if (cost <= CoinController.instance.currentCoins)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLevel()
    {
        valueText.text = "已满级";
        costText.text = "";
        upgradeButton.SetActive(false);
    }
}
