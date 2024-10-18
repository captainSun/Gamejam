using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Debug.Log("开始交谈");
            //使用DialogueSys
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //退出Dialogue

    }
}
