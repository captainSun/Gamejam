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
    public TextMeshProUGUI dataText1;
    public TextMeshProUGUI dataText2;
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

    public void UpdateTime(float time)
    {
        remainTimeText.text = string.Format("{0}", TimeUtil.FloatForTime(time));
    }

    public void UpdateScore()
    {
        scoreText.text = string.Format("剩余次数：{0}", curLvData.hp - LevelController.curScore);
    }

    public void UpdateDataText1(float distance)
    {
        dataText1.text = string.Format("距离：{0:F2}", Math.Round(distance, 2));
    }
    
    public void UpdateDataText2(float angle)
    {
        dataText2.text = string.Format("角度差：{0:F2}", Math.Round(angle, 2));
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
        dataText1.gameObject.SetActive(flag);
        dataText2.gameObject.SetActive(flag);
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
