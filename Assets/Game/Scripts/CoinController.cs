using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentCoins;

    public CoinPickup coin;

    //增加金币
    public void AddCoins(int coinToAdd)
    {
        currentCoins += coinToAdd;

        UIController.instance.UpdateCoins();
    }

    //生成金币拾取物
    public void DropCoin(Vector3 position, int value)
    {
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(0.2f, 0.1f, 0f), Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
    }
}
