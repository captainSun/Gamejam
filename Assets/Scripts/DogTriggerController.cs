using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Debug.Log("��ʼ��̸");
            //ʹ��DialogueSys
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�˳�Dialogue

    }
}
