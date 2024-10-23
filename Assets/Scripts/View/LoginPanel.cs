using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class LoginPanel : MonoBehaviour
{
    private Button btn;
    void Start()
    {
        btn = transform.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        Destroy(gameObject);
        GameMgr.PlayOpen();
    }

}
