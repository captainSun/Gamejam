using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人移动控制器
/// </summary>
public class PeopleMoveController : MonoBehaviour
{
    public float moveSpeed = 3f;           // 移动速度
    public float rotationSpeed = 100f;     // 旋转速度
    public float fallThreshold = 2f;       // 摔跤的速度阈值
    public float recoveryTime = 2f;        // 摔倒后恢复时间

    private Rigidbody rb;                  // 刚体组件
    private bool isFallen = false;         // 摔倒状态
    private float recoveryTimer = 0f;
    public CheckPointManager checkPointMgr;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 防止刚体因碰撞自动旋转

    }


    void Update()
    {
        // 如果摔倒，处理恢复时间
        if (isFallen)
        {
            recoveryTimer += Time.deltaTime;
            if (recoveryTimer >= recoveryTime)
            {
                isFallen = false;
                recoveryTimer = 0f;
            }
            return; // 摔倒时不能移动
        }

        HandleMovement();
        HandleRotation();
        AddjustToGroundHeight();
        // 检查规则类，看是否需要触发摔跤
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnFall(); // 触发摔跤
        }
    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        // 检测 WASD 按键
        if (Input.GetKey(KeyCode.W)) movement += transform.forward;
        if (Input.GetKey(KeyCode.S)) movement -= transform.forward;
        if (Input.GetKey(KeyCode.A)) movement -= transform.right;
        if (Input.GetKey(KeyCode.D)) movement += transform.right;

        // 应用速度，保持恒定移动速度
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z).normalized * moveSpeed;
    }

    private void HandleRotation()
    {
        float rotation = 0f;
        if (Input.GetKey(KeyCode.A)) rotation = -1f;
        if (Input.GetKey(KeyCode.D)) rotation = 1f;

        // 应用旋转
        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        //float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        //float vertical = Input.GetAxis("Vertical"); //W S 上 下
        //if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.velocity = Vector3.forward * vertical * speed;
        //}
        //if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.velocity = Vector3.right * horizontal * speed;
        //}
    }

    public void OnFall()
    {
        checkPointMgr.ResetToStart();
    }

    private void AddjustToGroundHeight()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 1.5f))
        {
            Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        }
    }


}
