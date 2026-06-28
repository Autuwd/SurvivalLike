using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    public static PlayerStatController instance;

    private void Awake()
    {
        instance = this;
    }

    public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;

    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

    // Start is called before the first frame update
    void Start()
    {
        //自动填充速度属性列表数据
        for(int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[i].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        //自动填充生命值属性列表数据
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[i].cost, health[i].value + (health[1].value - health[0].value)));
        }

        //自动填充拾取范围属性列表数据
        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[i].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(UIController.instance.levelUpPanel.activeSelf == true)
        {
            UpdateDisplay();
        }
    }


    //显示属性升级选项
    public void UpdateDisplay()
    {
        //移动速度
        if(moveSpeedLevel >= moveSpeed.Count - 1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }
        else
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        
        //最大生命值
        if(healthLevel >= health.Count - 1)
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        
        //拾取范围
        if(pickupRangeLevel >= pickupRange.Count - 1)
        {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }
        else
        {
            UIController.instance.pickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        }

        //最大武器数量
        if (maxWeaponsLevel >= maxWeapons.Count - 1)
        {
            UIController.instance.maxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
        else
        {
            UIController.instance.maxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        }    
    }


    //实现属性升级
    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();

        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    }

    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();

        PlayerHealthController.instance.maxHealth = health[healthLevel].value;
        PlayerHealthController.instance.currentHealth += health[healthLevel].value - health[healthLevel - 1].value;
    }

    public void PurchasePickupRange()
    {
        pickupRangeLevel++;
        CoinController.instance.SpendCoins(pickupRange[pickupRangeLevel].cost);
        UpdateDisplay();

        PlayerController.instance.pickupRange = pickupRange[pickupRangeLevel].value;
    }

    public void PurchaseMaxWeapons()
    {
        maxWeaponsLevel++;
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponsLevel].cost);
        UpdateDisplay();

        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);
    }
}

//玩家属性数值类
[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}
