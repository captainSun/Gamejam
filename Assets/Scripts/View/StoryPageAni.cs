using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StoryPageAni : MonoBehaviour
{
    public Image _image;
    public CanvasGroup _cg;
    public Button _button;
    public Dictionary<string, string[]> storyTexDic = new Dictionary<string, string[]>();
    private int curIndex = 0;
    private string curKey = "";
    private Action _endAction;
    private string[] pathList;

    void Awake()
    {
        storyTexDic.Add("bridge", new string[]{"bridge"});
        storyTexDic.Add("be", new string[]{"be_1"});
        storyTexDic.Add("he", new string[]{"he_1","he_2"});
    }

    void Start()
    {
        _cg.alpha = 1;
        _button.gameObject.SetActive(false);
        _button.onClick.AddListener(() =>
        {
            PlayAni();
        }); 
    }

    public void PlayAni(string key = null, Action action = null)
    {
        if (key != null)
        {
            storyTexDic.TryGetValue(key, out pathList);
            curKey = key;
            _endAction = action;
        }

        _button.gameObject.SetActive(false);
        if (curIndex == pathList.Length)
        {
            //播放完毕
            var seq = DOTween.Sequence();
            seq.Append(_cg.DOFade(0, 0.25f));
            seq.AppendCallback(() =>
            {
                Destroy(gameObject);
                _endAction.Invoke();
            });
        }
        else
        {
            //下一张
            var path = pathList[curIndex];
            _image.sprite = ResourceMgr.LoadResAsset<Sprite>("ani/" + path, AssetsEnum.Texture);
            _image.SetNativeSize();
            if (path == "he_1")
            {
                _image.transform.localScale = Vector3.one * 0.75f;
            }
            else
            {
                _image.transform.localScale = Vector3.one;
            }
            _image.color = Color.clear;
            var seq = DOTween.Sequence();
            seq.Append(_image.DOColor(Color.white, 0.5f));
            seq.AppendInterval(1);
            seq.AppendCallback(() =>
            {
                curIndex++;
                _button.gameObject.SetActive(true);
            });
        }
        
        
       
    }


}
