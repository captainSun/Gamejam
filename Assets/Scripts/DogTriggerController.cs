using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTriggerController : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            PeopleTalk peopleTalk = other.GetComponent<PeopleTalk>();
            if (peopleTalk != null)
            {
                peopleTalk.Talk();
            }
            else {
                print("该对象上没有PeopleTalk组件");
                
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //退出Dialogue
        if (other.CompareTag("Character"))
        {
            PeopleTalk peopleTalk = other.GetComponent<PeopleTalk>();
            if (peopleTalk != null)
            {
                peopleTalk.StopDia();
            }
          

        }
    }
}
