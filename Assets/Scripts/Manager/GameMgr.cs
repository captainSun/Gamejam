using System;
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
    private static bool inEditor = true;
    
    public static GameObject Environment; //相关设置根节点
    public static GameObject people; //人
    public static GameObject dog; //狗
    public static GameObject rope;//绳子
    public static MainMenu mainMenu;
    public static Canvas canvas; //canvas
    public static Volume globalVolume; //volume组件
    public static Camera mainCamera;

    private static GameObject openPageAni;
  
        
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

        people.SetActive(false);
        dog.SetActive(false);
        LoadLoginPanel();
    }

   
    public static void LoadLoginPanel()
    {
        ResetGameObj();
        ResourceMgr.CreateObj("LoginPanel", canvas.transform);
        EventMgr.AddEvent("OpenAniEnd", StartGame);
    }

    //开始游戏
    public static void StartGame()
    {
        GameObject.DestroyImmediate(openPageAni);
        globalVolume.enabled = true;
        mainCamera.gameObject.SetActive(false);
        
        LevelController.ActiveTrigger(true);
        people.SetActive(true);
        dog.SetActive(true);
        
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

    //开关移动组件
    public static void SetMoveControl(bool flag)
    {
        people.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        people.GetComponent<PeopleMoveController>().enabled = flag;
        dog.GetComponent<POLYGON_DogAnimationController>().enabled = flag;
    }
    
    public static void ResetGameObj()
    {
        SetMoveControl(false);
        people.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
    
    //播放开场动画
    public static void PlayOpen()
    {
        if (inEditor)
        {
            StartGame();
        }
        else
        {
            openPageAni = ResourceMgr.CreateObj("openPageAni", canvas.transform);
        }
        
    }
    
    //播放摔倒过场
    public static void PlayFall(Action action)
    {
        mainCamera.gameObject.SetActive(true);
        ResetGameObj();
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
        ResetGameObj();
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
        ResetGameObj();
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
        ResetGameObj();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2);
        seq.AppendCallback(() =>
        {
            ResetGame();
            action.Invoke();
        });
    }
}