﻿using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Sequence = DG.Tweening.Sequence;

/// <summary>
/// 游戏管理
/// </summary>
public static class GameMgr
{
    public static GameObject Environment; //相关设置根节点
    public static GameObject people; //人
    public static GameObject dog; //狗
    public static GameObject rope;//绳子
    public static MainMenu mainMenu;
    public static Canvas canvas; //canvas
    public static Volume globalVolume; //volume组件
    public static Camera mainCamera;

  
        
    //初始化
    public static void Initialize()
    {
        Environment = GameObject.Find("Environment");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        globalVolume = Environment.transform.Find("GlobalVolume").GetComponent<Volume>();
        people = GameObject.FindGameObjectWithTag("People");
        dog = GameObject.FindGameObjectWithTag("Dog");
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        globalVolume.gameObject.SetActive(true);
        globalVolume.enabled = false;
        LoadLoginPanel();
    }

   
    public static void LoadLoginPanel()
    {
        GameObject LoginPanel = ResourceMgr.CreateObj("LoginPanel", canvas.transform);
        InitObj();
    }

    public static void SetMoveControl(bool flag)
    {
        people.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        people.GetComponent<PeopleMoveController>().enabled = flag;
        dog.GetComponent<POLYGON_DogAnimationController>().enabled = flag;
    }

    public static void InitObj()
    {
        SetMoveControl(false);
        people.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    //开始游戏
    public static void StartGame()
    {
        globalVolume.enabled = true;
        mainCamera.gameObject.SetActive(false);
        mainMenu = ResourceMgr.CreateObj("MainMenu", canvas.transform).GetComponent<MainMenu>();
        LevelController.StartLevel(1);
    }

    public static void ResetGame()
    {
        globalVolume.enabled = false;
        mainCamera.gameObject.SetActive(true);
        ResourceMgr.DestroyObj(mainMenu.gameObject);
        LoadLoginPanel();
    }

    //播放摔倒过场
    public static void PlayFall(Action action)
    {
        mainCamera.gameObject.SetActive(true);
        InitObj();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2);
        seq.AppendCallback(() =>
        {
            mainCamera.gameObject.SetActive(false);
            action.Invoke();
        });
    }
    
    //播放中间过场
    public static void PlayBridge(Action action)
    {
        mainCamera.gameObject.SetActive(true);
        InitObj();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2);
        seq.AppendCallback(() =>
        {
            mainCamera.gameObject.SetActive(false);
            action.Invoke();
        });
    }
    
    //播放失败过场
    public static void PlayDefeat(Action action)
    {
        mainCamera.gameObject.SetActive(true);
        InitObj();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2);
        seq.AppendCallback(() =>
        {
            ResetGame();
            action.Invoke();
        });
    }
    
    //播放胜利过场
    public static void PlayWin(Action action)
    {
        mainCamera.gameObject.SetActive(true);
        InitObj();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2);
        seq.AppendCallback(() =>
        {
            ResetGame();
            action.Invoke();
        });
    }
}