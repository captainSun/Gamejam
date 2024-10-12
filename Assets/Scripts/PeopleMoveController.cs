using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人移动控制器
/// </summary>
public class PeopleMoveController : MonoBehaviour
{
    private GameObject people;
    private Rigidbody _rigidbody;
    public float speed = 1;
    void Start()
    {
        people = gameObject;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S))
        {
            _rigidbody.velocity = Vector3.forward * vertical * speed;
        }
        if (Input.GetKey(KeyCode.A)|Input.GetKey(KeyCode.D))
        {
            _rigidbody.velocity = Vector3.right * horizontal * speed;
        }   
    }
}
