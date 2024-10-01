using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject Canvas = GameObject.Find("Canvas");
        Object uiPrefab = ResourceMgr.LoadResAsset("UIImage",AssetsEnum.Prefab);
        GameObject UIImage = Instantiate(uiPrefab, Canvas.transform) as GameObject;
        UIImage.GetComponent<Image>().sprite = ResourceMgr.LoadResAsset<Sprite>("equip_1", AssetsEnum.Texture);

        UIImage.transform.DOScale(1.2f, 0.25f).OnComplete(() =>
        {
            UIImage.transform.DOScale(1f, 0.25f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
