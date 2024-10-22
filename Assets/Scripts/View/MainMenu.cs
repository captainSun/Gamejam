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
    
    public void StartLevel(LevelData data)
    {
        curLvData = data;
        scoreText.SetText(string.Format("剩余次数：{0}", curLvData.hp)) ;
       
    }

    public void UpdateTime(float timer)
    {
        remainTimeText.text = string.Format("剩余时间：{0}", TimeUtil.FloatForTime(timer));
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
