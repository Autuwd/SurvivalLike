using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider expLevelSlider;
    public TMP_Text expLevelText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelUpPanel;

    public TMP_Text coinText;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay;
    public PlayerStatUpgradeDisplay healthUpgradeDisplay;
    public PlayerStatUpgradeDisplay pickupRangeUpgradeDisplay;
    public PlayerStatUpgradeDisplay maxWeaponsUpgradeDisplay;

    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public TMP_Text endTimeText;

    public string mainMenuName;

    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    //更新经验值和等级显示
    public void UpdateExperience(int currentExp, int levelExp, int currentLevel)
    {
        expLevelSlider.maxValue = levelExp;
        expLevelSlider.value = currentExp;

        expLevelText.text = "等级 " + currentLevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void UpdateCoins()
    {
        coinText.text = "黄金: " + CoinController.instance.currentCoins;
    }


    //选择升级
    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }



    //更新游戏时间显示
    public void UpdateTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);

        timeText.text = "时间: " + minutes + ":" + seconds.ToString("00");
    }


    //按钮功能实现
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);

        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);

            //如果升级面板没有显示，则恢复时间流逝
            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
