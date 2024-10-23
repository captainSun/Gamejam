using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI remainTimeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI noticeText;
    public GameObject PeopleRoot;
    public GameObject DogRoot;
    
    private LevelData curLvData;
    private Timer timer;
    void Start()
    {
        noticeText.SetText("");
        //事件监听
        EventMgr.AddEvent("UpdateScore", UpdateScore, "MainMenu");
    }
    
    public void StartLevel(LevelData data)
    {
        ActiveInfo(true);
        
        curLvData = data;
        scoreText.SetText(string.Format("剩余次数：{0}", curLvData.hp));
        levelText.SetText(string.Format("关卡{0}", LevelController.curLevelIndex));
    }

    public void UpdateTime(float timer)
    {
        remainTimeText.text = string.Format("剩余时间：{0}", TimeUtil.FloatForTime(timer));
    }

    public void UpdateScore()
    {
        scoreText.text = string.Format("剩余次数：{0}", curLvData.hp - LevelController.curScore);
    }

    //显示失败原因
    public void ShowStopNotice(int type)
    {
        ActiveInfo(false);
        
        if (type == 1)
        {
            noticeText.SetText("没有时间了！");
        }
        else if (type == 2)
        {
            noticeText.SetText("体力用尽了！");
        }
        else if (type == 3)
        {
            noticeText.SetText("摔倒了！");
        }
    }

    public void ActiveInfo(bool flag)
    {
        PeopleRoot.SetActive(flag);
        DogRoot.SetActive(flag);
        remainTimeText.gameObject.SetActive(flag);
        scoreText.gameObject.SetActive(flag);
        levelText.gameObject.SetActive(flag);
        if (flag)
        {
            noticeText.SetText("");
        }
    }

    private void OnDestroy()
    {
        EventMgr.RemoveAllEvents("MainMenu");
    }
}
