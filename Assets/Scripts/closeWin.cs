using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class closeWin : MonoBehaviour
{
    public void ScaleSizeTwo()
    {
        this.gameObject.transform.localScale = Vector3.one * 1.5f;
        
    }
     public void ScaleSizeOne()
    {
        this.gameObject.transform.localScale = Vector3.one;
    }

    public void ExitApp()
    {
        Application.Quit();
    }


}
