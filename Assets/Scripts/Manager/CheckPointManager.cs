using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Transform[] startPoints;  // ÿһ�ص����
    public Transform[] endPoints;    // ÿһ�ص��յ�
    public Transform player;         // ��ҽ�ɫ�� Transform
    public Transform guideDog;         // ��äȮ��ɫ�� Transform

    private int currentLevel = 0;    // ��ǰ�ؿ�����
    private Vector3 offset;

    private Rigidbody playerRb;
    private Rigidbody dogRb;
    void Start()
    {
        playerRb = player.gameObject.GetComponent<Rigidbody>();
        dogRb = guideDog.gameObject.GetComponent<Rigidbody>();
        offset = new Vector3(30,0,30);
        this.ResetToStart();  // ��ʼ��ʱ���õ���1�ص����
    }

    // �������λ�õ���ǰ�ؿ������
    public void ResetToStart()
    {
        this.SetKinematic(true); //����ʱ��������Ч�������������������ƶ���λ��ʱ����bug     
        this.ResetTrans();
        this.SetKinematic(false);
    }

    public void SetKinematic(bool _is)
    {
        playerRb.isKinematic= _is;
        dogRb.isKinematic= _is;
    }

    public void ResetTrans()
    {
        player.position = startPoints[currentLevel].position;
        player.rotation = startPoints[currentLevel].rotation;
        guideDog.position = startPoints[currentLevel].position + offset;
        guideDog.rotation = startPoints[currentLevel].rotation;
    }

    // �л�����һ�ؿ�
    public void NextLevel()
    {
        if (currentLevel < startPoints.Length - 1)
        {
            currentLevel++;
            ResetToStart();  // ���õ��¹ؿ������
        }
        else
        {
            Debug.Log("��Ϸ������");
        }
    }
}
