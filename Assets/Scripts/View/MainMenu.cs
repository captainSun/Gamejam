using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI remainTimeText;
    public TextMeshProUGUI scoreText;
    private LevelData curLvData;
    private Timer timer;
    public float remainTime = 0;
    void Start()
    {
        remainTimeText = transform.Find("RemainTime").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        
        //事件监听
        EventMgr.AddEvent("UpdateScore", UpdateScore, "MainMenu");
    }

    //初始化界面设置
    public void StartLevel(LevelData data)
    {
        curLvData = data;
        scoreText.SetText(string.Format("剩余次数：{0}", curLvData.hp)) ;
        remainTime = curLvData.timeLimit * 1000;
        timer = Timer.Create(5, (int)remainTime / 5, UpdateTime);
    }

    public void UpdateTime(Timer timer)
    {
        remainTime = remainTime - timer.delay;
        remainTimeText.text = string.Format("剩余时间：{0}", TimeUtil.FloatForTime(remainTime));
    }

    public void UpdateScore()
    {
        scoreText.text = string.Format("剩余次数：{0}", curLvData.hp - LevelController.curScore);
    }

    private void OnDestroy()
    {
        EventMgr.RemoveAllEvents("MainMenu");
    }
}
