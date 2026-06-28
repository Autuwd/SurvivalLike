using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTime(timer);
        }
    }

    //结束当前关卡，将游戏状态设置为非活跃，并启动显示结束画面的协程
    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());

    }


    //协程：等待指定时间后，计算并显示通关时间，激活关卡结束界面。
    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60f);

        UIController.instance.endTimeText.text = minutes.ToString() + "分" + seconds.ToString("00" + " 秒");
        UIController.instance.levelEndScreen.SetActive(true);
    }
}
